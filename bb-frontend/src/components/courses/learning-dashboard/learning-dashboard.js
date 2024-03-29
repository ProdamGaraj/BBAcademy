﻿import {useEffect, useState, useContext} from "react";
import LearningLayout from "../learning-layout/learning-layout";
import backend from "backend";
import LoaderModalContext from "contexts/loader-modal-context";
import ErrorModalContext from "contexts/error-modal-context";
import LessonContentView from "../lesson-content-view/lesson-content-view";

import ExamContentView from "../exam-content-view/exam-content-view";
import CertContentView from "../cert-content-view/cert-content-view";
import translations from "../../../translations";
import LangContext from "../../../contexts/lang-context";

const LESSON_MODE = 1;
const EXAM_MODE = 2;
const CERT_MODE = 3;

export default (_) => {

    let loaderModal = useContext(LoaderModalContext)
    let errorModal = useContext(ErrorModalContext)

    const currentLang = useContext(LangContext).lang

    let [mode, setMode] = useState(LESSON_MODE);

    let [course, setCourse] = useState(null)
    let [lessonIndex, setLessonIndex] = useState(0)

    useEffect(() => {
        const query = new URLSearchParams(window.location.search);
        const courseId = query.get('id')

        if (courseId !== null) {
            loaderModal.showModal()
            backend.Course.GetForLearning(courseId)
                .then(response => setCourse(response))
                .catch(e => errorModal.showModal(e.message))
                .finally(() => loaderModal.close())
        }
        else {
            errorModal.showModal('courseId missing')
            window.history.back()
        }
    }, [])

    let toPrevLesson = () => {
        setLessonIndex(prev => prev - 1)
    }
    let toNextLesson = () => {
        setLessonIndex(prev => prev + 1)
    }

    const toFinalPage = () => {
        if (course.certName) {
            return switchToCert();
        }

        return switchToExam();
    }

    const switchToLesson = i => {
        if (mode !== LESSON_MODE && course.certName === null) {
            errorModal.showModal('Вы больше не можете просматривать контент уроков.')
            return
        }
        setLessonIndex(i);
        setMode(LESSON_MODE)
    };

    const switchToExam = () => {
        setMode(EXAM_MODE)
    };

    const switchToCert = () => {
        setMode(CERT_MODE)
    };

    const onExamUpdated = e => {
        setCourse({...course, exam: e});
    };
    const onSubmitExam = () => {
        loaderModal.showModal()
        backend.Exam.SaveCourseExamResults({
            courseId: course.id,
            questions: course.exam.questions.map(q => ({
                id: q.id,
                selectedAnswerIds: q.selectedAnswerIndices?.map(i => q.answerOptions[i].id) ?? []
            }))
        })
            .then(({passed, certName}) => {
                if (passed) {
                    setCourse({...course, certName: certName})
                    switchToCert()
                }
                else {
                    errorModal.showModal('К сожалению, вы не прошли экзамен, попробуйте ещё раз')
                }
            })
            .catch(e => errorModal.showModal(e.response.data.error))
            .finally(() => loaderModal.close())
    };

    const getFinalPageButtonTitle = () => {
        if (course.certName) {
            return translations[currentLang].ending
        }

        if (course.exam) {
            return course.exam.title;
        }

        return null;
    }

    const switchLayoutMode = (mode) => {

        switch (mode) {
            case LESSON_MODE:
                return (
                    <LessonContentView
                        lesson={course.lessons[lessonIndex]}
                        toNextLesson={toNextLesson}
                        toPrevLesson={toPrevLesson}
                        toFinalPage={toFinalPage}
                        isFirst={lessonIndex === 0}
                        isLast={lessonIndex === (course.lessons.length - 1)}
                        finalPage={getFinalPageButtonTitle()}
                    />)
            case EXAM_MODE:
                return (
                    <ExamContentView
                        onUpdated={(e) => onExamUpdated(e)}
                        onSubmitExam={() => onSubmitExam()}
                        exam={course.exam}
                    />
                )
            case CERT_MODE:
                return (
                    <CertContentView certName={course.certName} courseTitle={course.title}></CertContentView>
                )
            default:
                return 'UNKNOWN LAYOUT MODE'
        }
    }

    return (<>
        {course === null ? '' :
            <LearningLayout
                course={course}
                activeLessonIndex={lessonIndex}
                activeMode={mode}
                toLesson={(i) => switchToLesson(i)}
                toExam={() => switchToExam()}
                toCert={() => switchToCert()}>
                {switchLayoutMode(mode)}
            </LearningLayout>
        }
    </>)
}

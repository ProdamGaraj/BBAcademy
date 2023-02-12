import {useEffect, useState, useContext} from "react";
import LearningLayout from "../learning-layout/learning-layout";
import backend from "backend";
import LoaderModalContext from "contexts/loader-modal-context";
import ErrorModalContext from "contexts/error-modal-context";
import LessonContentView from "../lesson-content-view/lesson-content-view";

import NavigationArrows from "../navigation-arrows/navigation-arrows";
import ExamContentView from "../exam-content-view/exam-content-view";

const LESSON_MODE = 1;
const EXAM_MODE = 2;
const CERT_MODE = 3;

export default (props) => {

    let loaderModal = useContext(LoaderModalContext)
    let errorModal = useContext(ErrorModalContext)

    let [mode, setMode] = useState(LESSON_MODE);

    let [course, setCourse] = useState(null)
    let [lessonIndex, setLessonIndex] = useState(0)

    useEffect(() => {
        const query = new URLSearchParams(window.location.search);
        const courseId = query.get('id')

        if (courseId !== undefined) {
            loaderModal.showModal()
            backend.Course.GetForLearning(courseId)
                .then(response => setCourse(response))
                .catch(e => errorModal.showModal(e.message))
                .finally(() => loaderModal.close())
        }
        else {
            errorModal.showModal('courseId missing')
            window.location.back()
        }
    }, [])

    let toPrevLesson = () => {
        setLessonIndex(prev => prev - 1)
    }
    let toNextLesson = () => {
        setLessonIndex(prev => prev + 1)
    }

    const switchToLesson = i => {
        if (mode !== LESSON_MODE) {
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
        // alert('SUBMIT EXAM NOT IMPLEMENTED')

        loaderModal.showModal()
        backend.Exam.SaveCourseExamResults({
            courseId: course.id,
            questions: course.exam.questions.map(q => ({
                id: q.id,
                selectedAnswerIds: q.selectedAnswerIndices?.map(i => q.answerOptions[i].id) ?? []
            }))
        })
            .then(() => {
            })
            .catch(e => errorModal.showModal(e.response.data.error))
            .finally(() => loaderModal.close())
    };

    const switchLayoutMode = (mode) => {

        switch (mode) {
            case LESSON_MODE:
                return (
                    <LessonContentView
                        lesson={course.lessons[lessonIndex]}
                        toNextLesson={toNextLesson}
                        toPrevLesson={toPrevLesson}
                        isFirst={lessonIndex === 0}
                        isLast={lessonIndex === (course.lessons.length - 1)}/>)
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
                    <div>CERT VIEW IS NOT IMPLEMENTED</div>
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
import {useEffect, useState, useContext} from "react";
import LearningLayout from "../learning-layout/learning-layout";
import backend from "backend";
import LoaderModalContext from "contexts/loader-modal-context";
import ErrorModalContext from "contexts/error-modal-context";
import LessonContentView from "../lesson-content-view/lesson-content-view";

import NavigationArrows from "../navigation-arrows/navigation-arrows";

export default (props) => {

    let loaderModal = useContext(LoaderModalContext)
    let errorModal = useContext(ErrorModalContext)

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

    let prev = () => {
        setLessonIndex(prev => prev - 1)
    }
    let next = () => {
        setLessonIndex(prev => prev + 1)
    }

    return (<>
        {course === null ? '' :
            <LearningLayout course={course}>
                <LessonContentView lesson={course.lessons[lessonIndex]}/>
                <NavigationArrows
                    onNext={next}
                    onPrev={prev}
                    isFirst={lessonIndex === 0}
                    isLast={lessonIndex === (course.lessons.length - 1)}
                />
            </LearningLayout>
        }
    </>)
}
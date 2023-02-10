import {useEffect, useState, useContext} from "react";
import LearningLayout from "../learning-layout/learning-layout";
import LangContext from "contexts/lang-context";
import translations from 'translations'
import backend from "backend";
import LoaderModalContext from "contexts/loader-modal-context";
import ErrorModalContext from "contexts/error-modal-context";
import LessonContentView from "../lesson-content-view/lesson-content-view";

import styles from './learning.module.css'
import NavigationArrows from "../navigation-arrows/navigation-arrows";

export default (props) => {

    let lang = useContext(LangContext).lang

    let loaderModal = useContext(LoaderModalContext)
    let errorModal = useContext(ErrorModalContext)

    let [course, setCourse] = useState({
        title: 'Some title',
        lessons: [{
            title: 'Lesson 1 111',
            textContent: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.',
            mediaContentPath: null
        }, {
            title: 'Lesson 2 222',
            textContent: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.',
            mediaContentPath: null
        }, {
            title: 'Lesson 3 333',
            textContent: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.',
            mediaContentPath: null
        }]
    })
    let [lessonIndex, setLessonIndex] = useState(0)

    useEffect(() => {
        const query = new URLSearchParams(window.location.search);
        const courseId = query.get('id')

        if (courseId !== undefined) {
            // loaderModal.showModal()
            // backend.Course.GetFullInfoForView(courseId)
            //     .then(response => setCourse(response))
            //     .catch(e => errorModal.showModal(e.message))
            //     .finally(() => loaderModal.close())
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
                <NavigationArrows onNext={next} onPrev={prev} isFirst={lessonIndex === 0} isLast={lessonIndex === (course.lessons.length - 1)}/>
            </LearningLayout>
        }
    </>)
}
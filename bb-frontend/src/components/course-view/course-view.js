import {useEffect, useState, useContext} from "react";
import CourseViewContainer from "../course-view-container/course-view-container";
import LangContext from "contexts/lang-context";
import translations from 'translations'
import backend from "backend";
import LoaderModalContext from "contexts/loader-modal-context";
import ErrorModalContext from "contexts/error-modal-context";

export default (props) => {

    let lang = useContext(LangContext).lang

    let loaderModal = useContext(LoaderModalContext)
    let errorModal = useContext(ErrorModalContext)

    let [course, setCourse] = useState(null)
    let [lessonIndex, setLessonIndex] = useState(-1)

    useEffect(() => {
        const query = new URLSearchParams(window.location.search);
        const courseId = query.get('id')

        if (courseId !== undefined) {
            loaderModal.showModal()
            backend.Course.GetFullInfoForView(courseId)
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

    return (<CourseViewContainer>
        {course === null ? '' :
            <div className="main-container-list-result-cont">
                
                {(!(course.State !== 'Bought') ? (
                            <div className="back-next-btns">

                                <a className="course-back-button"
                                   href={"/Course/InCart/" + course.Id}>
                                    <div className="next">{(translations[lang].incart)}</div>
                                    <img className="next-button-icon" src="/img/Course/next.svg" alt=""/>
                                </a>
                            </div>) :
                        (
                            <div>
                                <div className="back-next-btns">
                                    <a className="course-back-button"
                                       onClick={() => prev()}>
                                        <img className="back-button-icon" src="/img/Course/left.svg" alt=""/>
                                        <div className="left">{(translations[lang].prev)}</div>
                                    </a>

                                    <a className="course-back-button"
                                       onClick={() => next()}>
                                        <div className="next">{(translations[lang].next)}</div>
                                        <img className="next-button-icon" src="/img/Course/next.svg" alt=""/>
                                    </a>
                                </div>
                            </div>
                        )
                )}
            </div>
        }
    </CourseViewContainer>)
}
import {useEffect, useState, useContext} from "react";
import './course-view.css';
import baseurl from 'base-url'
import CourseViewContainer from "../course-view-container/course-view-container";

const GetLesson = async () => {

}

export default (props) => {

    let [course, setCourse] = useState(null)
    let [lessonIndex, setLessonIndex] = useState(-1)


    useEffect(() => {
        const query = new URLSearchParams(window.location.search);
        const courseId = query.get('id')

        if (courseId === undefined) {
            fetch(baseurl + "/Course/GetFullInfoForView?id=" + courseId)
                .then(r => r.json())
                .then(r => setCourse(r))
        } else {
            alert('courseId missing');
            window.location.back()
        }
    }, [])

    let prev = () => {
        setLessonIndex(lessonIndex - 1)
    }
    let next = () => {
        setLessonIndex(lessonIndex + 1)
    }


    return (<CourseViewContainer>
        {course === null ? '' : <div className="main-container">
            <div className="main-container-cont">
                <div className="Demarcation-line"></div>
                <div className="main-container-list-result">
                    <div className="main-container-list-result-cont">
                        <div className="main-container-list-result-cont-1">@header</div>
                        <div className="main-container-list-result-cont-2">
                            {course.Lessons[lessonIndex].TextContent}
                        </div>

                        <div className="video-block">
                            <video id="testVideo" width="800" controls>
                                <source src={course.Lessons[lessonIndex].MediaContentPath} type="video/mp4"/>
                            </video>
                        </div>
                        {(!course.IsBought ? (<div className="back-next-btns">

                            <a className="course-back-button"
                               href={"/Course/InCart/" + course.Id}>
                                <div className="next">@incart</div>
                                <img className="next-button-icon" src="/img/Course/next.svg"/>
                            </a>
                        </div>) : (<div>

                            <div className="back-next-btns">
                                <a className="course-back-button"
                                   onClick={() => prev()}>
                                    <img className="back-button-icon" src="/img/Course/left.svg"/>
                                    <div className="left">@prev</div>
                                </a>

                                <a className="course-back-button"
                                   onClick={() => next()}>
                                    <div className="next">@next</div>
                                    <img className="next-button-icon" src="/img/Course/next.svg"/>
                                </a>
                            </div>
                        </div>))}
                    </div>
                </div>
            </div>
        </div>}
    </CourseViewContainer>)
}
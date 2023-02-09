﻿import { useEffect, useState, useContext } from "react";
import './course-view.css';
import CourseViewContainer from "../course-view-container/course-view-container";

const GetLesson = async () => {
    const query = new URLSearchParams(window.location.search);
    const course = query.get('id')
    let r = fetch('/Course/' + course , {
        headers: {
            'Content-Type': 'application/json;charset=utf-8'
        }, method: 'GET'
    })
    let currentLesson = fetch('/Lessons/' + 0 , {
       headers: {
            'Content-Type': 'application/json;charset=utf-8'
        }, method: 'GET'
    })
    return currentLesson
}

export default (props) => {

    let [lesson, setLesson] = useState({
        TextContent: 'there goes some content', MediaContentPath: 'example.mp4'
    })
    let [course, setCourse] = useState({
        IsBought: true
    })

    useEffect(() => {
        const query = new URLSearchParams(window.location.search);
        const course = query.get('id')
        const data = GetLesson(course)
        // TODO: load course
    }, []);

    let prev = () => {
        let currentLesson =  fetch('/Lessons/' + lesson , {
            headers: {
                'Content-Type': 'application/json;charset=utf-8'
            }, method: 'GET'
        })
    }
    let next = () => {
        let currentLesson =  fetch('/Lessons/' + lesson , {
            headers: {
                'Content-Type': 'application/json;charset=utf-8'
            }, method: 'GET'
        })
    }

    return (<CourseViewContainer>
        <div className="main-container">
            <div className="main-container-cont">
                <div className="Demarcation-line"></div>
                <div className="main-container-list-result">
                    <div className="main-container-list-result-cont">
                        <div className="main-container-list-result-cont-1">@header</div>
                        <div className="main-container-list-result-cont-2">
                            {lesson.TextContent}
                        </div>

                        <div className="video-block">
                            <video id="testVideo" width="800" controls>
                                <source src={lesson.MediaContentPath} type="video/mp4" />
                            </video>
                        </div>
                        {(!course.IsBought ? (<div className="back-next-btns">

                            <a className="course-back-button"
                                href={"/Course/InCart/" + course.Id}>
                                <div className="next">@incart</div>
                                <img className="next-button-icon" src="/img/Course/next.svg" />
                            </a>
                        </div>) : (<div>

                            <div className="back-next-btns">
                                <a className="course-back-button"
                                    onClick={() => prev()}>
                                    <img className="back-button-icon" src="/img/Course/left.svg" />
                                    <div className="left">@prev</div>
                                </a>

                                <a className="course-back-button"
                                    onClick={() => next()}>
                                    <div className="next">@next</div>
                                    <img className="next-button-icon" src="/img/Course/next.svg" />
                                </a>
                            </div>
                        </div>))}
                    </div>
                </div>
            </div>
        </div>
    </CourseViewContainer>)
}
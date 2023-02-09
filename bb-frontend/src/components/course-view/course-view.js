import { useEffect, useState} from "react";
import './course-view.css';
import baseurl from 'base-url'
import CourseViewContainer from "../course-view-container/course-view-container";
import LangContext from "../../contexts/lang-context";
import translations from 'translations'


const GetLesson = async () => {

}

export default (props) => {

    let [course, setCourse] = useState(null)
    let [lessonIndex, setlessonIndex] = useState(null)
    let currentLang = useContext(LangContext).lang

    useEffect(() => {
        const query = new URLSearchParams(window.location.search);
        const courseId = query.get('id')

        async function getData() {
            const response = await fetch(
                baseurl + "/Course/GetFullInfoForView?id=" + courseId
            )
            let actualData = await response.json();
            this.setCourse(actualData)
            console.log(actualData)
        }
        getData()
    }, [])

    let prev = () => {
        setlessonIndex(lessonIndex - 1)
    }
    let next = () => {
        setlessonIndex(lessonIndex + 1)
    }


    return (<CourseViewContainer>
        {course === null ? '' :
            <div className="main-container">
                <div className="main-container-cont">
                    <div className="Demarcation-line"></div>
                    <div className="main-container-list-result">
                        <div className="main-container-list-result-cont">
                            <div className="main-container-list-result-cont-1">{(translations[currentLang].header)}</div>
                            <div className="main-container-list-result-cont-2">
                                {course.Lessons[lessonIndex].TextContent}
                            </div>

                            <div className="video-block">
                                <video id="testVideo" width="800" controls>
                                    <source src={course.Lessons[lessonIndex].MediaContentPath} type="video/mp4" />
                                </video>
                            </div>
                            {(!course.IsBought ? (<div className="back-next-btns">

                                <a className="course-back-button"
                                    href={"/Course/InCart/" + course.Id}>
                                    <div className="next">{(translations[currentLang].incart)}</div>
                                    <img className="next-button-icon" src="/img/Course/next.svg" />
                                </a>
                            </div>) : (<div>

                                <div className="back-next-btns">
                                    <a className="course-back-button"
                                        onClick={() => prev()}>
                                        <img className="back-button-icon" src="/img/Course/left.svg" />
                                        <div className="left">{(translations[currentLang].prev)}</div>
                                    </a>

                                    <a className="course-back-button"
                                        onClick={() => next()}>
                                        <div className="next">{(translations[currentLang].next)}</div>
                                        <img className="next-button-icon" src="/img/Course/next.svg" />
                                    </a>
                                </div>
                            </div>))}
                        </div>
                    </div>
                </div>
            </div>
        }
    </CourseViewContainer>)
}
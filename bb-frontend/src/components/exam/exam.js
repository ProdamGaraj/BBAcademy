import {useEffect, useState, useContext} from "react";
import UserContext from "../../contexts/user-context";
import baseurl from "base-url";
import LangContext from "../../contexts/lang-context";
import translations from 'translations'
import {NavLink} from "react-router-dom";
import backend from "../../backend";
import LoaderModalContext from "../../contexts/loader-modal-context";
import ErrorModalContext from "../../contexts/error-modal-context";


let questionTypeConverter = ({type, answers}) => {

    switch (type) {
        case 1:
            return (<>
                {answers.map((answer, i) => (
                    <div className="test-que" key={i}>
                        <input className="test-que-checkbox" type="radio"/>
                        <label className="course-test-que-text-text">{answer.Content}</label>
                    </div>
                ))}
            </>)
        case 2:
            return (<>
                {answers.map((answer, i) => (
                    <div className="test-que" key={i}>
                        <input className="test-que-checkbox" type="checkbox"/>
                        <label className="course-test-que-text-text">{answer.Content}</label>
                    </div>
                ))}
            </>)
        default:
            return (<div>UNKNOWN TYPE {type}</div>)
    }
}

export default (props) => {
    let currentLang = useContext(LangContext).lang
    let user = useContext(UserContext).user;

    let loaderModal = useContext(LoaderModalContext)
    let errorModal = useContext(ErrorModalContext)

    let [exam, setExam] = useState(null)

    let sendExam = (data) => {
        loaderModal.showModal()
        backend.Exam.Send(data)
            .then(() => {
                // TODO: do something when exam is saved
                loaderModal.close()
            })
            .catch(e => errorModal.showModal(e.message))
            .finally(() => loaderModal.close())
    }

    useEffect(() => {

        const query = new URLSearchParams(window.location.search);
        const courseId = query.get('id')

        if (courseId !== undefined) {
            loaderModal.showModal()
            backend.Exam.GetByCourse(courseId)
                .then(r => {
                    setExam(r)
                    loaderModal.close()
                })
                .catch(e => errorModal.showModal(e.message))
                .finally(() => loaderModal.close())
        }
    }, [])


    return (<>
        <div className="main-container">
            <div className="account-data">
                <div className="user_data">
                    <img className="user_data-photo" src="/img/Account/ur_photo.png"/>
                    <div className="user_data-username">{user.FirstName}</div>
                </div>
                <div className="user_info">
                    <div className="user_info-block">
                        <img src="/img/Account/people.png"/>
                        <div className="user_info-block-name">{user.FirstName} {user.MiddleName} {user.LastName} </div>
                    </div>
                    <div clclassNameass="user_info-block">
                        <img src="/img/Account/bag.svg"/>
                        <div className="user_info-block-name">{user.JobTitle} in {user.Organisation}</div>
                    </div>
                    <div className="user_info-block">
                        <img src="/img/Account/rait.svg"/>
                        <div className="user_info-block-name">{user.Rating}</div>
                    </div>
                    <div className="user_info-block-1">
                        <div className="user_info-block-1-1">
                            <img src="/img/Account/rait.svg"/>
                            <div className="user_info-block-name">{user.RecommendedBy}</div>
                        </div>
                    </div>
                    <NavLink to='/my-certificates'>
                        <div className="user_info-block user_info-block-clickable">
                            <img src="/img/Account/sertif.svg"/>
                            <div className="user_info-block-name">{(translations[currentLang].mycert)}</div>
                        </div>
                    </NavLink>
                </div>
            </div>
        </div>

        <div className="Demarcation-line"></div>

        <div className="main-container-list-result-5">
            <div className="main-container-list-result-cont-test">
                <div className="main-container-list-result-cont-1">{translations[currentLang].exam}</div>
                {exam === null ? '' : exam.Questions.map((question, i) => (
                    <div key={i}>
                        <p>{question.Content}</p>
                        {questionTypeConverter(question.Type, question.Answers)}
                    </div>))}
                <button type="submit" className="course-next-button">
                    <div className="log-in-btn" onClick={() => sendExam(exam)}></div>
                </button>
            </div>
        </div>
    </>)
}
import { useEffect, useState, useContext } from "react";
import UserContext from "../../contexts/user-context";
import baseurl from "base-url";
import LangContext from "../../contexts/lang-context";
import translations from 'translations'
import { NavLink } from "react-router-dom";



let questionTypeConverter = ({ type, answers }) => {

    switch (type) {
        case 1:
            return (<>
                {answers.map((answer, i) => (<div className="test-que">
                    <input className="test-que-checkbox" type="radio" />
                    <label className="course-test-que-text-text">{answer.Content}</label>
                </div>))}
            </>)
        case 2:
            return (<>
                {answers.map((answer, i) => (<div className="test-que">
                    <input className="test-que-checkbox" type="checkbox" />
                    <label className="course-test-que-text-text">{answer.Content}</label>
                </div>))}
            </>)
    }
}

let sendExam = async (data) => {
    await fetch(baseurl + '/Exam/Send', {
        body: JSON.stringify(data),
        headers: { 'Content-Type': 'application/json;charset=utf-8' },
        method: 'POST'
    })
        .then(async r => {
            if (r.status === 200) {
                let response = await r.text();
                alert('Exam send succsesfully\n' + response)
            } else {
                alert('Received status code: ' + r.status + '\n' + r.statusText)
            }
        }, e => {
            alert(e)
        })
}

export default (props) => {
    let [exam, setExam] = useState(null)
    let currentLang = useContext(LangContext).lang
    let user = useContext(UserContext).user;

    useEffect(() => {

        const query = new URLSearchParams(window.location.search);
        const courseId = query.get('id')

        async function getData() {
            const response = await fetch(
                baseurl + "/Exam/GetByCourse&id=" + courseId
            )
            let actualData = await response.json();
            this.setExam(actualData)
            console.log(actualData)
        }
        getData()
    }, [])


    return (<>
        <div className="main-container">
            <div className="account-data">
                <div className="user_data">
                    <img className="user_data-photo" src="~/pict/Account/ur_photo.png" />
                    <div className="user_data-username">{user.FirstName}</div>
                </div>
                <div className="user_info">
                    <div className="user_info-block">
                        <img src="~/pict/Account/people.png" />
                        <div className="user_info-block-name">{user.FirstName} {user.MiddleName} {user.LastName} </div>
                    </div>
                    <div clclassNameass="user_info-block">
                        <img src="~/pict/Account/bag.svg" />
                        <div className="user_info-block-name">{user.JobTitle} in {user.Organisation}</div>
                    </div>
                    <div className="user_info-block">
                        <img src="~/pict/Account/rait.svg" />
                        <div className="user_info-block-name">{user.Rating}</div>
                    </div>
                    <div className="user_info-block-1">
                        <div className="user_info-block-1-1">
                            <img src="~/pict/Account/rait.svg" />
                            <div className="user_info-block-name">{user.RecommendedBy}</div>
                        </div>
                    </div>
                    <NavLink to='/my-certificates'>
                        <div className="user_info-block user_info-block-clickable">
                            <img src="/img/Account/sertif.svg" />
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
                        {questionTypeConverter(question.type, question.answers)}
                    </div>))}
                <button type="submit" className="course-next-button">
                    <div className="log-in-btn" onClick={() => sendExam(exam)}></div>
                </button>
            </div>
        </div>
    </>)
}
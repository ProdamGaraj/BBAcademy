import { useEffect, useState, useContext } from "react";
import UserContext from "../../contexts/user-context";
import baseurl from "base-url";
import LangContext from "../../contexts/lang-context";
import translations from 'translations'

export default (props) => {

    let currentLang = useContext(LangContext).lang
    let user = useContext(UserContext).user;
    let [certificates, setCertificates] = useState([])

    useEffect(() => {
        async function getData() {
            const response = await fetch(
                baseurl + "/Certificates/GetAll"
            )
            let actualData = await response.json();
            this.setCertificates(actualData)
            console.log(actualData)
        }
        getData()
    }, [])

    /*let download = index => {
        async function download(index) {
            const response = await fetch(
                baseurl + "/Certificates/Get&id=" + certificateId
            )
            let actualData = await response.json();
            this.setCourses(actualData)
            console.log(actualData)
        }
        download()
    }*/


    let showMyCert = () => {
        async function showMyCert() {
            fetch(
                baseurl + "/my-certificates/"
            ).then((response) => response.json()).then(response => showMyCert()).catch(error => alert(error))
        }
        showMyCert()
    }

    let showMyCourses = () => {
        async function showMyCourses() {
            fetch(
                baseurl + "/course-view/"
            ).then((response) => response.json()).then(response => showMyCourses()).catch(error => alert(error))
        }
    }
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
                    <div className="user_info-block user_info-block-clickable" onClick={() => showMyCert()}>
                        <img src="/img/Account/sertif.svg" />
                        <div className="user_info-block-name"> onClick(){(translations[currentLang].mycert)}</div>
                    </div>
                </div>
            </div>

            <div className="Demarcation-line"></div>

            <div className="sertif_dwnl-block">
                <div className="my_acc-tab_menu">
                    <div className="media-block">
                        <div className="media-name-1">
                            <img className="media-name-svg" src="pict\people.png" />
                            <div className="media-name-name">{(translations[currentLang].personalArea)}</div>
                        </div>
                        <div className="media-name">
                            <img className="media-name-svg" src="pict\Account\course.svg" />
                            <div className="media-name-name"><div className="media-name-name"><a onClick={() => showMyCert()}>{(translations[currentLang].mycourse)}</a></div></div>
                        </div>
                    </div>
                </div>
                <div className="sertif_dwnl">
                    {certificates.map((certificate, i) => (<div key={i} className="course-wrapper">
                        <div className="sertif_blank">
                            <div className="sertif_name">{/*certificate.Name*/"TODO: certificate name"}</div>
                            <img className="sertif_dwnl_img" src="pict\Account\sertif.png" />
                            <div className="sertif-dwnl-btn">
                                <a className="header-container-right_section-login">{(translations[currentLang].download)}</a>
                            </div>
                        </div>
                    </div>))}
                </div>
            </div>
        </div>
    </>)
}
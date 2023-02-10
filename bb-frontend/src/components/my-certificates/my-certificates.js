import {useEffect, useState, useContext} from "react";
import UserContext from "../../contexts/user-context";
import LangContext from "../../contexts/lang-context";
import translations from 'translations'
import {NavLink} from "react-router-dom";
import backend from "../../backend";
import LoaderModalContext from "../../contexts/loader-modal-context";
import ErrorModalContext from "../../contexts/error-modal-context";

export default (props) => {

    let currentLang = useContext(LangContext).lang
    let user = useContext(UserContext).user;
    let [certificates, setCertificates] = useState([])
    let loaderModal = useContext(LoaderModalContext)
    let errorModal = useContext(ErrorModalContext)

    useEffect(() => {
        loaderModal.showModal()
        backend.Certificate.GetAll()
            .then(r => setCertificates(r))
            .catch(e => errorModal.showModal(e.message))
            .finally(() => loaderModal.close())
    }, [])

    let navigateToMyCertificates = () => {
        window.location.href = '/my-certificates'
    }

    return (<>
        <div className="main-container">
            <div className="account-data">
                <div className="user_data">
                    <img className="user_data-photo" src="/img/perec-percovich.png"/>
                    <div className="user_data-username">{user.FirstName}</div>
                </div>
                <div className="user_info">
                    <div className="user_info-block">
                        <img src="/img/Course/people.svg"/>
                        <div className="user_info-block-name"> {user.LastName} {user.FirstName} {user.MiddleName}
                        </div>
                    </div>
                    <div className="user_info-block">
                        <img src="/img/Account/bag.svg"/>
                        <div className="user_info-block-name">{user.JobTitle} in {user.Organisation}</div>
                    </div>
                    <div className="user_info-block">
                        <img src="/img/Account/rait.svg"/>
                        <div className="user_info-block-name">{user.Rating}</div>
                    </div>
                    <div className="user_info-block-1">
                        <div className="user_info-block-1-1">
                            <img src="/img/Course/peoples.svg"/>
                            <div className="user_info-block-name">{user.RecommendedBy}</div>
                        </div>
                    </div>
                    <NavLink to={'/my-certificates/'}>
                        <div className="user_info-block user_info-block-clickable">
                            <img src="/img/Account/sertif.svg"/>
                            <div className="user_info-block-name">{translations[currentLang].mycert}</div>
                        </div>
                    </NavLink>
                </div>
            </div>

            <div className="Demarcation-line"></div>

            <div className="sertif_dwnl-block">
                <div className="my_acc-tab_menu">
                    <div className="media-block">
                        <div className="media-name-1">
                            <img className="media-name-svg" src="/img/Account/people.png"/>
                            <div className="media-name-name">{(translations[currentLang].personalArea)}</div>
                        </div>
                        <div className="media-name">
                            <img className="media-name-svg" src="/img/Account/course.svg"/>
                            <div className="media-name-name">
                                <div className="media-name-name">
                                    <a onClick={() => navigateToMyCertificates()}>{(translations[currentLang].mycourse)}</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div className="sertif_dwnl">
                    {certificates.map((certificate, i) => (
                        <div key={i} className="course-wrapper">
                            <div className="sertif_blank">
                                <div className="sertif_name">{/*certificate.Name*/"TODO: certificate name"}</div>
                                <img className="sertif_dwnl_img" src="/img/Account/sertif.svg"/>
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
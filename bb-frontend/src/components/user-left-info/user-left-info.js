import {NavLink} from "react-router-dom";
import translations from "translations";

import {useContext, useEffect, useState} from "react";
import LangContext from "contexts/lang-context";
import LoaderModalContext from "contexts/loader-modal-context";
import ErrorModalContext from "contexts/error-modal-context";
import backend from "backend";
import styles from './user-left-info.module.css'

export default () => {

    let lang = useContext(LangContext).lang
    let [user, setUser] = useState({})
    let loaderModal = useContext(LoaderModalContext)
    let errorModal = useContext(ErrorModalContext)

    useEffect(() => {
        loaderModal.showModal()
        user = backend.Account.GetUser()
            .then(u => setUser(u))
            .catch(e => errorModal.showModal(e.response.data.error))
            .finally(() => loaderModal.close())
    }, [])

    return (
        <div className={styles.userDataLeft}>
            <div className={styles.profileHeader}>
                <img className={styles.profilePhoto} src="/img/perec-percovich.png" alt=""/>
                <div className={styles.profilePhotoTitle}>{user.firstName}</div>
            </div>
            <div className={styles.userInfoLine}>
                <img src="/img/Course/people.svg" alt=""/>
                <span> {user.surname} {user.firstName} {user.middleName}</span>
            </div>
            <div className={styles.userInfoLine}>
                <img src="/img/Account/bag.svg" alt=""/>
                <span>{user.jobTitle} in {user.organisation}</span>
            </div>
            <div className={styles.userInfoLine}>
                <img src="/img/Account/rait.svg" alt=""/>
                <span>{user.rating}</span>
            </div>
            <div className={styles.userInfoLine}>
                <img src="/img/Course/peoples.svg" alt=""/>
                <span>{user.recommendedBy}</span>
            </div>
            <NavLink to={'/my-certificates'} className={styles.certA}>
                <div className={styles.userInfoLine + ' ' + styles.cursorPointer}>
                    <img src="/img/Account/sertif.svg" alt=""/>
                    <span>{translations[lang].mycert}</span>
                </div>
            </NavLink>
        </div>
    )
}
import {NavLink} from "react-router-dom";
import translations from "translations";

import styles from './user-left-layout.module.css'
import {useContext, useEffect, useState} from "react";
import UserContext from "contexts/user-context";
import LangContext from "contexts/lang-context";
import LoaderModalContext from "../../contexts/loader-modal-context";
import ErrorModalContext from "../../contexts/error-modal-context";
import backend from "../../backend";

export default ({children}) => {

    //let user = useContext(UserContext).user
    let currentLang = useContext(LangContext).lang
    let [user, setUser] = useState({})
    let loaderModal = useContext(LoaderModalContext)
    let errorModal = useContext(ErrorModalContext)
    
    useEffect(()=>{
        loaderModal.showModal()
        backend.Account.GetUser()
            .then(u=>setUser(u))
            .catch(e => errorModal.showModal(e.response.data.error))
            .finally(() => loaderModal.close())
    },[])
    
    
    return (<>
        <div className={styles.layout}>
            <div className={styles.layoutContainer}>
                <div className={styles.userDataLeft}>
                    <div className={styles.profileHeader}>
                        <img className={styles.profilePhoto} src="/img/perec-percovich.png" alt=""/>
                        <div className={styles.profilePhotoTitle}>{user.FirstName}</div>
                    </div>
                    <div className={styles.userInfoLine}>
                        <img src="/img/Course/people.svg" alt=""/>
                        <span> {user.LastName} {user.Surname} {user.MiddleName}</span>
                    </div>
                    <div className={styles.userInfoLine}>
                        <img src="/img/Account/bag.svg" alt=""/>
                        <span>{user.JobTitle} in {user.Organisation}</span>
                    </div>
                    <div className={styles.userInfoLine}>
                        <img src="/img/Account/rait.svg" alt=""/>
                        <span>{user.Rating}</span>
                    </div>
                    <div className={styles.userInfoLine}>
                        <img src="/img/Course/peoples.svg" alt=""/>
                        <span>{user.RecommendedBy}</span>
                    </div>
                    <NavLink to={'/my-certificates'} className={styles.certA}>
                        <div className={styles.userInfoLine + ' ' + styles.cursorPointer}>
                            <img src="/img/Account/sertif.svg" alt=""/>
                            <span>{translations[currentLang].mycert}</span>
                        </div>
                    </NavLink>
                </div>

                <div className={styles.divider}/>

                <div className={styles.content}>
                    {children}
                </div>
            </div>
        </div>
    </>)
}
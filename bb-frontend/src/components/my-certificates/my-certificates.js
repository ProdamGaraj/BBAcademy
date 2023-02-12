import {useEffect, useState, useContext} from "react";
import LangContext from "contexts/lang-context";
import translations from 'translations'
import backend from "backend";
import LoaderModalContext from "contexts/loader-modal-context";
import ErrorModalContext from "contexts/error-modal-context";
import UserLeftLayoutContainer from "../user-left-layout/user-left-layout";
import MainNavigator from "../main-navigator/main-navigator";

import styles from './my-certificates.module.css'

export default () => {

    let lang = useContext(LangContext).lang
    let [certificates, setCertificates] = useState([])
    let loaderModal = useContext(LoaderModalContext)
    let errorModal = useContext(ErrorModalContext)

    useEffect(() => {
        loaderModal.showModal()
        backend.Certificate.GetAllForDashboard()
            .then(r => setCertificates(r))
            .catch(e => errorModal.showModal(e.response.data.error))
            .finally(() => loaderModal.close())
    }, [])

    return (<>
        <UserLeftLayoutContainer>
            <MainNavigator/>
            <div className={styles.certContainer}>
                {certificates.map((certificate, i) => (
                    <div key={i} className={styles.certElement}>
                        <span className={styles.certTitle}>{"TODO: certificate name"}</span>
                        <img className={styles.certImage} src="/img/Account/sertif.png" alt=""/>
                            <span className={styles.certDownloadButton}>
                                {(translations[lang].downloadPDF)}
                            </span>
                    </div>
                ))}
            </div>
        </UserLeftLayoutContainer>
    </>)
}
import {useEffect, useState, useContext} from "react";
import LangContext from "contexts/lang-context";
import translations from 'translations'
import backend from "backend";
import LoaderModalContext from "contexts/loader-modal-context";
import ErrorModalContext from "contexts/error-modal-context";
import UserLeftLayoutContainer from "../user-left-layout/user-left-layout";
import MainNavigator from "../main-navigator/main-navigator";

import styles from './my-certificates.module.css'
import baseUrl from "../../base-url";

export default () => {

    let lang = useContext(LangContext).lang
    let [certNames, setCertNames] = useState([])
    let loaderModal = useContext(LoaderModalContext)
    let errorModal = useContext(ErrorModalContext)

    useEffect(() => {
        loaderModal.showModal()
        backend.Certificate.GetAllForDashboard()
            .then(r => setCertNames(r))
            .catch(e => errorModal.showModal(e.response.data.error))
            .finally(() => loaderModal.close())
    }, [])

    const downloadPdf = certName => {
        loaderModal.showModal()
        backend.Certificate.GetCertificate(certName)
            .then(response => {
                const url = window.URL.createObjectURL(new Blob([response])); // you can mention a type if you wish
                const link = document.createElement("a");
                link.href = url;
                link.setAttribute("download", "certificate.pdf"); //this is the name with which the file will be downloaded
                link.click();
                // no need to append link as child to body.
                setTimeout(() => window.URL.revokeObjectURL(url), 0); // this is important too, otherwise we will be unnecessarily spiking memory!
            })
            .catch(e => errorModal.showModal(e.response.data.error))
            .finally(() => loaderModal.close())
    };

    return (<>
        <UserLeftLayoutContainer>
            <MainNavigator/>
            <div className={styles.certContainer}>
                {certNames.map((certName, i) => (
                    <div key={i} className={styles.certElement}>
                        
                        {/*Это weird штука позволяет добавить троеточие посередине названия файла*/}
                        {/*https://stackoverflow.com/a/42901072*/}
                        
                        <span className={styles.certTitle}>
                            <span className={styles.certTitleEllipsis}>    
                                {certName}
                            </span>
                            <span className={styles.certTitleIndent}>    
                                {certName}
                            </span>
                        </span>
                        
                        {/*Эта штука может показывать PDF прямо в вёрстке, но выглядит калично*/}
                        
                        {/*<object className={styles.certImage}*/}
                        {/*        data={`${baseUrl}/Certificate/GetCertificate?name=${certName}&scrollbar=0`}*/}
                        {/*        width="256"*/}
                        {/*        height="362"*/}
                        {/*        type="application/pdf">*/}
                        {/*    <span>Здесь должна быть картинка сертификата</span>*/}
                        {/*</object>*/}
                        
                        <img className={styles.certImage} src="/img/Account/sertif.png" alt=""/>
                        <span className={styles.certDownloadButton} onClick={() => downloadPdf(certName)}>
                                {(translations[lang].downloadPDF)}
                        </span>
                    </div>
                ))}
            </div>
        </UserLeftLayoutContainer>
    </>)
}
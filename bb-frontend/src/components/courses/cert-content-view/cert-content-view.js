import styles from "./cert-content-view.module.css";
import translations from "translations";
import {useContext} from "react";
import LangContext from "contexts/lang-context";
import backend from "../../../backend";
import LoaderModalContext from "../../../contexts/loader-modal-context";
import ErrorModalContext from "../../../contexts/error-modal-context";

export default ({courseTitle, certName}) => {
    let lang = useContext(LangContext).lang

    let loaderModal = useContext(LoaderModalContext)
    let errorModal = useContext(ErrorModalContext)

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

    return (
        <>
            <div className={styles.contentContainer}>
                <div className={styles.contentHeading}>
                    {translations[lang].end}
                </div>

                <div className={styles.contentText}>
                    {translations[lang].congratz(courseTitle)}
                </div>

                <div className={styles.certImageContainer}>
                    <img className={styles.certImage} src="/img/Account/sertif.png" alt=""/>
                </div>

                <div className={styles.buttonsWrapper}>
                    <span className={styles.certDownloadButton} onClick={() => downloadPdf(certName)}>
                                    {(translations[lang].downloadPDF)}
                    </span>
                    <span className={styles.certDownloadButton} onClick={() => window.location.href='/courses'}>
                                    {(translations[lang].allcourses)}
                    </span>
                </div>
            </div>
        </>
    )

}
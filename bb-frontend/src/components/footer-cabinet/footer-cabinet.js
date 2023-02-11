import translations from 'translations'
import LangContext from "../../contexts/lang-context";

import styles from "./footer-cabinet.module.css";
import {useContext} from "react";

export default () => {
    let langContext = useContext(LangContext);
    let currentLang = langContext.lang
    return (
        <>
            <footer className={styles.footerContainer}>
                <div className={styles.rightContainer}>
                    <img className={styles.logoImg} src="/img/Shared/logo-icon-1.svg" alt=""/>
                    <label className={styles.allRights}>
                        {translations[currentLang].rights}
                    </label>
                    
                </div>
                <div className={styles.rightContainer}>
                    <a className={styles.contactHref} href="#">{translations[currentLang].learningproc}</a>
                    <a className={styles.contactHref} href="#">{translations[currentLang].usagepol}</a>
                    <a className={styles.contactHref} href="#">{translations[currentLang].privpol}</a>
                    <img src="/img/Shared/foot-teleg.png" alt="telegram"/>
                    <a className={styles.contactHref} href="#">@bilimbank</a>
                    <img src="/img/Shared/foot-inst.png" alt="instagram"/>
                    <a className={styles.contactHref} href="#">@bilimbank</a>
                </div>

            </footer>
        </>
    );
}

import translations from 'translations'
import LangContext from "../../contexts/lang-context";

import styles from "./footer-cabinet.module.css";
import {useContext} from "react";

export default () => {
    let lang = useContext(LangContext).lang;
    return (
        <>
            <footer className={styles.footerContainer}>
                <div className={styles.leftContainer}>
                    <img className={styles.logoImg} src="/img/Shared/logo-icon-1.svg" alt=""/>
                    <span className={styles.allRights}>
                        {translations[lang].rights}
                    </span>
                    
                </div>
                <div className={styles.rightContainer}>
                    <a className={styles.contactHref} href="#">{translations[lang].learningproc}</a>
                    <a className={styles.contactHref} href="#">{translations[lang].usagepol}</a>
                    <a className={styles.contactHref} href="#">{translations[lang].privpol}</a>
                    <img src="/img/Shared/foot-teleg.png" alt="telegram"/>
                    <a className={styles.contactHref} href="#">@bilimbank</a>
                    <img src="/img/Shared/foot-inst.png" alt="instagram"/>
                    <a className={styles.contactHref} href="#">@bilimbank</a>
                </div>

            </footer>
        </>
    );
}

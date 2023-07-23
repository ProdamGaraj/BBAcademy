import translations from 'translations'
import LangContext from "contexts/lang-context";
import styles from "./footer-landing.module.css";
import {useContext} from "react";

export default () => {
    let lang = useContext(LangContext).lang

    return (<>
        <footer className={styles.footer}>
            <div className={styles.footerTitleContainer}>
                <img className={styles.footerLogo} src="/img/Shared/logo-icon-1.svg" alt="logo"/>
                <label className={styles.footerTile}>BilimTextile</label>
            </div>
            <div className={styles.footerContentContainer}>
                <div className={styles.footerColumn}>
                    <div className={styles.columnElement}>{translations[lang].about}</div>
                    <div className={styles.columnElement}>{translations[lang].vacancies}</div>
                    <div className={styles.columnElement}>{translations[lang].company}</div>
                    <br/>
                    <div className={styles.columnElement}>{translations[lang].learningproc}</div>
                    <div className={styles.columnElement}>{translations[lang].usagepol}</div>
                    <div className={styles.columnElement}>{translations[lang].privpol}</div>
                    <div className={styles.columnElement}>{translations[lang].companyInfo}</div>
                </div>
                <div className={styles.footerColumn}>
                    <div className={styles.columnElement}>{translations[lang].online}</div>
                    <div className={styles.columnElement}>{translations[lang].programms}</div>
                    <div className={styles.columnElement}>{translations[lang].webinars}</div>
                    <div className={styles.columnElement}>{translations[lang].fests}</div>
                    <div className={styles.columnElement}>{translations[lang].carrier}</div>
                </div>
                <div className={styles.footerColumn}>
                    <div className={styles.columnElement}>
                        <img className={styles.footerIcon} src="/img/Shared/foot-teleg.png" alt="telegram"/>
                        <a className={styles.columnHref} href="#">@bilimtextile</a>
                    </div>
                    <div className={styles.columnElement}>
                        <img className={styles.footerIcon} src="/img/Shared/foot-inst.png" alt="instagram"/>
                        <a className={styles.columnHref} href="#">@bilimtextile</a>
                    </div>
                </div>
            </div>
            <div className={styles.allRights}>
                {translations[lang].rights}
            </div>
        </footer>
    </>)
}
import translations from 'translations'
import LangContext from "../../contexts/lang-context";
import styles from "./footer-login.module.css";
import {useContext} from "react";

export default () => {
    let langContext = useContext(LangContext);
    let currentLang = langContext.lang

    return (<>
        <footer className={styles.footer}>
            <div className={styles.footerTitleContainer}>
                <div>
                    <img className={styles.footerLogo} src="/img/Shared/logo-icon-1.svg" alt="logo"/>
                    <label className={styles.footerTile}>BilimBank</label>
                </div>
                <div></div>{/*TODO: refactoring 1000px gap*/}
            </div>
            <div className={styles.footerContentContainer}>
                <div className={styles.footerColumn}>
                    <div className={styles.columnElement}>{translations[currentLang].about}</div>
                    <div className={styles.columnElement}>{translations[currentLang].vacancies}</div>
                    <div className={styles.columnElement}>{translations[currentLang].company}</div>
                    <br/>
                    <div className={styles.columnElement}>{translations[currentLang].learningproc}</div>
                    <div className={styles.columnElement}>{translations[currentLang].usagepol}</div>
                    <div className={styles.columnElement}>{translations[currentLang].privpol}</div>
                    <div className={styles.columnElement}>{translations[currentLang].companyInfo}</div>
                </div>
                <div className={styles.footerColumn}>
                    <div className={styles.columnElement}>{translations[currentLang].online}</div>
                    <div className={styles.columnElement}>{translations[currentLang].programms}</div>
                    <div className={styles.columnElement}>{translations[currentLang].webinars}</div>
                    <div className={styles.columnElement}>{translations[currentLang].fests}</div>
                    <div className={styles.columnElement}>{translations[currentLang].carrier}</div>
                </div>
                <div className={styles.footerColumn}>
                    <div className={styles.columnElement}>
                        <img className={styles.footerIcon} src="/img/Shared/foot-teleg.png" alt="telegram"/>
                        <a className={styles.columnHref} href="#">@bilimbank</a>
                    </div>
                    <div className={styles.columnElement}>
                        <img className={styles.footerIcon} src="/img/Shared/foot-inst.png" alt="instagram"/>
                        <a className={styles.columnHref} href="#">@bilimbank</a>
                    </div>
                </div>
            </div>
            <div className={styles.allRights}>
                {translations[currentLang].rights}
            </div>
        </footer>
    </>)
}
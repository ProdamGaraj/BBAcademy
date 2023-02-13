import {useContext} from "react";
import styles from "../header-cabinet/header-cabinet.module.css"
import translations from 'translations'
import LangContext from "contexts/lang-context";

export default (props) => {

    let langContext = useContext(LangContext);
    let lang = langContext.lang

    let changeLang = (lng) => {
        langContext.setLang(lng)
    }

    const dropTokenNavigateToLogin = () => {
        localStorage.removeItem('token');
        window.location.href = '/login'
    };

    return (<>
        <header className={styles.headerContainer}>
            <a className={styles.leftContainer} href="/">
                <img src="/img/Shared/main-logo.png" className={styles.logoImage} alt="logo"/>
                <div className={styles.logoTitle}>BilimBank</div>
            </a>
            <div className={styles.rightContainer}>
                <div className={styles.headerLang}>

                    <div className={styles.headerLangElement + (lang === 'uz' ? (' ' + styles.langSelected) : '')}
                         onClick={() => changeLang('uz')}>uz
                    </div>

                    <div className={styles.headerLangElement + (lang === 'ru' ? (' ' + styles.langSelected) : '')}
                         onClick={() => changeLang('ru')}>ru
                    </div>
                </div>
                <a className={styles.allCoursesA} href="/courses">
                    <div className={styles.allCoursesButton}>
                        <img src="/img/Shared/all-courses-arrow.svg" alt="logo"/>
                        <span className={styles.allCoursesTitle}>{translations[lang].allcourses}</span>
                    </div>
                </a>
                <a href="/payment"><img src="/img/Account/shop.svg" alt="logo"/></a>
                <img src="/img/Shared/bell.svg" alt="logo"/>

                <span className={styles.headerContainerRightSectionLogin}
                      onClick={() => dropTokenNavigateToLogin()}>{translations[lang].exit}</span>
            </div>
        </header>
    </>)
}
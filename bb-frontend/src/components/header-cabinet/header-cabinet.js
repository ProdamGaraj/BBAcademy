import {useContext} from "react";
import styles from "../header-cabinet/header-cabinet.module.css"
import translations from 'translations'
import LangContext from "contexts/lang-context";

export default (props) => {

    let langContext = useContext(LangContext);
    let currentLang = langContext.lang

    let changeLang = (lang) => {
        langContext.setLang(lang)
    }

    let isLogin = window.location.href.endsWith('/login')

    return (<>
        <header className={styles.headerContainer}>
            <div className={styles.leftContainer}>
                <img src="/img/Shared/main-logo.png" className={styles.logoImage} alt="logo"/>
                <div className={styles.logoTitle}>BilimBank</div>
            </div>
            <div className={styles.rightContainer}>
                <div className={styles.headerLang}>
                            <div className={styles.headerLangElement}
                                  onClick={() => changeLang('uz')}>uz</div>

                            <div className={styles.headerLangElement}
                                  onClick={() => changeLang('ru')}>ru</div>
                </div>
                <a href="/courses">
                    <div className={styles.allCoursesButton}>
                        <img src="/img/Shared/all-courses-arrow.svg" className={styles.allCoursesArrow} alt="logo"/>
                        <span className={styles.allCoursesTitle}>{translations[currentLang].allcourses}</span>
                    </div>
                </a>
                <a href="/payment"><img src="/img/Account/shop.svg" alt="logo"/></a>
                <img src="/img/Shared/bell.svg"  alt="logo"/>
                <img src="/img/perec-percovich.png" className={styles.userMiniAvatar} alt="logo"/>
            </div>
        </header>
    </>)
}
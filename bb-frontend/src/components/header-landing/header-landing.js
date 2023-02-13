import translations from "translations";
import {useContext} from "react";
import LangContext from "contexts/lang-context";
import styles from './header-landing.module.css'

export default (props) => {

    let langContext = useContext(LangContext);
    let lang = langContext.lang

    let changeLang = (lang) => {
        langContext.setLang(lang)
    }

    let isLogin = window.location.pathname.endsWith('/login')

    const tryNavigateToLogin = () => {
        if (localStorage.getItem('token') !== null) {
            window.location.href = '/courses'
        }
        else{
            window.location.href = '/login'
        }
    };

    return (<>
        <header className={styles.headerWrapper}>
            <div className={styles.headerContainer}>
                <div className={styles.headerContainerLeftSection}>
                    <img className={styles.svgLogo} src="/img/Shared/logo-icon-2.svg" alt="logo"/>
                    <div className={styles.headerContainerLeftSectionText}>BilimBank</div>
                </div>
                <div className={styles.headerContainerRightSection}>
                    <div className={styles.headerContainerRightSectionLang}>
                        <div className={styles.headerContainerRightSectionLangElement}>
                            <span
                                className={styles.headerLangElement + (lang === 'uz' ? (' ' + styles.langSelected) : '')}
                                onClick={() => changeLang('uz')}>uz</span>
                        </div>
                        <div className={styles.headerContainerRightSectionLangElement}>
                            <span
                                className={styles.headerLangElement + (lang === 'ru' ? (' ' + styles.langSelected) : '')}
                                onClick={() => changeLang('ru')}>ru</span>
                        </div>
                    </div>
                    <span className={styles.headerContainerRightSectionLogin}
                          onClick={() => tryNavigateToLogin()}>{translations[lang][(isLogin ? 'reg' : 'enter')]}</span>
                </div>
            </div>
        </header>
    </>)
}
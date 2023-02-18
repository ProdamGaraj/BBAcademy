import {useContext, useEffect, useState} from "react";
import styles from "../header-cabinet/header-cabinet.module.css"
import translations from 'translations'
import LangContext from "contexts/lang-context";

export default (_) => {

    let langContext = useContext(LangContext);
    let lang = langContext.lang

    const [menuOpened, setMenyOpened] = useState(false);

    useEffect(() => {
        document.querySelector('body').style.overflow = menuOpened ? 'hidden' : 'auto';
    }, [menuOpened]);

    let changeLang = (lng) => {
        langContext.setLang(lng)
    }

    const dropTokenNavigateToLogin = () => {
        localStorage.removeItem('token');
        window.location.href = '/login'
    };

    const toggleMenuOpened = () => {
        setMenyOpened(!menuOpened);
    }

    return (<>
        <header className={styles.headerContainer}>
            <a className={styles.leftContainer} href="/">
                <img src="/img/Shared/main-logo.png" className={styles.logoImage} alt="logo"/>
                <div className={styles.logoTitle}>BilimBank</div>
            </a>
            <div className={styles.rightContainer}>
                <div className={styles.headerLang}>

                    <span className={styles.headerLangElement + (lang === 'uz' ? (' ' + styles.langSelected) : '')}
                         onClick={() => changeLang('uz')}>uz
                    </span>

                    <span className={styles.headerLangElement + (lang === 'ru' ? (' ' + styles.langSelected) : '')}
                         onClick={() => changeLang('ru')}>ru
                    </span>
                </div>
                <a className={styles.allCoursesA} href="/courses">
                    <div className={styles.allCoursesButton}>
                        <img src="/img/Shared/all-courses-arrow.svg" alt="logo"/>
                        <span className={styles.allCoursesTitle}>{translations[lang].allcourses}</span>
                    </div>
                </a>
                <a href="/payment"><img src="/img/Account/shop.svg" alt="logo"/></a>
                {/*<img src="/img/Shared/bell.svg" alt="logo"/>*/}

                <span className={styles.loginButton}
                      onClick={() => dropTokenNavigateToLogin()}>{translations[lang].exit}</span>
            </div>
            <button onClick={toggleMenuOpened}
                    className={styles.menuTrigger}>
                <img className={!menuOpened ? styles.shownTrigger : ''} src="/img/Shared/menu.svg"
                     alt="Open Menu"/>
                <img className={menuOpened ? styles.shownTrigger : ''} src="/img/Shared/close.svg"
                     alt="Close Menu"/>
            </button>
        </header>

        <nav className={`${styles.navMenu} ${menuOpened && styles.navMenuShown}`}>
            <div className={styles.headerLang}>

                <span className={styles.headerLangElement + (lang === 'uz' ? (' ' + styles.langSelected) : '')}
                     onClick={() => changeLang('uz')}>uz
                </span>

                <span className={styles.headerLangElement + (lang === 'ru' ? (' ' + styles.langSelected) : '')}
                     onClick={() => changeLang('ru')}>ru
                </span>
            </div>
            <a className={styles.allCoursesA} href="/courses">
                <div className={styles.allCoursesButton}>
                    <img src="/img/Shared/all-courses-arrow.svg" alt="logo"/>
                    <span className={styles.allCoursesTitle}>{translations[lang].allcourses}</span>
                </div>
            </a>
            <a href="/payment"><img src="/img/Account/shop.svg" alt="Go to cart"/></a>
            {/*<img src="/img/Shared/bell.svg" alt="logo"/>*/}

            <span className={styles.loginButton}
                  onClick={() => dropTokenNavigateToLogin()}>{translations[lang].exit}</span>
        </nav>
    </>);
}

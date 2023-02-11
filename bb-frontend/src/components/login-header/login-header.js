import '../template/template.css'
import translations from "translations";
import {useContext} from "react";
import LangContext from "contexts/lang-context";

export default (props) => {

    let langContext = useContext(LangContext);
    let currentLang = langContext.lang

    let changeLang = (lang) => {
        langContext.setLang(lang)
    }

    let isLogin = window.location.href.endsWith('/login')
    let atHome = window.location.href.endsWith('/home')

    return (<>
        <header>
            <div className="header-container">
                <div className="header-container-left_section">
                    <img className="svg-logo" src="/img/Shared/logo-icon-2.svg" alt="logo"/>
                    <div className="header-container-left_section-text">BilimBank</div>
                </div>
                <div className="header-container-right_section">
                    <div className="header-container-right_section-lang">
                        <div className="header-container-right_section-lang-element">
                            <span className="a-lang"
                                  onClick={() => changeLang('uz')}>uz</span>
                        </div>
                        <div className="header-container-right_section-lang-element">
                            <span className="a-lang"
                                  onClick={() => changeLang('ru')}>ru</span>
                        </div>
                    </div>
                    <a className="header-container-right_section-login"
                       href={(isLogin ? '/register' : '/login')}>{translations[currentLang][(isLogin ? 'reg' : 'enter')]}</a>
                </div>
            </div>
        </header>
    </>)
}
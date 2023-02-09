import './template.css'
import {useContext} from "react";
import translations from 'translations'
import LangContext from "../../contexts/lang-context";

export default (props) => {

    let langContext = useContext(LangContext);
    let currentLang = langContext.lang

    let changeLang = (lang) => {
        langContext.setLang(lang)
    }

    let isLogin = window.location.href.endsWith('/login')

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
                       href={(isLogin ? '/register' : '/login')}>{translations[currentLang]['enter']}</a>
                </div>
            </div>
        </header>

        <main role="main" className="pb-3">
            {props.children}
        </main>

        <footer>
            <div className="footer-cont">
                <div className="footer-cont-1">
                    <img className="footer-logo" src="/img/Shared/logo-icon-1.svg" alt="logo"/>
                    <div className="footer-cont-1-text">BilimBank</div>
                </div>
                <div className="footer-table">
                    <div className="footer-table-column-1">
                        <div className="footer-table-column-1-1">
                            <div className="column-element">{translations[currentLang].about}</div>
                            <div className="column-element">{translations[currentLang].vacancies}</div>
                            <div className="column-element">{translations[currentLang].company}</div>

                        </div>
                        <div className="footer-table-column-1-2">
                            <div className="column-element">{translations[currentLang].learningproc}</div>
                            <div className="column-element">{translations[currentLang].usagepol}</div>
                            <div className="column-element">{translations[currentLang].privpol}</div>
                            <div className="column-element">{translations[currentLang].companyinfo}</div>
                        </div>
                    </div>
                    <div className="footer-table-column-2">
                        <div className="column-element">{translations[currentLang].online}</div>
                        <div className="column-element">{translations[currentLang].prog}</div>
                        <div className="column-element">{translations[currentLang].webs}</div>
                        <div className="column-element">{translations[currentLang].fests}</div>
                        <div className="column-element">{translations[currentLang].carrier}</div>
                    </div>
                    <div className="footer-table-column-3">
                        <div className="column-element">
                            <img className="footer-icon" src="/img/Shared/foot_teleg.png" alt="telegram"/>
                            <a className="column-href" href="#">{translations[currentLang].bilimbank}</a>
                        </div>
                        <div className="column-element">
                            <img className="footer-icon" src="/img/Shared/foot_inst.png" alt="instagram"/>
                            <a className="column-href" href="#">{translations[currentLang].bilimbank}</a>
                        </div>
                    </div>
                </div>
            </div>
            <div className="all-rights">
                <div className="all-rights-1">
                    {translations[currentLang].rights}
                </div>
            </div>
        </footer>
    </>)
}
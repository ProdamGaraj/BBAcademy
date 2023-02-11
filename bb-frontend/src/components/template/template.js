import './template.css'
import {useContext} from "react";
import translations from 'translations'
import LangContext from "../../contexts/lang-context";
import Header from "../header/header";
import LoginHeader from "../header/login-header";

let headerResolver = (href) => {
    if (href.endsWith('/') ||
        href.endsWith('/login') ||
        href.endsWith('/register')
    ) {
        return <LoginHeader></LoginHeader>
    }
    return <Header></Header>
}
export default (props) => {

    let langContext = useContext(LangContext);
    let currentLang = langContext.lang

    let changeLang = (lang) => {
        langContext.setLang(lang)
    }

    return (<>
        {headerResolver(window.location.href)}
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

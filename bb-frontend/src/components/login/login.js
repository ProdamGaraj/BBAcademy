import {useContext} from "react";

import translations from 'translations'
import LangContext from "../../contexts/lang-context";

import './login.css'

export default (props) => {

    let currentLang = useContext(LangContext).lang

    return (<>
            <div className="main-container">
                <form className="log-block-form">
                    <div className="log-container">
                        <label className="log-label">{translations[currentLang].login}</label>
                        <input placeholder=""
                               className="form-control textbox-dg font-weight-bold text-center reg-log-input"
                               type="text"/>

                        <label className="log-label">{translations[currentLang].password}</label>
                        <input placeholder=""
                               className="form-control textbox-dg font-weight-bold text-center reg-log-input"
                               type="password"/>

                        <div className="d-grid gap-2 d-md-block float-right">

                            <button type="submit" className="registerbtn">
                                <div className="log-in-btn">{translations[currentLang].enter}</div>
                            </button>
                        </div>
                    </div>
                </form>
            </div>
        </>)
}
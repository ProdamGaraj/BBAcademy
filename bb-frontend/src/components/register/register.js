import './register.css'
import {useContext} from "react";

import translations from 'translations'
import LangContext from "../../contexts/lang-context";

export default () => {

    let currentLang = useContext(LangContext).lang
    
    return (<>
        <div className="main-container">
            <form className="log-block-form">
                <div className="log-container">
                    <label className="log-label">{translations[currentLang].firstname}</label>
                    <input className="reg-log-input" type="text" required/>

                    <label className="log-label">{translations[currentLang].surname}</label>
                    <input className="reg-log-input" type="text" required/>

                    <label className="log-label">{translations[currentLang].lastname}</label>
                    <input className="reg-log-input" type="text" required/>

                    <label className="log-label">{translations[currentLang].email}</label>
                    <input className="reg-log-input" type="text" required/>

                    <label className="log-label">{translations[currentLang].login}</label>
                    <input className="reg-log-input" type="text" required/>

                    <label className="log-label">{translations[currentLang].password}</label>
                    <input className="reg-log-input" type="password" required/>

                    <label className="log-label">{translations[currentLang].confirmpassword}</label>
                    <input className="reg-log-input" type="password" required/>

                    <label className="log-label-l">
                        {translations[currentLang].condition}
                        <br/>
                        <div className="log-label-l-sr">{translations[currentLang].unnesessaryfield}</div>
                    </label>
                    <input type="text" list="banks" className="reg-log-input"/>
                    <select id="banks">
                        <option value="bank0">BilimBank</option>
                        <option value="bank1">BilimBank</option>
                        <option value="bank2">BilimBank</option>
                    </select>
                    <button type="submit" className="registerbtn">
                        <div className="log-in-btn">{translations[currentLang].endreg}</div>
                    </button>
                </div>
            </form>
        </div>
    </>)
}
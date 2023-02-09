import {useContext, useState} from "react";

import translations from 'translations'
import baseurl from 'base-url'
import LangContext from "../../contexts/lang-context";

import './login.css'

const DoLogin = async (data) => {
    let r = await fetch(baseurl + '/Account/Login', {
        body: JSON.stringify(data), headers: {
            'Content-Type': 'application/json;charset=utf-8'
        }, method: 'POST'
    })

    if (r.status === 200) {
        let response = await r.text();
        alert('Logined successfully\n' + response)
    } else {
        alert('Received status code: ' + r.status + '\n' + r.statusText)
    }
}

export default (props) => {
    let currentLang = useContext(LangContext).lang

    let [login, setLogin] = useState('')
    let [password, setPassword] = useState('')

    const onLogin = () => {
        DoLogin({Login: login, Password: password})
            .then(() => {
                window.location.href = '/courses'
            });
    }

    return (<>
        <div className="main-container">
            <div className="log-container">
                <label className="log-label">{translations[currentLang].login}</label>
                <input placeholder="" value={login} onChange={e => setLogin(e.target.value)}
                       className="form-control textbox-dg font-weight-bold text-center reg-log-input"
                       type="text"/>

                <label className="log-label">{translations[currentLang].password}</label>
                <input placeholder="" value={password} onChange={e => setPassword(e.target.value)}
                       className="form-control textbox-dg font-weight-bold text-center reg-log-input"
                       type="password"/>

                <div className="d-grid gap-2 d-md-block float-right">
                    <button className="registerbtn"
                            onClick={() => onLogin()}>
                        <div className="log-in-btn">{translations[currentLang].enter}</div>
                    </button>
                </div>
            </div>
        </div>
    </>)
}
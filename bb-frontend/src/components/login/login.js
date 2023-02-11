import {useContext, useState} from "react";

import translations from 'translations'
import LangContext from "contexts/lang-context";

import './login.css'
import backend from "backend";
import LoaderModalContext from "contexts/loader-modal-context";
import ErrorModalContext from "contexts/error-modal-context";


export default (props) => {
    let currentLang = useContext(LangContext).lang
    let loaderModal = useContext(LoaderModalContext)
    let errorModal = useContext(ErrorModalContext)

    let [login, setLogin] = useState('')
    let [password, setPassword] = useState('')

    const onLogin = () => {
        loaderModal.showModal()
        backend.Account.Login({Login: login, Password: password})
            .then((token) => {
                localStorage.setItem('token', token)
                console.log('Authorized: ' + token)
                window.location.href = '/courses'
            })
            .catch(e => errorModal.showModal(e.message))
            .finally(() => loaderModal.close())
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
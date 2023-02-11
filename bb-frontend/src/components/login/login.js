import {useContext, useState} from "react";

import translations from 'translations'
import LangContext from "contexts/lang-context";

import styles from './login.module.css'
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
        <div className={styles.logContainer}>
            <label className={styles.logLabel}>{translations[currentLang].login}</label>
            <input placeholder="" value={login} onChange={e => setLogin(e.target.value)}
                   className={styles.loginInput}
                   type="text"/>
            <label className={styles.logLabel}>{translations[currentLang].password}</label>
            <input placeholder="" value={password} onChange={e => setPassword(e.target.value)}
                   className={styles.loginInput}
                   type="password"/>
            <div className="d-grid gap-2 d-md-block float-right">
                <div className={styles.loginBtn} onClick={() => onLogin()}>{translations[currentLang].enter}</div>
            </div>
        </div>
    </>)
}
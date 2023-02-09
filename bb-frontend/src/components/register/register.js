import './register.css'
import {useContext, useState} from "react";

import translations from 'translations'
import LangContext from "../../contexts/lang-context";
import backend from "../../backend";

export default () => {
    let currentLang = useContext(LangContext).lang

    let [login, setLogin] = useState('')
    let [password, setPassword] = useState('')
    let [confirmPassword, setConfirmPassword] = useState('')
    let [firstName, setFirstName] = useState('')
    let [middleName, setMiddleName] = useState('')
    let [lastName, setLastName] = useState('')
    let [email, setEmail] = useState('')


    const onRegister = () => {
        backend.Account.Register({
            Login: login,
            Password: password,
            FirstName: firstName,
            LastName: lastName,
            MiddleName: middleName,
            ConfirmPassword: confirmPassword,
            Email: email
        }).then((token) => {
            localStorage.setItem('token', token)
            window.location.href = '/login'
        })
    }

    return (<>
        <div className="main-container">
            <div className="log-container">
                <label className="log-label">{translations[currentLang].firstname}</label>
                <input className="reg-log-input" type="text" required
                       value={firstName} onChange={e => setFirstName(e.target.value)}
                />

                <label className="log-label">{translations[currentLang].surname}</label>
                <input className="reg-log-input" type="text" required
                       value={lastName} onChange={e => setLastName(e.target.value)}
                />

                <label className="log-label">{translations[currentLang].lastname}</label>
                <input className="reg-log-input" type="text" required
                       value={middleName} onChange={e => setMiddleName(e.target.value)}
                />

                <label className="log-label">{translations[currentLang].email}</label>
                <input className="reg-log-input" type="text" required
                       value={email} onChange={e => setEmail(e.target.value)}
                />

                <label className="log-label">{translations[currentLang].login}</label>
                <input className="reg-log-input" type="text" required
                       value={login} onChange={e => setLogin(e.target.value)}
                />

                <label className="log-label">{translations[currentLang].password}</label>
                <input className="reg-log-input" type="password" required
                       value={password} onChange={e => setPassword(e.target.value)}
                />

                <label className="log-label">{translations[currentLang].confirmpassword}</label>
                <input className="reg-log-input" type="password" required
                       value={confirmPassword} onChange={e => setConfirmPassword(e.target.value)}
                />

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
                <button type="submit" className="registerbtn"
                        onClick={() => onRegister()}>
                    <div className="log-in-btn">{translations[currentLang].endreg}</div>
                </button>
            </div>
        </div>
    </>)
}
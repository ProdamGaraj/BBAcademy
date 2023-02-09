import RegisterContext from "contexts/register-context";

import './register.css'
import {useContext} from "react";

import translations from 'translations'
import LangContext from "../../contexts/lang-context";

export default () => {

    let currentLang = useContext(LangContext).lang

    let context = useContext(RegisterContext)

    let [login, setLogin] = useState('')
    let [password, setPassword] = useState('')
    let [confirmPassword, setConfirmPassword] = useState('')
    let [middlename, setMiddlename] = useState('')
    let [name, setName] = useState('')
    let [surname, setSurname] = useState('')
    let [email, setEmail] = useState('')
   
    

    const onRegister = () => {
        context.Register({
            Name: name,
            Surname: surname,
            Middlename: middlename,
            Email: email,
            Login: login,
            Password: password,
            ConfirmPassword: confirmPassword,
        })
    }
    
    return (<>
        <div className="main-container">
            <form className="log-block-form">
                <div className="log-container">
                    <label className="log-label">{translations[currentLang].firstname}</label>
                    <input className="reg-log-input" type="text" required
                    value={name} onChange={e => setLogin(e.target.value)}
                    />

                    <label className="log-label">{translations[currentLang].surname}</label>
                    <input className="reg-log-input" type="text" required
                    value={} onChange={e => setLogin(e.target.value)}
                    />

                    <label className="log-label">{translations[currentLang].lastname}</label>
                    <input className="reg-log-input" type="text" required
                    value={login} onChange={e => setLogin(e.target.value)}
                    />

                    <label className="log-label">{translations[currentLang].email}</label>
                    <input className="reg-log-input" type="text" required
                    value={login} onChange={e => setLogin(e.target.value)}
                    />

                    <label className="log-label">{translations[currentLang].login}</label>
                    <input className="reg-log-input" type="text" required
                    value={login} onChange={e => setLogin(e.target.value)}
                    />

                    <label className="log-label">{translations[currentLang].password}</label>
                    <input className="reg-log-input" type="password" required
                    value={login} onChange={e => setLogin(e.target.value)}
                    />

                    <label className="log-label">{translations[currentLang].confirmpassword}</label>
                    <input className="reg-log-input" type="password" required
                    value={login} onChange={e => setLogin(e.target.value)}
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
                    <button type="submit" className="registerbtn">
                        <div className="log-in-btn">{translations[currentLang].endreg}</div>
                    </button>
                </div>
            </form>
        </div>
    </>)
}
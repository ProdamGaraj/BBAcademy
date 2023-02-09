import LoginContext from "contexts/login-context";
import { useContext } from "react";

import translations from 'translations'
import LangContext from "../../contexts/lang-context";

import './login.css'

export default (props) => {

    let currentLang = useContext(LangContext).lang
    let context = useContext(LoginContext)

    let [login, setLogin] = useState('')
    let [password, setPassword] = useState('')

    const onLogin = () => {
        context.Login({
            Login: login,
            Password: password
        })
    }

    return (<>
        <div className="main-container">
            <form className="log-block-form">
                <div className="log-container">
                    <label className="log-label">{translations[currentLang].login}</label>
                    <input placeholder="" value={login} onChange={e => setLogin(e.target.value)}
                        className="form-control textbox-dg font-weight-bold text-center reg-log-input"
                        type="text" />

                    <label className="log-label">{translations[currentLang].password}</label>
                    <input placeholder="" value={password} onChange={e => setPassword(e.target.value)}
                        className="form-control textbox-dg font-weight-bold text-center reg-log-input"
                        type="password" />

                    <div className="d-grid gap-2 d-md-block float-right">
                        <NavLink to={'/data/question'}>
                            <button type="submit" className="registerbtn">
                                <div className="log-in-btn">{translations[currentLang].enter}</div>

                                <div className="log-in-btn" onClick={() => onLogin()}>{translations[currentLang].enter}</div>
                            </button>
                        </NavLink>

                    </div>
                </div>
            </form>
        </div>
    </>)
}
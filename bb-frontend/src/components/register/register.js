import {useContext, useState} from "react";
import translations from 'translations'
import LangContext from "../../contexts/lang-context";
import backend from "../../backend";
import LoaderModalContext from "../../contexts/loader-modal-context";
import ErrorModalContext from "../../contexts/error-modal-context";
import styles from "./register.module.css"
export default () => {
    let currentLang = useContext(LangContext).lang
    let loaderModal = useContext(LoaderModalContext)
    let errorModal = useContext(ErrorModalContext)

    let [login, setLogin] = useState('')
    let [password, setPassword] = useState('')
    let [confirmPassword, setConfirmPassword] = useState('')
    let [firstName, setFirstName] = useState('')
    let [middleName, setMiddleName] = useState('')
    let [lastName, setLastName] = useState('')
    let [email, setEmail] = useState('')


    const onRegister = () => {
        loaderModal.showModal()
        backend.Account.Register({
            Login: login,
            Password: password,
            FirstName: firstName,
            LastName: lastName,
            MiddleName: middleName,
            ConfirmPassword: confirmPassword,
            Email: email
        })
            .then((token) => {
                localStorage.setItem('token', token)
                window.location.href = '/login'
            })
            .catch(e => errorModal.showModal(e.message))
            .finally(() => loaderModal.close())
    }

    return (<>
            <div className={styles.logContainer}>
                <label className={styles.logLabel}>{translations[currentLang].firstname}</label>
                <input className={styles.regLogInput} type="text" required
                       value={firstName} onChange={e => setFirstName(e.target.value)}
                />

                <label className={styles.logLabel}>{translations[currentLang].surname}</label>
                <input className={styles.regLogInput} type="text" required
                       value={lastName} onChange={e => setLastName(e.target.value)}
                />

                <label className={styles.logLabel}>{translations[currentLang].lastname}</label>
                <input className={styles.regLogInput} type="text" required
                       value={middleName} onChange={e => setMiddleName(e.target.value)}
                />

                <label className={styles.logLabel}>{translations[currentLang].email}</label>
                <input className={styles.regLogInput} type="text" required
                       value={email} onChange={e => setEmail(e.target.value)}
                />

                <label className={styles.logLabel}>{translations[currentLang].login}</label>
                <input className={styles.regLogInput} type="text" required
                       value={login} onChange={e => setLogin(e.target.value)}
                />

                <label className={styles.logLabel}>{translations[currentLang].password}</label>
                <input className={styles.regLogInput} type="password" required
                       value={password} onChange={e => setPassword(e.target.value)}
                />

                <label className={styles.logLabel}>{translations[currentLang].confirmpassword}</label>
                <input className={styles.regLogInput} type="password" required
                       value={confirmPassword} onChange={e => setConfirmPassword(e.target.value)}
                />
                                                           
                <label className={styles.logLabelUnnessesary}>    
                    {translations[currentLang].condition}
                    <div className={styles.logLabelUnnessesaryLabel}>{translations[currentLang].unnesessaryfield}</div>
                </label>
                <select className={styles.banksSelect}>
                    <option value="bank0">BilimBank</option>
                    <option value="bank1">BilimBank</option>
                    <option value="bank2">BilimBank</option>
                </select>
                <button type="submit" className={styles.registerBtn}
                        onClick={() => onRegister()}>
                    {translations[currentLang].endreg}
                </button>
            </div>
    </>)
}
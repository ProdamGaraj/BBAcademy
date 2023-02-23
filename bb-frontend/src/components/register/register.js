import {useContext, useState} from "react";
import translations from 'translations'
import LangContext from "../../contexts/lang-context";
import backend from "../../backend";
import LoaderModalContext from "../../contexts/loader-modal-context";
import ErrorModalContext from "../../contexts/error-modal-context";
import styles from "./register.module.css"
import QuestionCheckboxGroup from "../courses/question-checkbox-group/question-checkbox-group";

export default () => {
    let lang = useContext(LangContext).lang
    let loaderModal = useContext(LoaderModalContext)
    let errorModal = useContext(ErrorModalContext)

    let [login, setLogin] = useState('')
    let [password, setPassword] = useState('')
    let [confirmPassword, setConfirmPassword] = useState('')
    let [firstName, setFirstName] = useState('')
    let [middleName, setMiddleName] = useState('')
    let [surname, setSurname] = useState('')
    let [email, setEmail] = useState('')
    let [license, setLicense] = useState(false)


    const onRegister = () => {
        if (login === '' || password === '' || firstName === '' || surname === '' || confirmPassword === '' || email === '') {
            errorModal.showModal('Заполните все обязательные поля.');
            return;
        }
        if (login.length < 6) {
            errorModal.showModal('Логин должен содержать не менее 6 символов.');
            return;
        }
        if (!/^[a-zA-Z0-9-_]+$/.test(login)) {
            errorModal.showModal('Логин может состоять только из латинских букв, цифр и знаков "-" "_"');
            return;
        }
        if (password.length < 6) {
            errorModal.showModal('Слишком короткий пароль');
            return;
        }
        if (/^[!@#№%:^&?*()+="'};.~{,_<>]+$/.test(firstName + surname)) {
            errorModal.showModal('Поля имя, фамилия и отчество, не могут содержать спецсимволы кроме "`" "-"');
            return;
        }
        if (!/^((([0-9A-Za-z]{1}[-0-9A-z\.]{1,}[0-9A-Za-z]{1})|([0-9А-Яа-я]{1}[-0-9А-я\.]{1,}[0-9А-Яа-я]{1}))@([-0-9A-Za-z]{1,}\.){1,2}[-A-Za-z]{2,})$/.test(email)) {
            errorModal.showModal('Почта введена неправильно.');
            return;
        }
        if (password !== confirmPassword) {
            errorModal.showModal('Пароли не совпадают.');
            return;
        }
        if (license!==true){
            errorModal.showModal('Прочтите и подтвердите согласие с условиями соглашения.');
            return;
        }
        

        loaderModal.showModal()
        backend.Account
            .Register({
                Login: login,
                Password: password,
                FirstName: firstName,
                LastName: surname,
                MiddleName: middleName,
                ConfirmPassword: confirmPassword,
                Email: email
            })
            .then((token) => {
                localStorage.setItem('token', token)
                window.location.href = '/login'
            })
            .catch(e => errorModal.showModal(e.response.data.error))
            .finally(() => loaderModal.close())
    }

    return (<>
        <div className={styles.logContainer}>
            <label className={styles.logLabel}>{translations[lang].firstname}</label>
            <input className={styles.regLogInput} type="text" required
                   value={firstName} onChange={e => setFirstName(e.target.value)}
            />

            <label className={styles.logLabel}>{translations[lang].surname}</label>
            <input className={styles.regLogInput} type="text" required
                   value={surname} onChange={e => setSurname(e.target.value)}
            />

            <label className={styles.logLabel}>{translations[lang].middleName}</label>
            <input className={styles.regLogInput} type="text" required
                   value={middleName} onChange={e => setMiddleName(e.target.value)}
            />

            <label className={styles.logLabel}>{translations[lang].email}</label>
            <input className={styles.regLogInput} type="text" required
                   value={email} onChange={e => setEmail(e.target.value)}
            />

            <label className={styles.logLabel}>{translations[lang].login}</label>
            <input className={styles.regLogInput} type="text" required
                   value={login} onChange={e => setLogin(e.target.value)}
            />

            <label className={styles.logLabel}>{translations[lang].password}</label>
            <input className={styles.regLogInput} type="password" required
                   value={password} onChange={e => setPassword(e.target.value)}
            />

            <label className={styles.logLabel}>{translations[lang].confirmpassword}</label>
            <input className={styles.regLogInput} type="password" required
                   value={confirmPassword} onChange={e => setConfirmPassword(e.target.value)}
            />

            <label className={styles.logLabelUnnecessary}>
                {translations[lang].condition}
                <div className={styles.logLabelUnnecessaryLabel}>{translations[lang].unnesessaryfield}</div>
            </label>
            <select className={styles.banksSelect}>
                <option>Не выбрано</option>
                <option value="bank0">BilimBank</option>
                <option value="bank1">BilimBank</option>
                <option value="bank2">BilimBank</option>
            </select>
            <span className={styles.licenseCheck}>
                <input
                    className={styles.checkInput}
                    type="checkbox" 
                    required
                    checked={license}
                    onChange={e=>setLicense(e.target.checked)}
                />
                <label className={styles.logLabel}><a href="license\license.docx" download="" target="_blank">{translations[lang].license}</a>{translations[lang].iConfirmLicense}</label>
            </span>
            <button type="submit" className={styles.registerBtn}
                    onClick={() => onRegister()}>
                {translations[lang].endreg}
            </button>
        </div>
    </>)
}
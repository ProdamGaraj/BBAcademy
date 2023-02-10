import './App.css';
import Template from "./components/template/template";
import Landing from "./components/landing/landing";
import LangContext from "./contexts/lang-context";
import UserContext from "./contexts/user-context";
import {BrowserRouter, Route, Routes} from "react-router-dom";
import Login from "./components/login/login";
import {useRef, useState} from "react";
import Register from "./components/register/register";
import CourseView from "./components/course-view/course-view";
import Cart from "./components/cart/cart";
import CoursesDashboard from "./components/courses-dashboard/courses-dashboard";
import MyCertificates from 'components/my-certificates/my-certificates';
import Exam from 'components/exam/exam'
import LoaderModalContext from "./contexts/loader-modal-context";
import ErrorModalContext from "./contexts/error-modal-context";
import {LoaderModal} from "./components/loader-modal/loader-modal";
import {ErrorModal} from "./components/error-modal/error-modal";
import DataDashboard from "./components/data/data-dashboard/data-dashboard";
import Payment from "./components/payment/payment";

let getLsLang = () => {
    let lsLang = localStorage.getItem('lang');
    if (!lsLang) {
        lsLang = 'ru'
        localStorage.setItem('lang', 'ru')
    }
    return lsLang
};

let setLsLang = (lang) => {
    localStorage.setItem('lang', lang);
};

let getLsUser = () => {
    let lsUser = JSON.parse(localStorage.getItem('user'));
    if (!lsUser) {
        lsUser = {
            LastName: 'Superskiy',
            FirstName: 'Perec',
            MiddleName: 'Percovich',
            JobTitle: 'Common grower',
            Organisation: 'Gryadka',
            Rating: 505,
            RecommendedBy: 'Snoop Dog',
        }
        localStorage.setItem('user', JSON.stringify(lsUser))
    }
    return lsUser
};

let setLsUser = (user) => {
    localStorage.setItem('user', JSON.stringify(user));
};

function App() {

    let [lang, setLang] = useState(() => getLsLang())
    let [user, setUser] = useState(() => getLsUser())

    let onSetLang = (lang) => {
        setLang(lang)
        setLsLang(lang)
    }
    let onSetUser = (user) => {
        setUser(user)
        setLsUser(user)
    }

    const [loaderVisible, setLoaderVisible] = useState(false)
    const [errorVisible, setErrorVisible] = useState(false)
    const [errorText, setErrorText] = useState('')

    let openLoaderModal = () => {
        setLoaderVisible(true)
    }
    let closeLoaderModal = () => {
        setLoaderVisible(false)
    }
    let openErrorModal = (message) => {
        setErrorVisible(true)
        setErrorText(prev => message)
    }
    let closeErrorModal = () => {
        setErrorVisible(false)
    }

    return (<>
        <LangContext.Provider value={{lang: lang, setLang: onSetLang}}>
            <UserContext.Provider value={{user: user, setUser: onSetUser}}>
                <LoaderModalContext.Provider
                    value={{isOpen: loaderVisible, showModal: openLoaderModal, close: closeLoaderModal}}>
                    <ErrorModalContext.Provider value={{
                        isOpen: errorVisible,
                        message: errorText,
                        showModal: openErrorModal,
                        close: closeErrorModal
                    }}>
                        <Template>
                            <LoaderModal/>
                            <ErrorModal/>
                            <BrowserRouter>
                                <Routes>
                                    <Route path='/' element={<Landing/>}/>
                                    <Route path='/login' element={<Login/>}/>
                                    <Route path='/register' element={<Register/>}/>
                                    <Route path='/courses' element={<CoursesDashboard/>}/>
                                    <Route path='/course-view' element={<CourseView/>}/>
                                    <Route path='/data/*' element={<DataDashboard/>}/>
                                    <Route path='/cart/*' element={<Cart/>}/>
                                    <Route path='/my-certificates' element={<MyCertificates/>}/>
                                    <Route path='/exam' element={<Exam/>}/>
                                    <Route path='/payment' element={<Payment/>}/>
                                </Routes>
                            </BrowserRouter>
                        </Template>
                    </ErrorModalContext.Provider>
                </LoaderModalContext.Provider>
            </UserContext.Provider>
        </LangContext.Provider>
    </>);
}

export default App;

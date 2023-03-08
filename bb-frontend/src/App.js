import './App.css';
import Template from "./components/template/template";
import Landing from "./components/landing/landing";
import LangContext from "./contexts/lang-context";
import {BrowserRouter, Route, Routes} from "react-router-dom";
import Login from "./components/login/login";
import {useState} from "react";
import Register from "./components/register/register";
import CoursesDashboard from "./components/courses-dashboard/courses-dashboard";
import MyCertificates from 'components/my-certificates/my-certificates';
import LoaderModalContext from "./contexts/loader-modal-context";
import ErrorModalContext from "./contexts/error-modal-context";
import SuccessModalContext from "./contexts/success-modal-context";
import {LoaderModal} from "./components/loader-modal/loader-modal";
import {ErrorModal} from "./components/error-modal/error-modal";
import DataDashboard from "./components/data/data-dashboard/data-dashboard";
import LearningDashboard from "./components/courses/learning-dashboard/learning-dashboard";
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


function App() {

    let [lang, setLang] = useState(() => getLsLang())

    let onSetLang = (lang) => {
        setLang(lang)
        setLsLang(lang)
    }

    const [loaderVisible, setLoaderVisible] = useState(false)
    const [errorVisible, setErrorVisible] = useState(false)
    const [errorText, setErrorText] = useState('')
    const [successVisible, setSuccessVisible] = useState(false)
    const [successText, setSuccessText] = useState('')

    let openLoaderModal = () => {
        setLoaderVisible(true)
    }
    let closeLoaderModal = () => {
        setLoaderVisible(false)
    }
    let openErrorModal = (message) => {
        setErrorVisible(true)
        setErrorText(_ => message)
    }
    let closeErrorModal = () => {
        setErrorVisible(false)
    }
    let openSuccessModal = (message) => {
        setSuccessVisible(true)
        setSuccessText(_ => message)
    }
    let closeSuccessModal = () => {
        setSuccessVisible(false)
    }

    return (<>
        <LangContext.Provider value={{lang: lang, setLang: onSetLang}}>
            <LoaderModalContext.Provider
                value={{isOpen: loaderVisible, showModal: openLoaderModal, close: closeLoaderModal}}>
                <ErrorModalContext.Provider value={{
                    isOpen: errorVisible,
                    message: errorText,
                    showModal: openErrorModal,
                    close: closeErrorModal
                }}>
                    <SuccessModalContext.Provider value={{
                        isOpen: successVisible,
                        message: successText,
                        showModal: openSuccessModal,
                        close: closeSuccessModal
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
                                    <Route path='/learning' element={<LearningDashboard/>}/>
                                    <Route path='/data/*' element={<DataDashboard/>}/>
                                    <Route path='/my-certificates' element={<MyCertificates/>}/>
                                    <Route path='/payment' element={<Payment/>}/>
                                </Routes>
                            </BrowserRouter>
                        </Template>
                    </SuccessModalContext.Provider>
                </ErrorModalContext.Provider>
            </LoaderModalContext.Provider>
        </LangContext.Provider>
    </>);
}

export default App;

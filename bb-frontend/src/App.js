import './App.css';
import Template from "./components/template/template";
import Landing from "./components/landing/landing";
import LangContext from "./contexts/lang-context";
import {BrowserRouter, Route, Routes} from "react-router-dom";
import Login from "./components/login/login";
import {useState} from "react";
import Register from "./components/register/register";

let getLang = () => {
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

    let [lang, setLang] = useState(() => getLang())
    
    let onSetLang = (lang) => {
        setLang(lang)
        setLsLang(lang)
    }

    return (<>
        <LangContext.Provider value={{lang: lang, setLang: onSetLang}}>
            <Template>
                <BrowserRouter>
                    <Routes>
                        <Route path='/' element={<Landing/>}/>
                        <Route path='/login' element={<Login/>}/>
                        <Route path='/register' element={<Register/>}/>
                        {/*<Route path='/roster/:number' component={Player}/>*/}
                    </Routes>
                </BrowserRouter>
            </Template>
        </LangContext.Provider>
    </>);
}

export default App;

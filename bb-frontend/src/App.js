import './App.css';
import Template from "./components/template/template";
import Landing from "./components/landing/landing";
import LangContext from "./contexts/lang-context";
import UserContext from "./contexts/user-context";
import {BrowserRouter, Route, Routes} from "react-router-dom";
import Login from "./components/login/login";
import {useState} from "react";
import Register from "./components/register/register";
import Courses from "./components/courses/courses";
import CourseView from "./components/course-view/course-view";

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
            FirstName: 'Perec',
            LastName: 'Superskiy',
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

    return (<>
        <LangContext.Provider value={{lang: lang, setLang: onSetLang}}>
            <UserContext.Provider value={{user: user, setUser: onSetUser}}>
                <Template>
                    <BrowserRouter>
                        <Routes>
                            <Route path='/' element={<Landing/>}/>
                            <Route path='/login' element={<Login/>}/>
                            <Route path='/register' element={<Register/>}/>
                            <Route path='/courses' element={<Courses/>}/>
                            <Route path='/course-view' element={<CourseView/>}/>
                            {/*<Route path='/roster/:number' component={Player}/>*/}
                        </Routes>
                    </BrowserRouter>
                </Template>
            </UserContext.Provider>
        </LangContext.Provider>
    </>);
}

export default App;

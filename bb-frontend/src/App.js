import './App.css';
import Template from "./components/template/template";
import Landing from "./components/landing/landing";
import LangContext from "./contexts/lang-context";
import UserContext from "./contexts/user-context";
import {BrowserRouter, Route, Routes} from "react-router-dom";
import Login from "./components/login/login";
import {useState} from "react";
import Register from "./components/register/register";
import CourseView from "./components/course-view/course-view";
import Data from "./components/data/data";
// import Cart from "./components/cart/cart";
import CoursesDashboard from "./components/courses-dashboard/courses-dashboard";

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

    return (<>
        <LangContext.Provider value={{lang: lang, setLang: onSetLang}}>
            <UserContext.Provider value={{user: user, setUser: onSetUser}}>
                <Template>
                    <BrowserRouter>
                        <Routes>
                            <Route path='/' element={<Landing/>}/>
                            <Route path='/login' element={<Login/>}/>
                            <Route path='/register' element={<Register/>}/>
                            <Route path='/courses' element={<CoursesDashboard/>}/>
                            <Route path='/course-view' element={<CourseView/>}/>
                            <Route path='/data/*' element={<Data/>}/>
                            {/*<Route path='/cart' element={<Cart/>}/>*/}
                        </Routes>
                    </BrowserRouter>
                </Template>
            </UserContext.Provider>
        </LangContext.Provider>
    </>);
}

export default App;

import './course-view-container.css'
import LangContext from "../../contexts/lang-context";
import translations from 'translations'
import {useContext} from "react";


export default ({children}) => {
    let currentLang = useContext(LangContext).lang
    
    return (<>
        <div className="main-container flex-to-center">
            <div className="main-container-cont">
                <div className="main-container-list">
                    <div className="main-container-list-p-flex">
                        <p className="main-container-list-p">{(translations[currentLang].infestionForBeginners)}</p>
                    </div>

                    <ul className="main-ul">
                        <li>{(translations[currentLang].introduction)}</li>
                        <li>{(translations[currentLang].links)}</li>
                        <li>{(translations[currentLang].block)} 1</li>
                        <li className="not-main-li">{(translations[currentLang].lesson )} 1</li>
                        <li>{(translations[currentLang].lesson)} 2</li>
                        <li>{(translations[currentLang].lesson)} 3</li>
                        <li>{(translations[currentLang].block).test}</li>
                        <li>{(translations[currentLang].ending)}</li>
                    </ul>
                </div>
                <div className="Demarcation-line"></div>
                <div className="container">
                    <main role="main" className="pb-3">
                        {children}
                    </main>
                </div>
            </div>
        </div>
    </>)
}
import {NavLink} from "react-router-dom";
import {useContext, useState} from "react";
import DataEditContext from "contexts/data-edit-context";
import LangContext from "contexts/lang-context";
import translations from 'translations'

export default () => {

    let context = useContext(DataEditContext)
    let currentLang = useContext(LangContext).lang


    let [title, setTitle] = useState('')
    let [weight, setWeight] = useState(0)
    let [isCorrect, setIsCorrect] = useState(false)
    let [mediaPath, setMediaPath] = useState('')

    const onSave = () => {
        context.addAnswer({
            Title: title, 
            Weight: weight, 
            IsCorrect: isCorrect, 
            MediaPath: mediaPath
        })
    }

    return (<>
        <div className="logContainer">
            <div className="heading">{(translations[currentLang].editingAnswerOption)}</div>
            <label className="logLabel">Title</label>
            <input placeholder="" value={title} onChange={e => setTitle(e.target.value)}
                   className="form-control textbox-dg font-weight-bold text-center regLogInput" type="text"/>
            <label className="logLabel">Weight</label>
            <input placeholder="" value={weight} onChange={e => setWeight(~~e.target.value)}
                   className="form-control textbox-dg font-weight-bold text-center regLogInput" type="text" required/>
            <label className="logLabel">Media Path</label>
            <input placeholder="" value={mediaPath} onChange={e => setMediaPath(e.target.value)}
                   className="form-control textbox-dg font-weight-bold text-center regLogInput" type="text" required/><label
            className="logLabel">Is Correct</label>
            <input placeholder="" checked={isCorrect} onChange={e => setIsCorrect(e.target.checked)}
                   className="form-control textbox-dg font-weight-bold text-center regLogInput check" type="checkbox"
                   required/>
            <NavLink to={'/data/question'}>
                <button className="add-course-btn"  onClick={() => onSave()}>
                    <div className="registerBtn">{translations[currentLang].saveAnswerandreturntoQuestion}</div>
                </button>
            </NavLink>
        </div>
    </>)
}
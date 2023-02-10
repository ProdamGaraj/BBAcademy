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
        <div className="log-container">
            <div className="heading">{(translations[currentLang].editingAnswerOption)}</div>
            <label className="log-label">Title</label>
            <input placeholder="" value={title} onChange={e => setTitle(e.target.value)}
                   className="form-control textbox-dg font-weight-bold text-center reg-log-input" type="text"/>
            <label className="log-label">Weight</label>
            <input placeholder="" value={weight} onChange={e => setWeight(~~e.target.value)}
                   className="form-control textbox-dg font-weight-bold text-center reg-log-input" type="text" required/>
            <label className="log-label">Media Path</label>
            <input placeholder="" value={mediaPath} onChange={e => setMediaPath(e.target.value)}
                   className="form-control textbox-dg font-weight-bold text-center reg-log-input" type="text" required/><label
            className="log-label">Is Correct</label>
            <input placeholder="" checked={isCorrect} onChange={e => setIsCorrect(e.target.checked)}
                   className="form-control textbox-dg font-weight-bold text-center reg-log-input check" type="checkbox"
                   required/>
            <NavLink to={'/data/question'}>
                <button className="add-course-btn"  onClick={() => onSave()}>
                    <div className="log-in-btn">{translations[currentLang].saveAnswerandreturntoQuestion}</div>
                </button>
            </NavLink>
        </div>
    </>)
}
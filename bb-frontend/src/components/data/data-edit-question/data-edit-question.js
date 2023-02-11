import {NavLink} from "react-router-dom";
import {useContext, useState} from "react";
import DataEditContext from "contexts/data-edit-context";

export default (props) => {

    let context = useContext(DataEditContext)

    let [name, setName] = useState('')
    let [content, setContent] = useState('')
    let [type, setType] = useState('')
    let [mediaPath, setMediaPath] = useState('')

    const onSave = () => {
        context.addQuestion({
            MediaPath: mediaPath, Content: content, QuestionType: type
        })
    };

    return (<>
        <div className="logContainer">
            <div className="heading">Editing Question</div>
            <label className="logLabel">Name</label>
            <input placeholder="" value={name} onChange={e => setName(e.target.value)}
                   className="form-control textbox-dg font-weight-bold text-center regLogInput" type="text"/>
            <label className="logLabel">Content</label>
            <input placeholder="" value={content} onChange={e => setContent(e.target.value)}
                   className="form-control textbox-dg font-weight-bold text-center regLogInput" type="text"/>
            <label className="logLabel">Question Type</label>
            <input placeholder="" value={type} onChange={e => setType(e.target.value)}
                   className="form-control textbox-dg font-weight-bold text-center regLogInput" type="text"/>
            <label className="logLabel">Media Path</label>
            <input placeholder="" value={mediaPath} onChange={e => setMediaPath(e.target.value)}
                   className="form-control textbox-dg font-weight-bold text-center regLogInput" type="text"/>
            <div className="gapper">
                <NavLink to={'/data/answer'}>
                    <button type="submit" className="add-course-btn">
                        <div className="registerBtn" onClick={() => onSave()}>Save and Add answer</div>
                    </button>
                </NavLink>
                <NavLink to={'/data/exam'}>
                    <button className="add-course-btn" onClick={() => onSave()}>
                        <div className="registerBtn">Save and Return to Exam</div>
                    </button>
                </NavLink>
            </div>
        </div>
    </>)
}
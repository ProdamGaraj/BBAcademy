import {NavLink} from "react-router-dom";
import {useContext, useState} from "react";
import DataEditContext from "contexts/data-edit-context";

export default () => {


    let context = useContext(DataEditContext)

    let [description, setDescription] = useState('')
    let [type, setType] = useState('')
    let [content, setContent] = useState('')
    let [mediaPath, setMediaPath] = useState('')

    const onSave = () => {
        context.addLesson({
            Description: description,
            LessonContentType: type,
            Content: content,
            MediaContentPath: mediaPath
        })
    };

    return (<>
        <div className="logContainer">
            <div className="heading">Editing Lesson</div>
            <label className="logLabel">Description</label>
            <input placeholder="" value={description} onChange={e => setDescription(e.target.value)}
                   className="form-control textbox-dg font-weight-bold text-center regLogInput" type="text"
                   required/>
            <label className="logLabel">Content Type</label>
            <input placeholder="" value={type} onChange={e => setType(e.target.value)}
                   className="form-control textbox-dg font-weight-bold text-center regLogInput" type="text"
                   required/>
            <label className="logLabel">Content</label>
            <input placeholder="" value={content} onChange={e => setContent(e.target.value)}
                   className="form-control textbox-dg font-weight-bold text-center regLogInput" type="text"
                   required/>
            <label className="logLabel">Media content path (can be empty)</label>
            <input placeholder="" value={mediaPath} onChange={e => setMediaPath(e.target.value)}
                   className="form-control textbox-dg font-weight-bold text-center regLogInput" type="text"
                   required/>

            <NavLink to={'/data/course'}>
                <button type="submit" className="add-course-btn" onClick={() => onSave()}>
                    <div className="registerBtn">Save and Return to Course</div>
                </button>
            </NavLink>
        </div>
    </>)
}
import {NavLink} from "react-router-dom";
import {useContext, useState} from "react";
import DataEditContext from "../../contexts/data-edit-context";

export default () => {

    let context = useContext(DataEditContext)

    let [duration, setDuration] = useState(context.edit.Course.DurationHours ?? '')
    let [price, setPrice] = useState(context.edit.Course.Price ?? 0)
    let [description, setDescription] = useState(context.edit.Course.Description ?? '')
    let [mediaPath, setMediaPath] = useState(context.edit.Course.MediaPath ?? '')

    const onSaveToLesson = () => {
        context.addCourseInfo({
            DurationHours: duration, Price: price, Description: description, MediaPath: mediaPath
        })
    };
    const onSaveToExam = () => {
        context.addCourseInfo({
            DurationHours: duration, Price: price, Description: description, MediaPath: mediaPath
        })
    };

    return (<>
        <div className="log-container">
            <div className="heading">Editing Course</div>
            <label className="log-label">Duration</label>
            <input placeholder="" value={duration} onChange={e => setDuration(e.target.value)}
                   className="form-control textbox-dg font-weight-bold text-center reg-log-input" type="text"/>
            <label className="log-label">Price</label>
            <input placeholder="" value={price} onChange={e => setPrice(~~e.target.value)}
                   className="form-control textbox-dg font-weight-bold text-center reg-log-input" type="number"/>
            <label className="log-label">Description</label>
            <input placeholder="" value={description} onChange={e => setDescription(e.target.value)}
                   className="form-control textbox-dg font-weight-bold text-center reg-log-input" type="text"/>
            <label className="log-label">Media Path</label>
            <input placeholder="" value={mediaPath} onChange={e => setMediaPath(e.target.value)}
                   className="form-control textbox-dg font-weight-bold text-center reg-log-input" type="text"/>

            <div className="gapper">
                <NavLink to={'/data/lesson'}>
                    <button type="submit" className="add-course-btn">
                        <div className="log-in-btn" onClick={() => onSaveToLesson()}>Save and Add Lesson</div>
                    </button>
                </NavLink>
                <NavLink to={'/data/exam'}>
                    <button type="submit" className="add-course-btn">
                        <div className="log-in-btn" onClick={() => onSaveToExam()}>Save and Add/Edit Exam</div>
                    </button>
                </NavLink>
            </div>
        </div>
    </>)
}
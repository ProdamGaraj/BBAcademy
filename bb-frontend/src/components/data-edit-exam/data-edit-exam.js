import {NavLink} from "react-router-dom";
import {useContext, useState} from "react";
import DataEditContext from "../../contexts/data-edit-context";

export default () => {

    let context = useContext(DataEditContext)

    let [description, setDescription] = useState(context.edit.Course.Exam.Description ?? '')
    let [passingGrade, setPassingGrade] = useState(context.edit.Course.Exam.MinimumPassingGrade ?? 0)

    const onSaveToQuestion = () => {
        context.addExam({
            Description: description,
            MinimumPassingGrade: passingGrade
        })
    };
    const onSaveToCourse = () => {
        context.addExam({
            Description: description,
            MinimumPassingGrade: passingGrade
        })
    };

    return (<>
        <div className="log-container">
            <div className="heading">Editing Exam</div>
            <label className="log-label">Description</label>
            <input placeholder="" value={description} onChange={e => setDescription(e.target.value)}
                   className="form-control textbox-dg font-weight-bold text-center reg-log-input" type="text"/>
            <label className="log-label">Passing grade</label>
            <input placeholder="" value={passingGrade} onChange={e => setPassingGrade(e.target.value)}
                   className="form-control textbox-dg font-weight-bold text-center reg-log-input" type="number"/>

            <div className="gapper">
                <NavLink to={'/data/question'}>
                    <button type="submit" className="add-course-btn">
                        <div className="log-in-btn" onClick={() => onSaveToQuestion()}>Save and Add Question</div>
                    </button>
                </NavLink>
                <NavLink to={'/data/course'}>
                    <button type="submit" className="add-course-btn">
                        <div className="log-in-btn" onClick={() => onSaveToCourse()}>Save and Return To Course</div>
                    </button>
                </NavLink>
            </div>
        </div>
    </>)
}
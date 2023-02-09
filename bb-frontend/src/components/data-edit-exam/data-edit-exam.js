import {NavLink} from "react-router-dom";
import {useContext, useState} from "react";
import DataEditContext from "../../contexts/data-edit-context";
import styles from "../data-edit-course/data-edit-course.module.css";


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

    const removeQuestion = index => {
        context.removeQuestion(index)
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

            {/*MediaPath: mediaPath,*/}
            {/*Content: content,*/}
            {/*QuestionType: type*/}
            
            <p className={styles.text}>Existing Questions</p>
            <div className={styles.lessons_wrapper + ' ' + styles.text}>
                {context.edit.Course.Exam.Questions.map((lesson, index) => (<div key={index} className={styles.lessons_item}>
                    <p><b>Question {index + 1}</b></p>
                    <p>
                        {lesson.Content}
                    </p>
                    <p>
                        {lesson.QuestionType}
                    </p>
                    <button className={styles.small_delete + ' btn-warn'} onClick={() => removeQuestion(index)}>Delete</button>
                </div>))}
            </div>

            {context.edit.Course.Exam.Questions.length ? '' : <div className={styles.no_lessons}>
                <span
                    className={styles.text}>
                    No Questions Were Added
                </span>
            </div>}
            
            <div className="gapper">
                <NavLink to={'/data/question'}>
                    <button className="add-course-btn" onClick={() => onSaveToQuestion()}>
                        <div className="log-in-btn">Save and Add Question</div>
                    </button>
                </NavLink>
                <NavLink to={'/data/course'}>
                    <button className="add-course-btn" onClick={() => onSaveToCourse()}>
                        <div className="log-in-btn">Save and Return To Course</div>
                    </button>
                </NavLink>
            </div>
        </div>
    </>)
}
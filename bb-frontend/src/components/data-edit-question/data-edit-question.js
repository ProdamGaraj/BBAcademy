﻿import {NavLink} from "react-router-dom";
import {useContext, useState} from "react";
import DataEditContext from "../../contexts/data-edit-context";
import styles from "../data-edit-course/data-edit-course.module.css";

export default (props) => {

    let context = useContext(DataEditContext)

    let [name, setName] = useState('')
    let [content, setContent] = useState('')
    let [type, setType] = useState('')
    let [mediaPath, setMediaPath] = useState('')

    const onSave = () => {
        context.setActiveQuestion(context.edit.Course.Exam.Questions.length)
        context.addQuestion({
            MediaPath: mediaPath, 
            Content: content, 
            QuestionType: type
        })
    };

    function removeAnswer(index) {
        context.removeAnswer(index)
    }

    return (<>
        <div className="log-container">
            <div className="heading">Editing Question</div>
            <label className="log-label">Name</label>
            <input placeholder="" value={name} onChange={e => setName(e.target.value)}
                   className="form-control textbox-dg font-weight-bold text-center reg-log-input" type="text"/>
            <label className="log-label">Content</label>
            <input placeholder="" value={content} onChange={e => setContent(e.target.value)}
                   className="form-control textbox-dg font-weight-bold text-center reg-log-input" type="text"/>
            <label className="log-label">Question Type</label>
            <input placeholder="" value={type} onChange={e => setType(e.target.value)}
                   className="form-control textbox-dg font-weight-bold text-center reg-log-input" type="text"/>
            <label className="log-label">Media Path</label>
            <input placeholder="" value={mediaPath} onChange={e => setMediaPath(e.target.value)}
                   className="form-control textbox-dg font-weight-bold text-center reg-log-input" type="text"/>

            {/*Title: title,*/}
            {/*Weight: weight,*/}
            {/*IsCorrect: isCorrect,*/}
            {/*MediaPath: mediaPath*/}
            
            <p className={styles.text}>Existing Answers</p>
            <div className={styles.lessons_wrapper + ' ' + styles.text}>
                {context.edit.Course.Exam.Questions[context.edit.CurrentQuestionId].Answers.map((answer, index) => (<div key={index} className={styles.lessons_item}>
                    <p><b>Answer {index + 1}</b></p>
                    <p>
                        {answer.Title}
                    </p>
                    <p>
                        {answer.Weight}
                    </p>
                    <button className={styles.small_delete + ' btn-warn'} onClick={() => removeAnswer(index)}>Delete</button>
                </div>))}
            </div>

            {context.edit.Course.Exam.Questions[context.edit.CurrentQuestionId].Answers.length ? '' : <div className={styles.no_lessons}>
                <span
                    className={styles.text}>
                    No Answers Were Added
                </span>
            </div>}
            
            <div className="gapper">
                <NavLink to={'/data/answer'}>
                    <button type="submit" className="add-course-btn">
                        <div className="log-in-btn" onClick={() => onSave()}>Save and Add answer</div>
                    </button>
                </NavLink>
                <NavLink to={'/data/exam'}>
                    <button className="add-course-btn" onClick={() => onSave()}>
                        <div className="log-in-btn">Save and Return to Exam</div>
                    </button>
                </NavLink>
            </div>
        </div>
    </>)
}
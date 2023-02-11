import {NavLink} from "react-router-dom";
import {useContext, useState} from "react";
import DataEditContext from "contexts/data-edit-context";
import styles from './data-edit-course.module.css'

export default () => {

    let context = useContext(DataEditContext)

    let [duration, setDuration] = useState(context.edit.Course.DurationHours ?? '')
    let [price, setPrice] = useState(context.edit.Course.Price ?? 0)
    let [description, setDescription] = useState(context.edit.Course.Description ?? '')
    let [mediaPath, setMediaPath] = useState(context.edit.Course.MediaPath ?? '')

    const onSave = () => {
        context.addCourseInfo({
            DurationHours: duration, Price: price, Description: description, MediaPath: mediaPath
        })
    };

    const onFinish = () => {
        context.finish({
            DurationHours: duration, Price: price, Description: description, MediaPath: mediaPath
        });
    };

    const removeLesson = index => {
        context.removeLesson(index)
    };

    return (<>
        <div className="logContainer">
            <div className="heading">Editing Course</div>
            <label className="logLabel">Duration</label>
            <input placeholder="" value={duration} onChange={e => setDuration(e.target.value)}
                   className="form-control textbox-dg font-weight-bold text-center regLogInput" type="text"/>
            <label className="logLabel">Price</label>
            <input placeholder="" value={price} onChange={e => setPrice(~~e.target.value)}
                   className="form-control textbox-dg font-weight-bold text-center regLogInput" type="number"/>
            <label className="logLabel">Description</label>
            <input placeholder="" value={description} onChange={e => setDescription(e.target.value)}
                   className="form-control textbox-dg font-weight-bold text-center regLogInput" type="text"/>
            <label className="logLabel">Media Path</label>
            <input placeholder="" value={mediaPath} onChange={e => setMediaPath(e.target.value)}
                   className="form-control textbox-dg font-weight-bold text-center regLogInput" type="text"/>

            {/*Description: description,*/}
            {/*LessonContentType: mediaPath,*/}
            {/*Content: content,*/}
            {/*MediaContentPath: mediaPath*/}

            <p className={styles.text}>Existing Lessons</p>
            <div className={styles.lessons_wrapper + ' ' + styles.text}>
                {context.edit.Course.Lessons.map((lesson, index) => (<div key={index} className={styles.lessons_item}>
                    <p><b>Lesson {index + 1}</b></p>
                    <p>
                        {lesson.Description}
                    </p>
                    <p>
                        {lesson.LessonContentType}
                    </p>
                    <button className={styles.small_delete + ' btn-warn'} onClick={() => removeLesson(index)}>Delete</button>
                </div>))}
            </div>

            {context.edit.Course.Lessons.length ? '' : <div className={styles.no_lessons}>
                <span
                    className={styles.text}>
                    No Lessons Were Added
                </span>
            </div>}

            <div className="gapper">
                <NavLink to={'/data/lesson'}>
                    <button className="add-course-btn" onClick={() => onSave()}>
                        <div className="registerBtn">Save and Add Lesson</div>
                    </button>
                </NavLink>
                <NavLink to={'/data/exam'}>
                    <button className="add-course-btn" onClick={() => onSave()}>
                        <div className="registerBtn">Save and Add/Edit Exam</div>
                    </button>
                </NavLink>
                <button className="add-course-btn btn-warn" onClick={() => onFinish()}>
                    <div className="registerBtn">Save and Finish Editing</div>
                </button>
            </div>
        </div>
    </>)
}
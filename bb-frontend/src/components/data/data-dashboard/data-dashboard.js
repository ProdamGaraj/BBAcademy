import {NavLink, Route, Routes} from "react-router-dom";
import DataEditCourse from "../data-edit-course/data-edit-course";

import './data-dashboard.css'
import baseurl from 'base-url'
import DataEditLesson from "../data-edit-lesson/data-edit-lesson";
import DataEditQuestion from "../data-edit-question/data-edit-question";
import DataEditAnswerOption from "../data-edit-answer-option/data-edit-answer-option";
import DataEditContext from "contexts/data-edit-context";
import {useContext, useState} from "react";
import DataEditExam from "../data-edit-exam/data-edit-exam";
import styles from "../data-edit-course/data-edit-course.module.css";


let getLsEdit = () => {
    let lsEdit = JSON.parse(localStorage.getItem('edit'));
    if (!lsEdit) {
        lsEdit = {
            Course: {
                MediaPath: '',
                DurationHours: 0,
                Description: '',
                Price: 0,
                Exam: {Description: '', MinimumPassingGrade: 0, Questions: []},
                Lessons: []
            }, CurrentQuestionId: undefined,
        }
        localStorage.setItem('edit', JSON.stringify(lsEdit))
    }
    return lsEdit
};

let setLsEdit = (edit) => {
    localStorage.setItem('edit', JSON.stringify(edit));
};

const pushToBackend = async (data) => {
    await fetch(baseurl + '/Data/SaveCourse', {
        body: JSON.stringify(data), headers: {
            'Content-Type': 'application/json;charset=utf-8',
            credentials: "include"
        }, method: 'POST'
    })
        .then(r => {
            if (r.status === 200) {
                alert('Saved successfully')
            }
            else {
                alert('Received status code: ' + r.status + '\n' + r.statusText)
            }
        }, e => {
            alert(e)
        })
}

let Page = () => {

    let context = useContext(DataEditContext)

    let allCourses = [{
        DurationHours: 10, Price: 69420, Description: "Example Course1", MediaPath: '/media1'
    }, {
        DurationHours: 10, Price: 69420, Description: "Example Course2", MediaPath: '/media2'
    }, {
        DurationHours: 10, Price: 69420, Description: "Example Course3", MediaPath: '/media3'
    }, {
        DurationHours: 10, Price: 69420, Description: "Example Course4", MediaPath: '/media4'
    },]

    const onBegin = () => {
        context.beginEdit()
    };

    const removeCourse = id => {
        alert('unsupported Course Deletion')
    };

    return (<>

        {/*DurationHours: duration, */}
        {/*Price: price, */}
        {/*Description: description, */}
        {/*MediaPath: mediaPath*/}
        <div style={{padding: '0 50px', width: '100%', boxSizing: 'border-box'}}>
            <p className={styles.text}>Existing Courses</p>
            <div className={styles.lessons_wrapper + ' ' + styles.text}>
                {allCourses.map((course, index) => (<div key={index} className={styles.lessons_item}>
                    <p><b>Course {index + 1}</b></p>
                    <p>
                        {course.Description}
                    </p>
                    <p>
                        {course.Price}
                    </p>
                    <p>
                        {course.DurationHours} h
                    </p>
                    <button className={styles.small_delete + ' btn-warn'}
                            onClick={() => removeCourse(course.Id)}>Delete
                    </button>
                </div>))}
            </div>
        </div>

        {allCourses.length ? '' :
            <div className={styles.no_lessons}>
                <span
                    className={styles.text}>
                    No Courses Were Added
                </span>
            </div>}
        <div className="gapper">
            <NavLink to={'/data/course'}>
                <button className="add-course-btn" onClick={() => onBegin()}>
                    <span>Add course</span>
                </button>
            </NavLink>
        </div>
    </>)
}

export default () => {

    let [edit, setEdit] = useState(getLsEdit());

    const onSetEdit = (new_edit) => {
        setLsEdit(new_edit)
        setEdit(prev => ({...prev, ...new_edit}))
    }

    const onAddLesson = (lesson) => {
        onSetEdit({...edit, Course: {...edit.Course, Lessons: [...edit.Course.Lessons, lesson]}})
    };

    const onRemoveLesson = (index) => {
        onSetEdit({...edit, Course: {...edit.Course, Lessons: edit.Course.Lessons.filter((l, i) => i !== index)}})
    };

    const onAddQuestion = (question) => {
        onSetEdit({
            ...edit, Course: {
                ...edit.Course, Exam: {
                    ...edit.Course.Exam, Questions: [...edit.Course.Exam.Questions, question]
                }
            }
        })
    };

    const onRemoveQuestion = (index) => {
        onSetEdit({
            ...edit, Course: {
                ...edit.Course, Exam: {
                    ...edit.Course.Exam, Questions: edit.Course.Exam.Questions.filter((q, i) => i !== index)
                }
            }
        })
    };

    const onAddCourseInfo = (course) => {
        onSetEdit({...edit, Course: {...edit.Course, ...course}})
    };

    const onAddAnswer = (answer) => {
        onSetEdit({
            ...edit, Course: {
                ...edit.Course, Exam: {
                    ...edit.Course.Exam,
                    Questions: edit.Course.Exam.Questions.map((q, i) => (i === edit.CurrentQuestionId ? {
                        ...q, Answers: [...q.Answers, answer]
                    } : q))
                }
            }
        })
    };

    const onRemoveAnswer = (index) => {
        onSetEdit({
            ...edit, Course: {
                ...edit.Course, Exam: {
                    ...edit.Course.Exam,
                    Questions: edit.Course.Exam.Questions.map((q, i) => (i === edit.CurrentQuestionId ? {
                        ...q, Answers: q.Answers.filter((a, ii) => ii !== index)
                    } : q))
                }
            }
        })
    };

    const onAddExam = (exam) => {
        onSetEdit({...edit, Course: {...edit.Course, Exam: {...edit.Course.Exam, ...exam}}})
    };

    const onBeginEdit = () => {
        onSetEdit({
            Course: {
                MediaPath: '',
                DurationHours: 0,
                Description: '',
                Price: 0,
                Exam: {Description: '', MinimumPassingGrade: 0, Questions: []},
                Lessons: []
            }, CurrentQuestionId: undefined,
        });
    };

    const onFinish = (latestCourse) => {

        let data = ({...edit.Course, ...latestCourse});

        if (data.Exam === undefined) {
            alert('Не заполнена информация об экзамене')
            return;
        }

        if (!data.Lessons.length) {
            alert('Не заполнена информация о занятиях')
            return;
        }

        pushToBackend(data)
            .then(() => console.log('finished pushing'))
    }

    function onSetActiveQuestion(question) {
        onSetEdit({
            ...edit, CurrentQuestionId: question
        })
    }

    return (<>
        <DataEditContext.Provider value={{
            edit: edit,
            addCourseInfo: onAddCourseInfo,
            addLesson: onAddLesson,
            removeLesson: onRemoveLesson,
            addQuestion: onAddQuestion,
            removeQuestion: onRemoveQuestion,
            addAnswer: onAddAnswer,
            removeAnswer: onRemoveAnswer,
            addExam: onAddExam,
            beginEdit: onBeginEdit,
            finish: onFinish,
            setActiveQuestion: onSetActiveQuestion
        }}>
            <Routes>
                <Route path='/' element={<Page/>}/>
                <Route path='/course' element={<DataEditCourse/>}/>
                <Route path='/lesson' element={<DataEditLesson/>}/>
                <Route path='/question' element={<DataEditQuestion/>}/>
                <Route path='/answer' element={<DataEditAnswerOption/>}/>
                <Route path='/exam' element={<DataEditExam/>}/>
            </Routes>
        </DataEditContext.Provider>
    </>)
}
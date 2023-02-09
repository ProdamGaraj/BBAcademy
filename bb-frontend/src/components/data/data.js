import {NavLink, Route, Routes} from "react-router-dom";
import DataEditCourse from "../data-edit-course/data-edit-course";

import './data.css'
import DataEditLesson from "../data-edit-lesson/data-edit-lesson";
import DataEditQuestion from "../data-edit-question/data-edit-question";
import DataEditAnswerOption from "../data-edit-answer-option/data-edit-answer-option";
import DataEditContext from "../../contexts/data-edit-context";
import {useContext, useState} from "react";
import DataEditExam from "../data-edit-exam/data-edit-exam";

let Page = () => {

    let context = useContext(DataEditContext)

    const onBegin = () => {
        context.beginEdit()
    };

    return (<>
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

    let [edit, setEdit] = useState({
        Course: {
            MediaPath: '',
            DurationHours: 0,
            Description: '',
            Price: 0,
            Exam: {Description: '', MinimumPassingGrade: 0, Questions: []},
            Lessons: []
        }
    });

    const pushToBackend = async (data) => {
        await fetch('/Data/SaveCourse', {body: JSON.stringify(data), method: 'POST'})
            .then(r => {
                if(r.status === 200) {
                    alert('Saved successfully')
                }
                else{
                    alert('Received status code: ' + r.status + '\n' + r.statusText)
                }
            }, e => {
                alert(e)
            })
    }

    const onAddLesson = (lesson) => {
        setEdit({...edit, Course: {...edit.Course, Lessons: [...edit.Course.Lessons, lesson]}})
    };

    const onAddQuestion = (question) => {
        setEdit({
            ...edit, Course: {
                ...edit.Course, Exam: {
                    ...edit.Course.Exam, Questions: [...edit.Course.Exam.Questions, question]
                }
            }
        })
    };

    const onAddCourseInfo = (course) => {
        setEdit({...edit, Course: {...edit.Course, ...course}})
    };

    const onAddAnswer = (answer) => {

    };

    const onAddExam = (exam) => {
        setEdit({...edit, Course: {...edit.Course, Exam: {...edit.Course.Exam, ...exam}}})
    };

    const onBeginEdit = () => {
        setEdit({
            Course: {
                MediaPath: '',
                DurationHours: 0,
                Description: '',
                Price: 0,
                Exam: {Description: '', MinimumPassingGrade: 0, Questions: []},
                Lessons: []
            }
        });
    };

    const onFinish = (latestCourse) => {
        pushToBackend(({...edit, Course: {...edit.Course, ...latestCourse}}))
            .then(() => console.log('finished pushing'))
    }
    
    return (<>
        <DataEditContext.Provider value={{
            edit: edit,
            addLesson: onAddLesson,
            addQuestion: onAddQuestion,
            addCourseInfo: onAddCourseInfo,
            addAnswer: onAddAnswer,
            addExam: onAddExam,
            beginEdit: onBeginEdit,
            finish: onFinish
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
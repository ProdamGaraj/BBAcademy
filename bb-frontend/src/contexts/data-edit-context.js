import React from 'react'

let context = React.createContext({
    edit: {
        Course: {
            MediaPath: '',
            DurationHours: 0,
            Description: '',
            Price: 0,
            Exam: {Description: '', MinimumPassingGrade: 0, Questions: []},
            Lessons: []
        }
    },

    beginEdit: () => {
    },
    CourseId:1,
    LessonId:1,
    QuestionId:1,
    AnswerOptionId:1,

    addCourseInfo: (course) => {
    }, addLesson: (lesson) => {
    }, addQuestion: (question) => {
    }, addAnswer: (answer) => {
    }, addExam: (exam) => {
    }
});

export default context;
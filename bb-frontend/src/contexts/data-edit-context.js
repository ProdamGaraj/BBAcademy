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

    addCourseInfo: (course) => {
    }, addLesson: (lesson) => {
    }, addQuestion: (question) => {
    }, addAnswer: (answer) => {
    }, addExam: (exam) => {
    }
});
export default context;
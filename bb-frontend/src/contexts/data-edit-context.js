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

    CurrentQuestionId: undefined,

    addCourseInfo: (course) => {
    }, addLesson: (lesson) => {
    }, removeLesson: (index) => {
    }, addQuestion: (question) => {
    }, removeQuestion: (index) => {
    }, addAnswer: (answer) => {
    }, removeAnswer: (index) => {
    }, addExam: (exam) => {
    }, setActiveQuestion: (question) => {
    }
});

export default context;
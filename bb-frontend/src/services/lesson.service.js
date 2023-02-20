import backend from "../backend";

export const getRandomLesson = async () => {
    let randCourse = {
        "id": 0,
        "mediaPath": "string",
        "title": "string",
        "description": "string",
        "lessonsCount": 0,
        "durationHours": 0,
        "state": 1
    };

    await backend.Course.GetForDashboard()
        .then(courses => {
            const idx = Math.floor(Math.random() * courses.length);
            randCourse = {...courses[idx]};
        });

    return backend.Course.GetForLearning(randCourse.id)
        .then(course => {
            const videoLessons = course.lessons.filter(lesson => lesson.lessonContentType === 2);
            const idx = Math.floor(Math.random() * videoLessons.length);
            return {...videoLessons[idx]};
        });
}

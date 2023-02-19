import translations from "translations";

import styles from './learning-layout.module.css'
import {useContext, useEffect} from "react";
import LangContext from "contexts/lang-context";

const LESSON_MODE = 1;
const EXAM_MODE = 2;
const CERT_MODE = 3;

export default (props) => {

    const {children, course, toLesson, toExam, toCert, activeLessonIndex, activeMode} = props;

    let currentLang = useContext(LangContext).lang;

    const handleChapterChange = (value) => {
        const selectedOptionId = value.target.selectedOptions[0].id;
        if (selectedOptionId === 'exam') {
            return toExam();
        }
        if (selectedOptionId === 'certificate') {
            return toCert();
        }

        const selectedLessonIndex = value.target.selectedIndex;
        return toLesson(selectedLessonIndex);
    }

    useEffect(() => {
        document.getElementById('lesson-select').selectedIndex = activeLessonIndex;
    }, [activeLessonIndex])

    const getLastOption = (certName) => {
        return certName === null ?
            (
                <option id={'exam'} key={'exam'}>
                    {course.exam.title}
                </option>
            )
            :
            (
                <option id={'certificate'} key={'certificate'}>
                    {translations[currentLang].ending}
                </option>
            );
    }

    return (<>
        <div className={styles.layout}>
            <div className={styles.layoutContainer}>
                <div className={styles.navigationsLeft}>
                    <div className={styles.courseHeader}>
                        <div className={styles.courseHeaderTitle}>{course.title}</div>
                    </div>

                    {course.lessons.map((lesson, i) =>
                        <div
                            className={styles.lessonTitleLine + ' ' + styles.cursorPointer + (activeMode === LESSON_MODE && activeLessonIndex === i ? (' ' + styles.selectedLine) : '')}
                            key={i} onClick={() => toLesson(i)}>
                            <span>{lesson.title}</span>
                        </div>
                    )}

                    {course.certName === null ?
                        <div
                            className={styles.lessonTitleLine + ' ' + styles.cursorPointer + (activeMode === EXAM_MODE ? (' ' + styles.selectedLine) : '')}
                            onClick={() => toExam()}>
                            <span>{course.exam.title}</span>
                        </div> : ''
                    }

                    {course.certName !== null ?
                        <div
                            className={styles.lessonTitleLine + ' ' + styles.cursorPointer + (activeMode === CERT_MODE ? (' ' + styles.selectedLine) : '')}
                            onClick={() => toCert()}>
                            <span>{translations[currentLang].ending}</span>
                        </div> : ''
                    }
                </div>

                <div className={styles.divider}/>

                <div className={styles.content}>
                    {children}
                </div>
            </div>

            <div className={styles.layoutContainerMobile}>
                <header className={styles.courseHeader}>
                    <h1>{course.title}</h1>

                    <nav className={styles.courseNavigation}>
                        <label htmlFor="lesson-select">Lesson:</label>
                        <select onChange={handleChapterChange} name="lesson" id="lesson-select">
                            {course.lessons.map((lesson, i) =>
                                <option key={i} value={lesson.title}>{lesson.title}</option>
                            )}

                            {getLastOption(course.certName)}
                        </select>
                    </nav>
                </header>
                <div className={styles.lessonContent}>
                    {children}
                </div>
            </div>
        </div>
    </>)
}

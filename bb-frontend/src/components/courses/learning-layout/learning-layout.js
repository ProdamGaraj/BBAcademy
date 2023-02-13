import {NavLink} from "react-router-dom";
import translations from "translations";

import styles from './learning-layout.module.css'
import {useContext} from "react";
import LangContext from "contexts/lang-context";

const LESSON_MODE = 1;
const EXAM_MODE = 2;
const CERT_MODE = 3;

export default ({children, course, toLesson, toExam, toCert, activeLessonIndex, activeMode}) => {

    let currentLang = useContext(LangContext).lang

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
        </div>
    </>)
}
import {NavLink} from "react-router-dom";
import translations from "translations";

import styles from './learning-layout.module.css'
import {useContext} from "react";
import LangContext from "contexts/lang-context";

export default ({children, course, toLesson, toExam}) => {

    let currentLang = useContext(LangContext).lang

    return (<>
        <div className={styles.layout}>
            <div className={styles.layoutContainer}>
                <div className={styles.navigationsLeft}>
                    <div className={styles.courseHeader}>
                        <div className={styles.courseHeaderTitle}>{course.title}</div>
                    </div>

                    {course.lessons.map((lesson, i) =>
                        <div className={styles.lessonTitleLine + ' ' + styles.cursorPointer} key={i} onClick={() => toLesson(i)}>
                            <span>{lesson.title}</span>
                        </div>
                    )}

                    <div className={styles.lessonTitleLine + ' ' + styles.cursorPointer} onClick={() => toExam()}>
                        <span>{course.exam.title}</span>
                    </div>

                    <div className={styles.lessonTitleLine + ' ' + styles.cursorPointer}>
                        <span>{translations[currentLang].ending}</span>
                    </div>
                </div>

                <div className={styles.divider}/>

                <div className={styles.content}>
                    {children}
                </div>
            </div>
        </div>
    </>)
}
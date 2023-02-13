import styles from './order.module.css';
import backend from "backend";
import translations from "../../translations";
import {useContext} from "react";
import LoaderModalContext from "../../contexts/loader-modal-context";
import ErrorModalContext from "../../contexts/error-modal-context";
import LangContext from "../../contexts/lang-context";

export default (props) => {

    let lang = useContext(LangContext).lang

    const course = props.course;

    const removeItem = () => {
        props.onCourseRemoved(course.id);
    }

    return (
        <div className={styles.courseCard}>
            <img className={styles.cardImage} src={course.mediaPath} alt=""/>
            <div className={styles.cardInfoFlex}>
                <div className={styles.cardTextBlock}>
                    <div className={styles.cardTitle}>{course.title}</div>
                    <div className={styles.cardDescription}>{course.description}</div>
                </div>
                <div className={styles.cardInfoBlock}>
                    <img src="/img/Account/bell.svg" alt=""/>
                    <div className={styles.cardInfoText}>{course.lessonsCount} уроков</div>
                    <div className={styles.cardInfoText}>{course.durationHours} часов</div>

                    <span
                        className={styles.cardButton + ' ' + styles.cardButtonRed}
                        onClick={() => props.onCourseRemoved(course.id)}>
                        {translations[lang].inkart}
                    </span>
                </div>
            </div>
        </div>
    );
}

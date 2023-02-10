import styles from './lesson-content-view.module.css';
import translations from "translations";
import {useContext} from "react";
import LangContext from "contexts/lang-context";

export default ({lesson}) => {

    let lang = useContext(LangContext).lang
    
    return (
        <>
            <div className={styles.contentContainer}>
                <div className={styles.contentHeading}>
                    {(translations[lang].header)}
                </div>

                <div className={styles.contentText}>
                    {lesson.textContent}
                </div>

                <div className={styles.contentMedia}>
                    <video width="800" controls>
                        <source src={lesson.mediaContentPath} type="video/mp4"/>
                    </video>
                </div>
            </div>
        </>
    )
}
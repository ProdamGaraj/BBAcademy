import styles from "./navigation-arrows.module.css";
import translations from "translations";
import {useContext} from "react";
import LangContext from "contexts/lang-context";

export default ({onPrev, onNext, isFirst, isLast}) => {

    let lang = useContext(LangContext).lang

    return (
        <div className={styles.buttonLine}>
            {!isFirst ?
                <div className={styles.lessonButton}
                     onClick={() => onPrev()}>
                    <img className={styles.arrowLeft} src="/img/Course/arrow-left.svg"
                         alt=""/>
                    <span>{(translations[lang].prev)}</span>
                </div> : ''}

            {!isLast ?
                <div className={styles.lessonButton}
                     onClick={() => onNext()}>
                    <div className="next">{(translations[lang].next)}</div>
                    <img className={styles.arrowRight} src="/img/Course/arrow-right.svg"
                         alt=""/>
                </div> : ''}
        </div>)
}
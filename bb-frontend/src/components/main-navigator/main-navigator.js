import {useContext} from "react";
import LangContext from "contexts/lang-context";
import styles from "./main-navigator.module.css";
import {NavLink} from "react-router-dom";
import translations from "translations";

export default () => {
    let lang = useContext(LangContext).lang
    return (
        <div className={styles.navigator}>
            <NavLink to={"/courses"}>
                <span className={styles.navigatorElement + " " + styles.navigatorElementClickable}>
                    <img src="/img/Account/user.svg" alt=""/>
                    <span>
                        {translations[lang].personalArea}
                    </span>
                </span>
            </NavLink>
            <NavLink to={"/courses"}>
                <span className={styles.navigatorElement + " " + styles.navigatorElementClickable}>
                    <img src="/img/Account/course.svg" alt=""/>
                    <span>
                        {translations[lang].mycourse}
                    </span>
                </span>
            </NavLink>
        </div>);
};
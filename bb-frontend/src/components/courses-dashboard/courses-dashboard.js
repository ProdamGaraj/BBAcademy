import styles from './courses-dashboard.module.css'
import {useContext, useEffect, useState} from "react";
import LangContext from "../../contexts/lang-context";
import translations from "../../translations";
import backend from "../../backend";
import LoaderModalContext from "../../contexts/loader-modal-context";
import ErrorModalContext from "../../contexts/error-modal-context";
import UserLeftLayout from "../user-left-layout/user-left-layout";
import MainNavigator from "../main-navigator/main-navigator";
import {NavLink} from "react-router-dom";

export default () => {

    let lang = useContext(LangContext).lang

    let loaderModal = useContext(LoaderModalContext)
    let errorModal = useContext(ErrorModalContext)

    let [courses, setCourses] = useState([]);

    useEffect(() => {
        loaderModal.showModal()
        backend.Course.GetForDashboard()
            .then(response => setCourses(response))
            .catch(e => errorModal.showModal(e.message))
            .finally(() => loaderModal.close())
    }, [])

    const removeFromCart = (id) => {

    }

    return (<>
        <UserLeftLayout>
            <MainNavigator/>
            <div className={styles.filterContainer}>
                <div className={styles.filterButtonsContainer}>
                    <span className={styles.filterElement}>
                        <span>
                            {translations[lang].allcourses}
                        </span>
                    </span>
                    <span className={styles.filterElement}>
                        <img className={styles.filterIcon} src="/img/Account/book-open.svg" alt=""/>
                        <span>
                            {translations[lang].startedcourses}
                        </span>
                    </span>
                    <span className={styles.filterElement}>
                        <img className={styles.filterIcon} src="/img/Account/sber.svg" alt=""/>
                        <span>
                            {translations[lang].passedcourses}
                        </span>
                    </span>
                </div>

                <img src="/img/Account/sort.svg" alt=""/>
            </div>

            <div className={styles.coursesListWrapper}>
                {courses.map((item, i) => (
                    <div key={i} className={styles.courseCard}>
                        <img className={styles.cardImage} src={item.mediaPath} alt=""/>
                        <div className={styles.cardInfoFlex}>
                            <div className={styles.cardTextBlock}>
                                <div className={styles.cardTitle}>{item.title}</div>
                                <div className={styles.cardDescription}>{item.description}</div>
                            </div>
                            <div className={styles.cardInfoBlock}>
                                <img src="/img/Account/bell.svg" alt=""/>
                                <div className={styles.cardInfoText}>{item.lessonsCount} уроков</div>
                                <div className={styles.cardInfoText}>{item.durationHours} часов</div>
                                {item.state === 'InCart' ?
                                    <span
                                        className={styles.cardButton + ' ' + styles.cardButtonRed}
                                        onClick={() => removeFromCart(item.id)}>
                                    {translations[lang].incart}
                                </span> :
                                    <NavLink to={"/learning?id=" + item.id} className={styles.cardButton}>
                                        {translations[lang].start}
                                    </NavLink>
                                }
                            </div>
                        </div>
                    </div>
                ))}
            </div>
        </UserLeftLayout>
    </>)
}
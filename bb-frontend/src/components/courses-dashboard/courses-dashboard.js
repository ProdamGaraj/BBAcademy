import styles from './courses-dashboard.module.css'
import {useContext, useEffect, useState} from "react";
import LangContext from "../../contexts/lang-context";
import translations from "../../translations";
import backend from "../../backend";
import LoaderModalContext from "../../contexts/loader-modal-context";
import ErrorModalContext from "../../contexts/error-modal-context";
import UserLeftLayoutContainer from "../user-left-layout/user-left-layout";
import MainNavigator from "../main-navigator/main-navigator";
import {NavLink} from "react-router-dom";

export default () => {

    let currentLang = useContext(LangContext).lang

    let loaderModal = useContext(LoaderModalContext)
    let errorModal = useContext(ErrorModalContext)

    let [courses, setCourses] = useState([
        // {
        //     name: 'Инвестиции для начинающих',
        //     description: 'Lorem ipsum dolor sit amet lorem ipsum dolor sit amet Lorem ipsum dolor sit amet. Lorem ipsum dolor sit ametLorem ipsum dolor sit amet',
        //     LessonsCount: 9,
        //     Duration: 3.5,
        //     State: 'InCart',
        //     Id: 1,
        //     MediaPath: '/img/Shared/course_guy.png'
        // },
        // {
        //     Name: 'Инвестиции для начинающих',
        //     Description: 'Lorem ipsum dolor sit amet lorem ipsum dolor sit amet Lorem ipsum dolor sit amet. Lorem ipsum dolor sit ametLorem ipsum dolor sit amet',
        //     LessonsCount: 9,
        //     Duration: 3.5,
        //     State: 'InCart',
        //     Id: 1,
        //     MediaPath: '/img/Shared/course_guy.png'
        // }
    ]);

    useEffect(() => {
        loaderModal.showModal()
        backend.Course.GetForDashboard()
            .then(response => setCourses(response))
            .catch(e => errorModal.showModal(e.message))
            .finally(() => loaderModal.close())
    }, [])

    useEffect(() => {
        loaderModal.showModal()
        backend.Account.Tester()
            .then(response => console.log(response))
            .catch(e => errorModal.showModal(e.message))
            .finally(() => loaderModal.close())
    }, [])

    const removeFromCart = (id) => {

    }

    return (<>
        <UserLeftLayoutContainer>
            <MainNavigator/>
            <div className={styles.filterContainer}>
                <div className={styles.filterButtonsContainer}>
                    <span className={styles.filterElement}>
                        <span>
                            {translations[currentLang].allcourses}
                        </span>
                    </span>
                    <span className={styles.filterElement}>
                        <img className={styles.filterIcon} src="/img/Account/book-open.svg" alt=""/>
                        <span>
                            {translations[currentLang].startedcourses}
                        </span>
                    </span>
                    <span className={styles.filterElement}>
                        <img className={styles.filterIcon} src="/img/Account/sber.svg" alt=""/>
                        <span>
                            {translations[currentLang].passedcourses}
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
                                <div className={styles.cardTitle}>{item.name}</div>
                                <div className={styles.cardDescription}>{item.description}</div>
                            </div>
                            <div className={styles.cardInfoBlock}>
                                <img src="/img/Account/bell.svg" alt=""/>
                                <div className={styles.cardInfoText}>{item.lessonsCount} уроков</div>
                                <div className={styles.cardInfoText}>{item.durationHours} часов</div>
                                {item.State === 'InCart' ?
                                    <span
                                        className={styles.cardButton + ' ' + styles.cardButtonRed}
                                        onClick={() => removeFromCart(item.id)}>
                                    {translations[currentLang].incart}
                                </span> :
                                    <NavLink to={"/learning?id=" + item.id}>
                                        <span className={styles.cardButton}>{translations[currentLang].start}</span>
                                    </NavLink>
                                }
                            </div>
                        </div>
                    </div>
                ))}
            </div>
        </UserLeftLayoutContainer>
    </>)
}
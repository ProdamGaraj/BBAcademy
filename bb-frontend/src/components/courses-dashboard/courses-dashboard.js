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

const Unknown = 1
const Bought = 2
const InCart = 3
const Passed = 4

const MODE_ALL = 1
const MODE_BOUGHT = 2
const MODE_PASSED = 3

export default () => {

    let lang = useContext(LangContext).lang

    let loaderModal = useContext(LoaderModalContext)
    let errorModal = useContext(ErrorModalContext)

    let [allCourses, setAllCourses] = useState([]);
    let [visibleCourses, setVisibleCourses] = useState([])
    let [mode, setMode] = useState(MODE_ALL)

    useEffect(() => {
        loaderModal.showModal()
        backend.Course.GetForDashboard()
            .then(response => {
                setAllCourses(response)
                setVisibleCourses(response.filter(c => c.state === Unknown))
            })
            .catch(e => errorModal.showModal(e.message))
            .finally(() => loaderModal.close())
    }, [])

    const removeFromCart = (id) => {
        loaderModal.showModal()
        backend.Cart.RemoveCourse(id)
            .then(response => {
                let courses = allCourses.map(c => (c.id === id ? {...c, state: Unknown} : c))
                setAllCourses(courses)
                setVisibleCourses(courses)
            })
            .catch(e => errorModal.showModal(e.message))
            .finally(() => loaderModal.close())
    }

    const addToCart = id => {
        loaderModal.showModal()
        backend.Cart.AddCourse(id)
            .then(response => {
                let courses = allCourses.map(c => (c.id === id ? {...c, state: InCart} : c))
                setAllCourses(courses)
                setVisibleCourses(courses)
            })
            .catch(e => errorModal.showModal(e.message))
            .finally(() => loaderModal.close())
    };

    function showAll() {
        setMode(MODE_ALL)
        setVisibleCourses(allCourses.filter(c => c.state === Unknown))
    }

    function showBought() {
        setMode(MODE_BOUGHT)
        setVisibleCourses(allCourses.filter(c => c.state === Bought))
    }

    function showPassed() {
        setMode(MODE_PASSED)
        setVisibleCourses(allCourses.filter(c => c.state === Passed))
    }

    return (<>
        <UserLeftLayout>
            <MainNavigator/>
            <div className={styles.filterContainer}>
                <div className={styles.filterButtonsContainer}>
                    <span className={styles.filterElement + (mode === MODE_ALL ? ' ' + styles.filterActive: '')} onClick={() => showAll()}>
                        <span>
                            {translations[lang].allcourses}
                        </span>
                    </span>
                    <span className={styles.filterElement + (mode === MODE_BOUGHT ? ' ' + styles.filterActive: '')} onClick={() => showBought()}>
                        <img className={styles.filterIcon} src="/img/Account/book-open.svg" alt=""/>
                        <span>
                            {translations[lang].startedcourses}
                        </span>
                    </span>
                    <span className={styles.filterElement + (mode === MODE_PASSED ? ' ' + styles.filterActive: '')} onClick={() => showPassed()}>
                        <img className={styles.filterIcon} src="/img/Account/sber.svg" alt=""/>
                        <span>
                            {translations[lang].passedcourses}
                        </span>
                    </span>
                </div>

                <img src="/img/Account/sort.svg" alt=""/>
            </div>

            <div className={styles.coursesListWrapper}>
                {visibleCourses.map((item, i) => (
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
                                {mode === MODE_ALL && item.state !== InCart ?
                                    <span
                                        className={styles.cardButton}
                                        onClick={() => addToCart(item.id)}>
                                        {translations[lang].incart}
                                    </span> : ''}
                                {mode === MODE_ALL && item.state === InCart ?
                                    <span
                                        className={styles.cardButton + ' ' + styles.cardButtonRed}
                                        onClick={() => removeFromCart(item.id)}>
                                        {translations[lang].inkart}
                                    </span> : ''
                                }
                                {mode === MODE_BOUGHT ?
                                    <span
                                        className={styles.cardButton}
                                        onClick={() => window.location.href = '/learning?id=' + item.id}>
                                        {translations[lang].start}
                                    </span> : ''
                                }
                                {mode === MODE_PASSED ?
                                    <span
                                        onClick={() => window.location.href = '/learning?id=' + item.id}
                                        className={styles.cardButton}>
                                        {translations[lang].start}
                                    </span> : ''
                                }
                            </div>
                        </div>
                    </div>
                ))}
            </div>
        </UserLeftLayout>
    </>)
}
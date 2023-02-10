import {useEffect, useState, useContext} from "react";
import UserContext from "../../contexts/user-context";

import LangContext from "../../contexts/lang-context";
import translations from 'translations';
import {NavLink} from "react-router-dom";
import backend from "../../backend";
import LoaderModalContext from "../../contexts/loader-modal-context";
import styles from "./cart.module.css";
import ErrorModalContext from "../../contexts/error-modal-context";

export default () => {

    let currentLang = useContext(LangContext).lang
    let user = useContext(UserContext).user;
    let loaderModal = useContext(LoaderModalContext)
    let errorModal = useContext(ErrorModalContext)

    let [courses, setCourses] = useState([])

    useEffect(() => {
        loaderModal.showModal()
        backend.Cart.GetAll()
            .then(response => setCourses(response))
            .catch(e => errorModal.showModal(e.message))
            .finally(() => loaderModal.close())
    }, [])

    let checkout = () => {
        loaderModal.showModal()
        backend.Cart.Checkout()
            .then(() => {
                // TODO: Do something with checkout
                loaderModal.close()
            })
            .catch(e => errorModal.showModal(e.message))
            .finally(() => loaderModal.close())
    }

    let removeFromCart = (courseId) => {
        loaderModal.showModal()
        backend.Cart.RemoveCourse(courseId)
            .then(() => setCourses(prev => prev.filter(c => c.Id !== courseId)))
            .catch(e => errorModal.showModal(e.message))
            .finally(() => loaderModal.close())
    }

    return (<>
        <div className="courses-container">
            <div className="account-data">
                <div className="user_data">
                    <img className="user_data-photo" src="/img/Account/ur_photo.png"/>
                    <div className="user_data-username">{user.FirstName}</div>
                </div>
                <div className="user_info">
                    <div className="user_info-block">
                        <img src="/img/Course/people.svg"/>
                        <div className="user_info-block-name"> {user.LastName} {user.FirstName} {user.MiddleName}
                        </div>
                    </div>
                    <div className="user_info-block">
                        <img src="/img/Account/bag.svg"/>
                        <div className="user_info-block-name">{user.JobTitle} in {user.Organisation}</div>
                    </div>
                    <div className="user_info-block">
                        <img src="/img/Account/rait.svg"/>
                        <div className="user_info-block-name">{user.Rating}</div>
                    </div>
                    <div className="user_info-block-1">
                        <div className="user_info-block-1-1">
                            <img src="/img/Course/peoples.svg"/>
                            <div className="user_info-block-name">{user.RecommendedBy}</div>
                        </div>
                    </div>
                    <NavLink to='/my-certificates'>
                        <div className="user_info-block user_info-block-clickable">
                            <img src="/img/Account/sertif.svg"/>
                            <div className="user_info-block-name">{(translations[currentLang].mycert)}</div>
                        </div>
                    </NavLink>
                </div>
            </div>

            <div className="Demarcation-line"></div>
            <div className="mine_course">

                <div className="ShoppingCart">Корзина</div>
                <div className="items-wrapper">
                    {courses.map((course, i) =>
                        (<div key={i} className="course-wrapper">

                            <img className="cours-scroll" src={course.MediaPath}/>
                            <div className="cours-text">
                                <div className="cours-text-top">{course.Name}</div>
                                <div className="cours-text-bot">{course.Description}</div>
                            </div>
                            <div className="cours-info-block">
                                <img className="cours-info-block-src" src="/img/Account/bell.svg"/>
                                <div className="cours-info-block-hours-1">{course.Lessons.length} lessons</div>
                                <div className="cours-info-block-hours-2">{course.Duration}</div>
                                {course.State === "InCart" ? (
                                    <a className="cours-info-block-button button-special"
                                       onClick={() => removeFromCart(course.Id)}>{translations[currentLang].incart}</a>) : (
                                    <a className="cours-info-block-button button-special"
                                       href={"/course-view?id=" + course.Id}>{translations[currentLang].incart}</a>)}
                            </div>
                        </div>))}
                </div>

                {courses.length ? '' :
                    <div className={styles.no_courses}>
                <span
                    className={styles.text}>
                    No Courses Were Added
                </span>
                    </div>}

                <a className="Buy_Button"
                   onClick={() => checkout()}>Купить {courses.map(c => c.Price).reduce((acc, cur) => acc + cur, 0)}</a>
            </div>
        </div>
    </>)
}
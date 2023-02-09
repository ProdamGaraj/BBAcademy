﻿import './courses-dashboard.css'
import { useContext, useEffect, useState } from "react";
import UserContext from "../../contexts/user-context";
import LangContext from "../../contexts/lang-context";
import translations from "../../translations";
import backend from "../../backend";
import {NavLink} from "react-router-dom";

export default () => {

    let user = useContext(UserContext).user

    let currentLang = useContext(LangContext).lang

    let [courses, setCourses] = useState([{
        MediaPath: 'some-path',
        Name: 'some-name',
        Description: 'some-description',
        LessonsCount: 5,
        Duration: '5.5',
        IsBought: false,
        Id: 505
    },]);

    useEffect(() => {
        backend.Course.GetForDashboard()
            .then(response => {
                setCourses(response)
            })
    }, [])

    const showMyCert = () => {
        window.location.href = '/mycerts'
    };

    const showMyCourses = () => {
        window.location.href = '/courses'
    };

    const removeFromCart = courseId => {

    };

    return (<>
        <div className="courses-container">
            <div className="account-data">
                <div className="user_data">
                    <img className="user_data-photo" src="/img/Account/ur_photo.png" />
                    <div className="user_data-username">{user.FirstName}</div>
                </div>
                <div className="user_info">
                    <div className="user_info-block">
                        <img src="/img/Course/people.svg" />
                        <div className="user_info-block-name"> {user.LastName} {user.FirstName} {user.MiddleName}
                        </div>
                    </div>
                    <div className="user_info-block">
                        <img src="/img/Account/bag.svg" />
                        <div className="user_info-block-name">{user.JobTitle} in {user.Organisation}</div>
                    </div>
                    <div className="user_info-block">
                        <img src="/img/Account/rait.svg" />
                        <div className="user_info-block-name">{user.Rating}</div>
                    </div>
                    <div className="user_info-block-1">
                        <div className="user_info-block-1-1">
                            <img src="/img/Course/peoples.svg" />
                            <div className="user_info-block-name">{user.RecommendedBy}</div>
                        </div>
                    </div>
                    <NavLink to={'/my-certificates/'}>
                        <div className="user_info-block user_info-block-clickable">
                            <img src="/img/Account/sertif.svg" />
                            <div className="user_info-block-name">{translations[currentLang].mycert}</div>
                        </div>
                    </NavLink>
                </div>
            </div>

            <div className="Demarcation-line"></div>

            <div className="my_acc-tab_menu">
                <div className="media-block">
                    <div className="media-name">
                        <img className="media-name-svg" src="/img/Account/course.svg" />
                        <div className="media-name-name media-name-name-clickable" onClick={() => showMyCourses()}>
                            {translations[currentLang].mycourse}
                        </div>
                    </div>
                </div>
                <div className="mine_course">
                    <div className="mine_course-sort">
                        <div className="mine_course-sort-element-1">

                            <button type="submit" className="button1">
                                {translations[currentLang].allcourses}
                            </button>

                        </div>
                        <div className="mine_course-sort-element-2">
                            <img className="mine_course-sort-svg-icon" src="/img/Account/book-open.svg" />

                            <button type="submit" className="button1">
                                {translations[currentLang].startedcourses}
                            </button>

                            <div className="mine_course-sort-element-3">
                                <img className="mine_course-sort-svg-icon" src="/img/Account/sber.svg" />

                                <button type="submit" className="button1">
                                    {translations[currentLang].passedcourses}
                                </button>

                            </div>
                            <div className="mine_course-sort-element-4">
                                <img src="/img/Account/sort.svg" />
                            </div>
                        </div>
                    </div>
                    <div className="items-wrapper">
                        <div className="item-wrapper">
                            {courses.map((item, i) => (<div key={i} className="course-wrapper">

                                <img className="cours-scroll" src={item.MediaPath} />
                                <div className="cours-text">
                                    <div className="cours-text-top">{item.Name}</div>
                                    <div className="cours-text-bot">{item.Description}</div>
                                </div>
                                <div className="cours-info-block">
                                    <img className="cours-info-block-src" src="/img/Account/bell.svg" />
                                    <div className="cours-info-block-hours-1">{item.LessonsCount} lessons</div>
                                    <div className="cours-info-block-hours-2">{item.Duration}</div>
                                    {item.IsBought ? (<a className="cours-info-block-button button-special"
                                        onClick={() => removeFromCart(item.Id)}>{translations[currentLang].incart}</a>) : (
                                        <a className="cours-info-block-button button-special"
                                            href={"/course-view?id=" + item.Id}>{translations[currentLang].incart}</a>)}
                                </div>
                            </div>))}
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </>)
}
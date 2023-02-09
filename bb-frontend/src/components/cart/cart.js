import { useEffect, useState, useContext } from "react";
import UserContext from "../../contexts/user-context";
import baseurl from "base-url";
import LangContext from "../../contexts/lang-context";
import translations from 'translations'

export default () => {

    let currentLang = useContext(LangContext).lang
    let user = useContext(UserContext).user;
    let [courses, setCourses] = useState([])

    useEffect(() => {
        async function getData() {
            const response = await fetch(
                baseurl + "/Cart/GetAll"
            )
            let actualData = await response.json();
            this.setCourses(actualData)
            console.log(actualData)
        }
        getData()
    }, [])

    let buy = () => {
        async function buy() {
            const response = await fetch(
                baseurl + "/Cart/Buy"
            )
            let actualData = await response.json();
            this.setCourses(actualData)
            console.log(actualData)
        }
        buy()
    }

    let removeFromCart = (index) => {
        async function removeFromCart() {
            const response = await fetch(
                baseurl + "/Cart/Buy"
            )
            let actualData = await response.json();
            this.setCourses(actualData)
            console.log(actualData)
        }
        removeFromCart()
    }

    let showMyCert = () => {
        async function showMyCert() {
            const response = await fetch(
                baseurl + "/myCertificates"
            ).then((response) => response.json()).then(response => showMyCert()).catch(error => alert(error))
        }
        showMyCert()
    }

    return (<>
        <body>
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
                        <div className="user_info-block user_info-block-clickable" onClick={() => showMyCert()}>
                            <img src="/img/Account/sertif.svg" />
                            <div className="user_info-block-name">{(translations[currentLang].mycert)}</div>
                        </div>
                    </div>
                </div>

                <div className="Demarcation-line"></div>
                <div className="mine_course">

                    <div className="ShoppingCart">Корзина</div>
                    <div className="items-wrapper">
                        <div className="item-wrapper">
                            {courses.map((course, i) => 
                            (<div key={i} className="course-wrapper">

                                <img className="cours-scroll" src={course.MediaPath} />
                                <div className="cours-text">
                                    <div className="cours-text-top">{course.Name}</div>
                                    <div className="cours-text-bot">{course.Description}</div>
                                </div>
                                <div className="cours-info-block">
                                    <img className="cours-info-block-src" src="/img/Account/bell.svg" />
                                    <div className="cours-info-block-hours-1">{course.Lessons.length} lessons</div>
                                    <div className="cours-info-block-hours-2">{course.Duration}</div>
                                    {course.IsBought ? (<a className="cours-info-block-button button-special"
                                        onClick={() => removeFromCart(course.Id)}>{translations[currentLang].incart}</a>) : (
                                        <a className="cours-info-block-button button-special"
                                            href={"/course-view?id=" + course.Id}>{translations[currentLang].incart}</a>)}
                                </div>
                            </div>))}
                        </div>
                    </div>
                    <a className="Buy_Button" onClick={() => buy()}>Купить {courses.reduce((acc, cur) => acc + cur.Price, 0)}</a>
                </div>
            </div>
        </body>
    </>)
}
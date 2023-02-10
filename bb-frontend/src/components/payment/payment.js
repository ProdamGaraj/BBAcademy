import Template from "../template/template";
import {useContext, useEffect, useState} from "react";
import Order from "./order";
import backend from "../../backend";
import LoaderModalContext from "../../contexts/loader-modal-context";
import ErrorModalContext from "../../contexts/error-modal-context";
import styles from './payment.css';

export default () => {
    const loaderModal = useContext(LoaderModalContext)
    const errorModal = useContext(ErrorModalContext)

    const [courses, setCourses] = useState([]);

    function useMock() {
        setCourses([
            {
                id: 1,
                mediaPath: '#',
                name: 'lorem',
                description: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Dolore doloribus ea necessitatibus porro recusandae unde?\n' +
                    '    Ab, culpa dicta fuga impedit ipsa libero, officiis pariatur quaerat qui quia, sapiente sunt voluptatum.',
                lessonsCount: 2,
                durationHours: 5.5
            },
            {
                id: 2,
                mediaPath: '#',
                name: 'lorem',
                description: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Dolore doloribus ea necessitatibus porro recusandae unde?\n' +
                    '    Ab, culpa dicta fuga impedit ipsa libero, officiis pariatur quaerat qui quia, sapiente sunt voluptatum.',
                lessonsCount: 2,
                durationHours: 5.5
            },
            {
                id: 3,
                mediaPath: '#',
                name: 'lorem',
                description: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Dolore doloribus ea necessitatibus porro recusandae unde?\n' +
                    '    Ab, culpa dicta fuga impedit ipsa libero, officiis pariatur quaerat qui quia, sapiente sunt voluptatum.',
                lessonsCount: 2,
                durationHours: 5.5
            }
        ])
    }

    useEffect(() => {
        useMock();
        //     backend.Cart.GetAll()
        //         .then(courses => {
        //             setCourses(courses);
        //         })
        //         .catch(e => {
        //             // errorModal.showModal(e.message)
        //             useMock();
        //         })
        //         .finally(() => loaderModal.close())
    }, [setCourses])


    function removeCourse(courseId) {
        backend.Cart.RemoveCourse(courseId)
            .then(() => {
                const _courses = courses.filter(value => value.id !== courseId);
                setCourses(_courses);
            });
    }

    return (<>
        <div className="grid-content">
            <div className="order-completion">
                <h1>Оформление заказа</h1>

                <div className="field-wrapper">
                    <label htmlFor="email-for-receipt">Электронная почта для получения чека</label>
                    <input id="email-for-receipt" type="email"/>
                </div>

                <div className="field-wrapper">
                    <label htmlFor="reciver">Получатель</label>
                    <input id="reciver" type="text"/>
                </div>
            </div>

            <div className="total-info-card">
                <h2>
                    <span>Всего: 2 курса</span>
                    <span>10 000</span>
                </h2>
                <h4>
                    <span>Введение в банковскую деятельность</span>
                    <span>4000</span>
                </h4>
                <h4>
                    <span>Инвестиции для начинающих</span>
                    <span>4000</span>
                </h4>
                <button>Перейти к оплате</button>
            </div>

            <div className="orders-container">
                <h2>Заказы</h2>

                {courses.map(course => (
                    <Order key={course.id} course={course} onCourseRemoved={removeCourse}/>
                ))}
            </div>
        </div>
    </>);
}

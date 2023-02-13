import Template from "../template/template";
import {useContext, useEffect, useState} from "react";
import Order from "../order/order";
import backend from "../../backend";
import LoaderModalContext from "../../contexts/loader-modal-context";
import ErrorModalContext from "../../contexts/error-modal-context";
import styles from './payment.module.css';
import LangContext from "../../contexts/lang-context";
import translations from "../../translations";


export default () => {
    let lang = useContext(LangContext).lang
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
        loaderModal.showModal()
        backend.Cart.GetAll()
            .then(courses => setCourses(courses))
            .catch(e => errorModal.showModal(e.message))
            .finally(() => loaderModal.close())
    }, [])


    function removeCourse(courseId) {
        loaderModal.showModal()
        backend.Cart.RemoveCourse(courseId)
            .then(() => {
                const _courses = courses.filter(value => value.id !== courseId);
                setCourses(_courses);
            })
            .catch(e => errorModal.showModal(e.response.data.error))
            .finally(() => loaderModal.close())
        ;
    }

    const checkout = () => {
        loaderModal.showModal()
        backend.Cart.Checkout()
            .then(courses => window.location.href = '/courses')
            .catch(e => errorModal.showModal(e.message))
            .finally(() => loaderModal.close())
    };

    return (<>
        <div className={styles.gridContent}>
            <div className={styles.orderCompletion}>
                <h1>{translations[lang].orderCompletion}</h1>

                <div className={styles.fieldWrapper}>
                    <label htmlFor="email-for-receipt">{translations[lang].mailForReceipt}</label>
                    <input id="email-for-receipt" type="email"/>
                </div>

                <div className={styles.fieldWrapper}>
                    <label htmlFor="receiver">{translations[lang].Reciever}</label>
                    <input id="receiver" type="text"/>
                </div>
            </div>

            <div className={styles.totalInfoCard}>
                <h2>
                    <span>{translations[lang].Total}: {courses.length} {translations[lang].course}</span>
                    <span>{courses.map(c => c.price).reduce((acc, cur) => acc + cur, 0)} р</span>
                </h2>
                {courses.map((c, i) =>
                    (
                        <h4>
                            <span>{c.title}</span>
                            <span>{c.price} р</span>
                        </h4>
                    ))}

                <button onClick={() => checkout()}>{translations[lang].GoToThePayment}</button>
            </div>

            <div className={styles.ordersContainer}>
                <h2>{translations[lang].Orders}</h2>

                {courses.map((course, i) => (
                    <Order key={i} course={course} onCourseRemoved={removeCourse}/>
                ))}
            </div>
        </div>
    </>);
}

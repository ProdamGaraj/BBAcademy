import {useContext, useEffect, useState} from "react";
import Order from "../order/order";
import backend from "backend";
import LoaderModalContext from "contexts/loader-modal-context";
import ErrorModalContext from "contexts/error-modal-context";
import styles from './payment.module.css';
import LangContext from "contexts/lang-context";
import translations from "translations";
import SuccessModalContext from "contexts/success-modal-context";

export default () => {
    let lang = useContext(LangContext).lang
    const loaderModal = useContext(LoaderModalContext)
    const errorModal = useContext(ErrorModalContext)
    const successModal = useContext(SuccessModalContext)


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

    const createPayment = () => {
        loaderModal.showModal()
        backend.Payment.CreatePayment()
            .then(({
                       merchantId,
                       merchantUserId,
                       serviceId,
                       transId,
                       transAmount,
                       returnUrl
                   }) => {
                console.log("Received response: ", merchantId,
                    merchantUserId,
                    serviceId,
                    transId,
                    transAmount,
                    returnUrl);
                successModal.showModal(
                    <>
                        <span>Вам успешно выставлен счёт</span>

                        <form action="https://my.click.uz/services/pay" id="click_form" method="get" target="_blank">
                            <input type="hidden" name="amount" value={transAmount}/>
                            <input type="hidden" name="merchant_id" value={merchantId}/>
                            <input type="hidden" name="merchant_user_id" value={merchantUserId}/>
                            <input type="hidden" name="service_id" value={serviceId}/>
                            <input type="hidden" name="transaction_param" value={transId}/>
                            <input type="hidden" name="return_url" value={returnUrl}/>
                            {/*<input type="hidden" name="card_type" value="$cardType"/>*/}
                            <button type="submit" className={styles.clickLogo}><i></i>Оплатить через CLICK</button>
                        </form>

                    </>
                );

            })
            .catch(e => errorModal.showModal(e.message))
            .finally(() => loaderModal.close())
    }
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
                        <h4 key={i}>
                            <span>{c.title}</span>
                            <span>{c.price} р</span>
                        </h4>
                    ))}

                <button onClick={() => createPayment()}>{translations[lang].GoToThePayment}</button>
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

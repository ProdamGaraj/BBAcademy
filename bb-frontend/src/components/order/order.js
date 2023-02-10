import styles from './order.css';
import backend from "../../backend";

export default (props) => {
    const course = props.course;

    const  removeItem = () => {
        props.onCourseRemoved(course.id);
    }

    function computeLessonsString(count) {
        const remainder = count % 10;

        if (remainder === 1) {
            return count + ' урок'
        }

        if (1 < remainder && remainder < 6) {
            return count + ' урока'
        }

        return count + ' уроков'
    }

    function computeHoursString(count) {
        const remainder = count % 10;

        if (remainder === 1) {
            return count + ' час'
        }

        if (1 < remainder && remainder < 5) {
            return count + ' часа'
        }

        return count + ' часов'
    }

    return (
        <div className="order-container" tabIndex={0}>
            <img alt="" aria-hidden="true" role="presentation" src="/img/perec-percovich.png"/>

            <div className="order-info-container">
                <h3>{course.name}</h3>
                <p>{course.description}</p>
            </div>

            <div className="course-stats">
                <h6>{computeLessonsString(course.lessonsCount)}</h6>
                <h6>{computeHoursString(course.durationHours)}</h6>
            </div>

            <button onClick={removeItem} tabIndex={0} className="delete-order">x</button>
        </div>
    );
}

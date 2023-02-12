import styles from './lesson-content-view.module.css';
import NavigationArrows from "../navigation-arrows/navigation-arrows";

const mapContentType = (type, path) => {
    switch (type) {
        case 1: //text
            return (
                <div className={styles.contentText}>
                    {path}
                </div>
            )
        case 2: //video
            return (<div className={styles.contentMedia}>
                <video width="100%" controls>
                    <source src={path} type="video/mp4"/>
                </video>
            </div>)
        case 3: //photo
            return (<div className={styles.contentMedia}>
                <object data="/img/home/girl2.png" type="image/png">
                    <img src={path} alt=""/>
                </object>

            </div>)
        default:
            return 'Unknown Content Type'
    }
}

export default ({lesson, toNextLesson, toPrevLesson, isFirst, isLast}) => {

    return (
        <>
            <div className={styles.contentContainer}>
                <div className={styles.contentHeading}>
                    {lesson.title}
                </div>

                <div className={styles.contentText}>
                    {lesson.content}
                </div>

                {mapContentType(lesson.lessonContentType, lesson.mediaContentPath)}

                <NavigationArrows
                    onNext={toNextLesson}
                    onPrev={toPrevLesson}
                    isFirst={isFirst}
                    isLast={isLast}
                />
            </div>
        </>
    )
}
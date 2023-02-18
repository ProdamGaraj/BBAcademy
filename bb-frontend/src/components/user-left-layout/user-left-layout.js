import styles from './user-left-layout.module.css'
import UserLeftInfo from "../user-left-info/user-left-info";

export default (props) => {
    const children = props.children;

    return (<>
        <div className={styles.layout}>
            <div className={styles.layoutContainer}>
                <UserLeftInfo />

                <div className={styles.divider}/>

                <div className={styles.content}>
                    {children}
                </div>
            </div>
        </div>
    </>)
}

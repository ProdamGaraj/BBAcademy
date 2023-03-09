import {useContext, useEffect, useRef} from "react";
import styles from './success-modal.module.css'
import SuccessModalContext from "contexts/success-modal-context";

const Modal = () => {

    let dialogRef = useRef(null);

    let context = useContext(SuccessModalContext);

    useEffect(() => {
        if (context.isOpen) {
            dialogRef.current?.showModal();
        }
        else {
            dialogRef.current?.close();
        }
    }, [context.isOpen])

    return (<>
        <div className={styles.background} style={{display: context.isOpen ? 'block' : 'none'}}>
            <dialog
                ref={dialogRef}
                className={styles.modal}>
                <p className={styles.heading}><b>Success</b></p>
                <p className={styles.heading}>{context.message}</p>
                <button className={styles.closeButton} onClick={() => context.close()} autoFocus={false}>CLOSE</button>
            </dialog>
        </div>
    </>)
}

export const SuccessModal = Modal
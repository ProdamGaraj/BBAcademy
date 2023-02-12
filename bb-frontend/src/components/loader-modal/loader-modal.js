import {useContext, useEffect, useRef} from "react";
import styles from './loader-modal.module.css'
import {Loader} from "../loader/loader";
import LoaderModalContext from "../../contexts/loader-modal-context";

const Modal = () => {

    let dialogRef = useRef(null);
    
    let context = useContext(LoaderModalContext)

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
                className={styles.modal}
            >
                <p className={styles.heading}>Loading ...</p>
                <Loader/>
            </dialog>
        </div>
    </>)
}

export const LoaderModal = Modal
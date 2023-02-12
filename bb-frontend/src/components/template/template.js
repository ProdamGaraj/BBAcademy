import HeaderLanding from "../header-landing/header-landing";
import HeaderCabinet from "../header-cabinet/header-cabinet";
import FooterLogin from "../footer-landing/footer-landing";
import FooterCabinet from "../footer-cabinet/footer-cabinet";

import styles from './template.module.css'

let headerResolver = () => {
    // console.log(window.location.pathname)
    if (window.location.pathname === '/' ||
        window.location.pathname.endsWith('/login') ||
        window.location.pathname.endsWith('/register')
    ) {
        return <HeaderLanding></HeaderLanding>
    }
    return <HeaderCabinet></HeaderCabinet>
}

let footerResolver = () => {
    if (window.location.pathname === '/') {
        return <FooterLogin></FooterLogin>
    }
    return <FooterCabinet></FooterCabinet>
}

export default (props) => (
    <>
        <div className={styles.verticalStretch}>
            {headerResolver()}
                <div className={styles.content}>
                    {props.children}
                </div>
            {footerResolver()}
        </div>
    </>)


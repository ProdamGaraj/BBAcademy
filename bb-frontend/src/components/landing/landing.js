import translations from 'translations'
import LangContext from "../../contexts/lang-context";
import {useContext} from "react";

import styles from './landing.module.css'

export default () => {

    let lang = useContext(LangContext).lang

    return (<>
        <main role="main" className={styles.pb3}>
            <div className={styles.blockLearnIn}>
                <img className={styles.blockLearnInLeftSection} src="/img/Home/guy3.png" alt=""/>
                <div className={styles.blockLearnInRightSection}>
                    <div className={styles.blockLearnInRightSectionTopTop}>
                        {translations[lang].learnWithBBAcademy}
                    </div>
                    <div className={styles.blockLearnInRightSectionTopBot}>
                        {translations[lang].improveYourProfessionalCapital}
                    </div>
                </div>
            </div>
            <div className={styles.advContainer}>
                <div className={styles.advBlock}>
                    <div className={styles.advTop}>
                        {translations[lang].investYourself}
                    </div>
                    <div className={styles.advLearnRight}>
                        <div className={styles.advLearnRightBlock}>
                            <div className={styles.advCardBook}>
                                <img className={styles.bookSvg} src="/img/Home/book.svg" alt=""/>
                            </div>
                            <div className={styles.cardText}>{translations[lang].aLotOfCourses}</div>
                        </div>
                        <div className={styles.advLearnRightBlock}>
                            <div className={styles.advCardBook}>
                                <img className={styles.bookSvg} src="/img/Home/book.svg" alt=""/>
                            </div>
                            <div className={styles.cardText}>{translations[lang].rateSystem}</div>
                        </div>
                        <div className={styles.advLearnRightBlock}>
                            <div className={styles.advCardBook}>
                                <img className={styles.bookSvg} src="/img/Home/book.svg" alt=""/>
                            </div>
                            <div className={styles.cardText}>{translations[lang].autoExamination}</div>
                        </div>
                        <div className={styles.advLearnRightBlock}>
                            <div className={styles.advCardBook}>
                                <img className={styles.bookSvg} src="/img/Home/book.svg" alt=""/>
                            </div>
                            <div className={styles.cardText}>{translations[lang].professorCommunication}</div>
                        </div>
                    </div>
                </div>
            </div>
            <div className="video-block"></div>
            <div className="category_and_popular">
                <div className="category">
                    <div className="category-top">
                        <div className="category-top-left">{translations[lang].categories}</div>
                        <div className="category-top-right">{translations[lang].showMore}</div>
                    </div>
                    <div className="category-bot">
                        <div className="category-bot-block">
                            <div className="category-bot-block-text">
                                <div className="category-bot-block-text-1">
                                    {translations[lang].bBanking}
                                </div>
                                <div className="category-bot-block-text-2">
                                    {translations[lang].courses14}
                                </div>
                            </div>
                            <img className="category-percent" src="/img/Home/percent.svg" alt=""/>
                            <div className="cat-circle-1"></div>
                        </div>
                        <div className="category-bot-block">
                            <div className="category-bot-block-text">
                                <div className="category-bot-block-text-1">
                                    {translations[lang].bBanking}
                                </div>
                                <div className="category-bot-block-text-2">
                                    {/* BUG: UNKNOWN STRING */}{translations[lang].courses14}
                                </div>
                            </div>
                            <img className="category-percent" src="/img/Home/percent.svg" alt=""/>
                            <div className="cat-circle-2"></div>
                        </div>
                        <div className="category-bot-block">
                            <div className="category-bot-block-text">
                                <div className="category-bot-block-text-1">
                                    {translations[lang].bBanking}
                                </div>
                                <div className="category-bot-block-text-2">
                                    {/* BUG: UNKNOWN STRING */}{translations[lang].courses14}
                                </div>
                            </div>
                            <img className="category-percent" src="/img/Home/percent.svg" alt=""/>
                            <div className="cat-circle-3"></div>
                        </div>
                        <div className="category-bot-block">
                            <div className="category-bot-block-text">
                                <div className="category-bot-block-text-1">
                                    {translations[lang].bBanking}
                                </div>
                                <div className="category-bot-block-text-2">
                                    {/* BUG: UNKNOWN STRING */}{translations[lang].courses14}
                                </div>
                            </div>
                            <img className="category-percent" src="/img/Home/percent.svg" alt=""/>
                            <div className="cat-circle-4"></div>
                        </div>
                    </div>
                </div>
                <div className="popular">
                    <div className="popular-top">
                        <div className="popular-top-left">{translations[lang].popularCourses}</div>
                    </div>
                    <div className="popular-bot">
                        <div className="popular-bot-block">
                            <img className="popular-bot-block" src="/img/Shared/photo.png" alt=""/>
                            <div className="popular-bot-block-deco-1">
                                <div
                                    className="popular-bot-block-deco-title">{translations[lang].infestionForBeginners}</div>
                            </div>
                        </div>
                        <div className="popular-bot-block">
                            <img className="popular-bot-block" src="/img/Shared/photo.png" alt=""/>
                            <div className="popular-bot-block-deco-2">
                                <div
                                    className="popular-bot-block-deco-title">{translations[lang].infestionForBeginners}</div>
                            </div>
                        </div>
                        <div className="popular-bot-block">
                            <img className="popular-bot-block" src="/img/Shared/photo.png" alt=""/>
                            <div className="popular-bot-block-deco-3">
                                <div
                                    className="popular-bot-block-deco-title">{translations[lang].infestionForBeginners}</div>
                            </div>
                        </div>
                    </div>
                </div>
                <button className="show_more">
                    {translations[lang].showMore}
                </button>
            </div>
            <div className="mobility">
                <div className="mobility-left">
                    <div className="mobility-left-top">
                        <div className="mobility-left-top-1">{translations[lang].moreStability}</div>
                        <div className="mobility-left-top-2">{translations[lang].mobileApp}</div>
                    </div>
                    <button className="mobility-left-download">{translations[lang].download}</button>
                </div>
                <div className="mobility-right">
                    <img src="/img/Home/Mask group.png" alt=""/>
                </div>
            </div>
            <div className="partners">
                <div className="partners-top">{translations[lang].ourPartners}</div>
                <div className="partners-photo-block">
                    <img className="partners-photo" src="/img/Home/partn.png" alt=""/>
                    <img className="partners-photo" src="/img/Home/partn.png" alt=""/>
                    <img className="partners-photo" src="/img/Home/partn.png" alt=""/>
                    <img className="partners-photo" src="/img/Home/partn.png" alt=""/>
                    <img className="partners-photo" src="/img/Home/partn.png" alt=""/>
                    <img className="partners-photo" src="/img/Home/partn.png" alt=""/>
                </div>
            </div>

        </main>
    </>)
}
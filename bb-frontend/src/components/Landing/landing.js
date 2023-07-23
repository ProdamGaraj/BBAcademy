import translations from 'translations'
import LangContext from "../../contexts/lang-context";
import {useContext, useEffect, useState} from "react";

import styles from './landing.module.css'
import {getRandomLesson} from "../../services/lesson.service";

export default () => {

    let lang = useContext(LangContext).lang

    const [videoPath, setVideoPath] = useState('');
    {/*const fetchVideoPath = () => {
        getRandomLesson().then(lesson => {
            setVideoPath(lesson.mediaContentPath)
        });
    }

    useEffect(() => {
        fetchVideoPath();
    }, []);
    */}

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

                <div className={styles.advTop}>
                    {translations[lang].investYourself}
                </div>

                <div className={styles.advContentContainer}>
                    <div className={styles.advLeftContainer}>
                        <div className={styles.advLeftCard}>
                            <img className={styles.advCardLeftImage} src="/img/Home/AdvLeftImage.svg" alt=""/>
                            <span className={styles.advCardTitle}>
                                    {translations[lang].rateSystem}
                            </span>
                            <span className={styles.advCardText}>
                                {translations[lang].someDescriptionForAdv}
                            </span>
                        </div>
                    </div>
                    <div className={styles.advRightContainer}>

                        <div className={styles.advRightCardTop}>
                            <img className={styles.advCardImage} src="/img/Home/AdvRightTopImage.svg" alt=""/>
                            <span className={styles.advCardTitle}>
                                {translations[lang].aLotOfCourses}
                            </span>
                        </div>


                        <div className={styles.advRightCardMiddle}>
                            <img className={styles.advCardImage} src="/img/Home/AdvRightMiddleImage.svg" alt=""/>
                            <span className={styles.advCardTitle}>
                                {translations[lang].autoExamination}
                            </span>
                        </div>


                        <div className={styles.advRightCardBottom}>
                            <img className={styles.advCardImage} src="/img/Home/AdvRightBottomImage.svg" alt=""/>
                            <span className={styles.advCardTitle}>
                                {translations[lang].professorCommunication}
                            </span>
                        </div>

                    </div>
                </div>

                {/* Exists only for mobile version */}
                <div className={styles.advContentContainerGrid}>
                    <div className={styles.advLeftContainer}>
                        <div className={styles.advLeftCard}>
                            <img className={styles.advCardLeftImage} src="/img/Home/AdvLeftImage.svg" alt=""/>
                            <span className={styles.advCardTitle}>
                                        {translations[lang].rateSystem}
                                </span>
                            <span className={styles.advCardText}>
                                    {translations[lang].someDescriptionForAdv}
                                </span>
                        </div>
                    </div>
                    <div className={styles.advRightCardTop}>
                        <img className={styles.advCardImage} src="/img/Home/AdvRightTopImage.svg" alt=""/>
                        <span className={styles.advCardTitle}>
                                {translations[lang].aLotOfCourses}
                            </span>
                    </div>


                    <div className={styles.advRightCardMiddle}>
                        <img className={styles.advCardImage} src="/img/Home/AdvRightMiddleImage.svg" alt=""/>
                        <span className={styles.advCardTitle}>
                                {translations[lang].autoExamination}
                            </span>
                    </div>


                    <div className={styles.advRightCardBottom}>
                        <img className={styles.advCardImage} src="/img/Home/AdvRightBottomImage.svg" alt=""/>
                        <span className={styles.advCardTitle}>
                                {translations[lang].professorCommunication}
                            </span>
                    </div>

                </div>

            </div>
            {/* 
            <section className={styles.videoContainer}>
                <video width="100%" controls>
                    <source src="/videos/landing.mp4" type="video/mp4"/>
                </video>
            </section>
 */}
            {/*<div className={styles.categoryAndPopular}>
                <div className={styles.category}>
                    <div className={styles.categoryTop}>
                        <div className={styles.categoryTopLeft}>{translations[lang].categories}</div>
                        <div className={styles.categoryTopRight}>{translations[lang].showMore}</div>
                    </div>
                    <div className={styles.categoryBot}>
                        <div className={styles.categoryBotBlock}>
                            <div className={styles.categoryBotBlockText}>
                                <div className={styles.categoryBotBlockTitle}>
                                    {translations[lang].bBanking}
                                </div>
                                <div className={styles.categoryBotBlockDuration}>
                                    {translations[lang].fourteenCourses}
                                </div>
                            </div>
                            <img className={styles.categoryPercent} src="/img/Home/percent.svg" alt=""/>
                            <div className={styles.catCircleSky}></div>
                        </div>
                        <div className={styles.categoryBotBlock}>
                            <div className={styles.categoryBotBlockText}>
                                <div className={styles.categoryBotBlockTitle}>
                                    {translations[lang].bBanking}
                                </div>
                                <div className={styles.categoryBotBlockDuration}>
                                    {translations[lang].fourteenCourses}
                                </div>
                            </div>
                            <img className={styles.categoryPercent} src="/img/Home/percent.svg" alt=""/>
                            <div className={styles.catCircleBlue}></div>
                        </div>
                        <div className={styles.categoryBotBlock}>
                            <div className={styles.categoryBotBlockText}>
                                <div className={styles.categoryBotBlockTitle}>
                                    {translations[lang].bBanking}
                                </div>
                                <div className={styles.categoryBotBlockDuration}>
                                    {translations[lang].fourteenCourses}
                                </div>
                            </div>
                            <img className={styles.categoryPercent} src="/img/Home/percent.svg" alt=""/>
                            <div className={styles.catCircleMint}></div>
                        </div>
                        <div className={styles.categoryBotBlock}>
                            <div className={styles.categoryBotBlockText}>
                                <div className={styles.categoryBotBlockTitle}>
                                    {translations[lang].bBanking}
                                </div>
                                <div className={styles.categoryBotBlockDuration}>
                                    {translations[lang].fourteenCourses}
                                </div>
                            </div>
                            <img className={styles.categoryPercent} src="/img/Home/percent.svg" alt=""/>
                            <div className={styles.catCircleRed}></div>
                        </div>
                    </div>
                </div>
                <div className={styles.popular}>
                    <div className={styles.popularTop}>
                        <div className={styles.popularTopLeft}>{translations[lang].popularCourses}</div>
                    </div>
                    <div className={styles.popularBot}>
                        <div className={styles.popularBotBlock}>
                            <img className={styles.popularBotBlock} src="/img/Shared/course_guy.jpg" alt=""/>
                            <div className={styles.popularBotBlockBlue}>
                                <div
                                    className={styles.popularBotBlockTitle}>{translations[lang].infestionForBeginners}</div>
                            </div>
                        </div>
                        <div className={styles.popularBotBlock}>
                            <img className={styles.popularBotBlock} src="/img/Shared/course_guy.jpg" alt=""/>
                            <div className={styles.popularBotBlockMint}>
                                <div
                                    className={styles.popularBotBlockTitle}>{translations[lang].infestionForBeginners}</div>
                            </div>
                        </div>
                        <div className={styles.popularBotBlock}>
                            <img className={styles.popularBotBlock} src="/img/Shared/course_guy.jpg" alt=""/>
                            <div className={styles.popularBotBlockSky}>
                                <div
                                    className={styles.popularBotBlockTitle}>{translations[lang].infestionForBeginners}</div>
                            </div>
                        </div>
                    </div>
                </div>
                <button className={styles.showMore}>
                    {translations[lang].showMore}
                </button>
            </div>*/}

            <div className={styles.mobility}>
                <div className={styles.mobilityLeft}>
                    <span className={styles.mobilityTitle}>{translations[lang].moreStability}</span>
                    <span className={styles.mobilityText}>{translations[lang].mobileApp}</span>
                    {/*<button className={styles.mobilityLeftDownload}>{translations[lang].download}</button>*/}
                </div>
                <div className={styles.mobilityRight}>
                    <img src="/img/Home/Mask group.png" alt=""/>
                </div>
            </div>

            {/*
            <div className={styles.partners}>
                <div className={styles.partnersTop}>{translations[lang].ourPartners}</div>
                <div className={styles.partnersPhotoBlock}>
                    <img className={styles.partnersPhoto} src="/img/Home/partn.png" alt=""/>
                    <img className={styles.partnersPhoto} src="/img/Home/partn.png" alt=""/>
                    <img className={styles.partnersPhoto} src="/img/Home/partn.png" alt=""/>
                    <img className={styles.partnersPhoto} src="/img/Home/partn.png" alt=""/>
                    <img className={styles.partnersPhoto} src="/img/Home/partn.png" alt=""/>
                    <img className={styles.partnersPhoto} src="/img/Home/partn.png" alt=""/>
                </div>
            </div>*/}

        </main>
    </>)
}

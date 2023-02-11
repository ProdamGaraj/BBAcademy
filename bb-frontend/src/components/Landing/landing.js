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
            <div className={styles.categoryAndPopular}>
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
                            <img className={styles.popularBotBlock} src="/img/Shared/photo.png" alt=""/>
                            <div className={styles.popularBotBlockBlue}>
                                <div
                                    className={styles.popularBotBlockTitle}>{translations[lang].infestionForBeginners}</div>
                            </div>
                        </div>
                        <div className={styles.popularBotBlock}>
                            <img className={styles.popularBotBlock} src="/img/Shared/photo.png" alt=""/>
                            <div className={styles.popularBotBlockMint}>
                                <div
                                    className={styles.popularBotBlockTitle}>{translations[lang].infestionForBeginners}</div>
                            </div>
                        </div>
                        <div className={styles.popularBotBlock}>
                            <img className={styles.popularBotBlock} src="/img/Shared/photo.png" alt=""/>
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
            </div>
            <div className={styles.mobility}>
                <div className={styles.mobilityLeft}>
                        <div className={styles.mobilityTitle}>{translations[lang].moreStability}</div>
                        <div className={styles.mobilityText}>{translations[lang].mobileApp}</div>
                    <button className={styles.mobilityLeftDownload}>{translations[lang].download}</button>
                </div>
                <div className={styles.mobilityRight}>
                    <img src="/img/Home/Mask group.png" alt=""/>
                </div>
            </div>
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
            </div>

        </main>
    </>)
}
import translations from 'translations'
import LangContext from "../../contexts/lang-context";
import {useContext} from "react";
import {Loader} from "../loader/loader";

export default ({match}) => {

    let currentLang = useContext(LangContext).lang

    return (<>
        <div className="block-learn-in">
            <img className="block-learn-in_left-section" src="/img/Home/guy3.png"/>
            <div className="block-learn-in_right-section">
                <div className="block-learn-in_right-section-top">
                    <div
                        className="block-learn-in_right-section-top-top">{translations[currentLang].learnWithBBAcademy}</div>
                    <div
                        className="block-learn-in_right-section-top-bot">{translations[currentLang].improveYourProfessionalCapital}</div>
                </div>
                <div className="block-learn-in_right-section-bot"></div>
            </div>
        </div>
        <div className="adv-learn">
            <div className="adv-top">
                {translations[currentLang].investYourself}
            </div>
            <div className="adv-learn-right">
                <div className="adv-learn-right-block">
                    <div className="adv-card-book">
                        <img className="book-svg" src="/img/Home/book.svg"/>
                    </div>
                    <div className="card-text">{translations[currentLang].aLotOfCourses}</div>
                </div>
                <div className="adv-learn-right-block">
                    <div className="adv-card-book">
                        <img className="book-svg" src="/img/Home/book.svg"/>
                    </div>
                    <div className="card-text">{translations[currentLang].rateSystem}</div>
                </div>
                <div className="adv-learn-right-block">
                    <div className="adv-card-book">
                        <img className="book-svg" src="/img/Home/book.svg"/>
                    </div>
                    <div className="card-text">{translations[currentLang].autoExamination}</div>
                </div>
                <div className="adv-learn-right-block">
                    <div className="adv-card-book">
                        <img className="book-svg" src="/img/Home/book.svg"/>
                    </div>
                    <div className="card-text">{translations[currentLang].professorCommunication}</div>
                </div>
            </div>
        </div>
        <div className="video-block"></div>
        <div className="category_and_popular">
            <div className="category">
                <div className="category-top">
                    <div className="category-top-left">{translations[currentLang].categories}</div>
                    <div className="category-top-right">{translations[currentLang].showMore}</div>
                </div>
                <div className="category-bot">
                    <div className="category-bot-block">
                        <div className="category-bot-block-text">
                            <div className="category-bot-block-text-1">
                                {translations[currentLang].bBanking}
                            </div>
                            <div className="category-bot-block-text-2">
                                {translations[currentLang].courses14}
                            </div>
                        </div>
                        <img className="category-percent" src="/img/Home/percent.svg"/>
                        <div className="cat-circle-1"></div>
                    </div>
                    <div className="category-bot-block">
                        <div className="category-bot-block-text">
                            <div className="category-bot-block-text-1">
                                {translations[currentLang].bBanking}
                            </div>
                            <div className="category-bot-block-text-2">
                                {translations[currentLang].courses14}
                            </div>
                        </div>
                        <img className="category-percent" src="/img/Home/percent.svg"/>
                        <div className="cat-circle-2"></div>
                    </div>
                    <div className="category-bot-block">
                        <div className="category-bot-block-text">
                            <div className="category-bot-block-text-1">
                                {translations[currentLang].bBanking}
                            </div>
                            <div className="category-bot-block-text-2">
                                {translations[currentLang].courses14}
                            </div>
                        </div>
                        <img className="category-percent" src="/img/Home/percent.svg"/>
                        <div className="cat-circle-3"></div>
                    </div>
                    <div className="category-bot-block">
                        <div className="category-bot-block-text">
                            <div className="category-bot-block-text-1">
                                {translations[currentLang].bBanking}
                            </div>
                            <div className="category-bot-block-text-2">
                                {translations[currentLang].courses14}
                            </div>
                        </div>
                        <img className="category-percent" src="/img/Home/percent.svg"/>
                        <div className="cat-circle-4"></div>
                    </div>
                </div>
            </div>
            <div className="popular">
                <div className="popular-top">
                    <div className="popular-top-left">{translations[currentLang].popularCourses}</div>
                </div>
                <div className="popular-bot">
                    <div className="popular-bot-block">
                        <img className="popular-bot-block" src="/img/Shared/photo.png"/>
                        <div className="popular-bot-block-deco-1">
                            <div
                                className="popular-bot-block-deco-title">{translations[currentLang].infestionForBeginners}</div>
                        </div>
                    </div>
                    <div className="popular-bot-block">
                        <img className="popular-bot-block" src="/img/Shared/photo.png"/>
                        <div className="popular-bot-block-deco-2">
                            <div
                                className="popular-bot-block-deco-title">{translations[currentLang].infestionForBeginners}</div>
                        </div>
                    </div>
                    <div className="popular-bot-block">
                        <img className="popular-bot-block" src="/img/Shared/photo.png"/>
                        <div className="popular-bot-block-deco-3">
                            <div
                                className="popular-bot-block-deco-title">{translations[currentLang].infestionForBeginners}</div>
                        </div>
                    </div>
                </div>
            </div>
            <button className="show_more">
                {translations[currentLang].showMore}
            </button>
        </div>
        <div className="mobility">
            <div className="mobility-left">
                <div className="mobility-left-top">
                    <div className="mobility-left-top-1">{translations[currentLang].moreStability}</div>
                    <div className="mobility-left-top-2">{translations[currentLang].mobileApp}</div>
                </div>
                <button className="mobility-left-download">{translations[currentLang].download}</button>
            </div>
            <div className="mobility-right">
                <img src="/img/Home/Mask group.png"/>
            </div>
        </div>
        <div className="partners">
            <div className="partners-top">{translations[currentLang].ourPartners}</div>
            <div className="partners-photo-block">
                <img className="partners-photo" src="/img/Home/partn.png"/>
                <img className="partners-photo" src="/img/Home/partn.png"/>
                <img className="partners-photo" src="/img/Home/partn.png"/>
                <img className="partners-photo" src="/img/Home/partn.png"/>
                <img className="partners-photo" src="/img/Home/partn.png"/>
                <img className="partners-photo" src="/img/Home/partn.png"/>
            </div>
        </div>
    </>)
}

import translations from 'translations'
import LangContext from "../../contexts/lang-context";
import {useContext} from "react";

export default ({match}) => {

    let currentLang = useContext(LangContext).lang
    
    return (<>
            <div className="block-learn-in">
                <img className="block-learn-in_left-section" src="/img/Home/guy3.png"/>
                <div className="block-learn-in_right-section">
                    <div className="block-learn-in_right-section-top">
                        <div className="block-learn-in_right-section-top-top">Учись <br/> в BilimBank</div>
                        <div className="block-learn-in_right-section-top-bot">Увеличивай свой профессиональный капитал</div>
                    </div>
                    <div className="block-learn-in_right-section-bot"></div>
                </div>
            </div>
            <div className="adv-learn">
                <div className="adv-top">
                    Инвестируй в себя
                </div>
                <div className="adv-learn-right">
                    <div className="adv-learn-right-block">
                        <div className="adv-card-book">
                            <img className="book-svg" src="/img/Home/book.svg"/>
                        </div>
                        <div className="card-text">Широкий выбор <br/>курсов</div>
                    </div>
                    <div className="adv-learn-right-block">
                        <div className="adv-card-book">
                            <img className="book-svg" src="/img/Home/book.svg"/>
                        </div>
                        <div className="card-text">Система <br/>рейтинга</div>
                    </div>
                    <div className="adv-learn-right-block">
                        <div className="adv-card-book">
                            <img className="book-svg" src="/img/Home/book.svg"/>
                        </div>
                        <div className="card-text">Автоматическая проверка работы</div>
                    </div>
                    <div className="adv-learn-right-block">
                        <div className="adv-card-book">
                            <img className="book-svg" src="/img/Home/book.svg"/>
                        </div>
                        <div className="card-text">Комуникация <br/>с преподавателем</div>
                    </div>
                </div>
            </div>
            <div className="video-block"></div>
            <div className="category_and_popular">
                <div className="category">
                    <div className="category-top">
                        <div className="category-top-left">Категории</div>
                        <div className="category-top-right">Показать больше</div>
                    </div>
                    <div className="category-bot">
                        <div className="category-bot-block">
                            <div className="category-bot-block-text">
                                <div className="category-bot-block-text-1">
                                    Банкинг
                                </div>
                                <div className="category-bot-block-text-2">
                                    14 курсов
                                </div>
                            </div>
                            <img className="category-percent" src="/img/Home/percent.svg"/>
                            <div className="cat-circle-1"></div>
                        </div>
                        <div className="category-bot-block">
                            <div className="category-bot-block-text">
                                <div className="category-bot-block-text-1">
                                    Банкинг
                                </div>
                                <div className="category-bot-block-text-2">
                                    14 курсов
                                </div>
                            </div>
                            <img className="category-percent" src="/img/Home/percent.svg"/>
                            <div className="cat-circle-2"></div>
                        </div>
                        <div className="category-bot-block">
                            <div className="category-bot-block-text">
                                <div className="category-bot-block-text-1">
                                    Банкинг
                                </div>
                                <div className="category-bot-block-text-2">
                                    14 курсов
                                </div>
                            </div>
                            <img className="category-percent" src="/img/Home/percent.svg"/>
                            <div className="cat-circle-3"></div>
                        </div>
                        <div className="category-bot-block">
                            <div className="category-bot-block-text">
                                <div className="category-bot-block-text-1">
                                    Банкинг
                                </div>
                                <div className="category-bot-block-text-2">
                                    14 курсов
                                </div>
                            </div>
                            <img className="category-percent" src="/img/Home/percent.svg"/>
                            <div className="cat-circle-4"></div>
                        </div>
                    </div>
                </div>
                <div className="popular">
                    <div className="popular-top">
                        <div className="popular-top-left">Популярные курсы</div>
                    </div>
                    <div className="popular-bot">
                        <div className="popular-bot-block">
                            <img className="popular-bot-block" src="/img/Shared/photo.png"/>
                            <div className="popular-bot-block-deco-1">
                                <div className="popular-bot-block-deco-title">Курс: инвестиции<br/> для начинающих</div>
                            </div>
                        </div>
                        <div className="popular-bot-block">
                            <img className="popular-bot-block" src="/img/Shared/photo.png"/>
                            <div className="popular-bot-block-deco-2">
                                <div className="popular-bot-block-deco-title">Курс: инвестиции<br/> для начинающих</div>
                            </div>
                        </div>
                        <div className="popular-bot-block">
                            <img className="popular-bot-block" src="/img/Shared/photo.png"/>
                            <div className="popular-bot-block-deco-3">
                                <div className="popular-bot-block-deco-title">Курс: инвестиции<br/> для начинающих</div>
                            </div>
                        </div>
                    </div>
                </div>
                <button className="show_more">
                    Показать больше
                </button>
            </div>
            <div className="mobility">
                <div className="mobility-left">
                    <div className="mobility-left-top">
                        <div className="mobility-left-top-1">Ещё больше стабильности, ещё больше комфорта</div>
                        <div className="mobility-left-top-2">Наше мобильное приложение скоро в гугл плэй и эппл стор</div>
                    </div>
                    <button className="mobility-left-download">Скачать</button>
                </div>
                <div className="mobility-right">
                    <img src="/img/Home/Mask group.png"/>
                </div>
            </div>
            <div className="partners">
                <div className="partners-top">Наши партнёры</div>
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
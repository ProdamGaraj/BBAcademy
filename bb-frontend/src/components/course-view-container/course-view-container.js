import './course-view-container.css'

export default ({children}) => {

    return (<>
        <div className="main-container flex-to-center">
            <div className="main-container-cont">
                <div className="main-container-list">
                    <div className="main-container-list-p-flex">
                        <p className="main-container-list-p">Введение в банковскую деятельность</p>
                    </div>

                    <ul className="main-ul">
                        <li>Введение</li>
                        <li>Ссылки и материалы</li>
                        <li>Блок 1</li>
                        <li className="not-main-li">Урок 1</li>
                        <li>Урок 2</li>
                        <li>Урок 3</li>
                        <li>Тест</li>
                        <li>Заключение и сертификат</li>
                    </ul>
                </div>
                <div className="Demarcation-line"></div>
                <div className="container">
                    <main role="main" className="pb-3">
                        {children}
                    </main>
                </div>
            </div>
        </div>
    </>)
}
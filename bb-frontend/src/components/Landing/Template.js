export default () => {

    let changeLang = (lang) => {

    }

    let goToLogin = () => {
        window.location.href = '/Account/Login'
    }

    return (<>
        <header>
            <div className="header-container">
                <div className="header-container-left_section">
                    <img className="svg-logo" src="/img/Shared/logo-icon-2.svg" alt="logo"/>
                    <div className="header-container-left_section-text">BilimBank</div>
                </div>
                <div className="header-container-right_section">
                    <div className="header-container-right_section-lang">
                        <div className="header-container-right_section-lang-element"><a className="a-lang"
                                                                                        onClick={changeLang('uz')}>uz</a>
                        </div>
                        <div className="header-container-right_section-lang-element"><a className="a-lang"
                                                                                        onClick={changeLang('ru')}>ru</a>
                        </div>
                    </div>
                    <a className="header-container-right_section-login" onClick={goToLogin()}>@enter</a>
                </div>
            </div>
        </header>

        <main role="main" className="pb-3">
            @RenderBody()
        </main>


        <script src="~/lib/jquery/dist/jquery.min.js"></script>
        <script src="~/lib/bootstrap/dist/js/bootstrap.bundle.min.js"></script>
        <script src="~/js/site.js" asp-append-version="true"></script>

        @await RenderSectionAsync("Scripts", required: false)
        <footer>
            <div className="footer-cont">
                <div className="footer-cont-1">
                    <img className="footer-logo" src="~/pict/Shared/logo-icon 1.svg">
                        <div className="footer-cont-1-text">BilimBank</div>
                </div>
                <div className="footer-table">
                    <div className="footer-table-column-1">
                        <div className="footer-table-column-1-1">
                            <div className="column-element">@about</div>
                            <div className="column-element">@vacancies</div>
                            <div className="column-element">@company</div>

                        </div>
                        <div className="footer-table-column-1-2">
                            <div className="column-element">@learningproc</div>
                            <div className="column-element">@usagepol</div>
                            <div className="column-element">@privpol</div>
                            <div className="column-element">@companyinfo</div>
                        </div>
                    </div>
                    <div className="footer-table-column-2">
                        <div className="column-element">@online</div>
                        <div className="column-element">@prog</div>
                        <div className="column-element">@webs</div>
                        <div className="column-element">@fests</div>
                        <div className="column-element">@carrier</div>
                    </div>
                    <div className="footer-table-column-3">
                        <div className="column-element">
                            <img className="footer-icon" src="~/pict/Shared/foot_teleg.png">
                                <a className="column-href" href="#">@bilimbank</a>
                        </div>
                        <div className="column-element">
                            <img className="footer-icon" src="~/pict/Shared/foot_inst.png">
                                <a className="column-href" href="#">@bilimbank</a>
                        </div>
                    </div>
                </div>
            </div>
            <div className="all-rights">
                <div className="all-rights-1">
                    @rights
                </div>
            </div>
        </footer>
    </>
)
}
using Backend.Models;
using Backend.Services;
using Backend.Services.AccountService;
using Backend.Services.AccountService.Interfaces;
using Backend.Services.Repository;
using Backend.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    public class CertificateController : Controller
    {

        private readonly IAccountService accountService;

        [HttpGet]
        public async Task<IActionResult> Index(CertificateViewModel certificateViewModel)
        {
            certificateViewModel.User = (await accountService.GetUserByLogin(HttpContext.User.Identity.Name)).Data;
            var responce = await new CertificateService().GetCertificates(new CertificateViewModel() { User = certificateViewModel.User });
            if (responce.StatusCode == Models.Enum.StatusCode.OK)
            {
                certificateViewModel.Certificates= responce.Data;
                return View(certificateViewModel);
            }
            return View(certificateViewModel);
        }

    }
}
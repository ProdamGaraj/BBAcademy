using Backend.Models;
using Backend.Services;
using Backend.Services.AccountService;
using Backend.Services.AccountService.Interfaces;
using Backend.Services.Interfaces;
using Backend.Services.Repository;
using Backend.Services.Repository.Interfaces;
using Backend.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.FileIO;
using System.Reflection;

namespace Backend.Controllers
{
    [Route("Certificate")]
    public class CertificateController : Controller
    {
        private IUserRepository ur;
        private readonly IAccountService accountService;
        private ICertificateService cr;
        public CertificateController(IUserRepository ur, IAccountService accountService, ICertificateService cr)
        {
            this.ur = ur;
            this.accountService = accountService;
            this.cr = cr;
        }

        [HttpGet]
        public async Task<IActionResult> Index(CertificateViewModel certificateViewModel)
        {
            certificateViewModel.User = (await accountService.GetUserByLogin(HttpContext.User.Identity.Name)).Data;
            if (HttpContext.Session.TryGetValue("language", out byte[] value))
                certificateViewModel.User.Lang = HttpContext.Session.GetInt32("language");
            if (certificateViewModel.User.Lang is null)
            {
                certificateViewModel.User.Lang = 1;
                HttpContext.Session.SetInt32("language", 1);
            }
            else
            {
                HttpContext.Session.SetInt32("language", (int)certificateViewModel.User.Lang);
            }

            await ur.Update(certificateViewModel.User);
            var responce = await cr.GetCertificates(new CertificateViewModel() { User = certificateViewModel.User });
            if (responce.StatusCode == Models.Enum.StatusCode.OK)
            {
                certificateViewModel.Certificates = responce.Data;
                return View(certificateViewModel);
            }
            return View(certificateViewModel);
        }
        [HttpGet("/DownloadCertificate/{id}")]
        public async Task<IActionResult> DownloadCertificate(CertificateViewModel certificateViewModel,long id)
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Files\\Certificates\\"+id+".pdf";
            return File(System.IO.File.OpenRead(path), "application/octet-stream", Path.GetFileName(path));
        }

        [HttpGet("/ChangeLang/{id}")]
        public async Task<IActionResult> ChangeLang(int id)
        {
            User user = (await accountService.GetUserByLogin(HttpContext.User.Identity.Name)).Data;
            user.Lang = id;
            await ur.Update(user);
            return RedirectToAction("Index");
        }
    }
}
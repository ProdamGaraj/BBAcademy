using System.Reflection;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Controller]
    [Route("[controller]/[action]")]
    public class CertificateController : Controller
    {
        private readonly ICertificateService _certificateService;

        public CertificateController(ICertificateService certificateService)
        {
            _certificateService = certificateService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // certificateViewModel.User = (await accountService.GetUserByLogin(HttpContext.User.Identity.Name)).Data;
            // if (HttpContext.Session.TryGetValue("language", out _))
            //     certificateViewModel.User.Lang = HttpContext.Session.GetInt32("language");
            // if (certificateViewModel.User.Lang is null)
            // {
            //     certificateViewModel.User.Lang = 1;
            //     HttpContext.Session.SetInt32("language", 1);
            // }
            // else
            // {
            //     HttpContext.Session.SetInt32("language", (int)certificateViewModel.User.Lang);
            // }

            var certificates = await _certificateService.GetCertificatesByUser(HttpContext.User.GetId());

            return View(certificates);
        }

        [HttpGet("/DownloadCertificate/{id}")]
        public async Task<IActionResult> DownloadCertificate(long id)
        {
            string path = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\Files\\Certificates\\{id}.pdf";
            return File(path, "application/octet-stream", Path.GetFileName(path));
        }
    }
}
using Backend.Models;
using Backend.Services;
using Backend.Services.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    public class CertificateController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }

    }
}

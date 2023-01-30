using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    public class LKController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
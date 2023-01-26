using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
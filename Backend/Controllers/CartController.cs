using Backend.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("Cart")]
    public class CartController : Controller
    {
        public IActionResult Index(CartViewModel cvm)
        {
            return View(cvm);
        }
        [HttpGet("/Payment")]
        public IActionResult Payment(CartViewModel cvm)
        {
            return View(cvm);
        }
    }
}
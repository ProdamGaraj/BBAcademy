using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{

	public class HomeController : Controller
	{
		[HttpGet]
		public IActionResult Index() => View();
	}

}
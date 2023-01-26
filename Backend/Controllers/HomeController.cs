using Backend.Models;
using Backend.Services.AccountService;
using Backend.Services.AccountService.Interfaces;
using Backend.Services.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Backend.Controllers
{

    public class HomeController : Controller
    {
        private readonly IAccountService accountService;
        public HomeController(IAccountService _accountService)
        {
            accountService = _accountService;
        }
		public IActionResult Index() 
		{

            return View();
		}
        [HttpGet("Home/ChangeLang/{id}")]
        public async Task<IActionResult> ChangeLang(int id)
		{
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                User user = (await accountService.GetUserByLogin(HttpContext.User.Identity.Name)).Data;
                user.Lang = id;
                UserRepository ur = new UserRepository();
                await ur.Update(user);
            }
            HttpContext.Session.SetString("language", "ru");//Setting language for entire session 
            return RedirectToAction("");
		}
	}

}
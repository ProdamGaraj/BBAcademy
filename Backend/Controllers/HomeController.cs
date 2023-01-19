using Backend.Models;
using Backend.Services.AccountService;
using Backend.Services.AccountService.Interfaces;
using Backend.Services.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{

    [Route("Home")]
    public class HomeController : Controller
    {
        private readonly IAccountService accountService;
        public HomeController(IAccountService _accountService)
        {
            accountService = _accountService;
        }
        [HttpGet]
		public IActionResult Index() 
		{  
			return View();
		}
        [HttpGet("/ChangeLang/{id}")]
        public async Task<IActionResult> ChangeLang(int id)
		{
            User user = (await accountService.GetUserByLogin(HttpContext.User.Identity.Name)).Data;
            user.Lang = id;
            UserRepository ur = new UserRepository();
            await ur.Update(user);
            return RedirectToAction("Index");
		}
	}

}
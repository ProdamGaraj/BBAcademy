using Backend.Models;
using Backend.Services.AccountService;
using Backend.Services.AccountService.Interfaces;
using Backend.Services.Repository;
using Backend.ViewModels;
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
		public IActionResult Index(HomeViewModel hvm) 
		{
            hvm.lang = HttpContext.Session.GetInt32("language");
            if(hvm.lang is null)
            {
                hvm.lang = 1;
                HttpContext.Session.SetInt32("language", 1); 
            }
            return View(hvm);
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
            HttpContext.Session.SetInt32("language", id);//Setting language for entire session 
            return RedirectToAction("");
		}
	}

}
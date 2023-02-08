using System;
using System.Threading.Tasks;
using Backend.Models;
using Backend.Services.AccountService.Interfaces;
using Backend.Services.Repository.Interfaces;
using Backend.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{

    public class HomeController : Controller
    {
        private IUserRepository ur;

        private readonly IAccountService accountService;
        public HomeController(IUserRepository _ur, IAccountService _accountService)
        {
            this.ur = _ur;
            this.accountService = _accountService;
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
                try
                {
                    User user = (await accountService.GetUserByLogin(HttpContext.User.Identity.Name)).Data;
                    user.Lang = id;
                    await ur.Update(user);
                }
                catch (Exception)
                {
                    Redirect("Account/Register");
                }
                
            }
            HttpContext.Session.SetInt32("language", id);//Setting language for entire session 
            return RedirectToAction("");
		}
	}

}
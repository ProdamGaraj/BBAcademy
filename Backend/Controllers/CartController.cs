﻿using Backend.Services;
using Backend.Services.AccountService;
using Backend.Services.AccountService.Interfaces;
using Backend.Services.Interfaces;
using Backend.Services.Repository;
using Backend.Services.Repository.Interfaces;
using Backend.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("Cart")]
    public class CartController : Controller
    {
        private readonly IAccountService accountService;
        private IUserRepository ur;
        private ICartService cs;


        public CartController(IAccountService accountService, IUserRepository ur, ICartService cs)
        {
            this.accountService = accountService;
            this.ur = ur;
            this.cs = cs;
        }

        public async Task<IActionResult> IndexAsync(CartViewModel cvm)
        {
            cvm.User = (await accountService.GetUserByLogin(HttpContext.User.Identity.Name)).Data;
            if (HttpContext.Session.TryGetValue("language", out byte[] value))
                cvm.User.Lang = HttpContext.Session.GetInt32("language");
            if (cvm.User.Lang is null)
            {
                cvm.User.Lang = 1;
                HttpContext.Session.SetInt32("language", 1);
            }
            else
            {
                HttpContext.Session.SetInt32("language", (int)cvm.User.Lang);
            }

            await ur.Update(cvm.User);

            cvm.Courses =(await cs.GetInCartCourses(cvm.User)).Data;
            return View(cvm);
        }
        [HttpGet("/Payment")]
        public async Task<IActionResult> PaymentAsync(CartViewModel cvm)
        {
            cvm.User = (await accountService.GetUserByLogin(HttpContext.User.Identity.Name)).Data;
            if (HttpContext.Session.TryGetValue("language", out byte[] value))
                cvm.User.Lang = HttpContext.Session.GetInt32("language");
            if (cvm.User.Lang is null)
            {
                cvm.User.Lang = 1;
                HttpContext.Session.SetInt32("language", 1);
            }
            else
            {
                HttpContext.Session.SetInt32("language", (int)cvm.User.Lang);
            }
            await ur.Update(cvm.User);
            cvm.Courses = (await cs.GetInCartCourses(cvm.User)).Data;
            return View(cvm);
        }
    }
}
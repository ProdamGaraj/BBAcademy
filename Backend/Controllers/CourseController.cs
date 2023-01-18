﻿using Backend.Models;
using Backend.Services;
using Backend.Services.AccountService;
using Backend.Services.AccountService.Interfaces;
using Backend.Services.Repository;
using Backend.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    public class CourseController : Controller
    {

        private readonly IAccountService accountService;

        public CourseController(IAccountService _accountService)
        {
            accountService = _accountService;
        }
        [HttpGet]
		public async Task<IActionResult> Index(CourseViewModel vm)
        {
            vm.User = (await accountService.GetUserByLogin(HttpContext.User.Identity.Name)).Data;
            if(TempData["idCourse"] is not null)
                vm.IdCourse = long.Parse(TempData["idCourse"].ToString());
            if(TempData["currentLesson"] is not null)
                vm.CurrentLesson = int.Parse(TempData["currentLesson"].ToString());

            CourseService cs = new CourseService();
            vm = (await cs.GetCourse(vm)).Data;
            return View(vm);
        }

        [HttpGet]
        public IActionResult Buy() => View();
        [HttpPost]
        public async Task<IActionResult> Buy(CourseViewModel vm)
        {
            if (ModelState.IsValid)
            {
                CourseService cs = new CourseService();//TODO:Payment
                vm = (await cs.BuyCourse(vm)).Data;
            }
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> NextLesson(CourseViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if(vm.CurrentLesson + 1 != vm.AllLessons.Count)
                    vm.CurrentLesson = vm.CurrentLesson + 1;
            }
            return RedirectToAction("Index", vm);
        }
        [HttpPost]
        public async Task<IActionResult> PrevLesson(CourseViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.CurrentLesson != 0)
                {
                    vm.CurrentLesson = vm.CurrentLesson - 1;
                    TempData["currentLesson"] = vm.CurrentLesson;
                }
            }
            return RedirectToAction("Index");
        }
    }
}
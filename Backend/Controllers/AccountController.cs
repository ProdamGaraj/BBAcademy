using Backend.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Backend.Services.AccountService.Interfaces;
using System.Security.Claims;
using Backend.Services;
using Backend.Services.AccountService;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Backend.Models;
using Backend.Services.Repository;

namespace Backend.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService _accountService)
        {
            accountService = _accountService;
        }
        [HttpGet]
        public async Task<IActionResult> Index(AccountViewModel accountViewModel)
        {
            if (TempData["filter"] is not null)
            {
                accountViewModel.filter = TempData["filter"].ToString();
            }
            else
            {
                accountViewModel.filter = "AllCourses";
            }
            accountViewModel.User = (await accountService.GetUserByLogin(HttpContext.User.Identity.Name)).Data;
            if(HttpContext.Session.TryGetValue("language", out byte[] value))
                accountViewModel.User.Lang = HttpContext.Session.GetInt32("language");
            if (accountViewModel.User.Lang is null)
            {
                accountViewModel.User.Lang = 1;
                HttpContext.Session.SetInt32("language", 1);
            }
            else
            {
                HttpContext.Session.SetInt32("language", (int)accountViewModel.User.Lang);
            }
            UserRepository ur = new UserRepository();
            await ur.Update(accountViewModel.User);
            var responce = (await new CourseService().GetCourses(new CourseViewModel() { User = accountViewModel.User }));
            TempData["currentLesson"] = 0;
            if (responce.StatusCode == Models.Enum.StatusCode.OK)
            {
                accountViewModel.AllCourses = responce.Data.AllCourses;
                accountViewModel.BoughtCourses = responce.Data.BoughtCourses;
                accountViewModel.EndedCourses = responce.Data.EndedCourses;
                accountViewModel.InKartCourses = responce.Data.InKartCourses;
                return View(accountViewModel);
            }
            //accountViewModel.AllCourses.Add(new Models.Course());
            return View(accountViewModel);
        }

        [HttpGet]
        public IActionResult Register() => View();
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var responce = await accountService.Register(vm);
                if (responce.StatusCode == Models.Enum.StatusCode.OK)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(responce.Data));
                    return RedirectToAction("Index", "Account");
                }
                ModelState.AddModelError("", responce.Description);
            }
            return View(vm);
        }
        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var responce = await accountService.Login(vm);
                if (responce.StatusCode == Models.Enum.StatusCode.OK)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(responce.Data));
                    return RedirectToAction("Index", "Account");
                }
                ModelState.AddModelError("", responce.Description);
            }
            return View(vm);
        }
        [HttpGet("DeleteFromCart/{id}")]
        public async Task<IActionResult> DeleteFromCart(string id)
        {
            var cvm = new CourseViewModel();
            cvm.User = (await accountService.GetUserByLogin(HttpContext.User.Identity.Name)).Data;
            cvm.IdCourse = long.Parse(id);
            TempData["idCourse"] = cvm.IdCourse.ToString();
            CourseService cs = new CourseService();
            await cs.RemoveCartCourse(cvm);
            return RedirectToAction("");
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }



        [HttpGet]
        public async Task<IActionResult> ChangeFilterToAll(AccountViewModel accountViewModel)
        {
            accountViewModel.filter = "AllCourses";
            TempData["filter"] = accountViewModel.filter;
            return Redirect("/Account");
        }

        [HttpGet]
        public async Task<IActionResult> ChangeFilterToBought(AccountViewModel accountViewModel)
        {
            accountViewModel.filter = "BoughtCourses";
            TempData["filter"] = accountViewModel.filter;
            return Redirect("/Account");
        }

        [HttpGet]
        public async Task<IActionResult> ChangeFilterToPassed(AccountViewModel accountViewModel)
        {
            accountViewModel.filter = "PassedCourses";
            TempData["filter"] = accountViewModel.filter;
            return Redirect("/Account");
        }

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
        [HttpGet("NotFound")]
        public async Task<IActionResult> NotFound() => View();
    }
}

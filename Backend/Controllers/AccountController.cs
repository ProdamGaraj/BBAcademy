using Backend.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Backend.Services.AccountService.Interfaces;
using System.Security.Claims;
using Backend.Services;
using Backend.Services.AccountService;
using Microsoft.AspNetCore.Cors.Infrastructure;

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
            accountViewModel.User = (await accountService.GetUserByLogin(HttpContext.User.Identity.Name)).Data;
            var responce = (await new CourseService().GetCourses(new CourseViewModel() { User = accountViewModel.User }));
            if (responce.StatusCode == Models.Enum.StatusCode.OK)
            {
                accountViewModel.AllCourses = responce.Data.AllCourses;
                accountViewModel.BoughtCourses = responce.Data.BoughtCourses;
                accountViewModel.EndedCourses = responce.Data.EndedCourses;
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
        public IActionResult Login() => View();
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


    }
}

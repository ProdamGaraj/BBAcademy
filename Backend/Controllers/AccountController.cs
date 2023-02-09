using Backend.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using AutoMapper;
using BLL.AccountService;
using BLL.Models;
using BLL.Services.Interfaces;

namespace Backend.Controllers
{
    [Controller]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly ICourseService _courseService;
        private readonly IMapper _mapper;

        public AccountController(ICourseService courseService, IAccountService accountService, IMapper mapper)
        {
            _courseService = courseService;
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var courses = await _courseService.GetCourses(HttpContext.User.GetId());

            return View(courses);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var registerDto = _mapper.Map<RegisterDto>(viewModel);

            var claimsIdentity = await _accountService.Register(registerDto);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity)
            );

            return RedirectToAction("Index", "Account");
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity?.IsAuthenticated is true)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var loginDto = _mapper.Map<LoginDto>(viewModel);
                
                var claimsIdentity = await _accountService.Login(loginDto);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity)
                );
                return RedirectToAction("Index", "Account");
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        // [HttpGet("/ChangeLang/{langId}")]
        // public async Task<IActionResult> ChangeLang(int langId, string returnUrl)
        // {
        //     if (HttpContext.User.Identity?.IsAuthenticated is true)
        //     {
        //         await _accountService.SetUserLang(HttpContext.User.GetId(), langId);
        //         
        //         HttpContext.Session.SetInt32("language", langId);
        //     }
        //
        //     return Redirect(returnUrl ?? "/");
        // }

        [HttpGet("NotFound")]
        public async Task<IActionResult> NotFoundErrorPage()
        {
            return View("NotFound");
        }
    }
}
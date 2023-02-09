using System.Security.Claims;
using BLL.AccountService;
using BLL.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Controller]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var claimsIdentity = await _accountService.Register(dto);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity)
            );

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var claimsIdentity = await _accountService.Login(dto);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity)
            );

            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            long id = HttpContext.User.GetId();
            var user = await _accountService.GetUserShortById(id);
            return Ok(user);
        }
        [HttpPost]
        public async Task<IActionResult> User([FromBody] LoginDto dto)
        {
            var claimsIdentity = await _accountService.Login(dto);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity)
            );

            return Ok();
        }
    }
}
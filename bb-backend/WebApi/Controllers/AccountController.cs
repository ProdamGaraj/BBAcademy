using System.Security.Claims;
using BLL.AccountService;
using BLL.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Auth;

namespace WebApi.Controllers
{
    [Controller]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly AuthoriserService _authoriserService;

        public AccountController(IAccountService accountService, AuthoriserService authoriserService)
        {
            _accountService = accountService;
            _authoriserService = authoriserService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var result = await _accountService.Register(dto);

            var token = await _authoriserService.GenerateToken(result.UserId, result.Username);

            return Ok(token);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var result = await _accountService.Login(dto);

            var token = await _authoriserService.GenerateToken(result.UserId, result.Username);

            return Ok(token);
        }
    }
}
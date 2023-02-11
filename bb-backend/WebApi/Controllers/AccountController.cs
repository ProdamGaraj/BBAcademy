using System.Security.Claims;
using BLL.AccountService;
using BLL.Models.Login;
using BLL.Models.Register;
using Microsoft.AspNetCore.Mvc;
using WebApi.Auth;

namespace WebApi.Controllers
{
    [Controller]
    [Route("[controller]/[action]")]
    [ResponseCache(NoStore = true)]
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

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            long id = HttpContext.User.GetId();
            var user = await _accountService.GetUserShortById(id);
            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> Tester()
        {
            return Ok(string.Join(", ", HttpContext.User.Claims.Select(c => $"{c.ValueType}. {c.Type} = {c.Value}")));
        }
    }
}
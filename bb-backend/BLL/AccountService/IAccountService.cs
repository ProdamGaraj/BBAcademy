using System.Security.Claims;
using BLL.Models;
using Infrastructure.Models;

namespace BLL.AccountService
{
    public interface IAccountService
    {
        Task<ClaimsIdentity> Register(RegisterDto dto);

        Task<ClaimsIdentity> Login(LoginDto dto);

        // Task<bool> ChangePassword(ChangePasswordViewModel vm);

        Task<User> GetUserByLogin(string login);
    }
}

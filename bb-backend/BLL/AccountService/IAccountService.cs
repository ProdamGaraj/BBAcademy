using System.Security.Claims;
using BLL.Models;
using Infrastructure.Models;

namespace BLL.AccountService
{
    public interface IAccountService
    {
        Task<(long UserId, string Username)> Register(RegisterDto dto);

        Task<(long UserId, string Username)> Login(LoginDto dto);

        // Task<bool> ChangePassword(ChangePasswordViewModel vm);

        Task<User> GetUserByLogin(string login);
    }
}

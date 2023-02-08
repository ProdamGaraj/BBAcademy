using Backend.Models;
using Backend.Models.Responce;
using Backend.ViewModels;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Backend.Services.AccountService.Interfaces
{
    public interface IAccountService
    {
        Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel vm);

        Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel vm);

        Task<BaseResponse<bool>> ChangePassword(ChangePasswordViewModel vm);

        Task<BaseResponse<User>> GetUserByLogin(string login);
    }
}

using Backend.Models.Responce;
using Backend.ViewModels;
using System.Security.Claims;

namespace Backend.Services.AccountService.Interfaces
{
    public interface IAccountService
    {
        Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel vm);

        Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel vm);

        Task<BaseResponse<bool>> ChangePassword(ChangePasswordViewModel vm);
    }
}

using Backend.Models.Responce;
using Backend.ViewModels;
using System.Security.Claims;

namespace Backend.Services.AccountService.Interfaces
{
    public interface IAccountService
    {
        async Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel vm);

        async Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel vm);

        async Task<BaseResponse<bool>> ChangePassword(ChangePasswordViewModel vm);
    }
}

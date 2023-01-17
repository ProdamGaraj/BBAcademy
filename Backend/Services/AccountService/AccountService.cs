using Backend.Models;
using Backend.Models.Enum;
using Backend.Models.Responce;
using Backend.Services.AccountService.Interfaces;
using Backend.Services.Helpers;
using Backend.Services.Repository;
using Backend.Services.Repository.Interfaces;
using Backend.ViewModels;
using System.Security.Claims;

namespace Backend.Services.AccountService
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository userRepository;
        public AccountService(IUserRepository _userRepository)
        {
            userRepository = _userRepository;
        }

        public async Task<BaseResponse<User>> GetUserByLogin(string login)
        {
            var users = await userRepository.GetAll();
            var user = users.FirstOrDefault(x => x.Login == login);
            if (user is null)
            {
                return new BaseResponse<User>()
                {
                    Description = "User not found",
                    StatusCode = StatusCode.InternalServerError
                };
            }
            return new BaseResponse<User>() {
                Data = user,
                Description = "User has been found",
                StatusCode = StatusCode.OK
            };
        }

        public async Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel vm)
        {
            try
            {
                var users = await userRepository.GetAll();
                var user = users.FirstOrDefault(x => x.Login == vm.Login);
                if (user != null)
                {
                    return new BaseResponse<ClaimsIdentity>() { Description = "This login exist already" };
                }
                user = new User
                {
                    Name= vm.Name,
                    LastName= vm.LastName,
                    SurName=vm.Surname,
                    Login = vm.Login,
                    Role = UserRole.User,
                    PasswordHash = PasswordHasher.GetPasswordHash(vm.Password)
                };
                await userRepository.Add(user);
                var result = Authenticate(user);
                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    Description = "New User added",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }


        public async Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel vm)
        {
            try
            {
                var users = await userRepository.GetAll();
                var user = users.FirstOrDefault(x => x.Login == vm.Login);
                if (user == null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "User not found"
                    };
                }
                if (user.PasswordHash != PasswordHasher.GetPasswordHash(vm.Password))
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Wrong password"
                    };
                }
                var result = Authenticate(user);
                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    StatusCode = StatusCode.OK
                };

            }
            catch (Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }


        public async Task<BaseResponse<bool>> ChangePassword(ChangePasswordViewModel vm)
        {
            throw new NotImplementedException();
        }

        private ClaimsIdentity Authenticate(User user)
        {
            var claims = new List<Claim>{
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
            };
            return new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }
    }
}

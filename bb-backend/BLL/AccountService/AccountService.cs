using System.Security.Claims;
using AutoMapper;
using BLL.Helpers;
using BLL.Models;
using BLL.Models.GetCourseForView;
using BLL.Models.GetUserForAccount;
using Infrastructure.Common;
using Infrastructure.Models;
using Infrastructure.Models.Enum;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BLL.AccountService
{
    public class AccountService : IAccountService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;
        public AccountService(IRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<User> GetUserByLogin(string login)
        {
            return await _userRepository
                .GetAll()
                .FirstOrDefaultAsync(x => x.Login == login);
        }
        public async Task<GetUserShortForAccountDto> GetUserShortById(long id)
        {
            var user = await _userRepository
                .GetAll()
                .FirstOrDefaultAsync(x => x.Id == id);

            var resultDto = _mapper.Map<GetUserShortForAccountDto>(user);
            resultDto.RecommendedBy = "Kogda MVP?";
            resultDto.Rating = 404;
            return resultDto;
        }

        public async Task<ClaimsIdentity> Register(RegisterDto dto)
        {
            try
            {
                var user = await _userRepository
                    .GetAll()
                    .FirstOrDefaultAsync(x => x.Login == dto.Login);

                if (user is not null)
                {
                    // TODO: LOG
                    throw new BusinessException("login already exists");
                }

                user = new User
                {
                    FirstName = dto.Name,
                    MiddleName = dto.LastName,
                    Surname = dto.Surname,
                    Login = dto.Login,
                    Role = UserRole.User,
                    PasswordHash = PasswordHasher.GetPasswordHash(dto.Password)
                };
                _userRepository.Add(user);
                await _userRepository.SaveChangesAsync();
                return CreateClaimsIdentity(user);
            }
            catch (Exception ex)
            {
                // TODO: LOG
                throw new BusinessException("Failed Register", ex);
            }
        }


        public async Task<ClaimsIdentity> Login(LoginDto dto)
        {
            try
            {
                var user = await _userRepository
                    .GetAll()
                    .FirstOrDefaultAsync(x => x.Login == dto.Login);
                if (user is null)
                {
                    // TODO: LOG
                    throw new BusinessException("User not found");
                }

                if (user.PasswordHash != PasswordHasher.GetPasswordHash(dto.Password))
                {
                    // TODO: LOG
                    throw new BusinessException("Wrong password");
                }

                return CreateClaimsIdentity(user);
            }
            catch (Exception ex)
            {
                // TODO: LOG
                throw new BusinessException("Failed Login", ex);
            }
        }

        // public async Task<bool> ChangePassword(ChangePasswordViewModel vm)
        // {
        //     throw new NotImplementedException();
        // }

        private ClaimsIdentity CreateClaimsIdentity(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("UserId", user.Id.ToString()),
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
            };
            return new ClaimsIdentity(
                claims,
                "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType
            );
        }
    }
}
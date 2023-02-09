using System.Security.Claims;
using BLL.Helpers;
using BLL.Models;
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

        public AccountService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetUserByLogin(string login)
        {
            return await _userRepository
                .GetAll()
                .FirstOrDefaultAsync(x => x.Login == login);
        }

        public async Task<(long UserId, string Username)> Register(RegisterDto dto)
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
                return (user.Id, user.Email);
            }
            catch (Exception ex)
            {
                // TODO: LOG
                throw new BusinessException("Failed Register", ex);
            }
        }


        public async Task<(long UserId, string Username)> Login(LoginDto dto)
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

                return (user.Id, user.Login);
            }
            catch (Exception ex)
            {
                // TODO: LOG
                throw new BusinessException("Failed Login", ex);
            }
        }
    }
}
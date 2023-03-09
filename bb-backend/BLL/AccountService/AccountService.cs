using AutoMapper;
using BLL.Models;
using BLL.Models.GetCourseForLearning;
using BLL.Models.GetUserForAccount;
using BLL.Models.Login;
using BLL.Models.Register;
using Infrastructure.Common;
using Infrastructure.Helpers;
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
            resultDto.RecommendedBy = "Will Smith";
            resultDto.Rating = 505;
            return resultDto;
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
                    FirstName = dto.FirstName,
                    MiddleName = dto.MiddleName,
                    Surname = dto.Surname,
                    Login = dto.Login,
                    Phone = dto.Phone,
                    Email = dto.Email,
                    Role = UserRole.User,
                    PasswordHash = PasswordHasher.GetPasswordHash(dto.Password)
                };
                _userRepository.Add(user);
                await _userRepository.SaveChangesAsync();
                return (user.Id, user.Login);
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
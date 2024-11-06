using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using YouthProtection.Models;
using YouthProtection.Models.Dtos;
using YouthProtection.Services;
using YouthProtectionApi.Exceptions;
using YouthProtectionApi.Repositories;

namespace YouthProtectionApi.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly AuthService _authService;
        private readonly IHttpContextAccessor _httpContextAccessor;




        public UserService(IUserRepository userRepository, AuthService authService, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _authService = authService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> ExistentUser(string email)
        {
            return await _userRepository.FindByEmail(email); ;
        }

        public async Task<UserModelDto> GetByUserId()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                throw new UnauthorizedAccessException("Houve um erro na requisição, tente fazer login novamente.");
            }

            var userId = long.Parse(userIdClaim.Value);

            var existingUser = await _userRepository.GetUserById(userId);
            if (existingUser == null)
            {
                throw new UnauthorizedAccessException("Usuário não encontrado.");
            }

            return new UserModelDto
            {
                UserId = existingUser.UserId,
                FictionalName = existingUser.FictionalName,
                Email = existingUser.Email,
                Password = null,
                CellPhone = existingUser.CellPhone,
                BirthDate = existingUser.BirthDate,
                Uf = existingUser.Uf,
                City = existingUser.City,
                Role = existingUser.Role,
            };
        }

        public async Task<UserModelDto> UpdateUser(UserModelDto userModelDto)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
            
            if (userIdClaim == null)
            {
                throw new UnauthorizedAccessException("Houve um erro na requisição, tente fazer login novamente.");
            }

            var userId = long.Parse(userIdClaim.Value);

            var existingUser = await _userRepository.GetUserById(userId);
            if (existingUser == null)
            {
                throw new UnauthorizedAccessException("Usuário não encontrado.");
            }

            if (!string.IsNullOrEmpty(userModelDto.Password))
            {
                existingUser.PasswordHash = _authService.HashPassword(userModelDto.Password);
            }
            if (!string.IsNullOrEmpty(userModelDto.CellPhone))
            {
                existingUser.CellPhone = userModelDto.CellPhone;
            }
            if (!string.IsNullOrEmpty(userModelDto.Uf))
            {
                existingUser.Uf = userModelDto.Uf;
            }
            if (!string.IsNullOrEmpty(userModelDto.City))
            {
                existingUser.City = userModelDto.City;
            }
            if (!string.IsNullOrEmpty(userModelDto.FictionalName))
            {
                existingUser.FictionalName = userModelDto.FictionalName;
            }

            await _userRepository.UpdateUser(existingUser);

            return new UserModelDto
            {
                FictionalName = existingUser.FictionalName,
                CellPhone = existingUser.CellPhone,
                Uf = existingUser.Uf,
                City = existingUser.City,
                Email = existingUser.Email,
                Password = null
            };
        }

        public async Task<RegisterUserException> RegisterNewUser(UserModel userModel)
        {

           await _userRepository.AddNewUser(userModel);

            return new RegisterUserException
            {
                Success = true,
                User = userModel
            };
        }

        public async Task<UserModelDto> LoginAttempt(UserModelDto userModelDto)
        {
            var user = await _userRepository.AuthenticationUser(userModelDto.Email);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Houve um erro na requisição. Tente novamente");
            }

            var isPasswordValid = _authService.VerifyPassword(userModelDto.Password, user.PasswordHash);
            if (!isPasswordValid)
            {
                throw new UnauthorizedAccessException("Houve um erro na requisição. Tente novamente");
            }

            var token = _authService.CreateToken(user);

            return new UserModelDto
            {
                UserId = user.UserId,
                Email = user.Email,
                FictionalName = user.FictionalName,
                BirthDate = user.BirthDate,
                CellPhone = user.CellPhone,
                Uf = user.Uf,
                City = user.City,
                Role = user.Role,
                Token = token,
                Password = null
            };
        }   
    }
}

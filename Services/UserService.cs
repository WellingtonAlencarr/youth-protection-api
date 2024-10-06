using Microsoft.AspNetCore.Mvc;
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


        public UserService(IUserRepository userRepository, AuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        public async Task<bool> ExistentUser(string email)
        {
            return await _userRepository.FindByEmail(email); ;
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
                Email = user.Email,
                Role = user.Role,
                Token = token,
                Password = null
            };
        }   
    }
}

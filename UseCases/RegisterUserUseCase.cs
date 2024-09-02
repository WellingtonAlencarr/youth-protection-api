using Microsoft.AspNetCore.Mvc;
using YouthProtection.Models;
using YouthProtection.Models.Dtos;
using YouthProtection.Services;
using YouthProtectionApi.Exceptions;
using YouthProtectionApi.Repositories;
using YouthProtectionApi.Services;

namespace YouthProtectionApi.UseCases
{
    public class RegisterUserUseCase
    {
        private readonly AuthService _authService;
        private readonly IUserRepository _userRepository;
        private readonly UserService _userService;

        public RegisterUserUseCase(AuthService authService, IUserRepository userRepository, UserService userService) 
        {
            _authService = authService;
            _userRepository = userRepository;
            _userService = userService;
        }

        public async Task<RegisterUserException> RegisterUser(UserModelDto userModelDto)
        {
            try { 
                var emailExists = await _userService.ExistentUser(userModelDto.Email);
                if (emailExists)
                {
                    return new RegisterUserException
                    {
                        Success = false,
                        ErrorMessage = "Email já utilizado"
                    };
                }
            
                var passwordHash = _authService.HashPassword(userModelDto.Password);
                var userModel = new UserModel
                {
                    Email = userModelDto.Email,
                    PasswordHash = passwordHash
                };
            
                var result = await _userService.RegisterUser(userModel);

                return new RegisterUserException
                {
                    Success = true,
                    User = userModel
                };
            }
            catch (Exception ex)
            {
                return new RegisterUserException
                {
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}

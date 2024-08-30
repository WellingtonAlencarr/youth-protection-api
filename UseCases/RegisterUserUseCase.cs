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
        private readonly IConfiguration _configuration;
        private readonly UserService _userService;

        public RegisterUserUseCase(AuthService authService, IUserRepository userRepository, IConfiguration configuration, UserService userService) 
        {
            _authService = authService;
            _userRepository = userRepository;
            _configuration = configuration;
            _userService = userService;
        }

        public async Task<GenericExceptions> RegisterUser(UserModelDto userModelDto)
        {
            var verificationEmail = await _userService.ExistentUser(userModelDto.Email);
            if (verificationEmail != false)
            {
                throw new InvalidOperationException("Email já está em uso");
            }
            
            var passwordHash = _authService.HashPassword(userModelDto.Password);
            var userModel = new UserModel
            {
                Email = userModelDto.Email,
                PasswordHash = passwordHash
            };
            
            var userAdded = await _userService.RegisterUser(userModel);
            return new GenericExceptions 
            {
                Success = true
            };
        }
    }
}

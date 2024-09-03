using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.Win32;
using YouthProtection.Models;
using YouthProtection.Models.Dtos;
using YouthProtection.Services;
using YouthProtectionApi.Repositories;
using YouthProtectionApi.Services;

namespace YouthProtectionApi.UseCases
{
    public class LoginUserUseCase
    {
        private readonly AuthService _authService;
        private readonly UserService _userService;
        public LoginUserUseCase(AuthService authService, UserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        public async Task<UserModelDto> ExecuteLogin(UserModelDto userModelDto)
        {

            return await _userService.LoginAttempt(userModelDto);
        }
    }
}

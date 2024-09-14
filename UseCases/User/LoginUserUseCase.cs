using YouthProtection.Models.Dtos;
using YouthProtection.Services;
using YouthProtectionApi.Services;

namespace YouthProtectionApi.UseCases.User
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

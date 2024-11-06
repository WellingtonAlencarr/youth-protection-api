using YouthProtection.Services;
using YouthProtectionApi.Repositories;
using YouthProtectionApi.Services;

namespace YouthProtectionApi.UseCases.User
{
    public class UpdateUserUseCase
    {
        private readonly AuthService _authService;
        private readonly UserService _userService;
        public UpdateUserUseCase(AuthService authService, UserService userService)
        {
            _authService = authService;
            _userService = userService;
        }


    }
}

using Microsoft.Win32;
using YouthProtection.Models;
using YouthProtection.Models.Dtos;
using YouthProtection.Services;

namespace YouthProtectionApi.UseCases
{
    public class LoginUserUseCase
    {
        private readonly AuthService _authService;
        public static UserModel userModel = new UserModel();

        public LoginUserUseCase(AuthService authService)
        {
            _authService = authService;
        }

        public UserModel ExecuteLogin(UserModelDto userModelDto)
        {
            var verification = _authService.VerifyPassword(userModelDto.Password, userModel.PasswordHash);
            if(verification != true || userModelDto.Email != userModel.Email)
            {
                throw new UnauthorizedAccessException("Usuário ou senha inválido.");
            }

            return userModel;
        }
    }
}

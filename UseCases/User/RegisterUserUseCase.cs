using YouthProtection.Models;
using YouthProtection.Models.Dtos;
using YouthProtection.Services;
using YouthProtectionApi.Exceptions;
using YouthProtectionApi.Services;

namespace YouthProtectionApi.UseCases.User
{
    public class RegisterUserUseCase
    {
        private readonly AuthService _authService;
        private readonly UserService _userService;

        public RegisterUserUseCase(AuthService authService, UserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        public async Task<RegisterUserException> RegisterUser(UserModelDto userModelDto)
        {
            try
            {
                var emailExists = await _userService.ExistentUser(userModelDto.Email);
                if (emailExists)
                {
                    return new RegisterUserException
                    {
                        Success = false,
                        ErrorMessage = "Houve um erro de requisição, tente novamente."
                    };
                }

                var passwordHash = _authService.HashPassword(userModelDto.Password);
                var userModel = new UserModel
                {
                    FictionalName = userModelDto.FictionalName,
                    Email = userModelDto.Email,
                    PasswordHash = passwordHash,
                    CellPhone = userModelDto.CellPhone,
                    BirthDate = userModelDto.BirthDate,
                    Uf = userModelDto.Uf,
                    City = userModelDto.City,
                    Role = userModelDto.Role
                };

                var result = await _userService.RegisterNewUser(userModel);

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

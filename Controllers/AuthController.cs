using Microsoft.AspNetCore.Mvc;
using YouthProtection.Models;
using YouthProtection.Models.Dtos;
using YouthProtectionApi.UseCases;

namespace YouthProtection.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly RegisterUserUseCase _registerUserUseCase;
        private readonly LoginUserUseCase _loginUserUseCase;

        public AuthController(RegisterUserUseCase registerUserUseCase, LoginUserUseCase loginUserUseCase)
        {
            _registerUserUseCase = registerUserUseCase;
            _loginUserUseCase = loginUserUseCase;
        }

        [HttpPost("Register")]
        public IActionResult Register(UserModelDto userModelDto)
        {
            var registeredUser = _registerUserUseCase.RegisterUser(userModelDto);
            return Ok(registeredUser);
        }
        
        [HttpPost("Login")]
        public IActionResult Login(UserModelDto userModelDto)
        {
            try
            {
                var userModel = _loginUserUseCase.ExecuteLogin(userModelDto);
                return Ok(userModel);
            }
            catch (UnauthorizedAccessException ex)
            { 
                return BadRequest(ex.Message);
            }
            
        }
    }
}

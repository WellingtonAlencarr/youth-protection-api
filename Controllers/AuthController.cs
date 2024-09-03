using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Register(UserModelDto userModelDto)
        {
            var registerResult = await _registerUserUseCase.RegisterUser(userModelDto);
            return Ok(registerResult);
        }
        
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserModelDto userModelDto)
        {
            try
            {
                var loginResult = await _loginUserUseCase.ExecuteLogin(userModelDto);
                return Ok(loginResult);
            }
            catch (UnauthorizedAccessException ex)
            { 
                return BadRequest(ex.Message);
            }
            
        }
    }
}

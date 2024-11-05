using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using YouthProtection.Models;
using YouthProtection.Models.Dtos;
using YouthProtectionApi.Services;
using YouthProtectionApi.UseCases.User;

namespace YouthProtection.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly RegisterUserUseCase _registerUserUseCase;
        private readonly LoginUserUseCase _loginUserUseCase;
        private readonly UserService _userService;

        public AuthController(RegisterUserUseCase registerUserUseCase, LoginUserUseCase loginUserUseCase, UserService userService)
        {
            _registerUserUseCase = registerUserUseCase;
            _loginUserUseCase = loginUserUseCase;
            _userService = userService;
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

        [Authorize(Roles = "User")]
        [HttpGet("data")]
        public IActionResult GetProtectedData()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized("Token inválido ou expirado");
            }

            return Ok($"Dados Protegidos e acessados pelo usuário {userId}");
        }

        [Authorize(Roles = "User, Admin, Voluntary")]
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateUser(UserModelDto userModelDto)
        {
            try
            {
                var UpdateUser = await _userService.UpdateUser(userModelDto);
                return Ok(UpdateUser);
            }
            catch (UnauthorizedAccessException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "User, Admin, Voluntary")]
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId()
        {
            try
            {
                var user = await _userService.GetByUserId();
                return Ok(user);
            }
            catch (UnauthorizedAccessException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

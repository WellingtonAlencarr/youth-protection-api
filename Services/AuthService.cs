using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using YouthProtection.Models;
using YouthProtectionApi.Repositories;

namespace YouthProtection.Services
{
    public class AuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string passwordHash)
        { 
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }

        public string CreateToken(UserModel userModel)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userModel.Email),
                new Claim(ClaimTypes.NameIdentifier, userModel.Id.ToString()),
                new Claim(ClaimTypes.Role, userModel.Role.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddSeconds(3600),
                signingCredentials: credentials
                );

            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

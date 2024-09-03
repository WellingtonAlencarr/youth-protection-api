using YouthProtectionApi.Repositories;

namespace YouthProtection.Services
{
    public class AuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string passwordHash)
        { 
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
    }
}

using YouthProtection.Models;

namespace YouthProtectionApi.Exceptions
{
    public class RegisterUserException
    {
        public bool Success { get; set; }
        public UserModel User { get; set; }
        public string ErrorMessage { get; set; }
    }
}

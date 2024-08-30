namespace YouthProtection.Models
{
    public class UserModel
    {
        public long Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }
}

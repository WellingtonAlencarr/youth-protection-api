using YouthProtectionApi.Models.Enums;

namespace YouthProtection.Models
{
    public class UserModel
    {
        public long Id { get; set; }
        public string FictionalName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string CellPhone {  get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string uf { get; set; } = string.Empty;
        public string city { get; set; } = string.Empty;
        public UserRole Role { get; set; }
    }
}

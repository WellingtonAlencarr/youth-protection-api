using Microsoft.AspNetCore.Identity;
using YouthProtectionApi.Models.Enums;

namespace YouthProtection.Models.Dtos
{
    public class UserModelDto
    {
        public string FictionalName { get; set; } = string.Empty;
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string CellPhone { get; set; } = string.Empty;
        public string BirthDate { get; set; }
        public string Uf { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public string? Token { get; set; }
    }
}

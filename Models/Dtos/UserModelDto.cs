using Microsoft.AspNetCore.Identity;

namespace YouthProtection.Models.Dtos
{
    public class UserModelDto
    {
        public long Id { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}

﻿using System.ComponentModel.DataAnnotations.Schema;
using YouthProtectionApi.Models.Enums;

namespace YouthProtection.Models
{
    public class UserModel
    {
        public long UserId { get; set; }
        public string FictionalName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string CellPhone { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string Uf { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public UserStatus UserStatus {  get; set; }

        public List<PublicationsModel> Publications { get; set; } = new List<PublicationsModel>();
    }
}

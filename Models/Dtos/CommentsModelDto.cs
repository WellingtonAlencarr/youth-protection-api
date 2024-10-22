using YouthProtection.Models;

namespace YouthProtectionApi.Models.Dtos
{
    public class CommentsModelDto
    {
        public long idComment { get; set; }
        public long ResponseCommentId { get; set; }
        public string contentComment { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public UserModel userModel { get; set; }
        public PublicationsModel publicationsModel { get; set; }
    }
}

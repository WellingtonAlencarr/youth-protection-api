using YouthProtection.Models;
using YouthProtectionApi.Models.Enums;

namespace YouthProtectionApi.Models.Dtos
{
    public class CommentsModelDto
    {
        public long CommentId { get; set; }
        public long UserId { get; set; }
        public long PublicationId { get; set; }
        public string ContentComment { get; set; } = string.Empty;
        public CommentStatus CommentStatus { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

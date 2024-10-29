using YouthProtectionApi.Models.Enums;

namespace YouthProtection.Models
{
    public class CommentsModel
    {
        public long CommentId {  get; set; }
        public long UserId { get; set; }
        public long PublicationId {  get; set; }
        public string ContentComment { get; set; } = string.Empty;
        public CommentStatus CommentStatus { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public UserModel UserModel { get; set; }
        public PublicationsModel PublicationsModel { get; set; }

    }
}




























  
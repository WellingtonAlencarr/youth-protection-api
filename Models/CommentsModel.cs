namespace YouthProtection.Models
{
    public class CommentsModel
    {
        public long idComment {  get; set; }
        public long ResponseCommentId {  get; set; }
        public string contentComment { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public UserModel UserModel { get; set; }
        public PublicationsModel PublicationsModel { get; set; }

    }
}

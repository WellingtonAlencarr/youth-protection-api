using YouthProtectionApi.Models.Enums;

namespace YouthProtection.Models
{
    public class PublicationsModel
    {
        public long PublicationId {  get; set; }
        public long UserId {  get; set; }
        public string PublicationContent { get; set; } = string.Empty;
        public PublicationRole PublicationsRole { get; set; }
        public PublicationStatus PublicationStatus { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ModificationDate { get; set; } = DateTime.UtcNow;

        public UserModel UserModel {  get; set; }
    }
}

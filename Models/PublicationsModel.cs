using YouthProtectionApi.Models.Enums;

namespace YouthProtection.Models
{
    public class PublicationsModel
    {
        private long IdPublication {  get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModificationDate { get; set; }
        public string PublicationContent { get; set; } = string.Empty;
        public PublicationsRole PublicationsRole { get; set; }
    }
}

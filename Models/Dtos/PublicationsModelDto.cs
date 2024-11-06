using YouthProtection.Models;
using YouthProtectionApi.Models.Enums;

namespace YouthProtectionApi.Models.Dtos
{
    public class PublicationsModelDto
    {
        public long PublicationId { get; set; }
        public string PublicationContent { get; set; } = string.Empty;
        public PublicationRole PublicationsRole { get; set; }
        public PublicationStatus PublicationStatus { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ModificationDate { get; set; } = DateTime.UtcNow;

    }
}

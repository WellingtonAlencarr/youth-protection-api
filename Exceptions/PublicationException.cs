using YouthProtection.Models;

namespace YouthProtectionApi.Exceptions
{
    public class PublicationException
    {
        public bool Success { get; set; }
        public PublicationsModel Publications { get; set; }
        public string ErrorMessage { get; set; }
    }
}

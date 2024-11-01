using YouthProtectionApi.Models;
using YouthProtectionApi.Models.Enums;

namespace YouthProtection.Models
{
    public class MessageModel
    {
        public long Id {  get; set; }
        public long ChatId { get; set; }
        public ChatModel Chat {  get; set; }
        public long SenderId { get; set; }
        public UserModel Sender { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; } = DateTime.UtcNow;

    }
}
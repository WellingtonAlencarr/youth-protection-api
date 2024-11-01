namespace YouthProtectionApi.Models.Dtos
{
    public class MessageResponseDTO
    {
        public long MessageId { get; set; }
        public long ChatId { get; set; }
        public long SenderId { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
    }
}

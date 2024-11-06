namespace YouthProtectionApi.Models.Dtos
{
    public class MessageReceiveDTO
    {
        public long UserId {  get; set; }
        public long ChatId { get; set; }
        public string Content { get; set; }
    }
}

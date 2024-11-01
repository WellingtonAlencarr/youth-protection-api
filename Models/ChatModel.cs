using YouthProtection.Models;

namespace YouthProtectionApi.Models
{
    public class ChatModel
    {
        public long Id { get; set; }
        public long AttendanceId { get; set; }
        public AttendanceModel Attendance { get; set; }
        public List<MessageModel> Messages { get; set; }
    }
}

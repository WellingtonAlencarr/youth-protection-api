using YouthProtection.Models;

namespace YouthProtectionApi.Models
{
    public class AttendanceModel
    {
        public long Id { get; set; }
        public long VolunteerId { get; set; }
        public UserModel Volunteer { get; set; }
        public long PublicationId { get; set; }
        public PublicationsModel Publication {  get; set; }
        public DateTime StartedAt { get; set; } = DateTime.UtcNow;
        public bool IsCompleted {  get; set; }
    }
}

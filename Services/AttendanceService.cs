using YouthProtection.Models;
using YouthProtectionApi.Models;
using YouthProtectionApi.Repositories;

namespace YouthProtectionApi.Services
{
    public class AttendanceService
    {

        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IChatRepository _chatRepository;

        public AttendanceService(IAttendanceRepository attendanceRepository, IChatRepository chatRepository)
        {
            _attendanceRepository = attendanceRepository;
            _chatRepository = chatRepository;
        }

        public async Task<AttendanceModel> StartAttendance(long publicationId, long volunteerId)
        {
            var attendance = new AttendanceModel
            {
                PublicationId = publicationId,
                VolunteerId = volunteerId,
                StartedAt = DateTime.UtcNow,
                IsCompleted = false
            };

            var newAttendance = await _attendanceRepository.AddAttendance(attendance);

            var chat = new ChatModel
            {
                AttendanceId = newAttendance.Id,
                Messages = new List<MessageModel>()
            };

            await _chatRepository.AddChat(chat);

            return newAttendance;
        }

        public async Task CompleteAttendance(long attendanceId)
        {
            var attendance = await _attendanceRepository.GetAttendanceById(attendanceId);
            attendance.IsCompleted = true;
            await _attendanceRepository.UpdateAttendance(attendance);
        }
    }
}

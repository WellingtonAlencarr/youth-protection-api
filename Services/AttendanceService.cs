using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Claims;
using YouthProtection.Models;
using YouthProtectionApi.Models;
using YouthProtectionApi.Repositories;

namespace YouthProtectionApi.Services
{
    public class AttendanceService
    {

        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IChatRepository _chatRepository;
        private readonly IPublicationRepository _publicationRepository;
        private readonly IUserRepository _userRepository;

        public AttendanceService(IAttendanceRepository attendanceRepository, IChatRepository chatRepository, IPublicationRepository publicationRepository, IUserRepository userRepository)
        {
            _attendanceRepository = attendanceRepository;
            _chatRepository = chatRepository;
            _publicationRepository = publicationRepository;
            _userRepository = userRepository;
        }

        public async Task<long> StartAttendance(long publicationId, ClaimsPrincipal userClaims)
        {
            var publication = await _publicationRepository.GetPublicationById(publicationId);
            if(publication == null)
            {
                throw new KeyNotFoundException("Publicação não encontrada");
            }

            var volunteerIdClaims = userClaims.FindFirst(ClaimTypes.NameIdentifier);
            if(volunteerIdClaims == null)
            {
                throw new KeyNotFoundException("Voluntário não encontrado.");
            }
            long volunteerId = long.Parse(volunteerIdClaims.Value);

            var attendance = new AttendanceModel
            {
                PublicationId = publicationId,
                VolunteerId = volunteerId,
                StartedAt = DateTime.UtcNow,
                IsCompleted = false
            };

            await _attendanceRepository.AddAttendance(attendance);

            var chat = new ChatModel
            {
                AttendanceId = attendance.Id,
                Messages = new List<MessageModel>()
            };

            await _chatRepository.AddChat(chat);

            return chat.Id;
        }

        public async Task CompleteAttendance(long attendanceId)
        {
            var attendance = await _attendanceRepository.GetAttendanceById(attendanceId);
            attendance.IsCompleted = true;
            await _attendanceRepository.UpdateAttendance(attendance);
        }
    }
}



using YouthProtection.Models;
using YouthProtectionApi.Models;
using YouthProtectionApi.Repositories;

namespace YouthProtectionApi.Services
{
    public class ChatService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IChatRepository _chatRepository;

        public ChatService(IMessageRepository messageRepository, IAttendanceRepository attendanceRepository, IChatRepository chatRepository)
        {
            _messageRepository = messageRepository;
            _attendanceRepository = attendanceRepository;
            _chatRepository = chatRepository;
        }

        public async Task<MessageModel> SendMessage(long chatId, long senderId, string content)
        {
            var message = new MessageModel
            {
                ChatId = chatId,
                SenderId = senderId,
                Content = content,
                SentAt = DateTime.UtcNow,
            };

            return await _messageRepository.AddMessage(message);
        }

        public async Task<List<MessageModel>> GetMessagesByChatId(long chatId)
        {
            return await _messageRepository.GetByChatId(chatId);
        }

        public async Task<ChatModel> GetChatByPublication(long publicationId)
        {
            var attendance = await _attendanceRepository.GetAttendanceByPublicationId(publicationId);
            if (attendance == null)
            {
                throw new Exception("Não há atendimento ativo");
            }

            var chat = await _chatRepository.GetChatByAttendanceId(attendance.Id);
            if (chat == null)
            {
                throw new Exception("Nenhum chat associado ao atendimento.");
            }

            return chat;
        }
    }
}

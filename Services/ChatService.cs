

using YouthProtection.Models;
using YouthProtectionApi.Repositories;

namespace YouthProtectionApi.Services
{
    public class ChatService
    {
        private readonly IMessageRepository _messageRepository;

        public ChatService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
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
    }
}

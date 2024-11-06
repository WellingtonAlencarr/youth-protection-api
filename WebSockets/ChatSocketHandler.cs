using Microsoft.OpenApi.Writers;
using Newtonsoft.Json;
using YouthProtectionApi.Models.Dtos;
using YouthProtectionApi.Services;

namespace YouthProtectionApi.WebSockets
{
    public class ChatSocketHandler : WebSocketHandler
    {
        private readonly IServiceProvider _serviceProvider;

        public ChatSocketHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task SendMessageToAllAsync(string message)
        {

            var messageData = JsonConvert.DeserializeObject<MessageReceiveDTO>(message);

            var chatService = _serviceProvider.GetRequiredService<ChatService>();



            var createdMessage = await chatService.SendMessage(
                messageData.UserId,
                messageData.ChatId,
                messageData.Content
            );

            var responseDTO = new MessageResponseDTO
            {
                MessageId = createdMessage.Id,
                ChatId = createdMessage.ChatId,
                SenderId = createdMessage.SenderId,
                Content = createdMessage.Content,
                SentAt = createdMessage.SentAt
            };

            var responseMessage = JsonConvert.SerializeObject(responseDTO);
            await base.SendMessageToAllAsync(message);
        }
    }
}

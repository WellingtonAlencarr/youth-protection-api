using Microsoft.AspNetCore.Mvc;
using YouthProtection.Models;
using YouthProtectionApi.Models.Dtos;
using YouthProtectionApi.Services;

namespace YouthProtectionApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatController : ControllerBase
    {

        private readonly ChatService _chatService;

        public ChatController(ChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage(MessageResponseDTO messageModel)
        {
            var message = await _chatService.SendMessage(messageModel.ChatId, messageModel.SenderId, messageModel.Content);
            return Ok(message);
        }

        [HttpGet("messages/{chatId}")]
        public async Task<IActionResult> GetMessages(long chatId)
        {
            var messages = await _chatService.GetMessagesByChatId(chatId);
            return Ok(messages);
        }

        [HttpGet("GetChatBy/{publicationId}")]
        public async Task<IActionResult> GetChatByPublication(long publicationId)
        {
            try
            {
                var chat = await _chatService.GetChatByPublication(publicationId);
                if (chat == null)
                {
                    return NotFound(new { Message = "Nenhum chat encontrado para esta publicação." });
                }

                return Ok(chat);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Erro ao buscar chat", Details = ex.Message });
            }
        }
    }
}

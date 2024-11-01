using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> SendMessage(long chatId, long senderId, string content)
        {
            var message = await _chatService.SendMessage(chatId, senderId, content);
            return Ok(message);
        }

        [HttpGet("messages")]
        public async Task<IActionResult> GetMessages(long chatId)
        {
            var messages = await _chatService.GetMessagesByChatId(chatId);
            return Ok(messages);
        }
    }
}

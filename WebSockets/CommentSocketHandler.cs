using Microsoft.OpenApi.Writers;
using Newtonsoft.Json;
using YouthProtectionApi.Models.Dtos;
using YouthProtectionApi.Services;

namespace YouthProtectionApi.WebSockets
{
    public class CommentSocketHandler : WebSocketHandler
    {
        private readonly IServiceProvider _serviceProvider;

        public CommentSocketHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task SendMessageToAllAsync(string message)
        {
            var commentService = _serviceProvider.GetRequiredService<CommentService>();

            var commentData = JsonConvert.DeserializeObject<CommentsModelDto>(message);

            await commentService.CreateComment(
                commentData.UserId,
                commentData.PublicationId,
                commentData.ContentComment,
                commentData.CommentStatus
            );

            await base.SendMessageToAllAsync(message);
        }
    
    }
}

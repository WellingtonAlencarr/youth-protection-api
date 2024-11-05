using System.Net.WebSockets;

namespace YouthProtectionApi.WebSockets
{
    public class WebSocketMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly WebSocketHandler _webSocketHandler;

        public WebSocketMiddleware(RequestDelegate next, WebSocketHandler webSocketHandler)
        {
            _next = next;
            _webSocketHandler = webSocketHandler;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path == "/chat" && context.WebSockets.IsWebSocketRequest)
            {
                await _webSocketHandler.HandleWebSocketConnection(context);
            }
            else
            {
                await _next(context);
            }
        }
    }
}

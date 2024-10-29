using System.Net.WebSockets;
using System.Text;

namespace YouthProtectionApi.WebSockets
{
    public class WebSocketHandler
    {
        protected readonly List<WebSocket> ActiveSockets = new List<WebSocket>();
        private static readonly object _lock = new object();

        public async Task HandleWebSocketConnection(HttpContext context)
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                lock (_lock)
                {
                    ActiveSockets.Add(webSocket);
                }
                await ReceiveMessagesAsync(webSocket);
            }
            else
            {
                context.Response.StatusCode = 400;
            }
        }

        private async Task ReceiveMessagesAsync(WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];

            while (webSocket.State == WebSocketState.Open)
            {

                var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Text)
                {
                    var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    await SendMessageToAllAsync(message);
                }
                else if (result.MessageType == WebSocketMessageType.Close)
                {
                    lock (_lock)
                    {
                        ActiveSockets.Remove(webSocket);
                    }
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Connection closed", CancellationToken.None);
                }
            }
        }

        protected virtual async Task SendMessageToAllAsync(string message)
        {
            var buffer = Encoding.UTF8.GetBytes(message);

            foreach(var socket in ActiveSockets)
            {
                if (socket.State == WebSocketState.Open)
                {
                    await socket.SendAsync(new ArraySegment<byte>(buffer, 0, buffer.Length), 
                        WebSocketMessageType.Text, 
                        true, 
                        CancellationToken.None);
                }
            }
        }

    }
}

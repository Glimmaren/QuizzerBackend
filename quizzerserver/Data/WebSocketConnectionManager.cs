//using System.Collections.Concurrent;
//using System.Net.WebSockets;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//public class WebSocketManager
//{
//    private ConcurrentDictionary<string, WebSocket> _sockets = new ConcurrentDictionary<string, WebSocket>();

//    public async Task AddSocketAsync(WebSocket socket, string socketId)
//    {
//        _sockets.TryAdd(socketId, socket);

//        await SendMessageAsync($"User with ID '{socketId}' joined the quiz game.");
//    }

//    public async Task RemoveSocketAsync(string socketId)
//    {
//        _sockets.TryRemove(socketId, out WebSocket socket);

//        await SendMessageAsync($"User with ID '{socketId}' left the quiz game.");
//    }

//    public async Task SendMessageAsync(string message)
//    {
//        byte[] buffer = Encoding.UTF8.GetBytes(message);

//        foreach (var socket in _sockets.Values)
//        {
//            if (socket.State == WebSocketState.Open)
//            {
//                await socket.SendAsync(new ArraySegment<byte>(buffer, 0, buffer.Length), WebSocketMessageType.Text, true, CancellationToken.None);
//            }
//        }
//    }
//}

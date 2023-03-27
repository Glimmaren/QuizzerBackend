//using System;
//using System.Net.WebSockets;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//public static class WebSocketHandler
//{
//    private static readonly WebSocketConnectionManager _connectionManager = new WebSocketConnectionManager();

//    public static async Task ConnectAsync(WebSocket webSocket)
//    {
//        await _connectionManager.AddWebSocketAsync(webSocket);
//    }

//    public static async Task ReceiveAsync(WebSocket webSocket, CancellationToken cancellationToken)
//    {
//        var buffer = new byte[1024 * 4];

//        while (webSocket.State == WebSocketState.Open && !cancellationToken.IsCancellationRequested)
//        {
//            var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken);

//            if (result.MessageType == WebSocketMessageType.Text)
//            {
//                var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
//                await _connectionManager.SendMessageToAllAsync(message);
//            }
//            else if (result.MessageType == WebSocketMessageType.Close)
//            {
//                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", cancellationToken);
//                await _connectionManager.RemoveWebSocketAsync(webSocket);
//            }
//        }
//    }

//    public static async Task DisconnectAsync(WebSocket webSocket)
//    {
//        await _connectionManager.RemoveWebSocketAsync(webSocket);
//    }
//}

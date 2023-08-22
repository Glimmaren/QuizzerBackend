using WebSocketSharp.Server;
using System.Collections.Concurrent;
using WebSocketSharp;
using QuizzerServer.Data;
using QuizzerServer.Interfaces;
using QuizzerServer.Handlers;
using System.Text.Json;
using System.Text;

namespace QuizzerServer.Communication
{
    public class SocketHandler : WebSocketBehavior 
    {
        private static readonly ConcurrentDictionary<WebSocket, FirstToPointGameService> _services =
            new ConcurrentDictionary<WebSocket, FirstToPointGameService>();

        private readonly IQuestionHandler _questionHandler;

        //public SocketHandler()
        //{

        //}
        public SocketHandler(IQuestionHandler questionHandler)
        {
            _questionHandler = questionHandler;
        }

        protected override void OnOpen()
        {
            // Create a new instance of the FirstToPointGameService class for this connection
            var service = new FirstToPointGameService(_questionHandler);
            _services.TryAdd(Context.WebSocket, service);

            // Call the OnOpen method of the service
            //service.OnOpen();
        }

        protected override async void OnMessage(MessageEventArgs e)
        {
            // Get the service instance for this connection
            if (_services.TryGetValue(Context.WebSocket, out var service))
            {
                // Call the OnMessage method of the service
                byte[] buffer = await service.Message(e);
                string mess = Encoding.UTF8.GetString(e.RawData);
                JsonElement message = JsonSerializer.Deserialize<JsonElement>(mess);

                string messageType = message.GetProperty("Type").GetString();
                
                //if (buffer != null)
                //{
                    if(messageType == "Close")
                    {
                        CloseService();
                    }
                    else
                    {
                        Send(buffer);
                    }
                    
                //} 
            }
        }

        private void CloseService()
        {
            WebSocket webSocket = Context.WebSocket;

            // Try to get the FirstToPointGameService instance for this WebSocket
            if (_services.TryGetValue(webSocket, out var service))
            {
                // Call the StopService method of the service
                //service.StopService();

                // Close the WebSocket connection
                webSocket.Close(CloseStatusCode.Normal);

                // Remove the service instance from the dictionary
                _services.TryRemove(webSocket, out _);
            }


        }

        protected override void OnClose(CloseEventArgs e)
        {
            // Get the service instance for this connection
            if (_services.TryGetValue(Context.WebSocket, out var service))
            {
                // Call the OnClose method of the service
                service.OnClose();
            }

            // Remove the service instance from the dictionary
            _services.TryRemove(Context.WebSocket, out _);
        }
    }
}

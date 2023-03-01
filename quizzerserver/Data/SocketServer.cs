using QuizzerLibary;
using QuizzerServer.Handlers;
using System;
using System.Collections.Generic;
using System.Text.Json;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace QuizzerServer.Data
{
    public class SocketServer : WebSocketBehavior
    {
        private List<QuestionContent> questions = new List<QuestionContent>();
        private readonly WebSocketServer server;

        private readonly QuestionHandler _questionHandler;


        public SocketServer(QuestionHandler questionHandler)
        {
            _questionHandler = questionHandler;

            server = new WebSocketServer("ws://localhost:7800");
            server.AddWebSocketService<WebSocketBehavior>("/GetQuestion", () => this);
            server.Start();

            Console.WriteLine("Server running...");
        }

        protected override async void OnMessage(MessageEventArgs e)
        {
            var messageContent = JsonSerializer.Deserialize<ContentModel>(e.RawData);
            //questions = await _questionHandler.GetQuestion(messageContent);
            var question = questions[0];
            byte[] data = JsonSerializer.SerializeToUtf8Bytes(question);
            Send(data);
            Console.WriteLine($"Question sent: {data}");
        }
    }
}

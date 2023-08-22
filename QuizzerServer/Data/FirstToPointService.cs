using QuizzerLibary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;
using WebSocketSharp.Server;
using WebSocketSharp;

namespace QuizzerServer.Data
{
    class FirstToPointService : WebSocketBehavior
    {
        private static List<Player> _players = new List<Player>();
        private static List<QuestionContent> _questions = new List<QuestionContent>();

        public FirstToPointService()
        {

        }

        protected override void OnMessage(MessageEventArgs e)
        {
            //byte[] buffer = e.Data;
            //var message = JsonSerializer.Deserialize<Message>(e.RawData);
            //Console.WriteLine("Got this from client => " + message.MessageText);
            //Sessions.Broadcast(e.Data);
        }

    }
}

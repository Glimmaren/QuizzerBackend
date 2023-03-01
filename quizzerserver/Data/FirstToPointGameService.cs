using Microsoft.Owin.Security;
using QuizzerLibary;
using QuizzerServer.Handlers;
using QuizzerServer.Interfaces;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Text.Json;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace QuizzerServer.Data
{
    public class FirstToPointGameService : WebSocketBehavior, IFirstToPointGameService
    {

        //TODO: Ha en mer Dynamisk Socket handler som hanterar väljer vilket spel som skall startas!
        private List<QuestionContent> _questions;
        private Dictionary<int, Answer> _answers;
        private readonly List<Player> _players;
        GameState _gameState;
        private int _pointsToWin;
        private string Rules = "Varje spelare får 10 sekunder på sig att svara, spelarna får poäng utifrån svårhetsgrad och försten till vald poäng vinner";
        private readonly IQuestionHandler _questionHandler;
        private CurrentQuestion _currentQuestion;
        


        public FirstToPointGameService()
        {

        }
        public FirstToPointGameService(IQuestionHandler questionHandler)
        {
            _questionHandler = questionHandler;
        }

        public async Task PopulateQuestions(ContentModel model)
        {
            _questions = await _questionHandler.GetQuestions(model);
        }

        public Task<bool> CheckAnswerAsync(Answer answer)
        {
            throw new NotImplementedException();
        }

        public Task<Player> GetPlayerAsunc(int id)
        {
            throw new NotImplementedException();
        }

        public QuestionContent GetQuestionAsync(int questionNumber)
        {
            return _questions[questionNumber - 1];
        }

       
        protected override void OnMessage(MessageEventArgs e)
        {
         
            string mess = Encoding.UTF8.GetString(e.RawData);
            JsonElement message = JsonSerializer.Deserialize<JsonElement>(mess);
            string messageType = message.GetProperty("Type").GetString();

            switch (messageType)
            {
                case "StartFirstToPoint" :
                    StartForstToPointGame(mess);
                    break;
                case "GetQuestion" :
                    GetQuestion();
                    break;
                case "Answers":

                    break;
            }

            Console.WriteLine("Got this from client => ");
            //PopulateQuestions(model);
        }

        public async Task StartForstToPointGame(dynamic mess)
        {
            StartFirstToPointMessage FTPMessage = JsonSerializer.Deserialize<StartFirstToPointMessage>(mess);
            ContentModel model = FTPMessage.Data;
            Debug.Write("Hej");
            await PopulateQuestions(model);

            GetQuestion();

        }

        public void GetQuestion()
        {
            Random rand = new Random();
            byte[] buffer = new byte[1024];
            string[] tempAnswers = new string[4];
            int questionNumber = rand.Next(0, _questions.Count);
            _currentQuestion = new CurrentQuestion();
            NewCurrentQuestion questionMess = new NewCurrentQuestion();
            questionMess.type = "newQuestion";
            tempAnswers[0] = _questions[questionNumber].correctAnswer;
            tempAnswers[1] = _questions[questionNumber].incorrectAnswers[0];
            tempAnswers[2] = _questions[questionNumber].incorrectAnswers[1];
            tempAnswers[3] = _questions[questionNumber].incorrectAnswers[2];

            _currentQuestion.questionId = _questions[questionNumber].id;
            _currentQuestion.question = _questions[questionNumber].question;
            _currentQuestion.answers = tempAnswers;



            buffer = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(_currentQuestion));
            Send(buffer);
            
        }


        public void SubmitAnswer(int playerNumber, string answer, string questionNumber)
        {
            //if (!answers.ContainsKey(playerNumber))
            //{
            //    answers.Add(playerNumber, new Answer(playerNumber, questionNumber, answer));
            //}
        }

        public GameState StartGame()
        {
            _gameState = GameState.InProgress;
            return _gameState;
        }


       

        //TODO: Poängräknare

        //Temp class
        
    }

    
}

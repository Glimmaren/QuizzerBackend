using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Owin.Security;
using QuizzerLibary;
using QuizzerServer.Handlers;
using QuizzerServer.Interfaces;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.Json;
using WebSocketSharp;
using WebSocketSharp.Server;



namespace QuizzerServer.Data
{
    public class FirstToPointGameService :  IFirstToPointGameService
    {

        //TODO: Ha en mer Dynamisk Socket handler som hanterar väljer vilket spel som skall startas!
        //TODO: KOlla varför man inte can diconnecta och reconnecta utan att starta om! KOlla så att porten blir ledig igen kanske?
        private List<QuestionContent> _questions;
        private List<Player> _players;
        GameState _gameState;
        private int _pointsToWin;
        private string Rules = "Varje spelare får 10 sekunder på sig att svara, spelarna får poäng utifrån svårhetsgrad och försten till vald poäng vinner";
        private readonly IQuestionHandler _questionHandler;
        private CurrentQuestion _currentQuestion;

        //OnMessage deals with the incoming message from The TvHost
        public async Task<byte[]> Message(MessageEventArgs e)
        {

            string mess= Encoding.UTF8.GetString(e.RawData);
            JsonElement message = JsonSerializer.Deserialize<JsonElement>(mess); 

            string messageType = message.GetProperty("Type").GetString();

            switch (messageType)
            {
                //Temp
                //case "Close":
                //    CloseConnection();
                //    break;
                case "StartFirstToPoint":
                    return await StartForstToPointGame(mess);
                case "GetNewQuestion":
                    return GetQuestion();
                case "CheckAnswers":
                    return CheckAnswer(mess);

            }

            return null;
        }
        //Sends back a new Question to TVHost
        public byte[] GetQuestion()
        {
            Random rand = new Random();
            byte[] buffer = new byte[1024];
            string[] tempAnswers = new string[4];
            int questionNumber = rand.Next(0, _questions.Count);
            _currentQuestion = new CurrentQuestion();
            NewCurrentQuestion questionMess = new NewCurrentQuestion();
            questionMess.Type = "newQuestion";
            tempAnswers[0] = _questions[questionNumber].correctAnswer;
            tempAnswers[1] = _questions[questionNumber].incorrectAnswers[0];
            tempAnswers[2] = _questions[questionNumber].incorrectAnswers[1];
            tempAnswers[3] = _questions[questionNumber].incorrectAnswers[2];

            _currentQuestion.questionId = _questions[questionNumber].id;
            _currentQuestion.question = _questions[questionNumber].question;
            _currentQuestion.answers = tempAnswers;

            questionMess.question = _currentQuestion;

            buffer = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(questionMess));
            return buffer;

        }
        public void OnClose()
        {
           
            
        }

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
        public byte[] CheckAnswer(dynamic mess) //Det skall lägga till vinnare när man kommer till rätt poäng
        {      
            JsonElement message = JsonSerializer.Deserialize<JsonElement>(mess);
            JsonElement tempAnswer = message.GetProperty("Data");
            var answers = JsonSerializer.Deserialize<Answer[]>(tempAnswer);

            CorrectedAnswerMessage corrMess = new CorrectedAnswerMessage();
            var correctAnswer = _questions.SingleOrDefault(c => c.id == _currentQuestion.questionId).correctAnswer;
            corrMess.Data = answers;
            corrMess.Correctanswer = correctAnswer;
            for (int i = 0; i < answers.Length; i++)
            {
                var playerIndex = _players.FindIndex(c => c.Id == answers[i].playerId);
                if (answers[i].answer == correctAnswer)
                {                    
                    answers[i].correct = true;
                    corrMess.Data[i].newScore = _players[playerIndex].Score += 1;

                    if(_pointsToWin == _players[playerIndex].Score)
                    {
                        _players[playerIndex].isWinner = true;
                        _players[playerIndex].LastAnswerCorrect = true;

                        corrMess.Data[i].isWinner = true;
                        corrMess.Data[i].correct = true;
                    }
                }
                else
                {
                    _players[playerIndex].LastAnswerCorrect = false;
                    corrMess.Data[i].correct = false;
                    corrMess.Data[i].newScore = _players.SingleOrDefault(c => c.Id == answers[i].playerId).Score;
                }
            }

            byte[] buffer = new byte[1024];
          
            corrMess.Type = "CorrectedAnswers";
            
            buffer = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(corrMess));
            return buffer;
        }
        public Task<Player> GetPlayerAsunc(int id)
        {
            throw new NotImplementedException();
        }
        public QuestionContent GetQuestionAsync(int questionNumber)
        {
            return _questions[questionNumber - 1];
        }  
        private void CloseConnection()
        {
            
        }
        public async Task<byte[]> StartForstToPointGame(dynamic mess)
        {
            StartFirstToPointMessage FTPMessage = JsonSerializer.Deserialize<StartFirstToPointMessage>(mess);
            ContentModel model = FTPMessage.Data;
            _players = FTPMessage.Players;
            _pointsToWin = FTPMessage.PointsToWin;
            await PopulateQuestions(model);

            return GetQuestion();

        }
        public GameState StartGame()
        {
            _gameState = GameState.InProgress;
            return _gameState;
        }
        
    }

    
}

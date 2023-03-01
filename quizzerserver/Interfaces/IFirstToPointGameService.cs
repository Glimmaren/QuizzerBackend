using QuizzerLibary;
using WebSocketSharp.Server;

namespace QuizzerServer.Interfaces
{
    public interface IFirstToPointGameService
    {
     
        GameState StartGame();
        Task<bool> CheckAnswerAsync(Answer answer);
        QuestionContent GetQuestionAsync(int questionNumber);
        Task<Player> GetPlayerAsunc(int id);
        void SubmitAnswer(int playerNumber, string answer, string questionNumber);
    }
}

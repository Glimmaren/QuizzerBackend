using QuizzerLibary;

namespace QuizzerServer.Interfaces
{
    public interface IQuestionHandler
    {
        Task<List<QuestionContent>> GetQuestions(ContentModel model);
        
    }
}

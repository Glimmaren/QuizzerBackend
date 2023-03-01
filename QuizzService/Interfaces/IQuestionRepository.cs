using QuizzerLibary;

namespace QuizzService.Interfaces
{
    public interface IQuestionRepository  : IDisposable
    {
        Task<string[]> GetTags();
        Task<IList<Response>> GetQuestionsAsync(string[]? categorys, string diffyculty, string limit, string tags, string region);
    }
}

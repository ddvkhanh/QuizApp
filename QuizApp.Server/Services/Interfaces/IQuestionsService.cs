using QuizApp.Database.Models;


namespace QuizApp.Server.Services.Interfaces
{
    public interface IQuestionsService
    {
        Task<List<Question>> GetQuestionsAsync(bool random);
        Task<Question> GetQuestionByIdAsync(Guid id);
        Task AddQuestionAsync(Question question);
        Task UpdateQuestionAsync(Guid id, Question updatedQuestion);
        Task DeleteQuestionAsync(Guid id);
    }
}

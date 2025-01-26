using QuizApp.Database.Models;

namespace QuizApp.Server.Services.Interfaces
{
    public interface IResultsService
    {
        Task<List<QuizResult>> GetResultsAsync();
        Task AddResultAsync(QuizResult result);
    }
}

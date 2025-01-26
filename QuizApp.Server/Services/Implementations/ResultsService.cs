using QuizApp.Database.Models;
using QuizApp.Database.Repositories.Interfaces;
using QuizApp.Server.Services.Interfaces;

namespace QuizApp.Server.Services.Implementations
{
    public class ResultsService : IResultsService
    {
        private readonly IResultRepository _repository;
        public ResultsService(IResultRepository repository)
        {
            _repository = repository;
        }
        public async Task AddResultAsync(QuizResult result)
        {
            result.TakenAt = DateTime.UtcNow;
            await _repository.AddResultAsync(result);
        }

        public async Task<List<QuizResult>> GetResultsAsync()
        {
            return await _repository.GetResultsAsync();
        }
    }
}

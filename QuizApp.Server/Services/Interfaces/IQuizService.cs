using QuizApp.Server.Dtos;

namespace QuizApp.Server.Services.Interfaces
{
    public interface IQuizService
    {
        Task<int> CalculateScoreAsync(AnswerPayload answerPayload);
        Task SaveQuizResultAsync(int score);
    }
}

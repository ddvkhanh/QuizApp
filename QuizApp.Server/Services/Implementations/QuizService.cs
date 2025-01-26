using System;
using QuizApp.Database.Models;
using QuizApp.Database.Repositories.Interfaces;
using QuizApp.Server.Dtos;
using QuizApp.Server.Services.Interfaces;

namespace QuizApp.Server.Services.Implementations
{
    public class QuizService: IQuizService
    {
        private readonly IQuizRepository _repository;
        public QuizService(IQuizRepository repository) {
            _repository = repository;
        }

        public async Task<int> CalculateScoreAsync(AnswerPayload payload)
        {
            if (payload == null || payload.Answers == null || !payload.Answers.Any())

            {
                throw new ArgumentException("Answers cannot be empty");
            }

            var questionIds = payload.Answers.Select(a => a.QuestionId).ToList();
            var questions = await _repository.GetQuestionsByIdsAsync(questionIds);

            if (!questions.Any())
            {
                throw new KeyNotFoundException("No matching questions found");
            }

            int score = 0;
            foreach (var answer in payload.Answers)
            {
                var question = questions.FirstOrDefault((q) => q.Id == answer.QuestionId);

                if (question != null)
                {
                    var correctAnswer = question.CorrectAnswer.Split(",", StringSplitOptions.RemoveEmptyEntries);
                    var userAnswer = answer.Answer.Split(",", StringSplitOptions.RemoveEmptyEntries);

                    if (question.QuestionType == "single" && correctAnswer.SequenceEqual(userAnswer))
                    {
                        score++;
                    }
                    else if (question.QuestionType == "multiple" && correctAnswer.OrderBy(c => c).SequenceEqual(userAnswer.OrderBy(c => c)))
                    {
                        score++;
                    }
                }
            }
            return score;
        }

        public async Task SaveQuizResultAsync(int score)
        {
            var quizResult = new QuizResult
            {
                Id = Guid.NewGuid(),
                Score = score,
                TakenAt = DateTime.Now
            };
            await _repository.SaveQuizResultAsync(quizResult);

        }
    }
}

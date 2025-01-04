using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizApp.Database;
using QuizApp.Database.Models;
using QuizApp.Server.Dtos;

namespace QuizApp.Server.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class QuizController : Controller
    {
        private readonly QuizAppContext _appContext;
        private readonly ILogger<QuizController> _logger;

        public QuizController(QuizAppContext appContext, ILogger<QuizController> logger)
        {
            _appContext = appContext;
            _logger = logger;
        }

        [HttpPost("submit")]
        public async Task<IActionResult> SubmitAnswers([FromBody] AnswerPayload payload)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (payload == null || payload.Answers == null || !payload.Answers.Any())

            {
                return BadRequest(new { message = "Answers cannot be empty" });
            }

            Console.WriteLine(JsonSerializer.Serialize(payload));

            var questionsIds = payload.Answers.Select(a => a.QuestionId).ToList();
            var questions = await _appContext.Questions
                .Where(q => questionsIds.Contains(q.Id))
                .ToListAsync();

            if (!questions.Any())
            {
                return NotFound(new { message = "No matching questions found" });
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

            try
            {
                var quizResult = new QuizResult
                {
                    Id = Guid.NewGuid(),
                    Score = score,
                    TakenAt = DateTime.UtcNow
                };
                _appContext.Results.Add(quizResult);
                await _appContext.SaveChangesAsync();
                    return Ok(new { score });
            } catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while scoring questions.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Unable to score questions. Please try again later.");
            }

        } 
    }
}

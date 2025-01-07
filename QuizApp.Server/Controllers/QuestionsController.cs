using System;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizApp.Database;
using QuizApp.Database.Models;

namespace QuizApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly QuizAppContext _appContext;
        private readonly ILogger<QuestionsController> _logger;

        public QuestionsController(QuizAppContext appContext, ILogger<QuestionsController> logger)
        {
            _appContext = appContext;
            _logger = logger;
        }

        // GET: api/questions
        [HttpGet]
        public async Task<IActionResult> GetQuestions([FromQuery] bool random = false)
        {
            try
            {
                var questions = await _appContext.Questions.OrderBy(q => q.CreatedAt).ToListAsync();

                if (random)
                {
                    var randomIndex = new Random();
                    for (int i = questions.Count -1; i > 0; i--)
                    {
                        int j = randomIndex.Next(i + 1);
                        (questions[i], questions[j]) = (questions[j], questions[i]);
                    }
                }

                return Ok(questions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching questions.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Unable to fetch questions. Please try again later.");
            }
        }

        // GET: api/questions/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestion(Guid id)
        {
            try
            {
                var question = await _appContext.Questions.FindAsync(id);
                if (question == null)
                {
                    _logger.LogWarning("Question with id {Id} not found.", id);
                    return NotFound($"Question with ID {id} not found.");
                }

                return Ok(question);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the question with id {Id}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Unable to fetch the question. Please try again later.");
            }
        }

        // POST: api/questions
        [HttpPost]
        public async Task<IActionResult> AddQuestion([FromBody] Question question)
        {
            Console.WriteLine($"Received payload: {JsonSerializer.Serialize(question)}");

            try
            {
                if (question == null)
                {
                    _logger.LogWarning("Received a null Question payload.");
                    return BadRequest("Question data is null.");
                }

                question.CreatedAt = DateTime.Now;

                _appContext.Questions.Add(question);
                await _appContext.SaveChangesAsync();

                return CreatedAtAction(nameof(GetQuestion), new { id = question.Id }, question);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a question.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Unable to add the question. Please try again later.");
            }
        }

        // PUT: api/questions/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuestion(Guid id, [FromBody] Question updatedQuestion)
        {
            try
            {
                if (id != updatedQuestion.Id)
                {
                    _logger.LogWarning("Mismatch between route ID {Id} and payload ID {PayloadId}.", id, updatedQuestion.Id);
                    return BadRequest("ID mismatch.");
                }

                var existingQuestion = await _appContext.Questions.FindAsync(id);
                if (existingQuestion == null)
                {
                    _logger.LogWarning("Question with id {Id} not found.", id);
                    return NotFound($"Question with ID {id} not found.");
                }

                // Update properties
                existingQuestion.Description = updatedQuestion.Description;
                existingQuestion.CorrectAnswer = updatedQuestion.CorrectAnswer;
                existingQuestion.Options = updatedQuestion.Options;
                existingQuestion.QuestionType = updatedQuestion.QuestionType;

                await _appContext.SaveChangesAsync();

                return Ok(new { message = "Question updated successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the question with id {Id}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Unable to update the question. Please try again later.");
            }
        }

        // DELETE: api/questions/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(Guid id)
        {
            try
            {
                var question = await _appContext.Questions.FindAsync(id);
                if (question == null)
                {
                    _logger.LogWarning("Question with id {Id} not found.", id);
                    return NotFound($"Question with ID {id} not found.");
                }

                _appContext.Questions.Remove(question);
                await _appContext.SaveChangesAsync();

                return Ok(new { message = "Question deleted successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the question with id {Id}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Unable to delete the question. Please try again later.");
            }
        }
    }
}

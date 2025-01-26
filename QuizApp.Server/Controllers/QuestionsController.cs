using System;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizApp.Database;
using QuizApp.Database.Models;
using QuizApp.Server.Services.Implementations;
using QuizApp.Server.Services.Interfaces;

namespace QuizApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionsService _questionsService;
        private readonly ILogger<QuestionsController> _logger;

        public QuestionsController(IQuestionsService questionsService, ILogger<QuestionsController> logger)
        {
            _questionsService = questionsService;
            _logger = logger;
        }

        // GET: api/questions
        [HttpGet]
        public async Task<IActionResult> GetQuestions([FromQuery] bool random = false)
        {
            try
            {
                var questions = await _questionsService.GetQuestionsAsync(random);
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
                var question = await _questionsService.GetQuestionByIdAsync(id);
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
                await _questionsService.AddQuestionAsync(question);
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
                await _questionsService.UpdateQuestionAsync(id, updatedQuestion);
                return Ok(new { message = "Question updated successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
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
               await _questionsService.DeleteQuestionAsync(id);
               return Ok(new { message = "Question deleted successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the question with id {Id}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Unable to delete the question. Please try again later.");
            }
        }
    }
}

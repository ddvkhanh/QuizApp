using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizApp.Database;
using QuizApp.Database.Models;
using QuizApp.Server.Dtos;
using QuizApp.Server.Services.Implementations;
using QuizApp.Server.Services.Interfaces;

namespace QuizApp.Server.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class QuizController : Controller
    {
        private readonly QuizService _service;
        private readonly ILogger<QuizController> _logger;

        public QuizController(QuizService service, ILogger<QuizController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost("submit")]
        public async Task<IActionResult> SubmitAnswers([FromBody] AnswerPayload payload)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
        

            try
            {
                var score = await _service.CalculateScoreAsync(payload);
                await _service.SaveQuizResultAsync(score);
                return Ok(new { score });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the quiz.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Unable to process the quiz. Please try again later.");
            }

        } 
    }
}


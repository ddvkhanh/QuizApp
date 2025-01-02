using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizApp.Database.QuizAppContext;
using QuizApp.Database.Models;


namespace QuizApp.QuizApp.Server.Controllers
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

        //GET: api/questions
        [HttpGet]
        public async Task<IActionResult> GetQuestions()
        {
            try
            {
                var questions = await _appContext.Questions.ToListAsync();
                return Ok(questions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching questions.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        // POST: api/questions
        [HttpPost]
        public async Task<IActionResult> AddQuestion([FromBody] Question question)
        {
            try
            {
                if (question == null)
                {
                    _logger.LogWarning("Received a null Question");
                    return BadRequest("Question data is null");
                }
                _appContext.Questions.Add(question);
                await _appContext.SaveChangesAsync();
                return CreatedAtAction(nameof(GetQuestions), new { id = question.Id }, question);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured during AddQuestion");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }

        }
    }
}

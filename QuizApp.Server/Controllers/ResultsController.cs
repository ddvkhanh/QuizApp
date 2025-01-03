using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizApp.Database;
using QuizApp.Database.Models;

namespace QuizApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResultsController : ControllerBase
    {
        private readonly QuizAppContext _appContext;
        private readonly ILogger<ResultsController> _logger;

        public ResultsController(QuizAppContext appContext, ILogger<ResultsController> logger)
        {
            _appContext = appContext;
            _logger = logger;
        }

        //GET: api/results
        [HttpGet]
        public async Task<IActionResult> GetResults()
        {
            try
            {
                var results = await _appContext.Results.ToListAsync();
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting results.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Unable to get results. Please try again later.");
            }
        }


        //POST: api/results
        [HttpPost]
        public async Task<IActionResult> AddResult([FromBody] QuizResult result)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                result.TakenAt = DateTime.UtcNow;
                _appContext.Results.Add(result);
                await _appContext.SaveChangesAsync();
                return CreatedAtAction(nameof(GetResults), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding results.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Unable to submit result. Please try again later.");
            }
        }
    }
}

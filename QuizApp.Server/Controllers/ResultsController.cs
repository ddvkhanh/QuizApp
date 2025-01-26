using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizApp.Database;
using QuizApp.Database.Models;
using QuizApp.Server.Services.Interfaces;

namespace QuizApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ResultsController : ControllerBase
    {
        private readonly ILogger<ResultsController> _logger;
        private readonly IResultsService _service;

        public ResultsController(IResultsService service, ILogger<ResultsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        //GET: api/results
        [HttpGet]
        public async Task<IActionResult> GetResults()
        {
            try
            {
                var results = await _service.GetResultsAsync();
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
                await _service.AddResultAsync(result);
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


using Microsoft.AspNetCore.Mvc;
using SkillVerse.API.DTOs.Worker;
using SkillVerse.API.Services.Interfaces;

namespace SkillVerse.API.Controllers
{
    [Route("api/workers")]
    [ApiController]
    public class WorkerSearchController : ControllerBase
    {
        private readonly IWorkerService _workerService;

        public WorkerSearchController(IWorkerService workerService)
        {
            _workerService = workerService;
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchWorkers(
            [FromQuery] string? searchTerm = null,
            [FromQuery] string? city = null,
            [FromQuery] string? skill = null,
            [FromQuery] decimal? minRating = null)
        {
            var response = await _workerService.SearchWorkersAsync(searchTerm, city, skill, minRating);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWorkers()
        {
            var response = await _workerService.SearchWorkersAsync();
            return Ok(response);
        }
    }
}
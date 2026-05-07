
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillVerse.API.DTOs.Worker;
using SkillVerse.API.Helpers;
using SkillVerse.API.Services.Interfaces;

namespace SkillVerse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private readonly IWorkerService _workerService;

        public WorkerController(IWorkerService workerService)
        {
            _workerService = workerService;
        }


        [HttpGet("profile")]
        [Authorize(Roles = "Worker")]
        public async Task<IActionResult> GetProfile()
        {
            int userId = Convert.ToInt32(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);

            var response = await _workerService.GetWorkerProfileAsync(userId);
            return response.Success ? Ok(response) : NotFound(response);
        }

        [HttpPost("profile")]
        [Authorize(Roles = "Worker")]
        public async Task<IActionResult> SaveProfile([FromBody] WorkerProfileCreateDto dto)
        {
            int userId = Convert.ToInt32(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);

            var response = await _workerService.SaveWorkerProfileAsync(userId, dto);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPut("availability")]
        [Authorize(Roles = "Worker")]
        public async Task<IActionResult> ToggleAvailability([FromBody] bool isAvailable)
        {
            int userId = Convert.ToInt32(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);

            var response = await _workerService.ToggleAvailabilityAsync(userId, isAvailable);
            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}
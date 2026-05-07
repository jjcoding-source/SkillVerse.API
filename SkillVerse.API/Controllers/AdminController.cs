
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillVerse.API.Services.Interfaces;

namespace SkillVerse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboard()
        {
            var response = await _adminService.GetDashboardStatsAsync();
            return Ok(response);
        }

        [HttpPut("users/{userId}/status")]
        public async Task<IActionResult> ToggleUserStatus(int userId, [FromQuery] bool isActive)
        {
            var response = await _adminService.ToggleUserStatusAsync(userId, isActive);
            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}

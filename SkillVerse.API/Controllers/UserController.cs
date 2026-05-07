
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillVerse.API.DTOs.User;
using SkillVerse.API.Helpers;
using SkillVerse.API.Services.Interfaces;

namespace SkillVerse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]  
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            int userId = Convert.ToInt32(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);
            var response = await _userService.GetUserProfileAsync(userId);
            return response.Success ? Ok(response) : NotFound(response);
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UserProfileDto dto)
        {
            int userId = Convert.ToInt32(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);
            var response = await _userService.UpdateUserProfileAsync(userId, dto);
            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}
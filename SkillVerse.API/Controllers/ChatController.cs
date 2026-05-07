
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillVerse.API.DTOs.Chat;
using SkillVerse.API.Services.Interfaces;

namespace SkillVerse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageDto dto)
        {
            int senderId = Convert.ToInt32(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);

            var response = await _chatService.SendMessageAsync(senderId, dto);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("booking/{bookingId}")]
        public async Task<IActionResult> GetMessages(int bookingId)
        {
            var response = await _chatService.GetConversationMessagesAsync(bookingId);
            return Ok(response);
        }
    }
}
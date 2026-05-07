
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillVerse.API.DTOs.Review;
using SkillVerse.API.Services.Interfaces;

namespace SkillVerse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> AddReview([FromBody] ReviewCreateDto dto)
        {
            int customerId = Convert.ToInt32(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);

            var response = await _reviewService.AddReviewAsync(customerId, dto);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("worker/{workerId}")]
        public async Task<IActionResult> GetWorkerReviews(int workerId)
        {
            var response = await _reviewService.GetWorkerReviewsAsync(workerId);
            return Ok(response);
        }
    }
}
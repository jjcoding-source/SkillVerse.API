
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillVerse.API.DTOs.Booking;
using SkillVerse.API.Services.Interfaces;

namespace SkillVerse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> CreateBooking([FromBody] BookingCreateDto dto)
        {
            int customerId = Convert.ToInt32(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);

            var response = await _bookingService.CreateBookingAsync(customerId, dto);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("my-bookings")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetMyBookings([FromQuery] string? status = null)
        {
            int customerId = Convert.ToInt32(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);

            var response = await _bookingService.GetCustomerBookingsAsync(customerId, status);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("worker-bookings")]
        [Authorize(Roles = "Worker")]
        public async Task<IActionResult> GetWorkerBookings([FromQuery] string? status = null)
        {
            int workerUserId = Convert.ToInt32(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);

            var response = await _bookingService.GetWorkerBookingsAsync(workerUserId, status);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPut("{bookingId}/status")]
        [Authorize]
        public async Task<IActionResult> UpdateStatus(int bookingId, [FromBody] UpdateBookingStatusDto dto)
        {
            int userId = Convert.ToInt32(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);
            string role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value ?? "";

            var response = await _bookingService.UpdateBookingStatusAsync(
                bookingId,
                role == "Worker" ? userId : null,
                dto.Status,
                dto.CancelReason
            );

            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}
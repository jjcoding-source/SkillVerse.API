
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
            return Ok(response);
        }

        [HttpGet("worker-bookings")]
        [Authorize(Roles = "Worker")]
        public async Task<IActionResult> GetWorkerBookings([FromQuery] string? status = null)
        {
            int userId = Convert.ToInt32(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);
  
            var response = await _bookingService.GetWorkerBookingsAsync(userId, status);
            return Ok(response);
        }

        [HttpGet("{bookingId}")]
        [Authorize]
        public async Task<IActionResult> GetBookingDetails(int bookingId)
        {
            var response = await _bookingService.GetBookingDetailsByIdAsync(bookingId);
            return Ok(response);
        }

        [HttpPut("{bookingId}/accept")]
        [Authorize(Roles = "Worker")]
        public async Task<IActionResult> AcceptBooking(int bookingId)
        {
            int workerUserId = Convert.ToInt32(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);

            var response = await _bookingService.AcceptBookingAsync(bookingId, workerUserId);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPut("{bookingId}/reject")]
        [Authorize(Roles = "Worker")]
        public async Task<IActionResult> RejectBooking(int bookingId, [FromBody] RejectBookingDto dto)
        {
            var response = await _bookingService.RejectBookingAsync(bookingId, dto.CancelReason);
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
                dto.CancelReason);

            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}
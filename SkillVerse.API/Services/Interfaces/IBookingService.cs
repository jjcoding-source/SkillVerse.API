

using SkillVerse.API.DTOs.Booking;
using SkillVerse.API.DTOs.Common;
using System.Data;

namespace SkillVerse.API.Services.Interfaces
{
    public interface IBookingService
    {
        Task<ApiResponse<int>> CreateBookingAsync(int customerId, BookingCreateDto dto);
        Task<ApiResponse<List<BookingResponseDto>>> GetCustomerBookingsAsync(int customerId, string? status = null);
        Task<ApiResponse<List<BookingResponseDto>>> GetWorkerBookingsAsync(int workerId, string? status = null);
        Task<ApiResponse<DataTable>> GetBookingDetailsByIdAsync(int bookingId);

        Task<ApiResponse<bool>> UpdateBookingStatusAsync(int bookingId, int? workerId, string newStatus, string? cancelReason = null);

        Task<ApiResponse<bool>> AcceptBookingAsync(int bookingId, int workerId);
        Task<ApiResponse<bool>> RejectBookingAsync(int bookingId, string cancelReason);
    }
}
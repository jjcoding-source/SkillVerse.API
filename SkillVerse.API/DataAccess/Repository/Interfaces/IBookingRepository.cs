
using System.Data;
using SkillVerse.API.DTOs.Booking;

namespace SkillVerse.API.DataAccess.Repository.Interfaces
{
    public interface IBookingRepository
    {
        Task<object?> CreateBookingAsync(int customerId, BookingCreateDto dto);
        Task<DataTable> GetCustomerBookingsAsync(int customerId, string? status = null);
        Task<DataTable> GetWorkerBookingsAsync(int workerId, string? status = null);
        Task<int> UpdateBookingStatusAsync(int bookingId, int? workerId, string newStatus, string? cancelReason = null);
        Task<int> AcceptBookingAsync(int bookingId, int workerId);
        Task<int> RejectBookingAsync(int bookingId, string cancelReason);
    }
}


using System.Data;
using Microsoft.Data.SqlClient;
using SkillVerse.API.DataAccess.Repository.Interfaces;
using SkillVerse.API.DTOs.Booking;

namespace SkillVerse.API.DataAccess.Repository.Implementations
{
    public class BookingRepository : IBookingRepository
    {
        private readonly IBaseRepository _baseRepository;

        public BookingRepository(IBaseRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public Task<object?> CreateBookingAsync(int customerId, BookingCreateDto dto)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@CustomerID", customerId),
                new SqlParameter("@ServiceID", dto.ServiceID),
                new SqlParameter("@BookingDate", dto.BookingDate),
                new SqlParameter("@PreferredTimeSlot", dto.PreferredTimeSlot ?? (object)DBNull.Value),
                new SqlParameter("@Address", dto.Address),
                new SqlParameter("@City", dto.City),
                new SqlParameter("@PinCode", dto.PinCode ?? (object)DBNull.Value),
                new SqlParameter("@Description", dto.Description ?? (object)DBNull.Value),
                new SqlParameter("@TotalAmount", dto.TotalAmount)
            };

            return _baseRepository.ExecuteScalarAsync("sp_CreateBooking", parameters);
        }

        public Task<DataTable> GetCustomerBookingsAsync(int customerId, string? status = null)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@CustomerID", customerId),
                new SqlParameter("@Status", status ?? (object)DBNull.Value)
            };

            return _baseRepository.ExecuteDataTableAsync("sp_GetCustomerBookings", parameters);
        }

        public Task<DataTable> GetWorkerBookingsAsync(int workerId, string? status = null)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@WorkerID", workerId),
                new SqlParameter("@Status", status ?? (object)DBNull.Value)
            };

            return _baseRepository.ExecuteDataTableAsync("sp_GetWorkerBookings", parameters);
        }

        public Task<int> UpdateBookingStatusAsync(int bookingId, int? workerId, string newStatus, string? cancelReason = null)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@BookingID", bookingId),
                new SqlParameter("@WorkerID", workerId ?? (object)DBNull.Value),
                new SqlParameter("@NewStatus", newStatus),
                new SqlParameter("@CancelReason", cancelReason ?? (object)DBNull.Value)
            };

            return _baseRepository.ExecuteNonQueryAsync("sp_UpdateBookingStatus", parameters);
        }

        public Task<DataTable> GetBookingDetailsByIdAsync(int bookingId)
        {
            SqlParameter[] parameters = { new SqlParameter("@BookingID", bookingId) };
            return _baseRepository.ExecuteDataTableAsync("sp_GetBookingDetailsById", parameters);
        }

        public Task<int> AcceptBookingAsync(int bookingId, int workerId)
        {
            SqlParameter[] parameters =
            {
        new SqlParameter("@BookingID", bookingId),
        new SqlParameter("@WorkerID", workerId)
    };
            return _baseRepository.ExecuteNonQueryAsync("sp_AcceptBooking", parameters);
        }

        public Task<int> RejectBookingAsync(int bookingId, string cancelReason)
        {
            SqlParameter[] parameters =
            {
        new SqlParameter("@BookingID", bookingId),
        new SqlParameter("@CancelReason", cancelReason)
    };
            return _baseRepository.ExecuteNonQueryAsync("sp_RejectBooking", parameters);
        }

    }
}
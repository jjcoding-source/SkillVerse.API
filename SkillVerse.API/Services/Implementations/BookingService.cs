
using System.Data;
using Microsoft.Data.SqlClient;
using SkillVerse.API.DataAccess;
using SkillVerse.API.DTOs.Booking;
using SkillVerse.API.DTOs.Common;
using SkillVerse.API.Services.Interfaces;

namespace SkillVerse.API.Services.Implementations
{
    public class BookingService : IBookingService
    {
        private readonly DbHelper _dbHelper;

        public BookingService(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<ApiResponse<int>> CreateBookingAsync(int customerId, BookingCreateDto dto)
        {
            try
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

                object? result = await _dbHelper.ExecuteScalarAsync("sp_CreateBooking", parameters);

                int bookingId = Convert.ToInt32(result);
                return ApiResponse<int>.SuccessResponse(bookingId, "Booking created successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<int>.ErrorResponse("Failed to create booking", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponse<List<BookingResponseDto>>> GetCustomerBookingsAsync(int customerId, string? status = null)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@CustomerID", customerId),
                    new SqlParameter("@Status", status ?? (object)DBNull.Value)
                };

                DataTable dt = await _dbHelper.ExecuteDataTableAsync("sp_GetCustomerBookings", parameters);

                var bookings = new List<BookingResponseDto>();

                foreach (DataRow row in dt.Rows)
                {
                    bookings.Add(new BookingResponseDto
                    {
                        BookingID = Convert.ToInt32(row["BookingID"]),
                        CustomerID = Convert.ToInt32(row["CustomerID"]),
                        WorkerID = row["WorkerID"] != DBNull.Value ? Convert.ToInt32(row["WorkerID"]) : null,
                        ServiceName = row["ServiceName"].ToString() ?? "",
                        WorkerName = row["WorkerName"]?.ToString() ?? "",
                        BookingDate = Convert.ToDateTime(row["BookingDate"]),
                        PreferredTimeSlot = row["PreferredTimeSlot"]?.ToString(),
                        Address = row["Address"].ToString() ?? "",
                        City = row["City"].ToString() ?? "",
                        Status = row["Status"].ToString() ?? "",
                        TotalAmount = Convert.ToDecimal(row["TotalAmount"]),
                        CreatedAt = Convert.ToDateTime(row["CreatedAt"])
                    });
                }

                return ApiResponse<List<BookingResponseDto>>.SuccessResponse(bookings);
            }
            catch (Exception ex)
            {
                return ApiResponse<List<BookingResponseDto>>.ErrorResponse(ex.Message);
            }
        }

        public async Task<ApiResponse<List<BookingResponseDto>>> GetWorkerBookingsAsync(int workerId, string? status = null)
        {
            throw new NotImplementedException("Implement similar to GetCustomerBookings");
        }

        public async Task<ApiResponse<bool>> UpdateBookingStatusAsync(int bookingId, int? workerId, string newStatus, string? cancelReason = null)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@BookingID", bookingId),
                    new SqlParameter("@WorkerID", workerId ?? (object)DBNull.Value),
                    new SqlParameter("@NewStatus", newStatus),
                    new SqlParameter("@CancelReason", cancelReason ?? (object)DBNull.Value)
                };

                await _dbHelper.ExecuteNonQueryAsync("sp_UpdateBookingStatus", parameters);

                return ApiResponse<bool>.SuccessResponse(true, "Booking status updated");
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResponse(ex.Message);
            }
        }
    }
}
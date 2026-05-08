using System.Data;
using SkillVerse.API.DataAccess.Repository.Interfaces;
using SkillVerse.API.DTOs.Booking;
using SkillVerse.API.DTOs.Common;
using SkillVerse.API.Services.Interfaces;

namespace SkillVerse.API.Services.Implementations
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<ApiResponse<int>> CreateBookingAsync(int customerId, BookingCreateDto dto)
        {
            try
            {
                var result = await _bookingRepository.CreateBookingAsync(customerId, dto);
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
                var dt = await _bookingRepository.GetCustomerBookingsAsync(customerId, status);
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
            try
            {
                var dt = await _bookingRepository.GetWorkerBookingsAsync(workerId, status);
                var bookings = new List<BookingResponseDto>();

                foreach (DataRow row in dt.Rows)
                {
                    bookings.Add(new BookingResponseDto
                    {
                        BookingID = Convert.ToInt32(row["BookingID"]),
                        CustomerID = Convert.ToInt32(row["CustomerID"]),
                        ServiceName = row["ServiceName"].ToString() ?? "",
                        BookingDate = Convert.ToDateTime(row["BookingDate"]),
                        PreferredTimeSlot = row["PreferredTimeSlot"]?.ToString(),
                        Address = row["Address"].ToString() ?? "",
                        City = row["City"].ToString() ?? "",
                        TotalAmount = Convert.ToDecimal(row["TotalAmount"]),
                        Status = row["Status"].ToString() ?? "",
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

        public async Task<ApiResponse<bool>> AcceptBookingAsync(int bookingId, int workerId)
        {
            try
            {
                await _bookingRepository.AcceptBookingAsync(bookingId, workerId);
                return ApiResponse<bool>.SuccessResponse(true, "Booking accepted successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResponse(ex.Message);
            }
        }

        public async Task<ApiResponse<bool>> RejectBookingAsync(int bookingId, string cancelReason)
        {
            try
            {
                await _bookingRepository.RejectBookingAsync(bookingId, cancelReason);
                return ApiResponse<bool>.SuccessResponse(true, "Booking rejected successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResponse(ex.Message);
            }
        }

        public async Task<ApiResponse<bool>> UpdateBookingStatusAsync(int bookingId, int? workerId, string newStatus, string? cancelReason = null)
        {
            try
            {
                await _bookingRepository.UpdateBookingStatusAsync(bookingId, workerId, newStatus, cancelReason);
                return ApiResponse<bool>.SuccessResponse(true, "Status updated successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResponse(ex.Message);
            }
        }

        public async Task<ApiResponse<DataTable>> GetBookingDetailsByIdAsync(int bookingId)
        {
            try
            {
                
                var dt = await _bookingRepository.GetCustomerBookingsAsync(bookingId);
                return ApiResponse<DataTable>.SuccessResponse(dt);
            }
            catch (Exception ex)
            {
                return ApiResponse<DataTable>.ErrorResponse(ex.Message);
            }
        }
    }
}
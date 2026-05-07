
using System.Data;
using Microsoft.Data.SqlClient;
using SkillVerse.API.DataAccess;
using SkillVerse.API.DTOs.Common;
using SkillVerse.API.DTOs.Review;
using SkillVerse.API.Services.Interfaces;

namespace SkillVerse.API.Services.Implementations
{
    public class ReviewService : IReviewService
    {
        private readonly DbHelper _dbHelper;

        public ReviewService(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<ApiResponse<bool>> AddReviewAsync(int customerId, ReviewCreateDto dto)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@BookingID", dto.BookingID),
                    new SqlParameter("@CustomerID", customerId),
                    new SqlParameter("@Rating", dto.Rating),
                    new SqlParameter("@Comment", dto.Comment ?? (object)DBNull.Value)
                };

                await _dbHelper.ExecuteNonQueryAsync("sp_AddReview", parameters);

                return ApiResponse<bool>.SuccessResponse(true, "Review submitted successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResponse("Failed to submit review", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponse<List<ReviewDto>>> GetWorkerReviewsAsync(int workerId)
        {
            try
            {
                return ApiResponse<List<ReviewDto>>.SuccessResponse(new List<ReviewDto>());
            }
            catch (Exception ex)
            {
                return ApiResponse<List<ReviewDto>>.ErrorResponse(ex.Message);
            }
        }
    }
}
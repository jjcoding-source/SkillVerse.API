
using SkillVerse.API.DTOs.Common;
using SkillVerse.API.DTOs.Review;

namespace SkillVerse.API.Services.Interfaces
{
    public interface IReviewService
    {
        Task<ApiResponse<bool>> AddReviewAsync(int customerId, ReviewCreateDto dto);
        Task<ApiResponse<List<ReviewDto>>> GetWorkerReviewsAsync(int workerId);
    }
}
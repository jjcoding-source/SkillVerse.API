
using System.Data;
using SkillVerse.API.DTOs.Review;

namespace SkillVerse.API.DataAccess.Repository.Interfaces
{
    public interface IReviewRepository
    {
        Task<int> AddReviewAsync(int customerId, ReviewCreateDto dto);
    }
}

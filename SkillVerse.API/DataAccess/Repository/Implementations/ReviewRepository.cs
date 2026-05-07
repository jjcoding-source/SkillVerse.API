
using System.Data;
using Microsoft.Data.SqlClient;
using SkillVerse.API.DataAccess.Repository.Interfaces;
using SkillVerse.API.DTOs.Review;

namespace SkillVerse.API.DataAccess.Repository.Implementations
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly IBaseRepository _baseRepository;

        public ReviewRepository(IBaseRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public Task<int> AddReviewAsync(int customerId, ReviewCreateDto dto)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@BookingID", dto.BookingID),
                new SqlParameter("@CustomerID", customerId),
                new SqlParameter("@Rating", dto.Rating),
                new SqlParameter("@Comment", dto.Comment ?? (object)DBNull.Value)
            };

            return _baseRepository.ExecuteNonQueryAsync("sp_AddReview", parameters);
        }
    }
}

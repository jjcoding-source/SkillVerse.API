
using System.Data;
using Microsoft.Data.SqlClient;
using SkillVerse.API.DataAccess.Repository.Interfaces;
using SkillVerse.API.DTOs.Worker;

namespace SkillVerse.API.DataAccess.Repository.Implementations
{
    public class WorkerRepository : IWorkerRepository
    {
        private readonly IBaseRepository _baseRepository;

        public WorkerRepository(IBaseRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public Task<DataTable> GetWorkerProfileAsync(int userId)
        {
            SqlParameter[] parameters = { new SqlParameter("@UserID", userId) };
            return _baseRepository.ExecuteDataTableAsync("sp_GetWorkerProfile", parameters);
        }

        public Task<int> SaveWorkerProfileAsync(int userId, WorkerProfileCreateDto dto)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@UserID", userId),
                new SqlParameter("@Skills", dto.Skills),
                new SqlParameter("@ExperienceYears", dto.ExperienceYears ?? (object)DBNull.Value),
                new SqlParameter("@HourlyRate", dto.HourlyRate ?? (object)DBNull.Value),
                new SqlParameter("@Description", dto.Description ?? (object)DBNull.Value),
                new SqlParameter("@Address", dto.Address),
                new SqlParameter("@City", dto.City)
            };

            return _baseRepository.ExecuteNonQueryAsync("sp_SaveWorkerProfile", parameters);
        }

        public Task<int> ToggleAvailabilityAsync(int userId, bool isAvailable)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@UserID", userId),
                new SqlParameter("@IsAvailable", isAvailable)
            };

            return _baseRepository.ExecuteNonQueryAsync("sp_ToggleWorkerAvailability", parameters);
        }

        public Task<DataTable> SearchWorkersAsync(string? searchTerm, string? city, string? skill, decimal? minRating)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@SearchTerm", searchTerm ?? (object)DBNull.Value),
                new SqlParameter("@City", city ?? (object)DBNull.Value),
                new SqlParameter("@Skill", skill ?? (object)DBNull.Value),
                new SqlParameter("@MinRating", minRating ?? (object)DBNull.Value)
            };

            return _baseRepository.ExecuteDataTableAsync("sp_SearchWorkers", parameters);
        }
    }
}

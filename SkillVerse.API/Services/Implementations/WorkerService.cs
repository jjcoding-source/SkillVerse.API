
using System.Data;
using Microsoft.Data.SqlClient;
using SkillVerse.API.DataAccess;
using SkillVerse.API.DTOs.Common;
using SkillVerse.API.DTOs.Worker;
using SkillVerse.API.Services.Interfaces;

namespace SkillVerse.API.Services.Implementations
{
    public class WorkerService : IWorkerService
    {
        private readonly DbHelper _dbHelper;

        public WorkerService(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<ApiResponse<WorkerProfileDto>> GetWorkerProfileAsync(int userId)
        {
            try
            {
                SqlParameter[] parameters = { new SqlParameter("@UserID", userId) };

                DataTable dt = await _dbHelper.ExecuteDataTableAsync("sp_GetWorkerProfile", parameters);

                if (dt.Rows.Count == 0)
                    return ApiResponse<WorkerProfileDto>.ErrorResponse("Worker profile not found");

                DataRow row = dt.Rows[0];

                var profile = new WorkerProfileDto
                {
                    WorkerID = Convert.ToInt32(row["WorkerID"]),
                    UserID = Convert.ToInt32(row["UserID"]),
                    FullName = row["FullName"].ToString() ?? "",
                    Skills = row["Skills"]?.ToString() ?? "",
                    ExperienceYears = row["ExperienceYears"] != DBNull.Value ? Convert.ToInt32(row["ExperienceYears"]) : null,
                    HourlyRate = row["HourlyRate"] != DBNull.Value ? Convert.ToDecimal(row["HourlyRate"]) : null,
                    Description = row["Description"]?.ToString(),
                    Address = row["Address"]?.ToString() ?? "",
                    City = row["City"]?.ToString() ?? "",
                    IsAvailable = Convert.ToBoolean(row["IsAvailable"]),
                    Rating = Convert.ToDecimal(row["Rating"]),
                    TotalJobsCompleted = Convert.ToInt32(row["TotalJobsCompleted"])
                };

                return ApiResponse<WorkerProfileDto>.SuccessResponse(profile);
            }
            catch (Exception ex)
            {
                return ApiResponse<WorkerProfileDto>.ErrorResponse(ex.Message);
            }
        }

        public async Task<ApiResponse<bool>> SaveWorkerProfileAsync(int userId, WorkerProfileCreateDto dto)
        {
            try
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

                await _dbHelper.ExecuteNonQueryAsync("sp_SaveWorkerProfile", parameters);

                return ApiResponse<bool>.SuccessResponse(true, "Worker profile saved successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResponse(ex.Message);
            }
        }

        public async Task<ApiResponse<bool>> ToggleAvailabilityAsync(int userId, bool isAvailable)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@UserID", userId),
                    new SqlParameter("@IsAvailable", isAvailable)
                };

                await _dbHelper.ExecuteNonQueryAsync("sp_ToggleWorkerAvailability", parameters);

                return ApiResponse<bool>.SuccessResponse(true, "Availability updated");
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResponse(ex.Message);
            }
        }

        public async Task<ApiResponse<List<WorkerSearchDto>>> SearchWorkersAsync(
    string? searchTerm = null,
    string? city = null,
    string? skill = null,
    decimal? minRating = null)
        {
            try
            {
                SqlParameter[] parameters =
                {
            new SqlParameter("@SearchTerm", searchTerm ?? (object)DBNull.Value),
            new SqlParameter("@City", city ?? (object)DBNull.Value),
            new SqlParameter("@Skill", skill ?? (object)DBNull.Value),
            new SqlParameter("@MinRating", minRating ?? (object)DBNull.Value)
        };

                DataTable dt = await _dbHelper.ExecuteDataTableAsync("sp_SearchWorkers", parameters);

                var workers = new List<WorkerSearchDto>();

                foreach (DataRow row in dt.Rows)
                {
                    workers.Add(new WorkerSearchDto
                    {
                        WorkerID = Convert.ToInt32(row["WorkerID"]),
                        UserID = Convert.ToInt32(row["UserID"]),
                        FullName = row["FullName"].ToString() ?? "",
                        ProfileImage = row["ProfileImage"]?.ToString(),
                        Skills = row["Skills"]?.ToString() ?? "",
                        City = row["City"].ToString() ?? "",
                        Rating = Convert.ToDecimal(row["Rating"]),
                        TotalJobsCompleted = Convert.ToInt32(row["TotalJobsCompleted"]),
                        HourlyRate = row["HourlyRate"] != DBNull.Value ? Convert.ToDecimal(row["HourlyRate"]) : null,
                        IsAvailable = Convert.ToBoolean(row["IsAvailable"])
                    });
                }

                return ApiResponse<List<WorkerSearchDto>>.SuccessResponse(workers);
            }
            catch (Exception ex)
            {
                return ApiResponse<List<WorkerSearchDto>>.ErrorResponse(ex.Message);
            }
        }

    }
}
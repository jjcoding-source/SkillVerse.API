
using SkillVerse.API.DataAccess.Repository.Interfaces;
using SkillVerse.API.DTOs.Common;
using SkillVerse.API.DTOs.Worker;
using SkillVerse.API.Services.Interfaces;
using System.Data;

namespace SkillVerse.API.Services.Implementations
{
    public class WorkerService : IWorkerService
    {
        private readonly IWorkerRepository _workerRepository;

        public WorkerService(IWorkerRepository workerRepository)
        {
            _workerRepository = workerRepository;
        }

        public async Task<ApiResponse<WorkerProfileDto>> GetWorkerProfileAsync(int userId)
        {
            try
            {
                var dt = await _workerRepository.GetWorkerProfileAsync(userId);

                if (dt.Rows.Count == 0)
                    return ApiResponse<WorkerProfileDto>.ErrorResponse("Worker profile not found");

                var row = dt.Rows[0];

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
                await _workerRepository.SaveWorkerProfileAsync(userId, dto);
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
                await _workerRepository.ToggleAvailabilityAsync(userId, isAvailable);
                return ApiResponse<bool>.SuccessResponse(true, "Availability updated successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResponse(ex.Message);
            }
        }

        public async Task<ApiResponse<List<WorkerSearchDto>>> SearchWorkersAsync(
            string? searchTerm = null, string? city = null, string? skill = null, decimal? minRating = null)
        {
            try
            {
                var dt = await _workerRepository.SearchWorkersAsync(searchTerm, city, skill, minRating);

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
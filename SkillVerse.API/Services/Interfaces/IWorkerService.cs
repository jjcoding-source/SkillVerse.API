
using SkillVerse.API.DTOs.Common;
using SkillVerse.API.DTOs.Worker;

namespace SkillVerse.API.Services.Interfaces
{
    public interface IWorkerService
    {
        Task<ApiResponse<WorkerProfileDto>> GetWorkerProfileAsync(int userId);
        Task<ApiResponse<bool>> SaveWorkerProfileAsync(int userId, WorkerProfileCreateDto dto);
        Task<ApiResponse<bool>> ToggleAvailabilityAsync(int userId, bool isAvailable);
    }
}
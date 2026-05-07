
using System.Data;
using SkillVerse.API.DTOs.Worker;

namespace SkillVerse.API.DataAccess.Repository.Interfaces
{
    public interface IWorkerRepository
    {
        Task<DataTable> GetWorkerProfileAsync(int userId);
        Task<int> SaveWorkerProfileAsync(int userId, WorkerProfileCreateDto dto);
        Task<int> ToggleAvailabilityAsync(int userId, bool isAvailable);
        Task<DataTable> SearchWorkersAsync(string? searchTerm, string? city, string? skill, decimal? minRating);
    }
}
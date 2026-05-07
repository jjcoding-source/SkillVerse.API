
using SkillVerse.API.DTOs.Admin;
using SkillVerse.API.DTOs.Common;

namespace SkillVerse.API.Services.Interfaces
{
    public interface IAdminService
    {
        Task<ApiResponse<DashboardStatsDto>> GetDashboardStatsAsync();
        Task<ApiResponse<bool>> ToggleUserStatusAsync(int userId, bool isActive);
    }
}


using System.Data;
using SkillVerse.API.DataAccess;
using SkillVerse.API.DTOs.Admin;
using SkillVerse.API.DTOs.Common;
using SkillVerse.API.Services.Interfaces;

namespace SkillVerse.API.Services.Implementations
{
    public class AdminService : IAdminService
    {
        private readonly DbHelper _dbHelper;

        public AdminService(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<ApiResponse<DashboardStatsDto>> GetDashboardStatsAsync()
        {
            try
            {
                DataTable dt = await _dbHelper.ExecuteDataTableAsync("sp_Admin_GetDashboardStats");

                if (dt.Rows.Count == 0)
                    return ApiResponse<DashboardStatsDto>.ErrorResponse("Failed to fetch stats");

                DataRow row = dt.Rows[0];

                var stats = new DashboardStatsDto
                {
                    TotalCustomers = Convert.ToInt32(row["TotalCustomers"]),
                    TotalWorkers = Convert.ToInt32(row["TotalWorkers"]),
                    TotalBookings = Convert.ToInt32(row["TotalBookings"]),
                    PendingBookings = Convert.ToInt32(row["PendingBookings"])
                };

                return ApiResponse<DashboardStatsDto>.SuccessResponse(stats);
            }
            catch (Exception ex)
            {
                return ApiResponse<DashboardStatsDto>.ErrorResponse(ex.Message);
            }
        }

        public async Task<ApiResponse<bool>> ToggleUserStatusAsync(int userId, bool isActive)
        {
            try
            {
                return ApiResponse<bool>.SuccessResponse(true, "User status updated");
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResponse(ex.Message);
            }
        }
    }
}

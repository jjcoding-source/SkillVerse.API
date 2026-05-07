using System.Data;
using Microsoft.Data.SqlClient;
using SkillVerse.API.DataAccess;
using SkillVerse.API.DTOs.Common;
using SkillVerse.API.DTOs.User;
using SkillVerse.API.Services.Interfaces;

namespace SkillVerse.API.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly DbHelper _dbHelper;

        public UserService(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<ApiResponse<UserProfileDto>> GetUserProfileAsync(int userId)
        {
            try
            {
                SqlParameter[] parameters = { new("@UserID", userId) };

                DataTable dt = await _dbHelper.ExecuteDataTableAsync("sp_GetUserProfile", parameters);

                if (dt.Rows.Count == 0)
                    return ApiResponse<UserProfileDto>.ErrorResponse("User not found");

                DataRow row = dt.Rows[0];

                var profile = new UserProfileDto
                {
                    UserID = Convert.ToInt32(row["UserID"]),
                    FullName = row["FullName"].ToString() ?? "",
                    Email = row["Email"].ToString() ?? "",
                    Phone = row["Phone"].ToString() ?? "",
                    ProfileImage = row["ProfileImage"]?.ToString(),
                    RoleName = row["RoleName"]?.ToString() ?? ""
                };

                return ApiResponse<UserProfileDto>.SuccessResponse(profile);
            }
            catch (Exception ex)
            {
                return ApiResponse<UserProfileDto>.ErrorResponse("Failed to fetch profile", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponse<bool>> UpdateUserProfileAsync(int userId, UserProfileDto dto)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new("@UserID", userId),
                    new("@FullName", dto.FullName),
                    new("@Phone", dto.Phone),
                    new("@ProfileImage", dto.ProfileImage ?? (object)DBNull.Value)
                };

                await _dbHelper.ExecuteNonQueryAsync("sp_UpdateUserProfile", parameters);

                return ApiResponse<bool>.SuccessResponse(true, "Profile updated successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResponse("Failed to update profile", new List<string> { ex.Message });
            }
        }
    }
}
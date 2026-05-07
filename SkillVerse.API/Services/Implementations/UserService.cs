
using SkillVerse.API.DataAccess.Repository.Interfaces;
using SkillVerse.API.DTOs.Common;
using SkillVerse.API.DTOs.User;
using SkillVerse.API.Services.Interfaces;

namespace SkillVerse.API.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ApiResponse<UserProfileDto>> GetUserProfileAsync(int userId)
        {
            try
            {
                var dt = await _userRepository.GetUserProfileAsync(userId);

                if (dt.Rows.Count == 0)
                    return ApiResponse<UserProfileDto>.ErrorResponse("User not found");

                var row = dt.Rows[0];

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
                return ApiResponse<UserProfileDto>.ErrorResponse(ex.Message);
            }
        }

        public async Task<ApiResponse<bool>> UpdateUserProfileAsync(int userId, UserProfileDto dto)
        {
            try
            {
                await _userRepository.UpdateUserProfileAsync(userId, dto);
                return ApiResponse<bool>.SuccessResponse(true, "Profile updated successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResponse(ex.Message);
            }
        }
    }
}
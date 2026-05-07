
using SkillVerse.API.DTOs.Common;
using SkillVerse.API.DTOs.User;

namespace SkillVerse.API.Services.Interfaces
{
    public interface IUserService
    {
        Task<ApiResponse<UserProfileDto>> GetUserProfileAsync(int userId);
        Task<ApiResponse<bool>> UpdateUserProfileAsync(int userId, UserProfileDto dto);
    }
}
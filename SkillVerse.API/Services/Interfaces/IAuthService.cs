
using SkillVerse.API.DTOs.Auth;
using SkillVerse.API.DTOs.Common;

namespace SkillVerse.API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse<LoginResponseDto>> LoginAsync(LoginDto loginDto);
        Task<ApiResponse<object>> RegisterAsync(RegisterDto registerDto);
    }
}
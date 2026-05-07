
using SkillVerse.API.DataAccess.Repository.Interfaces;
using SkillVerse.API.DTOs.Auth;
using SkillVerse.API.DTOs.Common;
using SkillVerse.API.Helpers;
using SkillVerse.API.Services.Interfaces;

namespace SkillVerse.API.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtHelper _jwtHelper;

        public AuthService(IUserRepository userRepository, JwtHelper jwtHelper)
        {
            _userRepository = userRepository;
            _jwtHelper = jwtHelper;
        }

        public async Task<ApiResponse<LoginResponseDto>> LoginAsync(LoginDto loginDto)
        {
            try
            {
                var dt = await _userRepository.LoginUserAsync(loginDto.Email, loginDto.Password);

                if (dt.Rows.Count == 0)
                {
                    return ApiResponse<LoginResponseDto>.ErrorResponse("Invalid email or password");
                }

                var row = dt.Rows[0];

                var loginResponse = new LoginResponseDto
                {
                    UserID = Convert.ToInt32(row["UserID"]),
                    FullName = row["FullName"].ToString() ?? "",
                    Email = row["Email"].ToString() ?? "",
                    RoleName = row["RoleName"].ToString() ?? "",
                    Token = ""
                };

                loginResponse.Token = _jwtHelper.GenerateToken(
                    loginResponse.UserID,
                    loginResponse.Email,
                    loginResponse.RoleName
                );

                return ApiResponse<LoginResponseDto>.SuccessResponse(loginResponse, "Login successful");
            }
            catch (Exception ex)
            {
                return ApiResponse<LoginResponseDto>.ErrorResponse("Login failed", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponse<object>> RegisterAsync(RegisterDto registerDto)
        {
            try
            {
                int result = await _userRepository.CreateUserAsync(registerDto);

                if (result > 0)
                {
                    return ApiResponse<object>.SuccessResponse(null, "User registered successfully");
                }

                return ApiResponse<object>.ErrorResponse("Failed to register user");
            }
            catch (Exception ex)
            {
                return ApiResponse<object>.ErrorResponse(ex.Message);
            }
        }
    }
}
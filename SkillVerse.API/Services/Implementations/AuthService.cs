
using System.Data;
using Microsoft.Data.SqlClient; 
using SkillVerse.API.DataAccess;
using SkillVerse.API.DTOs.Auth;
using SkillVerse.API.DTOs.Common;
using SkillVerse.API.Helpers;
using SkillVerse.API.Services.Interfaces;

namespace SkillVerse.API.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly DbHelper _dbHelper;
        private readonly JwtHelper _jwtHelper;

        public AuthService(DbHelper dbHelper, JwtHelper jwtHelper)
        {
            _dbHelper = dbHelper;
            _jwtHelper = jwtHelper;
        }

        public async Task<ApiResponse<LoginResponseDto>> LoginAsync(LoginDto loginDto)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@Email", loginDto.Email),
                    new SqlParameter("@PasswordHash", loginDto.Password)   
                };

                DataTable dt = await _dbHelper.ExecuteDataTableAsync("sp_LoginUser", parameters);

                if (dt.Rows.Count == 0)
                {
                    return ApiResponse<LoginResponseDto>.ErrorResponse("Invalid email or password");
                }

                DataRow row = dt.Rows[0];

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
                return ApiResponse<LoginResponseDto>.ErrorResponse("Login failed: " + ex.Message);
            }
        }

        public async Task<ApiResponse<object>> RegisterAsync(RegisterDto registerDto)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@FullName", registerDto.FullName),
                    new SqlParameter("@Email", registerDto.Email),
                    new SqlParameter("@Phone", registerDto.Phone),
                    new SqlParameter("@PasswordHash", registerDto.Password), 
                    new SqlParameter("@RoleID", registerDto.RoleID)
                };

                int result = await _dbHelper.ExecuteNonQueryAsync("sp_CreateUser", parameters);

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
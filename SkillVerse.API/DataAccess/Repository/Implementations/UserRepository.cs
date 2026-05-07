
using System.Data;
using Microsoft.Data.SqlClient;
using SkillVerse.API.DataAccess.Repository.Interfaces;
using SkillVerse.API.DTOs.Auth;
using SkillVerse.API.DTOs.User;

namespace SkillVerse.API.DataAccess.Repository.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly IBaseRepository _baseRepository;

        public UserRepository(IBaseRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public Task<DataTable> LoginUserAsync(string email, string passwordHash)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@Email", email),
                new SqlParameter("@PasswordHash", passwordHash)
            };

            return _baseRepository.ExecuteDataTableAsync("sp_LoginUser", parameters);
        }

        public Task<int> CreateUserAsync(RegisterDto dto)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@FullName", dto.FullName),
                new SqlParameter("@Email", dto.Email),
                new SqlParameter("@Phone", dto.Phone),
                new SqlParameter("@PasswordHash", dto.Password),
                new SqlParameter("@RoleID", dto.RoleID)
            };

            return _baseRepository.ExecuteNonQueryAsync("sp_CreateUser", parameters);
        }

        public Task<DataTable> GetUserProfileAsync(int userId)
        {
            SqlParameter[] parameters = { new SqlParameter("@UserID", userId) };
            return _baseRepository.ExecuteDataTableAsync("sp_GetUserProfile", parameters);
        }

        public Task<int> UpdateUserProfileAsync(int userId, UserProfileDto dto)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@UserID", userId),
                new SqlParameter("@FullName", dto.FullName),
                new SqlParameter("@Phone", dto.Phone),
                new SqlParameter("@ProfileImage", dto.ProfileImage ?? (object)DBNull.Value)
            };

            return _baseRepository.ExecuteNonQueryAsync("sp_UpdateUserProfile", parameters);
        }
    }
}
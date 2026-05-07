
using System.Data;
using Microsoft.Data.SqlClient;
using SkillVerse.API.DTOs.Auth;
using SkillVerse.API.DTOs.User;

namespace SkillVerse.API.DataAccess.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<DataTable> LoginUserAsync(string email, string passwordHash);
        Task<int> CreateUserAsync(RegisterDto dto);
        Task<DataTable> GetUserProfileAsync(int userId);
        Task<int> UpdateUserProfileAsync(int userId, UserProfileDto dto);
    }
}

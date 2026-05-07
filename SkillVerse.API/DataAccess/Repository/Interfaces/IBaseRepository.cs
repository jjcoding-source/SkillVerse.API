
using System.Data;
using Microsoft.Data.SqlClient;

namespace SkillVerse.API.DataAccess.Repository.Interfaces
{
    public interface IBaseRepository
    {
        Task<DataTable> ExecuteDataTableAsync(string procedureName, SqlParameter[]? parameters = null);
        Task<int> ExecuteNonQueryAsync(string procedureName, SqlParameter[]? parameters = null);
        Task<object?> ExecuteScalarAsync(string procedureName, SqlParameter[]? parameters = null);
    }
}

using System.Data;
using Microsoft.Data.SqlClient;
using SkillVerse.API.DataAccess.Repository.Interfaces;

namespace SkillVerse.API.DataAccess.Repository.Implementations
{
    public class BaseRepository : IBaseRepository
    {
        private readonly DbHelper _dbHelper;

        public BaseRepository(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public Task<DataTable> ExecuteDataTableAsync(string procedureName, SqlParameter[]? parameters = null)
        {
            return _dbHelper.ExecuteDataTableAsync(procedureName, parameters);
        }

        public Task<int> ExecuteNonQueryAsync(string procedureName, SqlParameter[]? parameters = null)
        {
            return _dbHelper.ExecuteNonQueryAsync(procedureName, parameters);
        }

        public Task<object?> ExecuteScalarAsync(string procedureName, SqlParameter[]? parameters = null)
        {
            return _dbHelper.ExecuteScalarAsync(procedureName, parameters);
        }
    }
}
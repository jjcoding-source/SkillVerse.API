
using System.Data;
using Microsoft.Data.SqlClient; 

namespace SkillVerse.API.DataAccess
{
    public class DbHelper
    {
        private readonly string _connectionString;

        public DbHelper(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Database connection string is not configured.");
        }

        public async Task<DataTable> ExecuteDataTableAsync(string procedureName, SqlParameter[]? parameters = null)
        {
            DataTable dataTable = new DataTable();

            using SqlConnection connection = new(_connectionString); 
            using SqlCommand command = new(procedureName, connection); 
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 120;

            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }

            await connection.OpenAsync();
            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                dataTable.Load(reader);
            }

            return dataTable;
        }

        public async Task<int> ExecuteNonQueryAsync(string procedureName, SqlParameter[]? parameters = null)
        {
            using SqlConnection connection = new(_connectionString); 
            using SqlCommand command = new(procedureName, connection); 
            command.CommandType = CommandType.StoredProcedure;

            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }

            await connection.OpenAsync();
            return await command.ExecuteNonQueryAsync();
        }

        public async Task<object?> ExecuteScalarAsync(string procedureName, SqlParameter[]? parameters = null)
        {
            using SqlConnection connection = new(_connectionString);
            using SqlCommand command = new(procedureName, connection); 
            command.CommandType = CommandType.StoredProcedure;

            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }

            await connection.OpenAsync();
            return await command.ExecuteScalarAsync();
        }
    }
}
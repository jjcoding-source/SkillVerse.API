
using System.Data;
using Microsoft.Data.SqlClient;
using SkillVerse.API.DataAccess.Repository.Interfaces;

namespace SkillVerse.API.DataAccess.Repository.Implementations
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly IBaseRepository _baseRepository;

        public ServiceRepository(IBaseRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public Task<DataTable> GetAllCategoriesAsync()
        {
            return _baseRepository.ExecuteDataTableAsync("sp_GetAllServiceCategories");
        }

        public Task<DataTable> GetServicesByCategoryAsync(int? categoryId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@CategoryID", categoryId ?? (object)DBNull.Value)
            };

            return _baseRepository.ExecuteDataTableAsync("sp_GetServicesByCategory", parameters);
        }
    }
}
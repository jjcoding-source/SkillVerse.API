
using System.Data;

namespace SkillVerse.API.DataAccess.Repository.Interfaces
{
    public interface IServiceRepository
    {
        Task<DataTable> GetAllCategoriesAsync();
        Task<DataTable> GetServicesByCategoryAsync(int? categoryId);
    }
}
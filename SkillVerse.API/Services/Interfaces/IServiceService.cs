
using SkillVerse.API.DTOs.Common;
using SkillVerse.API.DTOs.Service;

namespace SkillVerse.API.Services.Interfaces
{
    public interface IServiceService
    {
        Task<ApiResponse<List<ServiceCategoryDto>>> GetAllCategoriesAsync();
        Task<ApiResponse<List<ServiceDto>>> GetServicesByCategoryAsync(int? categoryId = null);
    }
}
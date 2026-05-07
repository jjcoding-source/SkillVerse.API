
using System.Data;
using Microsoft.Data.SqlClient;
using SkillVerse.API.DataAccess;
using SkillVerse.API.DTOs.Common;
using SkillVerse.API.DTOs.Service;
using SkillVerse.API.Services.Interfaces;

namespace SkillVerse.API.Services.Implementations
{
    public class ServiceService : IServiceService
    {
        private readonly DbHelper _dbHelper;

        public ServiceService(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<ApiResponse<List<ServiceCategoryDto>>> GetAllCategoriesAsync()
        {
            try
            {
                DataTable dt = await _dbHelper.ExecuteDataTableAsync("sp_GetAllServiceCategories");

                var categories = new List<ServiceCategoryDto>();

                foreach (DataRow row in dt.Rows)
                {
                    categories.Add(new ServiceCategoryDto
                    {
                        CategoryID = Convert.ToInt32(row["CategoryID"]),
                        CategoryName = row["CategoryName"].ToString() ?? "",
                        Icon = row["Icon"]?.ToString()
                    });
                }

                return ApiResponse<List<ServiceCategoryDto>>.SuccessResponse(categories);
            }
            catch (Exception ex)
            {
                return ApiResponse<List<ServiceCategoryDto>>.ErrorResponse(ex.Message);
            }
        }

        public async Task<ApiResponse<List<ServiceDto>>> GetServicesByCategoryAsync(int? categoryId = null)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@CategoryID", categoryId ?? (object)DBNull.Value)
                };

                DataTable dt = await _dbHelper.ExecuteDataTableAsync("sp_GetServicesByCategory", parameters);

                var services = new List<ServiceDto>();

                foreach (DataRow row in dt.Rows)
                {
                    services.Add(new ServiceDto
                    {
                        ServiceID = Convert.ToInt32(row["ServiceID"]),
                        CategoryID = Convert.ToInt32(row["CategoryID"]),
                        ServiceName = row["ServiceName"].ToString() ?? "",
                        BasePrice = Convert.ToDecimal(row["BasePrice"]),
                        Description = row["Description"]?.ToString(),
                        CategoryName = row["CategoryName"]?.ToString() ?? ""
                    });
                }

                return ApiResponse<List<ServiceDto>>.SuccessResponse(services);
            }
            catch (Exception ex)
            {
                return ApiResponse<List<ServiceDto>>.ErrorResponse(ex.Message);
            }
        }
    }
}

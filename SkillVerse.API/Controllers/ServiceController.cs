
using Microsoft.AspNetCore.Mvc;
using SkillVerse.API.Services.Interfaces;

namespace SkillVerse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            var response = await _serviceService.GetAllCategoriesAsync();
            return Ok(response);
        }

        [HttpGet("services")]
        public async Task<IActionResult> GetServices([FromQuery] int? categoryId = null)
        {
            var response = await _serviceService.GetServicesByCategoryAsync(categoryId);
            return Ok(response);
        }
    }
}

namespace SkillVerse.API.DTOs.Service
{
    public class ServiceCategoryDto
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string? Icon { get; set; }
    }
}
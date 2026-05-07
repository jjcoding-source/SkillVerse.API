namespace SkillVerse.API.DTOs.Service
{
    public class ServiceDto
    {
        public int ServiceID { get; set; }
        public int CategoryID { get; set; }
        public string ServiceName { get; set; } = string.Empty;
        public decimal BasePrice { get; set; }
        public string? Description { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }
}
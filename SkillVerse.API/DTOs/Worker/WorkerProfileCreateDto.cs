namespace SkillVerse.API.DTOs.Worker
{
    public class WorkerProfileCreateDto
    {
        public string Skills { get; set; } = string.Empty;
        public int? ExperienceYears { get; set; }
        public decimal? HourlyRate { get; set; }
        public string? Description { get; set; }
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
    }
}
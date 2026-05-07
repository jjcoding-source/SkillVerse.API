namespace SkillVerse.API.DTOs.Worker
{
    public class WorkerProfileDto
    {
        public int WorkerID { get; set; }
        public int UserID { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Skills { get; set; } = string.Empty;           
        public int? ExperienceYears { get; set; }
        public decimal? HourlyRate { get; set; }
        public string? Description { get; set; }
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public bool IsAvailable { get; set; } = true;
        public decimal Rating { get; set; } = 0;
        public int TotalJobsCompleted { get; set; } = 0;
    }
}

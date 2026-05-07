namespace SkillVerse.API.DTOs.Worker
{
    public class WorkerSearchDto
    {
        public int WorkerID { get; set; }
        public int UserID { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? ProfileImage { get; set; }
        public string Skills { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public decimal Rating { get; set; }
        public int TotalJobsCompleted { get; set; }
        public decimal? HourlyRate { get; set; }
        public bool IsAvailable { get; set; }
    }
}
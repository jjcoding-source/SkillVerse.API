namespace SkillVerse.API.DTOs.Admin
{
    public class DashboardStatsDto
    {
        public int TotalCustomers { get; set; }
        public int TotalWorkers { get; set; }
        public int TotalBookings { get; set; }
        public int PendingBookings { get; set; }
    }
}
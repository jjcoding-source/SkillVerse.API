namespace SkillVerse.API.DTOs.Review
{
    public class ReviewDto
    {
        public int ReviewID { get; set; }
        public int BookingID { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public int WorkerID { get; set; }
        public int Rating { get; set; }           
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
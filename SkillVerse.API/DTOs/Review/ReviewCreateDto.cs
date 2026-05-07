namespace SkillVerse.API.DTOs.Review
{
    public class ReviewCreateDto
    {
        public int BookingID { get; set; }
        public int Rating { get; set; }          
        public string? Comment { get; set; }
    }
}
namespace SkillVerse.API.DTOs.Booking
{
    public class UpdateBookingStatusDto
    {
        public string Status { get; set; } = string.Empty;   
        public string? CancelReason { get; set; }
    }
}
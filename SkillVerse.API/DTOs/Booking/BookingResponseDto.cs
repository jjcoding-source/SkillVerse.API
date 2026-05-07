namespace SkillVerse.API.DTOs.Booking
{
    public class BookingResponseDto
    {
        public int BookingID { get; set; }
        public int CustomerID { get; set; }
        public int? WorkerID { get; set; }
        public string ServiceName { get; set; } = string.Empty;
        public string WorkerName { get; set; } = string.Empty;
        public DateTime BookingDate { get; set; }
        public string? PreferredTimeSlot { get; set; }
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending";
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
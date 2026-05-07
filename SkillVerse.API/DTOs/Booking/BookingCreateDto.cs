namespace SkillVerse.API.DTOs.Booking
{
    public class BookingCreateDto
    {
        public int ServiceID { get; set; }
        public DateTime BookingDate { get; set; }
        public string? PreferredTimeSlot { get; set; }
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string? PinCode { get; set; }
        public string? Description { get; set; }
        public decimal TotalAmount { get; set; }
    }
}

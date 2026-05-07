namespace SkillVerse.API.DTOs.Chat
{
    public class SendMessageDto
    {
        public int BookingID { get; set; }
        public string MessageText { get; set; } = string.Empty;
    }
}
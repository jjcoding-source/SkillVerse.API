namespace SkillVerse.API.DTOs.User
{
    public class UserProfileDto
    {
        public int UserID { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string? ProfileImage { get; set; }
        public string RoleName { get; set; } = string.Empty;
    }
}

using SkillVerse.API.DTOs.Chat;
using SkillVerse.API.DTOs.Common;

namespace SkillVerse.API.Services.Interfaces
{
    public interface IChatService
    {
        Task<ApiResponse<int>> CreateConversationAsync(int bookingId, int customerId, int workerId);
        Task<ApiResponse<bool>> SendMessageAsync(int senderId, SendMessageDto dto);
        Task<ApiResponse<List<MessageDto>>> GetConversationMessagesAsync(int bookingId);
    }
}

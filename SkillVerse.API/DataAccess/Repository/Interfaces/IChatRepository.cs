
using System.Data;
using SkillVerse.API.DTOs.Chat;

namespace SkillVerse.API.DataAccess.Repository.Interfaces
{
    public interface IChatRepository
    {
        Task<int> CreateConversationAsync(int bookingId, int customerId, int workerId);
        Task<int> SendMessageAsync(int bookingId, int senderId, string messageText);
        Task<DataTable> GetConversationMessagesAsync(int bookingId);
    }
}
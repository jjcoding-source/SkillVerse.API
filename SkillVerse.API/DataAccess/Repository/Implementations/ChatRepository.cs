
using System.Data;
using Microsoft.Data.SqlClient;
using SkillVerse.API.DataAccess.Repository.Interfaces;
using SkillVerse.API.DTOs.Chat;

namespace SkillVerse.API.DataAccess.Repository.Implementations
{
    public class ChatRepository : IChatRepository
    {
        private readonly IBaseRepository _baseRepository;

        public ChatRepository(IBaseRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public Task<int> CreateConversationAsync(int bookingId, int customerId, int workerId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@BookingID", bookingId),
                new SqlParameter("@CustomerID", customerId),
                new SqlParameter("@WorkerID", workerId)
            };

            return _baseRepository.ExecuteNonQueryAsync("sp_CreateConversation", parameters);
        }

        public Task<int> SendMessageAsync(int bookingId, int senderId, string messageText)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@BookingID", bookingId),
                new SqlParameter("@SenderID", senderId),
                new SqlParameter("@MessageText", messageText)
            };

            return _baseRepository.ExecuteNonQueryAsync("sp_SendMessage", parameters);
        }

        public Task<DataTable> GetConversationMessagesAsync(int bookingId)
        {
            SqlParameter[] parameters = { new SqlParameter("@BookingID", bookingId) };
            return _baseRepository.ExecuteDataTableAsync("sp_GetConversationMessages", parameters);
        }
    }
}

using System.Data;
using Microsoft.Data.SqlClient;
using SkillVerse.API.DataAccess;
using SkillVerse.API.DTOs.Chat;
using SkillVerse.API.DTOs.Common;
using SkillVerse.API.Services.Interfaces;

namespace SkillVerse.API.Services.Implementations
{
    public class ChatService : IChatService
    {
        private readonly DbHelper _dbHelper;

        public ChatService(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<ApiResponse<int>> CreateConversationAsync(int bookingId, int customerId, int workerId)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@BookingID", bookingId),
                    new SqlParameter("@CustomerID", customerId),
                    new SqlParameter("@WorkerID", workerId)
                };

                object? result = await _dbHelper.ExecuteScalarAsync("sp_CreateConversation", parameters);
                int conversationId = Convert.ToInt32(result);

                return ApiResponse<int>.SuccessResponse(conversationId);
            }
            catch (Exception ex)
            {
                return ApiResponse<int>.ErrorResponse(ex.Message);
            }
        }

        public async Task<ApiResponse<bool>> SendMessageAsync(int senderId, SendMessageDto dto)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@BookingID", dto.BookingID),   
                    new SqlParameter("@SenderID", senderId),
                    new SqlParameter("@MessageText", dto.MessageText)
                };

                await _dbHelper.ExecuteNonQueryAsync("sp_SendMessage", parameters);

                return ApiResponse<bool>.SuccessResponse(true, "Message sent");
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResponse(ex.Message);
            }
        }

        public async Task<ApiResponse<List<MessageDto>>> GetConversationMessagesAsync(int bookingId)
        {
            try
            {
                SqlParameter[] parameters = { new SqlParameter("@BookingID", bookingId) };

                DataTable dt = await _dbHelper.ExecuteDataTableAsync("sp_GetConversationMessages", parameters);

                var messages = new List<MessageDto>();

                foreach (DataRow row in dt.Rows)
                {
                    messages.Add(new MessageDto
                    {
                        MessageID = Convert.ToInt64(row["MessageID"]),
                        SenderID = Convert.ToInt32(row["SenderID"]),
                        SenderName = row["SenderName"]?.ToString() ?? "",
                        MessageText = row["MessageText"].ToString() ?? "",
                        SentAt = Convert.ToDateTime(row["SentAt"]),
                        IsRead = Convert.ToBoolean(row["IsRead"])
                    });
                }

                return ApiResponse<List<MessageDto>>.SuccessResponse(messages);
            }
            catch (Exception ex)
            {
                return ApiResponse<List<MessageDto>>.ErrorResponse(ex.Message);
            }
        }
    }
}

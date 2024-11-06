using Microsoft.EntityFrameworkCore;
using YouthProtectionApi.DataBase;
using YouthProtectionApi.Models;

namespace YouthProtectionApi.Repositories
{

    public interface IChatRepository
    {
        Task<ChatModel> AddChat(ChatModel chat);
        Task<ChatModel> GetChatByAttendanceId(long attendanceId);
    }
    public class ChatRepository : IChatRepository
    {

        private readonly DataContext _context;

        public ChatRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ChatModel> AddChat(ChatModel chat)
        {
            _context.TB_CHAT.Add(chat);
            await _context.SaveChangesAsync();
            return chat;
        }

        public async Task<ChatModel> GetChatByAttendanceId(long attendanceId)
        {
            return await _context.TB_CHAT
                .FirstOrDefaultAsync(c => c.AttendanceId == attendanceId);
        }
    }
}

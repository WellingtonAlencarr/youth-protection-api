using Microsoft.EntityFrameworkCore;
using YouthProtection.Models;
using YouthProtectionApi.DataBase;

namespace YouthProtectionApi.Repositories
{
    public interface IMessageRepository
    {
        Task<MessageModel> AddMessage(MessageModel message);
        Task<List<MessageModel>> GetByChatId(long chatId);
    }

    public class MessageRepository : IMessageRepository
    {
        private readonly DataContext _context;

        public MessageRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<MessageModel> AddMessage(MessageModel message)
        {
            _context.TB_MESSAGES.Add(message);
            await _context.SaveChangesAsync();
            return message;
        }

        public async Task<List<MessageModel>> GetByChatId(long chatId)
        {
            return await _context.TB_MESSAGES.Where(m => m.ChatId == chatId).ToListAsync();
        }
    }
}

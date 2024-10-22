using YouthProtection.Models;
using YouthProtectionApi.DataBase;

namespace YouthProtectionApi.Repositories
{
    public interface ICommentRepository
    {
        
    }

    public class CommentRepository : ICommentRepository
    {
        private readonly DataContext _context;

        public CommentRepository(DataContext context)
        {
            _context = context;
        }

        //Retirar este comando! Não é necessário visualizar todos os comentários do banco de dados (Exceto para testes)
        public async Task<List<CommentsModel>> GetAllComments(int pageNumber, int pageSize)
        {
            return await _context.TB_COMMENT
                .Skip((pageNumber -1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public async Task <List<CommentsModel>> GetAllCommentsByUser(long publicationId, int pageNumber, int pageSize)
        {
            return await _context.TB_PUBLICATION
            .Where(x => x.PublicationId == publicationId)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        }
    }
}

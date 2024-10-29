using Microsoft.EntityFrameworkCore;
using YouthProtection.Models;
using YouthProtectionApi.DataBase;

namespace YouthProtectionApi.Repositories
{
    public interface ICommentRepository
    {
        Task<List<CommentsModel>> GetAllComments(int pageNumber, int pageSize);
        Task<List<CommentsModel>> GetAllCommentsByPublication(long publicationId, int pageNumber, int pageSize);
        Task InactiveComment(CommentsModel commentsModel);
        Task<CommentsModel> CreateComment(long userId, long publicationId, CommentsModel comment);
        Task<CommentsModel> GetCommentById(long commentId);
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
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<CommentsModel>> GetAllCommentsByPublication(long publicationId, int pageNumber, int pageSize)
        {
            return await _context.TB_COMMENT
            .Where(x => x.PublicationId == publicationId)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        }

        public async Task<CommentsModel> GetCommentById(long commentId)
        {
            return await _context.TB_COMMENT
                .FirstOrDefaultAsync(x => x.CommentId == commentId);
        }

        public async Task InactiveComment(CommentsModel commentsModel)
        {
            _context.TB_COMMENT.Update(commentsModel);
            await _context.SaveChangesAsync();
        }

        public async Task<CommentsModel> CreateComment(long userId, long publicationId, CommentsModel comment)
        {
            comment.UserId = userId;
            comment.PublicationId = publicationId;
            comment.CreatedAt = DateTime.Now;

            await _context.TB_COMMENT.AddAsync(comment);
            await _context.SaveChangesAsync();

            return comment;
        } 
    }
}

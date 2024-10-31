using System.Security.Claims;
using YouthProtection.Models;
using YouthProtectionApi.Models.Dtos;
using YouthProtectionApi.Models.Enums;
using YouthProtectionApi.Repositories;

namespace YouthProtectionApi.Services
{
    public class CommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CommentService(ICommentRepository commentRepository, IHttpContextAccessor httpContextAccessor)
        {
            _commentRepository = commentRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<CommentsModel>> GetAllComments(int pageNumber, int pageSize)
        {
            var comments = await _commentRepository.GetAllComments(pageNumber, pageSize);
            return comments;
        }

        public async Task<List<CommentsModelDto>> GetAllCommentsByUser(int pageNumber, int pageSize)
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if(userIdClaim == null)
            {
                throw new UnauthorizedAccessException("Houve um erro na requisição, tente fazer login novamente.");
            }

            var userId = long.Parse(userIdClaim.Value);

            var comment = await _commentRepository.GetAllCommentsByPublication(userId, pageNumber, pageSize);

            return comment.Select(c => new CommentsModelDto
            {
                CommentId = c.CommentId,
                ContentComment = c.ContentComment,
                CommentStatus = c.CommentStatus,
                CreatedAt = c.CreatedAt
            }).ToList();            
        }

        public async Task<CommentsModel> CreateComment(long userId, long publicationId, string contentComment, CommentStatus status)
        {
            var comment = new CommentsModel
            {
                ContentComment = contentComment,
                CommentStatus = status,
                UserId = userId,
                PublicationId = publicationId
            };

            return await _commentRepository.CreateComment(userId ,publicationId, comment);
        }

        public async Task DeleteComment(long commentId, long publicationId)
        {
            var comment = await _commentRepository.GetCommentById(commentId);

            if (comment == null)
            {
                throw new Exception("Mensagem não encontrada");
            }

            if (comment.PublicationId != publicationId)
            {
                throw new Exception("Erro na requisição, tentar novamente mais tarde!");
            }

            comment.CommentStatus = CommentStatus.Inativo;
            await _commentRepository.InactiveComment(comment);
        }
    }
}

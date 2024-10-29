using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using YouthProtectionApi.Models.Dtos;
using YouthProtectionApi.Services;

namespace YouthProtectionApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly CommentService _commentService;

        public CommentsController(CommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("comments")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllComments([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var comments = await _commentService.GetAllComments(pageNumber, pageSize);
            return Ok(comments);
        }

        [HttpGet("publications/comment")]
        [Authorize(Roles = "Admin, User, Voluntary")]
        public async Task<IActionResult> GetAllCommentsByPublicationId([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var comments = await _commentService.GetAllCommentsByUser(pageNumber, pageSize);
            return Ok(comments);
        }

        [HttpPost("CreateComment")]
        [Authorize(Roles = "Admin, User, Voluntary")]
        public async Task<IActionResult> CreateComment(CommentsModelDto commentsModelDto)
        {
            var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new UnauthorizedAccessException());
            var comment = await _commentService.CreateComment(
                userId, 
                commentsModelDto.PublicationId, 
                commentsModelDto.ContentComment, 
                commentsModelDto.CommentStatus);

            return Ok(comment);
        }

        [HttpPut("comments/{commentId}")]
        public async Task<IActionResult> DeleteComment(long commentId, long publicationId)
        {
            try
            {
                var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new UnauthorizedAccessException());

                await _commentService.DeleteComment(commentId, publicationId);

                return Ok("Publicação excluída com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

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
    public class PublicationsController : ControllerBase
    {

        private readonly PublicationService _publicationService;

        public PublicationsController(PublicationService publicationService)
        {
            _publicationService = publicationService;
        }

        [HttpGet("publications")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllPublications([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var publications = await _publicationService.GetAllPublications(pageNumber, pageSize);
            return Ok(publications);
        }

        [HttpGet("publications/user")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetAllPublicationsByUserId([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var publications = await _publicationService.GetAllPublicationByUser(pageNumber, pageSize);
            return Ok(publications);
        }

        [HttpPost("CreatePublication")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> CreatePublication(PublicationsModelDto publicationsModelDto)
        {
            var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new UnauthorizedAccessException());
            var publication = await _publicationService.CreatePublication(userId, publicationsModelDto.PublicationContent, publicationsModelDto.PublicationsRole, publicationsModelDto.PublicationStatus);
            
            return Ok("Publicação criada com sucesso.");
        }

        [HttpPost("publicationsUpdate/{publicationiD}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> UpdatePublication(PublicationsModelDto publicationsModelDto)
        {
            var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new UnauthorizedAccessException());
            var publication = await _publicationService.UpdatePublication(userId ,publicationsModelDto.PublicationId, publicationsModelDto.PublicationContent, publicationsModelDto.PublicationsRole, publicationsModelDto.PublicationStatus);

            return Ok("Publicação atualizada com sucesso!");
        }

        [HttpDelete("publication/{publicationiD}")]
        public async Task<IActionResult> DeletePublication(long publicationiD)
        {
            try
            {
                var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new UnauthorizedAccessException());

                await _publicationService.DeletePublication(publicationiD, userId);

                return Ok("Publicação excluída com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}

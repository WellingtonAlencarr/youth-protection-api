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

        [HttpGet("GetAll")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var publications = await _publicationService.GetAllPublications();

            return Ok(publications);
        }



        [HttpGet("GetAllByUserId/{userId}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetAllByUserId(long userId, int pageNumber = 1, int pageSize = 10)
        {
            var identification = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new UnauthorizedAccessException());

            var paginatedResult = await _publicationService.GetAllPublicationsByUserId(userId, pageNumber, pageSize);

            return Ok(paginatedResult);
        }

        [HttpPost("CreatePublication")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> CreatePublication(PublicationsModelDto publicationsModelDto)
        {
            var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new UnauthorizedAccessException());
            var publication = await _publicationService.CreatePublication(userId, publicationsModelDto.PublicationContent, publicationsModelDto.PublicationsRole, publicationsModelDto.PublicationStatus);
            
            return Ok("Publicação criada com sucesso.");
        }

        [HttpDelete("publication/{publicationiD}")]
        public async Task<IActionResult> DeletePublication(long publicationiD)
        {
            try
            {
                var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new UnauthorizedAccessException());

                await _publicationService.DeletePublication(publicationiD, userId);

                return Ok("Publicação Inativada com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}

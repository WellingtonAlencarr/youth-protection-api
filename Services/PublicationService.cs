using System.Security.Claims;
using YouthProtection.Models;
using YouthProtectionApi.Models.Dtos;
using YouthProtectionApi.Models.Enums;
using YouthProtectionApi.Repositories;

namespace YouthProtectionApi.Services
{
    public class PublicationService
    {
        private readonly IPublicationRepository _publicationRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PublicationService(IPublicationRepository publicationRepository, IHttpContextAccessor httpContextAccessor)
        {
            _publicationRepository = publicationRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<PublicationsModel>> GetAllPublications(int pageNumber, int pageSize)
        {
            var publications = await _publicationRepository.GetAllPublications(pageNumber, pageSize);
            return publications;
        }

        public async Task<List<PublicationsModelDto>> GetAllPublicationByUser(int pageNumber, int pageSize)
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                throw new UnauthorizedAccessException("Houve um erro na requisição, tente fazer login novamente.");
            }

            var userId = long.Parse(userIdClaim.Value);

            var publications = await _publicationRepository.GetAllPublicationsByUser(userId, pageNumber, pageSize);

            return publications.Select(p => new PublicationsModelDto
            {
                PublicationId = p.PublicationId,
                PublicationContent = p.PublicationContent,
                PublicationsRole = p.PublicationsRole,
                PublicationStatus = p.PublicationStatus,
                CreatedAt = p.CreatedAt,
                ModificationDate = p.ModificationDate
            }).ToList();
        }

        public async Task<PublicationsModel> UpdatePublication(long userId, long publicationId, string newContent, PublicationRole newRole, PublicationStatus newStatus)
        {

            var publication = await _publicationRepository.GetPublicationById(publicationId);

            if(publication == null)
            {
                throw new Exception("Publicação não encontrada");
            }

            if(publication.UserId != userId)
            {
                throw new Exception("Você não tem permissão para atualizar esta publicação.");
            }

            publication.PublicationContent = newContent;
            publication.PublicationsRole = newRole;
            publication.ModificationDate = DateTime.UtcNow;
            publication.PublicationStatus = newStatus;

            return await _publicationRepository.UpdatePublication(publication);
        }

        public async Task<PublicationsModel> CreatePublication(long userId, string content, PublicationRole role, PublicationStatus status)
        {
            var publication = new PublicationsModel
            {
                PublicationContent = content,
                PublicationsRole = role,
                PublicationStatus = status
            };

            return await _publicationRepository.CreatePublication(userId, publication);
        }

        public async Task DeletePublication(long publicationId, long userId)
        {
            var publication = await _publicationRepository.GetPublicationById(publicationId);

            if(publication == null)
            {
                throw new Exception("Publicação não encontrada");
            }

            if(publication.UserId != userId)
            {
                throw new Exception("Você não possui permissão para deletar essa publicação");
            }

            publication.PublicationStatus = PublicationStatus.Inativo;
            await _publicationRepository.InactivePublication(publication);
        }
    }
}

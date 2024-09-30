using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.OpenApi.Models;
using YouthProtection.Models;
using YouthProtectionApi.Models.Dtos;
using YouthProtectionApi.Models.Enums;
using YouthProtectionApi.Repositories;

namespace YouthProtectionApi.Services
{
    public class PublicationService
    {
        private readonly IPublicationRepository _publicationRepository;

        public PublicationService(IPublicationRepository publicationRepository)
        {
            _publicationRepository = publicationRepository;
        }

        public async Task<PaginatedPublicationsDto> GetAllPublications(int pageNumber, int pageSize)
        {
            var publications = await _publicationRepository.GetAllPublications(pageNumber, pageSize);

            return publications;
        }

        public async Task<PaginatedPublicationsDto> GetAllPublicationByUser(long userId, int pageNumber, int pageSize)
        {
            var publications = await _publicationRepository.GetAllPublicationsByUser(userId, pageNumber, pageSize);


            return publications;
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

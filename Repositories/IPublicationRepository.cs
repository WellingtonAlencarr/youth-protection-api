using Microsoft.EntityFrameworkCore;
using YouthProtection.Models;
using YouthProtectionApi.DataBase;

namespace YouthProtectionApi.Repositories
{
    public interface IPublicationRepository
    {
        Task<List<PublicationsModel>> GetAllPublicationsByUser(long userId, int pageNumber, int pageSize);
        Task<List<PublicationsModel>> GetAllPublications(int pageNumber, int pageSize);
        Task InactivePublication(PublicationsModel publicationsModel);
        Task<PublicationsModel> UpdatePublication(PublicationsModel publication);
        Task<PublicationsModel> CreatePublication(long userId, PublicationsModel publication);
        Task<PublicationsModel> GetPublicationById(long publicationId);
    }

    public class PublicationRepository : IPublicationRepository
    {
        private readonly DataContext _context;

        public PublicationRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<PublicationsModel>> GetAllPublications(int pageNumber, int pageSize)
        {

            return await _context.TB_PUBLICATION
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<PublicationsModel>> GetAllPublicationsByUser(long userId, int pageNumber, int pageSize)
        {

            return await _context.TB_PUBLICATION
                .Where(x => x.UserId == userId)
                .Skip((pageNumber - 1)* pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<PublicationsModel> GetPublicationById(long publicationId)
        {
            return await _context.TB_PUBLICATION
                .FirstOrDefaultAsync(x => x.PublicationId == publicationId);
        }

        public async Task InactivePublication(PublicationsModel publicationsModel)
        {
            _context.TB_PUBLICATION.Update(publicationsModel);
            await _context.SaveChangesAsync();
        }

        public async Task<PublicationsModel> UpdatePublication(PublicationsModel publication)
        {

            _context.TB_PUBLICATION.Update(publication);
            await _context.SaveChangesAsync();

            return publication;
        }

        public async Task<PublicationsModel> CreatePublication(long userId, PublicationsModel publication)
        {
            publication.UserId = userId;
            publication.CreatedAt = DateTime.Now;
            publication.ModificationDate = DateTime.Now;

            await _context.TB_PUBLICATION.AddAsync(publication);
            await _context.SaveChangesAsync();

            return publication;
        }
    }
}

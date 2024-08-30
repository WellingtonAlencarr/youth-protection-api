using Microsoft.EntityFrameworkCore;
using YouthProtection.Models;
using YouthProtection.Models.Dtos;
using YouthProtectionApi.DataBase;

namespace YouthProtectionApi.Repositories
{
    public interface IUserRepository
    {
        Task<bool> FindByEmail(string email);
        public Task AddNewUser(UserModel userModel);
    }

    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public UserRepository(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<bool> FindByEmail(string email)
        {
            if (await _context.TB_USER.AnyAsync(x => x.Email.ToLower() == email.ToLower()))
            {
                return true;
            } 

            return false;
        }

        public async Task AddNewUser(UserModel userModel)
        {
            await _context.TB_USER.AddAsync(userModel);
            await _context.SaveChangesAsync();
        }
    }
}

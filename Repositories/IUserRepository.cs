using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YouthProtection.Models;
using YouthProtectionApi.DataBase;

namespace YouthProtectionApi.Repositories
{
    public interface IUserRepository
    {
        Task<bool> FindByEmail(string email);
        public Task AddNewUser(UserModel userModel);
        public Task<UserModel> AuthenticationUser(string email);
    }

    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> FindByEmail(string email)
        {
            if (_context == null)
            {
                throw new InvalidOperationException("O DataContext não foi inicializado.");
            }

            return await _context.TB_USER.AnyAsync(x => x.Email.ToLower() == email.ToLower());
        }
        public async Task<UserModel> AuthenticationUser(string email)
        {
            if (_context == null)
            {
                throw new InvalidOperationException("O DataContext não foi inicializado.");
            }

            var user = await _context.TB_USER
                .Where(x => x.Email.ToLower() == email.ToLower())
                .Select(x => new UserModel
                {
                    Id = x.Id,
                    Email = x.Email,
                    PasswordHash = x.PasswordHash
                })
                .FirstOrDefaultAsync();
            
            return user;
        }

        public async Task AddNewUser(UserModel userModel)
        {
            await _context.TB_USER.AddAsync(userModel);
            await _context.SaveChangesAsync();
        }
    }
}

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
        public Task<UserModel> UpdateUser(UserModel userModel);
        public Task<UserModel> GetUserById(long userId);

    }

    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> FindByEmail(string email)
        {

            return await _context.TB_USER.AnyAsync(x => x.Email.ToLower() == email.ToLower());
        }

        public async Task<UserModel> AuthenticationUser(string email)
        {

            var user = await _context.TB_USER
                .Where(x => x.Email.ToLower() == email.ToLower())
                .Select(x => new UserModel
                {
                    UserId = x.UserId,
                    Email = x.Email,
                    FictionalName = x.FictionalName,
                    Uf = x.Uf,
                    City = x.City,
                    CellPhone = x.CellPhone,
                    BirthDate = x.BirthDate,
                    PasswordHash = x.PasswordHash,
                    Role = x.Role,
                    UserStatus = x.UserStatus
                })
                .FirstOrDefaultAsync();
            
            return user;
        }

        public async Task<UserModel> GetUserById(long userId)
        {
            return await _context.TB_USER
                .FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<UserModel> UpdateUser(UserModel userModel)
        {
            _context.TB_USER.Update(userModel);
            await _context.SaveChangesAsync();

            return userModel;
        }

        public async Task AddNewUser(UserModel userModel)
        {
            await _context.TB_USER.AddAsync(userModel);
            await _context.SaveChangesAsync();
        }
    }
}

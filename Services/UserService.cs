using YouthProtection.Models;
using YouthProtectionApi.Exceptions;
using YouthProtectionApi.Repositories;

namespace YouthProtectionApi.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> ExistentUser(string email)
        {
            return await _userRepository.FindByEmail(email); ;
        }

        public async Task<UserModel> RegisterUser(UserModel userModel)
        {

           await _userRepository.AddNewUser(userModel);

            return new UserModel
            {
                Id = userModel.Id,
                Email = userModel.Email,
                PasswordHash = userModel.PasswordHash
            };
        }
    }
}

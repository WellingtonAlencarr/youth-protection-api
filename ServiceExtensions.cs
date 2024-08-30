using YouthProtection.Services;
using YouthProtectionApi.DataBase;
using YouthProtectionApi.Exceptions;
using YouthProtectionApi.Repositories;
using YouthProtectionApi.Services;
using YouthProtectionApi.UseCases;

namespace YouthProtectionApi
{
    public static class ServiceExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<AuthService>();
            services.AddScoped<RegisterUserUseCase>();
            services.AddScoped<LoginUserUseCase>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<DataContext>();
            services.AddScoped<GenericExceptions>();
            services.AddScoped<UserService>();
        }
    }
}

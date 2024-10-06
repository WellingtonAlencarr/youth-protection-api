using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using YouthProtection.Services;
using YouthProtectionApi.DataBase;
using YouthProtectionApi.Exceptions;
using YouthProtectionApi.Repositories;
using YouthProtectionApi.Services;
using YouthProtectionApi.UseCases.User;

namespace YouthProtectionApi
{
    public static class ServiceExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<AuthService>();
            services.AddScoped<RegisterUserUseCase>();
            services.AddScoped<LoginUserUseCase>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<DataContext>();
            services.AddScoped<GenericExceptions>();
            services.AddScoped<UserService>();
            services.AddScoped<PublicationService>();
            services.AddScoped<IPublicationRepository, PublicationRepository>();
            services.AddScoped<RegisterUserException>();
            services.AddScoped<PublicationException>();
            services.AddHttpContextAccessor();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AppSettings:Token"])),
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ValidateLifetime = true,
                            ClockSkew = TimeSpan.Zero
                        };
                 });   
        }
    }
}

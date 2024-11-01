using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using YouthProtection.Services;
using YouthProtectionApi.DataBase;
using YouthProtectionApi.Exceptions;
using YouthProtectionApi.Repositories;
using YouthProtectionApi.Services;
using YouthProtectionApi.UseCases.User;
using YouthProtectionApi.WebSockets;

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
            services.AddScoped<ChatService>();
            services.AddScoped<IChatRepository, ChatRepository>();
            services.AddScoped<AttendanceService>();
            services.AddScoped<IAttendanceRepository, AttendanceRepository>(); 
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddHttpContextAccessor();
            services.AddScoped<WebSocketHandler>();
            services.AddSingleton<ChatSocketHandler>();

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

        public class Startup
        {
            public void Configure(IApplicationBuilder app, IWebHostEnvironment env, WebSocketHandler webSocketHandler)
            {
                app.UseWebSockets();

                app.Use(async (context, next) =>
                {
                    if (context.Request.Path == "/ws")
                    {
                        await webSocketHandler.HandleWebSocketConnection(context);
                    }
                    else
                    {
                        await next();
                    }
                });

                app.UseRouting();
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
            }
        }
    }
}

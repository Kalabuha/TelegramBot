using TelegramBot.Services.Interfaces;

namespace TelegramBot.Services.Extensions
{
    public static class RegistrarServices
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IServiceUsers, ServiceUsers>();

            return services;
        }
    }
}

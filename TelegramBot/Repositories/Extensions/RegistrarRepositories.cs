using TelegramBot.Repositories.Interfaces;

namespace TelegramBot.Repositories.Extensions
{
    public static class RegistrarRepositories
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryUsers, RepositoryUsers>();

            return services;
        }
    }
}

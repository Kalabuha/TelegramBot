using Microsoft.EntityFrameworkCore;

namespace TelegramBot.DataContext.Extensions
{
    internal static class RegistrarDB
    {
        public static IServiceCollection RegisterDB(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDB>(options => options.UseSqlServer(connectionString));

            return services;
        }
    }
}

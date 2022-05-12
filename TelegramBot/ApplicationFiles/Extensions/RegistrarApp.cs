using TelegramBot.ApplicationFiles.Interfaces;

namespace TelegramBot.ApplicationFiles.Extensions
{
    internal static class RegistrarApp
    {
        public static IServiceCollection RegisterApp(this IServiceCollection services)
        {
            services.AddSingleton<ITelegramBotBuilder, TelegramBotBuilder>();
            services.AddSingleton<IBotApplication, BotApplication>();
            return services;
        }
    }
}

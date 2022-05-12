using Microsoft.EntityFrameworkCore;

namespace TelegramBot.DataContext.Extensions
{
    internal static class InitializerDB
    {
        public static void Initialize(this ApplicationDB content)
        {
            content.Database.Migrate();
        }
    }
}

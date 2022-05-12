using Microsoft.EntityFrameworkCore;
using TelegramBot.DataContext.TableConfigurations;
using TelegramBot.Entities;

namespace TelegramBot.DataContext
{
    public class ApplicationDB : DbContext
    {
        public DbSet<AppUserEntity> AppUsers => Set<AppUserEntity>();

        public ApplicationDB(DbContextOptions<ApplicationDB> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppUsersConfiguration());
        }
    }
}

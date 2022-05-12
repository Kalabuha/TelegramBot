using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TelegramBot.Entities;

namespace TelegramBot.DataContext.TableConfigurations
{
    internal class AppUsersConfiguration : IEntityTypeConfiguration<AppUserEntity>
    {
        public void Configure(EntityTypeBuilder<AppUserEntity> builder)
        {
            builder.ToTable("Users");

            builder
                .HasKey(u => u.Id)
                .HasName("PK_User_Id");

            builder
                .Property(u => u.DateTimeCreate)
                .IsRequired()
                .HasDefaultValue(DateTime.UtcNow)
                .HasColumnName("Create");

            builder
                .Property(u => u.Username)
                .HasMaxLength(20)
                .IsUnicode(true)
                .HasColumnName("Username");

            builder
                .Property(u => u.Firstname)
                .HasMaxLength(20)
                .IsUnicode(true)
                .IsRequired()
                .HasColumnName("Firstname");

            builder
                .Property(u => u.Lastname)
                .HasMaxLength(20)
                .IsUnicode(true)
                .HasColumnName("Lastname");
        }
    }
}

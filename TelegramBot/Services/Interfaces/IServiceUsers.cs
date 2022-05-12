using TelegramBot.Entities;

namespace TelegramBot.Services.Interfaces
{
    public interface IServiceUsers
    {
        AppUserEntity? GetOneUserById(int id);
        IList<AppUserEntity> GetAllUsers();
        IList<AppUserEntity> GetUsersByFirstnameAndLastname(string firstname, string lastname);
        IList<AppUserEntity> GetUsersByChatId(long chatId);
        void UpdateUser(AppUserEntity entity);
        void AddUser(AppUserEntity entity);
        void DeleteUser(int id);
    }
}

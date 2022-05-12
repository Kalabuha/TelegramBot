using TelegramBot.Entities;
using TelegramBot.Repositories.Base;

namespace TelegramBot.Repositories.Interfaces
{
    public interface IRepositoryUsers : IRepositoryBase<AppUserEntity>
    {
        IList<AppUserEntity> GetAll();
    }
}

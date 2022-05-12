using TelegramBot.DataContext;
using TelegramBot.Entities;
using TelegramBot.Repositories.Base;
using TelegramBot.Repositories.Interfaces;

namespace TelegramBot.Repositories
{
    public class RepositoryUsers : RepositoryBase<AppUserEntity>, IRepositoryUsers
    {
        public RepositoryUsers(ApplicationDB context) : base(context)
        {

        }

        public IList<AppUserEntity> GetAll()
        {
            return Context.AppUsers.ToList();
        }
    }
}

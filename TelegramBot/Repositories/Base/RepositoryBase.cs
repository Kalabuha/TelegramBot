using TelegramBot.DataContext;
using TelegramBot.Entities.Base;

namespace TelegramBot.Repositories.Base
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : BaseEntity
    {
        protected ApplicationDB Context { get; }

        public RepositoryBase(ApplicationDB context)
        {
            Context = context;
        }

        public TEntity? GetOne(int id)
        {
            return Context.Find<TEntity>(id);
        }

        public void Update(TEntity entity)
        {
            Context.Update(entity);
            Context.SaveChanges();
        }

        public void Add(TEntity entity)
        {
            Context.Add(entity);
            Context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            Context.Remove(entity);
            Context.SaveChanges();
        }
    }
}

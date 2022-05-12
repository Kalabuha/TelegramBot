using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Entities.Base;

namespace TelegramBot.Repositories.Base
{
    public interface IRepositoryBase<TEntity> where TEntity : BaseEntity
    {
        TEntity? GetOne(int id);
        void Update(TEntity entity);
        void Add(TEntity entity);
        void Delete(TEntity entity);
    }
}

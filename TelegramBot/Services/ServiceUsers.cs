using TelegramBot.Services.Interfaces;
using TelegramBot.Repositories.Interfaces;
using TelegramBot.Entities;

namespace TelegramBot.Services
{
    public class ServiceUsers : IServiceUsers
    {
        private readonly IRepositoryUsers _repositoryUsers;

        public ServiceUsers(IRepositoryUsers repositoryUsers)
        {
            _repositoryUsers = repositoryUsers;
        }

        public AppUserEntity? GetOneUserById(int id)
        {
            return _repositoryUsers.GetOne(id);
        }

        public IList<AppUserEntity> GetUsersByFirstnameAndLastname(string firstname, string lastname)
        {
            var foundByFirstnameUsers = _repositoryUsers
                .GetAll()
                .Where(u =>
                u.Firstname?.Trim().ToLower() == firstname.Trim().ToLower() &&
                u.Lastname?.Trim().ToLower() == lastname.Trim().ToLower())
                .ToList();

            return foundByFirstnameUsers;
        }

        public IList<AppUserEntity> GetUsersByChatId(long chatId)
        {
            var foundByChatIdUsers = _repositoryUsers
                .GetAll()
                .Where(u => u.ChatId == chatId)
                .ToList();

            return foundByChatIdUsers;
        }

        public IList<AppUserEntity> GetAllUsers()
        {
            return _repositoryUsers.GetAll().ToList();
        }

        public void UpdateUser(AppUserEntity entity)
        {
            _repositoryUsers.Update(entity);
        }

        public void AddUser(AppUserEntity entity)
        {
            _repositoryUsers.Add(entity);
        }

        public void DeleteUser(int id)
        {
            var entity = _repositoryUsers.GetOne(id);
            if (entity != null)
            {
                _repositoryUsers.Delete(entity);
            }
        }
    }
}

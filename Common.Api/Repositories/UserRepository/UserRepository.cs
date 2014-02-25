using Common.Api.Models;
using Common.Data;

namespace Common.Api.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private IUnitOfWork unitOfWork;

        public UserRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public User GetUser(int userId)
        {
            return this.unitOfWork.DbContext.Set<User>().Find(userId);
        }
    }
}

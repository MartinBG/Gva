using Common.Data;
using Common.Models;

namespace Common.Infrastructure
{
    // TO DO
    public class UserContextProvider : IUserContextProvider
    {
        private IUnitOfWork unitOfWork;

        public UserContextProvider(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public UserContext GetCurrentUserContext()
        {
            return new UserContext(this.unitOfWork.DbContext.Set<User>().Find(1));
        }
    }
}

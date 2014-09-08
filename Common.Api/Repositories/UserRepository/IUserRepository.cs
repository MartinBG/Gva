using Common.Api.Models;

namespace Common.Api.Repositories.UserRepository
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUser(int userId);

        void spSetUnitTokens(int? unitId = null);
    }
}

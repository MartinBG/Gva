using Common.Api.Models;

namespace Common.Api.Repositories.UserRepository
{
    public interface IUserRepository
    {
        User GetUser(int userId);
    }
}

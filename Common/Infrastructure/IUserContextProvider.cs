
using Common.Models;
namespace Common.Infrastructure
{
    public interface IUserContextProvider
    {
        UserContext GetCurrentUserContext();
    }
}

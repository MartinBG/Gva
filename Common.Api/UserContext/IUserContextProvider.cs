namespace Common.Api.UserContext
{
    public interface IUserContextProvider
    {
        UserContext GetCurrentUserContext();
    }
}

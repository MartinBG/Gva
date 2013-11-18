using Common.Infrastructure;

namespace Gva.Model.Tests.Common
{
    public class MockUserContextProvider : IUserContextProvider
    {
        public UserContext GetCurrentUserContext()
        {
            return new UserContext(
                1,
                "Администратор",
                true,
                new string[] { });
        }

        public void SetCurrentUserContext(UserContext userContext)
        {
        }

        public void ClearCurrentUserContext()
        {
        }
    }
}

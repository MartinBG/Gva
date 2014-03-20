using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Api.UserContext;

namespace Docs.Api.Tests.Mocks
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

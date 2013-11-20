using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Infrastructure;

namespace Regs.Api.Tests.Common
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

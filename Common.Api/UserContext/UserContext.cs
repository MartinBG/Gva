using System.Linq;
using System.Security;
using Common.Api.Models;

namespace Common.Api.UserContext
{
    public class UserContext
    {
        private int userId;

        public UserContext(int userId)
        {
            this.userId = userId;
        }

        public int UserId
        {
            get
            {
                return this.userId;
            }
        }
    }
}

using System.Linq;
using System.Security;
using Common.Api.Models;

namespace Common.Api.UserContext
{
    public class UserContext
    {
        private int userId;

        protected UserContext()
        {
        }

        public UserContext(int userId)
        {
            this.userId = userId;
        }

        public virtual int UserId
        {
            get
            {
                return this.userId;
            }
        }
    }
}

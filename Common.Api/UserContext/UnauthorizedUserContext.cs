using System;

namespace Common.Api.UserContext
{
    public class UnathorizedUserContext : UserContext
    {
        public UnathorizedUserContext()
        {
        }

        public override int UserId
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}

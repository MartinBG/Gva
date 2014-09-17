using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;

namespace Common.Api.OAuth
{
    public class ApplicationOAuthBearerProvider : OAuthBearerAuthenticationProvider
    {
        public override System.Threading.Tasks.Task ValidateIdentity(OAuthValidateIdentityContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (context.Ticket.Identity.Claims.Any(c => c.Issuer != "LOCAL AUTHORITY"))
            {
                context.Rejected();
            }

            return Task.FromResult<object>((object)null);
        }
    }
}

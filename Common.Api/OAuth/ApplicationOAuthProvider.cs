using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Autofac;
using Autofac.Integration.Owin;
using Common.Api.Models;
using Common.Data;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;

namespace Common.Api.OAuth
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        public ApplicationOAuthProvider()
        {
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            User user = await this.TryGetUserWithCredentials(context.UserName, context.Password, context.OwinContext);
            if (user != null)
            {
                ClaimsIdentity oAuthIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
                AuthenticationProperties properties = new AuthenticationProperties(new Dictionary<string, string>
                {
                    { "userId", user.UserId.ToString() }
                });
                context.Validated(new AuthenticationTicket(oAuthIdentity, properties));
                context.Request.Context.Authentication.SignIn(oAuthIdentity);
            }
            else
            {
                context.Rejected();
            }
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }

        private async Task<User> TryGetUserWithCredentials(string username, string password, IOwinContext owinContext)
        {
            var unitOfWork = owinContext.GetAutofacLifetimeScope().Resolve<IUnitOfWork>();
            User user =
                await unitOfWork
                .DbContext
                .Set<User>()
                .Where(u => u.Username == username)
                .SingleOrDefaultAsync();

            if (user != null && user.IsActive && user.HasPassword && user.VerifyPassword(password))
            {
                return user;
            }
            else
            {
                return null;
            }
        }
    }
}

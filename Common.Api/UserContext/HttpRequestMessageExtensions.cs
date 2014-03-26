using System.Net.Http;
using Microsoft.Owin.Security;

namespace Common.Api.UserContext
{
    public static class HttpRequestMessageExtensions
    {
        public static UserContext GetUserContext(this HttpRequestMessage request)
        {
            AuthenticationProperties properties = request.GetOwinEnvironment()["oauth.Properties"] as AuthenticationProperties;
            return new UserContext(int.Parse(properties.Dictionary["userId"]));
        }
    }
}

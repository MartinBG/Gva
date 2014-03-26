using Owin;

namespace Gva.Web.Owin
{
    public static class RerouteExtensions
    {
        public static IAppBuilder UseReroute(this IAppBuilder app, string fromPath, string toPath)
        {
            return app.Use<RerouteMiddleware>(fromPath, toPath);
        }
    }
}
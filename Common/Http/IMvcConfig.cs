using System.Web.Routing;

namespace Common.Http
{
    public interface IMvcConfig
    {
        void RegisterRoutes(RouteCollection routes);
    }
}

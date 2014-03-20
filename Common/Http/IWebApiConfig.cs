using System.Web.Http;

namespace Common.Http
{
    public interface IWebApiConfig
    {
        void RegisterRoutes(HttpConfiguration config);
    }
}

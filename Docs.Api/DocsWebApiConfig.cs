using Common.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Routing;
using System.Web.Http;

namespace Docs.Api
{
    public class DocsWebApiConfig : IWebApiConfig
    {
        public void RegisterRoutes(System.Web.Http.HttpConfiguration config)
        {
            //correspondents
            this.MapRoute(config, HttpMethod.Get   , "api/corrs"     , "Correspondent", "GetCorrespondents"  );
            this.MapRoute(config, HttpMethod.Get   , "api/corrs/new" , "Correspondent", "GetNewCorrespondent");
            this.MapRoute(config, HttpMethod.Get   , "api/corrs/{id}", "Correspondent", "GetCorrespondent"   );
            this.MapRoute(config, HttpMethod.Post  , "api/corrs/{id}", "Correspondent", "UpdateCorrespondent");
            this.MapRoute(config, HttpMethod.Post  , "api/corrs"     , "Correspondent", "CreateCorrespondent");
            this.MapRoute(config, HttpMethod.Delete, "api/corrs/{id}", "Correspondent", "DeleteCorrespondent");

            //mock noms
            this.MapRoute(config, HttpMethod.Get, "api/nomenclatures/correspondentType" , "MockNom", "GetCorrespondentTypes");
            this.MapRoute(config, HttpMethod.Get, "api/nomenclatures/correspondentGroup", "MockNom", "GetCorrespondentGroups");
        }

        private void MapRoute(HttpConfiguration config, HttpMethod method, string route, string controller, string action)
        {
            config.Routes.MapHttpRoute(
                name: Guid.NewGuid().ToString(),
                routeTemplate: route,
                defaults: new { controller = controller, action = action },
                constraints: new { httpMethod = new HttpMethodConstraint(method) });
        }
    }
}

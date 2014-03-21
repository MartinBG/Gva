﻿using Common.Http;
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

            //docs
            this.MapRoute(config, HttpMethod.Get , "api/docs"            , "Doc", "GetDocs");
            this.MapRoute(config, HttpMethod.Post, "api/docs/new/create" , "Doc", "CreateDoc"); //?
            this.MapRoute(config, HttpMethod.Post, "api/docs/{id}/create", "Doc", "CreateChildDoc"); //?
            this.MapRoute(config, HttpMethod.Get , "api/docs/{id}"       , "Doc", "GetDoc");
            this.MapRoute(config, HttpMethod.Post, "api/docs/{id}"       , "Doc", "UpdateDoc");

            //mock noms
            this.MapRoute(config, HttpMethod.Get, "api/nomenclatures/correspondentType" , "MockNom", "GetCorrespondentTypes");
            this.MapRoute(config, HttpMethod.Get, "api/nomenclatures/correspondentGroup", "MockNom", "GetCorrespondentGroups");
            this.MapRoute(config, HttpMethod.Get, "api/nomenclatures/docFormatType"     , "MockNom", "GetDocFormatTypes");
            this.MapRoute(config, HttpMethod.Get, "api/nomenclatures/docCasePartType"   , "MockNom", "GetDocCasePartTypes");
            this.MapRoute(config, HttpMethod.Get, "api/nomenclatures/docDirection"      , "MockNom", "GetDocDirections");
            this.MapRoute(config, HttpMethod.Get, "api/nomenclatures/docTypeGroup"      , "MockNom", "GetDocTypeGroups");
            this.MapRoute(config, HttpMethod.Get, "api/nomenclatures/docType"           , "MockNom", "GetDocTypes");
            this.MapRoute(config, HttpMethod.Get, "api/nomenclatures/unit"              , "MockNom", "GetUnits");
            this.MapRoute(config, HttpMethod.Get, "api/nomenclatures/units"             , "MockNom", "SearchUnits");
            this.MapRoute(config, HttpMethod.Get, "api/nomenclatures/employeeUnit"      , "MockNom", "GetEmployeeUnits");
            this.MapRoute(config, HttpMethod.Get, "api/nomenclatures/correspondent"     , "MockNom", "GetCorrespondents");
            this.MapRoute(config, HttpMethod.Get, "api/nomenclatures/docSourceType"     , "MockNom", "GetDocSourceTypes");
            this.MapRoute(config, HttpMethod.Get, "api/nomenclatures/assignmentType"    , "MockNom", "GetAssignmentTypes");
            this.MapRoute(config, HttpMethod.Get, "api/nomenclatures/docFileKind"       , "MockNom", "GetDocFileKinds");
            this.MapRoute(config, HttpMethod.Get, "api/nomenclatures/docFileType"       , "MockNom", "GetDocFileTypes");
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

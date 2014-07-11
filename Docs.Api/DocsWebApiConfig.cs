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

            //docs
            this.MapRoute(config, HttpMethod.Get   , "api/docs/forSelect"                   , "Doc", "GetDocsForSelect");
            this.MapRoute(config, HttpMethod.Post  , "api/docs/{id}/create"                 , "Doc", "CreateChildDoc"); //?
            this.MapRoute(config, HttpMethod.Post  , "api/docs/{id}/createAcknowledge"      , "Doc", "CreateChildAcknowledgeDoc");
            this.MapRoute(config, HttpMethod.Post  , "api/docs/{id}/createNotAcknowledge"   , "Doc", "CreateChildNotAcknowledgeDoc");
            this.MapRoute(config, HttpMethod.Post  , "api/docs/{id}/setCasePart"            , "Doc", "UpdateDocCasePartType");
            this.MapRoute(config, HttpMethod.Post  , "api/docs/{id}/setDocType"             , "Doc", "UpdateTechDoc");
            this.MapRoute(config, HttpMethod.Post  , "api/docs/{id}/register"               , "Doc", "RegisterDoc");
            this.MapRoute(config, HttpMethod.Post  , "api/docs/{id}/manualRegister"         , "Doc", "ManualRegisterDoc");
            this.MapRoute(config, HttpMethod.Get   , "api/docs/{id}/registerIndex"          , "Doc", "GetDocRegisterIndex");
            this.MapRoute(config, HttpMethod.Post  , "api/docs/{id}/next"                   , "Doc", "SetNextStatus");
            this.MapRoute(config, HttpMethod.Post  , "api/docs/{id}/reverse"                , "Doc", "ReverseStatus");
            this.MapRoute(config, HttpMethod.Post  , "api/docs/{id}/cancel"                 , "Doc", "CancelDoc");
            this.MapRoute(config, HttpMethod.Post  , "api/docs/{id}/workflow"               , "Doc", "CreateDocWorkflow");
            this.MapRoute(config, HttpMethod.Delete, "api/docs/{id}/workflow/{itemId}"      , "Doc", "DeleteDocWorkflow");
            this.MapRoute(config, HttpMethod.Get   , "api/docs/{id}/stages"                 , "Doc", "GetCurrentDocElectronicServiceStage");
            this.MapRoute(config, HttpMethod.Post  , "api/docs/{id}/stages/edit"            , "Doc", "UpdateCurrentDocElectronicServiceStage");
            this.MapRoute(config, HttpMethod.Post  , "api/docs/{id}/stages/end"             , "Doc", "EndCurrentDocElectronicServiceStage");
            this.MapRoute(config, HttpMethod.Delete, "api/docs/{id}/stages"                 , "Doc", "DeleteCurrentDocElectronicServiceStage");
            this.MapRoute(config, HttpMethod.Post  , "api/docs/{id}/stages"                 , "Doc", "CreateDocElectronicServiceStage");
            this.MapRoute(config, HttpMethod.Post  , "api/docs/{id}/markAsRead"             , "Doc", "MarkAsRead");
            this.MapRoute(config, HttpMethod.Post  , "api/docs/{id}/markAsUnread"           , "Doc", "MarkAsUnread");
            this.MapRoute(config, HttpMethod.Get   , "api/docs/{id}/getDocsForChange"       , "Doc", "GetDocsForChange");
            this.MapRoute(config, HttpMethod.Post  , "api/docs/{id}/changeDocParent"        , "Doc", "ChangeDocParent");
            this.MapRoute(config, HttpMethod.Post  , "api/docs/{id}/changeDocClassification", "Doc", "ChangeDocClassification");
            this.MapRoute(config, HttpMethod.Post  , "api/docs/{id}/createNewCase"          , "Doc", "CreateNewCase");
            this.MapRoute(config, HttpMethod.Get   , "api/docs/{id}/getDocSendEmail"        , "Doc", "GetDocSendEmail");
            this.MapRoute(config, HttpMethod.Post  , "api/docs/{id}/postDocSendEmail"       , "Doc", "PostDocSendEmail");
            this.MapRoute(config, HttpMethod.Get   , "api/docs/{id}"                        , "Doc", "GetDoc");
            this.MapRoute(config, HttpMethod.Post  , "api/docs/{id}"                        , "Doc", "UpdateDoc");
            this.MapRoute(config, HttpMethod.Post  , "api/docs/{id}/createTicket"           , "Doc", "CreateDocFileTicket");
            this.MapRoute(config, HttpMethod.Get   , "api/docs/{id}/getRioEditableFile"     , "Doc", "GetRioObjectEditableFile");
            this.MapRoute(config, HttpMethod.Get   , "api/docs"                             , "Doc", "GetDocs");
            this.MapRoute(config, HttpMethod.Post  , "api/docs"                             , "Doc", "CreateDoc");
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

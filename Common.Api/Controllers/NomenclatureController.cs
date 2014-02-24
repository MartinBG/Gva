using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Common.Api.Controllers
{
    public class NomenclatureController : ApiController
    {
        private INomRepository nomRepository;

        public NomenclatureController(INomRepository nomRepository)
        {
            this.nomRepository = nomRepository;
        }

        public HttpResponseMessage GetAddressTypes(string type, string term = null)
        {
            return ControllerContext.Request.CreateResponse(
                HttpStatusCode.OK,
                this.nomRepository.GetNomsWithProperty("addressTypes", "type", type, term));
        }

        public HttpResponseMessage GetDocumentRoles(string categoryAlias, string staffCode, string term = null)
        {
            return ControllerContext.Request.CreateResponse(
                HttpStatusCode.OK,
                this.nomRepository.GetDocumentRoles(categoryAlias, new string[] { staffCode }, term));
        }

        public HttpResponseMessage GetDocumentTypes(bool isIdDocument, string staffCode = null, string term = null)
        {
            return ControllerContext.Request.CreateResponse(
                HttpStatusCode.OK,
                this.nomRepository.GetDocumentTypes(isIdDocument, staffCode, term));
        }

        public HttpResponseMessage GetOtherRoles(string term = null)
        {
            return ControllerContext.Request.CreateResponse(
                HttpStatusCode.OK,
                this.nomRepository.GetDocumentRoles("other", new string[] { "F", "G", "T" }, term));
        }

        public HttpResponseMessage GetNoms(string alias, int? id = null, string term = null, int? parentValueId = null, int? staffTypeId = null, string parentAlias = null)
        {
            IEnumerable<NomValue> noms;

            if (parentValueId.HasValue)
            {
                noms = this.nomRepository.GetNomsForParent(alias, parentValueId.Value, term);
            }
            else
            {
                if (staffTypeId.HasValue)
                {
                    noms = this.nomRepository.GetNomsForGrandparent(alias, staffTypeId.Value, parentAlias, term);
                }
                else
                {
                    noms = this.nomRepository.GetNoms(alias, term);
                }
            }

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, noms);
        }
    }
}
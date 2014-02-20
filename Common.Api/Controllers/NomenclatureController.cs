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

        public HttpResponseMessage GetTrainingRoles(string term = null)
        {
            return ControllerContext.Request.CreateResponse(
                HttpStatusCode.OK,
                this.nomRepository.GetNomsWithProperty("documentRoles", "categoryCode", "O", term));
        }

        public HttpResponseMessage GetTrainingTypes(string term = null)
        {
            return ControllerContext.Request.CreateResponse(
                HttpStatusCode.OK,
                this.nomRepository.GetNomsNotWithCode("documentTypes", new string[] { "3", "4", "5" }, term));
        }

        public HttpResponseMessage GetCheckRoles(string term = null)
        {
            return ControllerContext.Request.CreateResponse(
                HttpStatusCode.OK,
                this.nomRepository.GetNomsWithProperty("documentRoles", "categoryCode", "T", term));
        }

        public HttpResponseMessage GetCheckTypes(string term = null)
        {
            return ControllerContext.Request.CreateResponse(
                HttpStatusCode.OK,
                this.nomRepository.GetNomsNotWithCode("documentTypes", new string[] { "3", "4", "5" }, term));
        }

        public HttpResponseMessage GetIdDocumentTypes(string term = null)
        {
            return ControllerContext.Request.CreateResponse(
                HttpStatusCode.OK,
                this.nomRepository.GetNomsWithCode("documentTypes", new string[] { "3", "4", "5" }, term));
        }

        public HttpResponseMessage GetOtherRoles(string term = null)
        {
            return ControllerContext.Request.CreateResponse(
                HttpStatusCode.OK,
                this.nomRepository.GetNomsWithProperty("documentRoles", "categoryCode", "А", term));
        }

        public HttpResponseMessage GetOtherTypes(string term = null)
        {
            return ControllerContext.Request.CreateResponse(
                HttpStatusCode.OK,
                this.nomRepository.GetNomsNotWithCode("documentTypes", new string[] { "3", "4", "5", "115" }, term));
        }

        public HttpResponseMessage GetNoms(string alias, string term = null, int? parentValueId = null, int? staffTypeId = null, string parentAlias = null)
        {
            IEnumerable<NomValueDO> noms;

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
using System.Collections.Generic;
using System.Web.Http;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;

namespace Common.Api.Controllers
{
    public class NomenclatureController : ApiController
    {
        private INomRepository nomRepository;

        public NomenclatureController(INomRepository nomRepository)
        {
            this.nomRepository = nomRepository;
        }

        public IHttpActionResult GetSchools(int? graduationId = null, string term = null)
        {
            if (!graduationId.HasValue)
            {
                return Ok(new List<NomValue>());
            }

            return Ok(this.nomRepository.GetNomsContainingProperty("schools", "graduationIds", graduationId.ToString(), term));
        }

        public IHttpActionResult GetAddressTypes(string type, string term = null)
        {
            return Ok(this.nomRepository.GetNomsWithProperty("addressTypes", "type", type, term));
        }

        public IHttpActionResult GetDocumentRoles(string categoryAlias, string staffCode, string term = null)
        {
            return Ok(this.nomRepository.GetDocumentRoles(categoryAlias, new string[] { staffCode }, term));
        }

        public IHttpActionResult GetDocumentTypes(bool isIdDocument, string staffCode = null, string term = null)
        {
            return Ok(this.nomRepository.GetDocumentTypes(isIdDocument, new string[] { staffCode }, term));
        }

        public IHttpActionResult GetOtherRoles(string term = null)
        {
            return Ok(this.nomRepository.GetDocumentRoles("other", new string[] { "F", "G", "T" }, term));
        }

        public IHttpActionResult GetOtherTypes(string term = null)
        {
            return Ok(this.nomRepository.GetDocumentTypes(false, new string[] { "F", "G", "T" }, term));
        }

        public IHttpActionResult GetNoms(string alias, int? id = null, string term = null, int? parentValueId = null, int? staffTypeId = null, string parentAlias = null)
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

            return Ok(noms);
        }
    }
}
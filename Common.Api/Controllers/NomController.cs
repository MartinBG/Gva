using System.Collections.Generic;
using System.Web.Http;
using System.Linq;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Linq;
using Newtonsoft.Json.Linq;

namespace Common.Api.Controllers
{
    public class NomController : ApiController
    {
        private INomRepository nomRepository;

        public NomController(INomRepository nomRepository)
        {
            this.nomRepository = nomRepository;
        }

        public IHttpActionResult GetNom(string alias, int id)
        {
            return Ok(this.nomRepository.GetNomValue(alias, id));
        }

        public IHttpActionResult GetNom(string alias, string valueAlias)
        {
            return Ok(this.nomRepository.GetNomValue(alias, valueAlias));
        }

        public IHttpActionResult GetNoms(string alias, [FromUri] int[] ids, [FromUri] string[] valueAliases, string term = null, int? parentValueId = null, int? grandParentValueId = null, int offset = 0, int? limit = null)
        {
            if (ids != null && ids.Length > 0)
            {
                return Ok(this.nomRepository.GetNomValues(alias, ids));
            }
            else if (valueAliases != null && valueAliases.Length > 0)
            {
                return Ok(this.nomRepository.GetNomValues(alias, valueAliases));
            }
            else
            {
                return Ok(this.nomRepository.GetNomValues(alias, term, parentValueId, grandParentValueId, offset, limit));
            }
        }
    }
}
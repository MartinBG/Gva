using System.Linq;
using System.Web.Http;
using Common.Api.Repositories.NomRepository;
using Gva.Api.Repositories.InventoryRepository;

namespace Gva.Api.Controllers
{
    [RoutePrefix("api/suggestions")]
    public class GvaSuggestionController : ApiController
    {
        private IInventoryRepository inventoryRepository;
        private INomRepository nomRepository;

        public GvaSuggestionController(
            IInventoryRepository inventoryRepository,
            INomRepository nomRepository)
        {
            this.inventoryRepository = inventoryRepository;
            this.nomRepository = nomRepository;
        }

        [Route("notes")]
        public IHttpActionResult GetNotes(string term = null)
        {
            var result = this.inventoryRepository.GetNotes(term);

            return Ok(result);
        }

        [Route("ratingNotes")]
        public IHttpActionResult GetRatingNotes(string term = null)
        {
            var result = this.nomRepository.GetNomValues("ratingNotes", term: term).Select(nv => nv.Name);

            return Ok(result);
        }
    }
}


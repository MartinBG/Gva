using System.Linq;
using System.Web.Http;
using Common.Api.Repositories.NomRepository;
using Gva.Api.Repositories.InventoryRepository;
using Gva.Api.Repositories.PersonDocumentRepository;

namespace Gva.Api.Controllers
{
    [RoutePrefix("api/suggestions")]
    public class GvaSuggestionController : ApiController
    {
        private IInventoryRepository inventoryRepository;
        private INomRepository nomRepository;
        private IPersonDocumentRepository personDocumentRepository;

        public GvaSuggestionController(
            IInventoryRepository inventoryRepository,
            INomRepository nomRepositor,
            IPersonDocumentRepository personDocumentRepository)
        {
            this.inventoryRepository = inventoryRepository;
            this.nomRepository = nomRepository;
            this.personDocumentRepository = personDocumentRepository;
        }

        [Route("notes")]
        public IHttpActionResult GetNotes(string term = null)
        {
            var result = this.inventoryRepository.GetNotes(term);

            return Ok(result);
        }

        [Route("personsNotes")]
        public IHttpActionResult GetPersonsNotes(string term = null)
        {
            var result = this.personDocumentRepository.GetNotes(term);

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


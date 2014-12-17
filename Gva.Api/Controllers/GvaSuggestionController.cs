using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Json;
using Common.Linq;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using Gva.Api.Repositories.AircraftRepository;
using Gva.Api.Repositories.AirportRepository;
using Gva.Api.Repositories.ApplicationRepository;
using Newtonsoft.Json.Linq;
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


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Gva.Api.ModelsDO.Persons.Reports;
using Gva.Api.Repositories.InventoryRepository;

namespace Gva.Api.Controllers.Persons
{
    [RoutePrefix("api/persons/personReferences")]
    [Authorize]
    public class PersonReferencesController : ApiController
    {
        private IInventoryRepository inventoryRepository;

        public PersonReferencesController(
            IInventoryRepository inventoryRepository)
        {
            this.inventoryRepository = inventoryRepository;
        }

        [Route("documents")]
        public IHttpActionResult GetDocuments(string documentPart = null, DateTime? fromDate = null, DateTime? toDate = null, int? typeId = null)
        {
            var documents = this.inventoryRepository.GetInventoryItems(setAlias: "Person", documentPart: documentPart, fromDate: fromDate, toDate: toDate, typeId: typeId);

            return Ok(documents.Select(i => new PersonReferenceDocumentDO(i)).ToList());
        }
    }
}

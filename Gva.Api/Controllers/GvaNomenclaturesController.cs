using System.Collections.Generic;
using System.Web.Http;
using Gva.Api.Models;
using Gva.Api.Repositories.PersonRepository;
using System.Linq;

namespace Gva.Api.Controllers
{
    [RoutePrefix("api/nomenclatures")]
    public class GvaNomenclaturesController : ApiController
    {
        private IPersonRepository personRepository;

        public GvaNomenclaturesController(IPersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }

        [Route("persons")]
        public IHttpActionResult GetPersons(int? id, string term, string va)
        {
            if (id.HasValue)
            {
                var person = this.personRepository.GetPerson(id.Value);

                var returnValue = new
                {
                    nomValueId = person.GvaPersonLotId,
                    name = person.Names,
                    isActive = true
                };

                return Ok(returnValue);
            }
            else
            {

                var returnValue =
                    this.personRepository.GetPersons(null, null, null, null, null, null, false)
                    .Select(e => new
                    {
                        nomValueId = e.GvaPersonLotId,
                        name = e.Names,
                        isActive = true
                    });

                return Ok(returnValue);
            }
        }
    }
}


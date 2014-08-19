using System.Web.Http;
using Common.Data;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.ApplicationRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Persons
{
    [RoutePrefix("api/persons/{lotId}/personAddresses")]
    [Authorize]
    public class PersonAddressesController : GvaApplicationPartController<PersonAddressDO>
    {
        public PersonAddressesController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IApplicationRepository applicationRepository,
            ILotEventDispatcher lotEventDispatcher)
            : base("personAddresses", unitOfWork, lotRepository, applicationRepository, lotEventDispatcher)
        {
        }

        [Route("new")]
        public IHttpActionResult GetNewAddress(int lotId)
        {
            PersonAddressDO newAddress = new PersonAddressDO();

            return Ok(new ApplicationPartVersionDO<PersonAddressDO>(newAddress));
        }
    }
}
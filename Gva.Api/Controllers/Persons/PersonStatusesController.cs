using System;
using System.Web.Http;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.ApplicationRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Persons
{
    [RoutePrefix("api/persons/{lotId}/personStatuses")]
    [Authorize]
    public class PersonStatusesController : GvaSimplePartController<PersonStatusDO>
    {
        public PersonStatusesController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("personStatuses", unitOfWork, lotRepository, lotEventDispatcher, userContext)
        {
        }

        [Route("new")]
        public IHttpActionResult GetNewStatus(int lotId)
        {
            PersonStatusDO newStatus = new PersonStatusDO()
            {
                DocumentDateValidFrom = DateTime.Now
            };

            return Ok(new SimplePartDO<PersonStatusDO>(newStatus));
        }
    }
}
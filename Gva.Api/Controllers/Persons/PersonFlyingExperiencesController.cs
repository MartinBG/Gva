using System.Web.Http;
using Common.Data;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.ApplicationRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Persons
{
    [RoutePrefix("api/persons/{lotId}/personFlyingExperiences")]
    [Authorize]
    public class PersonFlyingExperiencesController : GvaApplicationPartController<PersonFlyingExperienceDO>
    {
        public PersonFlyingExperiencesController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IApplicationRepository applicationRepository,
            ILotEventDispatcher lotEventDispatcher)
            : base("personFlyingExperiences", unitOfWork, lotRepository, applicationRepository, lotEventDispatcher)
        {
        }

        [Route("new")]
        public IHttpActionResult GetNewFlyingExperience(int lotId)
        {
            PersonFlyingExperienceDO newFlyingExperience = new PersonFlyingExperienceDO();

            return Ok(new ApplicationPartVersionDO<PersonFlyingExperienceDO>(newFlyingExperience));
        }
    }
}
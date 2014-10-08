using System.Web.Http;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.FileRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Persons
{
    [RoutePrefix("api/persons/{lotId}/personFlyingExperiences")]
    [Authorize]
    public class PersonFlyingExperiencesController : GvaCaseTypePartController<PersonFlyingExperienceDO>
    {
        public PersonFlyingExperiencesController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("personFlyingExperiences", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
        }

        [Route("new")]
        public IHttpActionResult GetNewFlyingExperience(int lotId)
        {
            PersonFlyingExperienceDO newFlyingExperience = new PersonFlyingExperienceDO();

            return Ok(new CaseTypePartDO<PersonFlyingExperienceDO>(newFlyingExperience));
        }
    }
}
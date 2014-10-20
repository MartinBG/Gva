using System.Web.Http;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.FileRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Persons
{
    [RoutePrefix("api/persons/{lotId}/personDocumentEducations")]
    [Authorize]
    public class PersonEducationsController : GvaCaseTypePartController<PersonEducationDO>
    {
        public PersonEducationsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("personDocumentEducations", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        { }

        [Route("new")]
        public IHttpActionResult GetNewEducation(int lotId)
        {
            PersonEducationDO newEducation = new PersonEducationDO();

            return Ok(new CaseTypePartDO<PersonEducationDO>(newEducation));
        }
    }
}
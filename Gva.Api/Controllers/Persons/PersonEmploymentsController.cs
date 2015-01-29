using System.Web.Http;
using Common.Api.Repositories.NomRepository;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.FileRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Persons
{
    [RoutePrefix("api/persons/{lotId}/personDocumentEmployments")]
    [Authorize]
    public class PersonEmploymentsController : GvaCaseTypePartController<PersonEmploymentDO>
    {
        private INomRepository nomRepository;

        public PersonEmploymentsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            INomRepository nomRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("personDocumentEmployments", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.nomRepository = nomRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewEmployment(int lotId)
        {
            PersonEmploymentDO newEmployment = new PersonEmploymentDO()
            {
                Valid = this.nomRepository.GetNomValue("boolean", "yes"),
                Country = this.nomRepository.GetNomValue("countries", "BG")
            };

            return Ok(new CaseTypePartDO<PersonEmploymentDO>(newEmployment));
        }
    }
}
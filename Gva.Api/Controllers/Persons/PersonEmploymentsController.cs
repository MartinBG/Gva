using System.Collections.Generic;
using System.Web.Http;
using Common.Api.Repositories.NomRepository;
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
    [RoutePrefix("api/persons/{lotId}/personDocumentEmployments")]
    [Authorize]
    public class PersonEmploymentsController : GvaCaseTypePartController<PersonEmploymentDO>
    {
        private IApplicationRepository applicationRepository;
        private ILotRepository lotRepository;
        private INomRepository nomRepository;

        public PersonEmploymentsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            IApplicationRepository applicationRepository,
            INomRepository nomRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("personDocumentEmployments", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.applicationRepository = applicationRepository;
            this.lotRepository = lotRepository;
            this.nomRepository = nomRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewEmployment(int lotId, int? appId = null)
        {
            PersonEmploymentDO newEmployment = new PersonEmploymentDO();
            newEmployment.Valid = this.nomRepository.GetNomValue("boolean", "yes");

            CaseDO caseDO = null;
            if (appId.HasValue)
            {
                this.lotRepository.GetLotIndex(lotId);
                caseDO = new CaseDO()
                {
                    IsAdded = true,
                    Applications = new List<ApplicationNomDO>()
                    {
                        this.applicationRepository.GetInitApplication(appId)
                    }
                };
            }

            return Ok(new CaseTypePartDO<PersonEmploymentDO>(newEmployment, caseDO));
        }
    }
}
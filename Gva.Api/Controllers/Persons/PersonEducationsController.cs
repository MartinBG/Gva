using System.Collections.Generic;
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
    [RoutePrefix("api/persons/{lotId}/personDocumentEducations")]
    [Authorize]
    public class PersonEducationsController : GvaFilePartController<PersonEducationDO>
    {
        private IApplicationRepository applicationRepository;
        private ILotRepository lotRepository;

        public PersonEducationsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            IApplicationRepository applicationRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("personDocumentEducations", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.applicationRepository = applicationRepository;
            this.lotRepository = lotRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewEducation(int lotId, int? appId = null)
        {
            PersonEducationDO newEducation = new PersonEducationDO();

            var cases = new List<CaseDO>();
            if (appId.HasValue)
            {
                this.lotRepository.GetLotIndex(lotId);
                cases.Add(new CaseDO()
                {
                    IsAdded = true,
                    Applications = new List<ApplicationNomDO>()
                    {
                        this.applicationRepository.GetInitApplication(appId)
                    }
                });
            }

            return Ok(new FilePartVersionDO<PersonEducationDO>(newEducation, cases));
        }
    }
}
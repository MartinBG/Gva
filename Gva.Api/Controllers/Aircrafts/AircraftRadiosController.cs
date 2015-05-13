using System;
using System.Linq;
using System.Web.Http;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Aircrafts;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.FileRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Aircrafts
{
    [RoutePrefix("api/aircrafts/{lotId}/aircraftCertRadios")]
    [Authorize]
    public class AircraftRadiosController : GvaCaseTypePartController<AircraftCertRadioDO>
    {
        private INomRepository nomRepository;
        private ICaseTypeRepository caseTypeRepository;
        private ILotRepository lotRepository;
        private IApplicationRepository applicationRepository;

        public AircraftRadiosController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            ICaseTypeRepository caseTypeRepository,
            INomRepository nomRepository,
            IApplicationRepository applicationRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("aircraftCertRadios", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.nomRepository = nomRepository;
            this.caseTypeRepository = caseTypeRepository;
            this.lotRepository = lotRepository;
            this.applicationRepository = applicationRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewCertRadio(int lotId, int? appId = null)
        {
            AircraftCertRadioDO newCertRadio = new AircraftCertRadioDO()
            {
                IssueDate = DateTime.Now,
                OwnerOperIsOrg = true
            };

            GvaCaseType caseType = this.caseTypeRepository.GetCaseTypesForSet("aircraft").Single();
            CaseDO caseDO = new CaseDO()
            {
                CaseType = new NomValue()
                {
                    NomValueId = caseType.GvaCaseTypeId,
                    Name = caseType.Name,
                    Alias = caseType.Alias
                }
            };

            if (appId.HasValue)
            {
                this.lotRepository.GetLotIndex(lotId);

                caseDO.Applications.Add(this.applicationRepository.GetInitApplication(appId));
            }

            return Ok(new CaseTypePartDO<AircraftCertRadioDO>(newCertRadio, caseDO));
        }
    }
}
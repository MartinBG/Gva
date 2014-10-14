using System;
using System.Linq;
using System.Web.Http;
using Common.Api.Models;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Common;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.FileRepository;
using Newtonsoft.Json.Linq;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Airports
{
    [RoutePrefix("api/airports/{lotId}/airportDocumentApplications")]
    [Authorize]
    public class AirportApplicationsController : GvaCaseTypePartController<DocumentApplicationDO>
    {
        private string path;
        private IUnitOfWork unitOfWork;
        private IFileRepository fileRepository;
        private IApplicationRepository applicationRepository;
        private ILotRepository lotRepository;
        private ILotEventDispatcher lotEventDispatcher;
        private UserContext userContext;
        private ICaseTypeRepository caseTypeRepository;

        public AirportApplicationsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            IApplicationRepository applicationRepository,
            ICaseTypeRepository caseTypeRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("airportDocumentApplications", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.path = "airportDocumentApplications";
            this.unitOfWork = unitOfWork;
            this.fileRepository = fileRepository;
            this.applicationRepository = applicationRepository;
            this.lotRepository = lotRepository;
            this.lotEventDispatcher = lotEventDispatcher;
            this.userContext = userContext;
            this.caseTypeRepository = caseTypeRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewApplication(int lotId)
        {
            DocumentApplicationDO newApplication = new DocumentApplicationDO()
            {
                DocumentDate = DateTime.Now
            };

            GvaCaseType caseType = this.caseTypeRepository.GetCaseTypesForSet("airport").Single();

            CaseDO caseDO = new CaseDO()
            {
                CaseType = new NomValue()
                {
                    NomValueId = caseType.GvaCaseTypeId,
                    Name = caseType.Name,
                    Alias = caseType.Alias
                }
            };

            return Ok(new CaseTypePartDO<DocumentApplicationDO>(newApplication, caseDO));
        }

        public override IHttpActionResult PostNewPart(int lotId, CaseTypePartDO<DocumentApplicationDO> application)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var lot = this.lotRepository.GetLotIndex(lotId);

                PartVersion<DocumentApplicationDO> partVersion = lot.CreatePart(
                    path + "/*",
                    application.Part,
                    this.userContext);

                this.fileRepository.AddFileReference(partVersion.Part, application.Case);

                lot.Commit(this.userContext, this.lotEventDispatcher);

                GvaApplication gvaApplication = new GvaApplication()
                {
                    LotId = lot.LotId,
                    GvaAppLotPartId = partVersion.Part.PartId
                };

                this.applicationRepository.AddGvaApplication(gvaApplication);

                this.unitOfWork.Save();

                this.lotRepository.ExecSpSetLotPartTokens(partVersion.PartId);

                transaction.Commit();

                return Ok();
            }
        }

        public override IHttpActionResult DeletePart(int lotId, int partIndex)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var lot = this.lotRepository.GetLotIndex(lotId);
                var partVersion = lot.DeletePart<DocumentApplicationDO>(string.Format("{0}/{1}", this.path, partIndex), this.userContext);

                this.fileRepository.DeleteFileReferences(partVersion.Part);
                this.applicationRepository.DeleteGvaApplication(partVersion.PartId);

                lot.Commit(this.userContext, lotEventDispatcher);

                this.unitOfWork.Save();

                this.lotRepository.ExecSpSetLotPartTokens(partVersion.PartId);

                transaction.Commit();

                return Ok();
            }
        }
    }
}
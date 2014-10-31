using System.Collections.Generic;
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
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Aircrafts
{
    [RoutePrefix("api/aircrafts/{lotId}/aircraftDocumentApplications")]
    [Authorize]
    public class AircraftDocumentApplicationsController : GvaCaseTypePartController<DocumentApplicationDO>
    {
        private string path;
        private ILotRepository lotRepository;
        private IFileRepository fileRepository;
        private IApplicationRepository applicationRepository;
        private IUnitOfWork unitOfWork;
        private ILotEventDispatcher lotEventDispatcher;
        private ICaseTypeRepository caseTypeRepository;
        private UserContext userContext;

        public AircraftDocumentApplicationsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            IApplicationRepository applicationRepository,
            ICaseTypeRepository caseTypeRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("aircraftDocumentApplications", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.path = "aircraftDocumentApplications";
            this.lotRepository = lotRepository;
            this.fileRepository = fileRepository;
            this.applicationRepository = applicationRepository;
            this.unitOfWork = unitOfWork;
            this.lotEventDispatcher = lotEventDispatcher;
            this.userContext = userContext;
            this.caseTypeRepository = caseTypeRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewDocumentApplication(int lotId)
        {
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

            return Ok(new CaseTypePartDO<DocumentApplicationDO>(new DocumentApplicationDO(), caseDO));
        }

        public override IHttpActionResult PostNewPart(int lotId, CaseTypePartDO<DocumentApplicationDO> application)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var lot = this.lotRepository.GetLotIndex(lotId);

                PartVersion<DocumentApplicationDO> partVersion = lot.CreatePart<DocumentApplicationDO>(path + "/*", application.Part, this.userContext);

                this.fileRepository.AddFileReference(partVersion.Part, application.Case);

                lot.Commit(this.userContext, lotEventDispatcher);

                GvaApplication gvaApplication = new GvaApplication()
                {
                    Lot = lot,
                    GvaAppLotPart = partVersion.Part
                };

                applicationRepository.AddGvaApplication(gvaApplication);

                this.unitOfWork.Save();

                this.lotRepository.ExecSpSetLotPartTokens(partVersion.PartId);

                transaction.Commit();
            }

            return Ok();
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


        public override IHttpActionResult GetParts(int lotId, int? caseTypeId = null)
        {
            var partVersions = this.lotRepository.GetLotIndex(lotId).Index.GetParts<DocumentApplicationViewDO>(this.path);

            var appStages = (from gas in this.unitOfWork.DbContext.Set<GvaApplicationStage>()
                             join ga in this.unitOfWork.DbContext.Set<GvaApplication>().Where(a => a.LotId == lotId) on gas.GvaApplicationId equals ga.GvaApplicationId
                             group gas by ga.GvaAppLotPartId into appSt
                             select new { appSt = appSt.OrderByDescending(s => s.GvaStageId).FirstOrDefault(), partId = appSt.Key.Value })
                            .ToList();
            var stages = this.unitOfWork.DbContext.Set<GvaStage>();

            List<CaseTypePartDO<DocumentApplicationViewDO>> partVersionDOs = new List<CaseTypePartDO<DocumentApplicationViewDO>>();
            foreach (var partVersion in partVersions)
            {
                var appStage = appStages.Where(ap => ap.partId == partVersion.Part.PartId);
                if (appStage.Count() > 0)
                {
                    int stageId = appStage.Single().appSt.GvaStageId;
                    var stage = stages.Where(s => s.GvaStageId == stageId).Single();
                    partVersion.Content.Stage = new NomValue()
                    {
                        Name = stage.Name,
                        Alias = stage.Alias
                    };
                }

                var lotFile = this.fileRepository.GetFileReference(partVersion.PartId, caseTypeId);
                if (!caseTypeId.HasValue || lotFile != null)
                {
                    partVersionDOs.Add(new CaseTypePartDO<DocumentApplicationViewDO>(partVersion, lotFile));
                }
            }

            return Ok(partVersionDOs);
        }
    }
}
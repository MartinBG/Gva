using System;
using System.Linq;
using System.Web.Http;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Equipments;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.FileRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Equipments
{
    [RoutePrefix("api/equipments/{lotId}/equipmentDocumentOthers")]
    [Authorize]
    public class EquipmentDocumentOthersController : GvaCaseTypePartController<EquipmentDocumentOtherDO>
    {
        private IFileRepository fileRepository;
        private IApplicationRepository applicationRepository;
        private ILotRepository lotRepository;
        private INomRepository nomRepository;
        private ICaseTypeRepository caseTypeRepository;

        public EquipmentDocumentOthersController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            IApplicationRepository applicationRepository,
            INomRepository nomRepository,
            ICaseTypeRepository caseTypeRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("equipmentDocumentOthers", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.applicationRepository = applicationRepository;
            this.lotRepository = lotRepository;
            this.nomRepository = nomRepository;
            this.fileRepository = fileRepository;
            this.caseTypeRepository = caseTypeRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewDocumentOther(int lotId, int? appId = null)
        {
            GvaCaseType caseType = this.caseTypeRepository.GetCaseTypesForSet("equipment").Single();
            CaseDO caseDO = new CaseDO()
            {
                CaseType = new NomValue()
                {
                    NomValueId = caseType.GvaCaseTypeId,
                    Name = caseType.Name,
                    Alias = caseType.Alias
                },
                BookPageNumber = this.fileRepository.GetNextBPN(lotId, caseType.GvaCaseTypeId).ToString()
            };

            if (appId.HasValue)
            {
                this.lotRepository.GetLotIndex(lotId);
                caseDO.Applications.Add(this.applicationRepository.GetInitApplication(appId));
            }

            EquipmentDocumentOtherDO newDocumentOther = new EquipmentDocumentOtherDO()
            {
                DocumentDateValidFrom = DateTime.Now,
                Valid = this.nomRepository.GetNomValue("boolean", "yes")
            };

            return Ok(new CaseTypePartDO<EquipmentDocumentOtherDO>(newDocumentOther, caseDO));
        }
    }
}
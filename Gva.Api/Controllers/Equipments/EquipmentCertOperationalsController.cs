using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Equipments;
using Gva.Api.ModelsDO.Organizations;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.FileRepository;
using Gva.Api.Repositories.OrganizationRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Equipments
{
    [RoutePrefix("api/equipments/{lotId}/equipmentCertOperationals")]
    [Authorize]
    public class EquipmentCertOperationalsController : GvaCaseTypePartController<EquipmentCertOperationalDO>
    {
        private INomRepository nomRepository;
        private ICaseTypeRepository caseTypeRepository;
        private IFileRepository fileRepository;

        public EquipmentCertOperationalsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IApplicationRepository applicationRepository,
            INomRepository nomRepository,
            ILotEventDispatcher lotEventDispatcher,
            ICaseTypeRepository caseTypeRepository,
            IFileRepository fileRepository,
            UserContext userContext)
            : base("equipmentCertOperationals", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.nomRepository = nomRepository;
            this.fileRepository = fileRepository;
            this.caseTypeRepository = caseTypeRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewCertOperational(int lotId)
        {
           var caseType = this.caseTypeRepository.GetCaseTypesForSet("Aircraft").Single();
           var caseDO = new CaseDO()
           {
               CaseType = new NomValue()
               {
                   NomValueId = caseType.GvaCaseTypeId,
                   Name = caseType.Name,
                   Alias = caseType.Alias
               },
               IsAdded = true
           };

           EquipmentCertOperationalDO newCertOperational = new EquipmentCertOperationalDO()
           {
               Valid = this.nomRepository.GetNomValue("boolean", "yes")
           };

           return Ok(new CaseTypePartDO<EquipmentCertOperationalDO>(newCertOperational, caseDO));
        }
    }
}
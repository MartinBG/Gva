using System;
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
        private ILotRepository lotRepository;
        private IApplicationRepository applicationRepository;
        private INomRepository nomRepository;
        private ICaseTypeRepository caseTypeRepository;
        private IFileRepository fileRepository;

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
            this.lotRepository = lotRepository;
            this.applicationRepository = applicationRepository;
            this.nomRepository = nomRepository;
            this.caseTypeRepository = caseTypeRepository;
            this.fileRepository = fileRepository;
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
                }
            };

            if (appId.HasValue)
            {
                caseDO.Applications.Add(this.applicationRepository.GetNomApplication(appId.Value));
            }

            EquipmentDocumentOtherDO newDocumentOther = new EquipmentDocumentOtherDO()
            {
                DocumentDateValidFrom = DateTime.Now,
                ValidId = this.nomRepository.GetNomValue("boolean", "yes").NomValueId
            };

            return Ok(new CaseTypePartDO<EquipmentDocumentOtherDO>(newDocumentOther, caseDO));
        }

        public override IHttpActionResult GetParts(int lotId, int? caseTypeId = null)
        {
            var documentOthers = this.lotRepository.GetLotIndex(lotId).Index.GetParts<EquipmentDocumentOtherDO>("equipmentDocumentOthers");

            List<EquipmentDocumentOtherViewDO> documentOtherViewDOs = new List<EquipmentDocumentOtherViewDO>();
            foreach (var documentOtherPartVersion in documentOthers)
            {
                var lotFile = this.fileRepository.GetFileReference(documentOtherPartVersion.PartId, caseTypeId);
                if (!caseTypeId.HasValue || lotFile != null)
                {
                    documentOtherViewDOs.Add(new EquipmentDocumentOtherViewDO()
                    {
                        Case = lotFile != null ? new CaseDO(lotFile) : null,
                        PartIndex = documentOtherPartVersion.Part.Index,
                        PartId = documentOtherPartVersion.PartId,
                        DocumentDateValidFrom = documentOtherPartVersion.Content.DocumentDateValidFrom,
                        DocumentDateValidTo = documentOtherPartVersion.Content.DocumentDateValidTo,
                        DocumentNumber = documentOtherPartVersion.Content.DocumentNumber,
                        DocumentPublisher = documentOtherPartVersion.Content.DocumentPublisher,
                        Notes = documentOtherPartVersion.Content.Notes,
                        Valid = documentOtherPartVersion.Content.ValidId.HasValue ? this.nomRepository.GetNomValue("boolean", documentOtherPartVersion.Content.ValidId.Value) : null,
                        DocumentType = documentOtherPartVersion.Content.DocumentTypeId.HasValue ? this.nomRepository.GetNomValue("documentTypes", documentOtherPartVersion.Content.DocumentTypeId.Value) : null,
                        DocumentRole = documentOtherPartVersion.Content.DocumentRoleId.HasValue ? this.nomRepository.GetNomValue("documentRoles", documentOtherPartVersion.Content.DocumentRoleId.Value) : null
                    });
                }
            }
            return Ok(documentOtherViewDOs);
        }
    }
}
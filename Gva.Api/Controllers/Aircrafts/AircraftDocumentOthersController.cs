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
using Gva.Api.ModelsDO.Aircrafts;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.FileRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Aircrafts
{
    [RoutePrefix("api/aircrafts/{lotId}/aircraftDocumentOthers")]
    [Authorize]
    public class AircraftDocumentOthersController : GvaCaseTypePartController<AircraftDocumentOtherDO>
    {
        private ICaseTypeRepository caseTypeRepository;
        private IApplicationRepository applicationRepository;
        private ILotRepository lotRepository;
        private INomRepository nomRepository;
        private IFileRepository fileRepository;

        public AircraftDocumentOthersController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            IApplicationRepository applicationRepository,
            ICaseTypeRepository caseTypeRepository,
            ILotEventDispatcher lotEventDispatcher,
            INomRepository nomRepository,
            UserContext userContext)
            : base("aircraftDocumentOthers", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.caseTypeRepository = caseTypeRepository;
            this.applicationRepository = applicationRepository;
            this.lotRepository = lotRepository;
            this.nomRepository = nomRepository;
            this.fileRepository = fileRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewDocumentOther(int lotId, int? appId = null)
        {
            AircraftDocumentOtherDO newDocumentOther = new AircraftDocumentOtherDO()
            {
                DocumentDateValidFrom = DateTime.Now,
                ValidId = this.nomRepository.GetNomValue("boolean", "yes").NomValueId
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
                caseDO.Applications.Add(this.applicationRepository.GetNomApplication(appId.Value));
            }

            return Ok(new CaseTypePartDO<AircraftDocumentOtherDO>(newDocumentOther, caseDO));
        }

        public override IHttpActionResult GetParts(int lotId, int? caseTypeId = null)
        {
            var documentOthers = this.lotRepository.GetLotIndex(lotId).Index.GetParts<AircraftDocumentOtherDO>("aircraftDocumentOthers");

            List<AircraftDocumentOtherViewDO> documentOtherViewDOs = new List<AircraftDocumentOtherViewDO>();
            foreach (var documentOtherPartVersion in documentOthers)
            {
                var lotFile = this.fileRepository.GetFileReference(documentOtherPartVersion.PartId, caseTypeId);
                if (!caseTypeId.HasValue || lotFile != null)
                {
                    documentOtherViewDOs.Add(new AircraftDocumentOtherViewDO()
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
                        DocumentType = documentOtherPartVersion.Content.OtherDocumentTypeId.HasValue ? this.nomRepository.GetNomValue("documentTypes", documentOtherPartVersion.Content.OtherDocumentTypeId.Value) : null,
                        DocumentRole = documentOtherPartVersion.Content.OtherDocumentRoleId.HasValue ? this.nomRepository.GetNomValue("documentRoles", documentOtherPartVersion.Content.OtherDocumentRoleId.Value) : null
                    });
                }
            }
            return Ok(documentOtherViewDOs);
        }
    }
}
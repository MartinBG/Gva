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
using Gva.Api.ModelsDO.Airports;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.FileRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Airports
{
    [RoutePrefix("api/airports/{lotId}/airportDocumentOthers")]
    [Authorize]
    public class AirportDocumentOthersController : GvaCaseTypePartController<AirportDocumentOtherDO>
    {
        private ILotRepository lotRepository;
        private IApplicationRepository applicationRepository;
        private ICaseTypeRepository caseTypeRepository;
        private INomRepository nomRepository;
        private IFileRepository fileRepository;

        public AirportDocumentOthersController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            IApplicationRepository applicationRepository,
            ILotEventDispatcher lotEventDispatcher,
            ICaseTypeRepository caseTypeRepository,
            UserContext userContext)
            : base("airportDocumentOthers", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.lotRepository = lotRepository;
            this.applicationRepository = applicationRepository;
            this.caseTypeRepository = caseTypeRepository;
            this.nomRepository = nomRepository;
            this.fileRepository = fileRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewDocumentOther(int lotId, int? appId = null)
        {
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

            if (appId.HasValue)
            {
                caseDO.Applications.Add(this.applicationRepository.GetNomApplication(appId.Value));
            }

            AirportDocumentOtherDO newDocumentOther = new AirportDocumentOtherDO()
            {
                DocumentDateValidFrom = DateTime.Now,
                ValidId = this.nomRepository.GetNomValue("boolean", "yes").NomValueId
            };

            return Ok(new CaseTypePartDO<AirportDocumentOtherDO>(newDocumentOther, caseDO));
        }

        public override IHttpActionResult GetParts(int lotId, int? caseTypeId = null)
        {
            var documentOthers = this.lotRepository.GetLotIndex(lotId).Index.GetParts<AirportDocumentOtherDO>("airportDocumentOthers");

            List<AirportDocumentOtherViewDO> documentOtherViewDOs = new List<AirportDocumentOtherViewDO>();
            foreach (var documentOtherPartVersion in documentOthers)
            {
                var lotFile = this.fileRepository.GetFileReference(documentOtherPartVersion.PartId, caseTypeId);
                if (!caseTypeId.HasValue || lotFile != null)
                {
                    documentOtherViewDOs.Add(new AirportDocumentOtherViewDO()
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
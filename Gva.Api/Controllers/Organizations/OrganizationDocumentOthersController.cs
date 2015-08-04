using System;
using System.Collections.Generic;
using System.Web.Http;
using Common.Api.Repositories.NomRepository;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Organizations;
using Gva.Api.Repositories.FileRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Organizations
{
    [RoutePrefix("api/organizations/{lotId}/organizationDocumentOthers")]
    [Authorize]
    public class OrganizationDocumentOthersController : GvaCaseTypePartController<OrganizationDocumentOtherDO>
    {
        private ILotRepository lotRepository;
        private INomRepository nomRepository;
        private IFileRepository fileRepository;

        public OrganizationDocumentOthersController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            INomRepository nomRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("organizationDocumentOthers", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.lotRepository = lotRepository;
            this.nomRepository = nomRepository;
            this.fileRepository = fileRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewDocumentOther(int lotId)
        {
            OrganizationDocumentOtherDO newDocumentOther = new OrganizationDocumentOtherDO()
            {
                DocumentDateValidFrom = DateTime.Now
            };

            return Ok(new CaseTypePartDO<OrganizationDocumentOtherDO>(newDocumentOther));
        }

        public override IHttpActionResult GetParts(int lotId, int? caseTypeId = null)
        {
            var documentOthers = this.lotRepository.GetLotIndex(lotId).Index.GetParts<OrganizationDocumentOtherDO>("equipmentDocumentOthers");

            List<OrganizationDocumentOtherViewDO> documentOtherViewDOs = new List<OrganizationDocumentOtherViewDO>();
            foreach (var documentOtherPartVersion in documentOthers)
            {
                var lotFile = this.fileRepository.GetFileReference(documentOtherPartVersion.PartId, caseTypeId);
                if (!caseTypeId.HasValue || lotFile != null)
                {
                    documentOtherViewDOs.Add(new OrganizationDocumentOtherViewDO()
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
using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Http;
using Common.Api.Repositories.NomRepository;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.FileRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Persons
{
    [RoutePrefix("api/persons/{lotId}/personDocumentOthers")]
    [Authorize]
    public class PersonDocumentOthersController : GvaCaseTypePartController<PersonDocumentOtherDO>
    {
        private INomRepository nomRepository;
        private IFileRepository fileRepository;
        private ILotRepository lotRepository;

        public PersonDocumentOthersController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            INomRepository nomRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("personDocumentOthers", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.nomRepository = nomRepository;
            this.lotRepository = lotRepository;
            this.fileRepository = fileRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewDocumentOther(int lotId)
        {
            PersonDocumentOtherDO newDocumentOther = new PersonDocumentOtherDO()
            {
                DocumentDateValidFrom = DateTime.Now,
                ValidId = this.nomRepository.GetNomValue("boolean", "yes").NomValueId
            };

            return Ok(new CaseTypePartDO<PersonDocumentOtherDO>(newDocumentOther, null));
        }

        public override IHttpActionResult GetParts(int lotId, int? caseTypeId = null)
        {
            var documentOthers = this.lotRepository.GetLotIndex(lotId).Index.GetParts<PersonDocumentOtherDO>("personDocumentOthers");

            List<PersonDocumentOtherViewDO> documentOtherViewDOs = new List<PersonDocumentOtherViewDO>();
            foreach (var documentOtherPartVersion in documentOthers)
            {
                var lotFile = this.fileRepository.GetFileReference(documentOtherPartVersion.PartId, caseTypeId);
                if (!caseTypeId.HasValue || lotFile != null)
                {
                    documentOtherViewDOs.Add(new PersonDocumentOtherViewDO()
                    {
                        Case = lotFile != null? new CaseDO(lotFile) : null,
                        PartIndex = documentOtherPartVersion.Part.Index,
                        PartId = documentOtherPartVersion.PartId,
                        DocumentDateValidFrom = documentOtherPartVersion.Content.DocumentDateValidFrom,
                        DocumentDateValidTo = documentOtherPartVersion.Content.DocumentDateValidTo,
                        DocumentNumber = documentOtherPartVersion.Content.DocumentNumber,
                        DocumentPublisher = documentOtherPartVersion.Content.DocumentPublisher,
                        Notes = documentOtherPartVersion.Content.Notes,
                        Valid = documentOtherPartVersion.Content.ValidId.HasValue ? this.nomRepository.GetNomValue("boolean", documentOtherPartVersion.Content.ValidId.Value) : null,
                        DocumentType = documentOtherPartVersion.Content.DocumentTypeId.HasValue ? this.nomRepository.GetNomValue("documentTypes", documentOtherPartVersion.Content.DocumentTypeId.Value) : null,
                        DocumentRole = documentOtherPartVersion.Content.DocumentRoleId.HasValue ? this.nomRepository.GetNomValue("documentRoles", documentOtherPartVersion.Content.DocumentRoleId.Value) : null,
                        DocumentPersonNumber = documentOtherPartVersion.Content.DocumentPersonNumber
                    });
                }
            }
            return Ok(documentOtherViewDOs);
        }
    }
}
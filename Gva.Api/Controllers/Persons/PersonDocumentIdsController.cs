using System;
using System.Collections.Generic;
using System.Linq;
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
    [RoutePrefix("api/persons/{lotId}/personDocumentIds")]
    [Authorize]
    public class PersonDocumentIdsController : GvaCaseTypesPartController<PersonDocumentIdDO>
    {
        private INomRepository nomRepository;
        private ILotRepository lotRepository;
        private IFileRepository fileRepository;
        private string path;

        public PersonDocumentIdsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            INomRepository nomRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("personDocumentIds", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.path = "personDocumentIds";
            this.nomRepository = nomRepository;
            this.lotRepository = lotRepository;
            this.fileRepository = fileRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewDocumentId(int lotId)
        {
            PersonDocumentIdDO newDocumentId = new PersonDocumentIdDO()
            {
                DocumentDateValidFrom = DateTime.Now,
                ValidId = this.nomRepository.GetNomValue("boolean", "yes").NomValueId
            };

            return Ok(new CaseTypesPartDO<PersonDocumentIdDO>(newDocumentId, new List<CaseDO>()));
        }

        public override IHttpActionResult GetParts(int lotId, int? caseTypeId = null)
        {
            var documentIds = this.lotRepository.GetLotIndex(lotId).Index.GetParts<PersonDocumentIdDO>(this.path);

            List<PersonDocumentIdViewDO> documentIdsViewDOs = new List<PersonDocumentIdViewDO>();
            foreach (var documentIdPartVersion in documentIds)
            {
                var lotFiles = this.fileRepository.GetFileReferences(documentIdPartVersion.PartId, caseTypeId);
                if (!caseTypeId.HasValue || lotFiles.Length != 0)
                {
                    documentIdsViewDOs.Add(new PersonDocumentIdViewDO() 
                    {
                        Cases = lotFiles.Select(lf => new CaseDO(lf)).ToList(),
                        PartIndex = documentIdPartVersion.Part.Index,
                        PartId = documentIdPartVersion.PartId,
                        DocumentDateValidFrom = documentIdPartVersion.Content.DocumentDateValidFrom,
                        DocumentDateValidTo = documentIdPartVersion.Content.DocumentDateValidTo,
                        DocumentNumber = documentIdPartVersion.Content.DocumentNumber,
                        DocumentPublisher = documentIdPartVersion.Content.DocumentPublisher,
                        Notes = documentIdPartVersion.Content.Notes,
                        Valid = documentIdPartVersion.Content.ValidId.HasValue ? this.nomRepository.GetNomValue("boolean", documentIdPartVersion.Content.ValidId.Value) : null,
                        DocumentType = documentIdPartVersion.Content.DocumentTypeId.HasValue ? this.nomRepository.GetNomValue("documentTypes", documentIdPartVersion.Content.DocumentTypeId.Value) : null
                    });
                }
            }
            return Ok(documentIdsViewDOs);
        }
    }
}
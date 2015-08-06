using System;
using System.Collections.Generic;
using System.Web.Http;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.FileRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Persons
{
    [RoutePrefix("api/persons/{lotId}/personDocumentMedicals")]
    [Authorize]
    public class PersonMedicalsController : GvaCaseTypePartController<PersonMedicalDO>
    {
        private ICaseTypeRepository caseTypeRepository;
        private INomRepository nomRepository;
        private ILotRepository lotRepository;
        private IFileRepository fileRepository;
        private string path;

        public PersonMedicalsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            ICaseTypeRepository caseTypeRepository,
            INomRepository nomRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("personDocumentMedicals", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.path = "personDocumentMedicals";
            this.caseTypeRepository = caseTypeRepository;
            this.lotRepository = lotRepository;
            this.fileRepository = fileRepository;
            this.nomRepository = nomRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewMedical(int lotId, int? caseTypeId = null)
        {
            PersonMedicalDO newMedical = new PersonMedicalDO()
            {
                DocumentNumberPrefix = "BGR",
                DocumentDateValidFrom = DateTime.Now
            };

            CaseDO caseDO = null;
            if (caseTypeId.HasValue)
            {
                GvaCaseType caseType = this.caseTypeRepository.GetCaseType(caseTypeId.Value);
                caseDO = new CaseDO()
                {
                    CaseType = new NomValue()
                    {
                        NomValueId = caseType.GvaCaseTypeId,
                        Name = caseType.Name,
                        Alias = caseType.Alias
                    }
                };
            }

            return Ok(new CaseTypePartDO<PersonMedicalDO>(newMedical, caseDO));
        }

        public override IHttpActionResult GetParts(int lotId, int? caseTypeId = null)
        {
            var medicals = this.lotRepository.GetLotIndex(lotId).Index.GetParts<PersonMedicalDO>(this.path);

            List<PersonMedicalViewDO> medicalViewDOs = new List<PersonMedicalViewDO>();
            foreach (var medicalPartVersion in medicals)
            {
                var lotFile = this.fileRepository.GetFileReference(medicalPartVersion.PartId, caseTypeId);
                if (!caseTypeId.HasValue || lotFile != null)
                {
                    medicalViewDOs.Add(new PersonMedicalViewDO()
                    {
                        Case = lotFile != null ? new CaseDO(lotFile) : null,
                        PartIndex = medicalPartVersion.Part.Index,
                        PartId = medicalPartVersion.PartId,
                        DocumentDateValidFrom = medicalPartVersion.Content.DocumentDateValidFrom,
                        DocumentDateValidTo = medicalPartVersion.Content.DocumentDateValidTo,
                        DocumentNumber = medicalPartVersion.Content.DocumentNumber,
                        DocumentNumberPrefix = medicalPartVersion.Content.DocumentNumberPrefix,
                        DocumentNumberSuffix = medicalPartVersion.Content.DocumentNumberSuffix,
                        Notes = medicalPartVersion.Content.Notes,
                        MedClass = medicalPartVersion.Content.MedClassId.HasValue ? this.nomRepository.GetNomValue("medClasses", medicalPartVersion.Content.MedClassId.Value) : null,
                        Limitations = medicalPartVersion.Content.Limitations.Count > 0 ? this.nomRepository.GetNomValues("medLimitation", medicalPartVersion.Content.Limitations.ToArray()) : null,
                        DocumentPublisherId = medicalPartVersion.Content.DocumentPublisherId.HasValue ? this.nomRepository.GetNomValue("medDocPublishers", medicalPartVersion.Content.DocumentPublisherId.Value) : null
                    });
                }
            }
            return Ok(medicalViewDOs);
        }

        [Route("{partIndex}/view")]
        public IHttpActionResult GetMedical(int lotId, int partIndex)
        {
            var medicalPartVersion = this.lotRepository.GetLotIndex(lotId).Index.GetPart<PersonMedicalDO>(string.Format("{0}/{1}", this.path, partIndex));
            var lotFile = this.fileRepository.GetFileReference(medicalPartVersion.PartId, null);

            PersonMedicalViewDO check = new PersonMedicalViewDO()
            {
                Case = lotFile != null ? new CaseDO(lotFile) : null,
                PartIndex = medicalPartVersion.Part.Index,
                PartId = medicalPartVersion.PartId,
                DocumentDateValidFrom = medicalPartVersion.Content.DocumentDateValidFrom,
                DocumentDateValidTo = medicalPartVersion.Content.DocumentDateValidTo,
                DocumentNumber = medicalPartVersion.Content.DocumentNumber,
                DocumentNumberPrefix = medicalPartVersion.Content.DocumentNumberPrefix,
                DocumentNumberSuffix = medicalPartVersion.Content.DocumentNumberSuffix,
                Notes = medicalPartVersion.Content.Notes,
                MedClass = medicalPartVersion.Content.MedClassId.HasValue ? this.nomRepository.GetNomValue("medClasses", medicalPartVersion.Content.MedClassId.Value) : null,
                Limitations = medicalPartVersion.Content.Limitations.Count > 0 ? this.nomRepository.GetNomValues("medLimitation", medicalPartVersion.Content.Limitations.ToArray()) : null,
                DocumentPublisherId = medicalPartVersion.Content.DocumentPublisherId.HasValue ? this.nomRepository.GetNomValue("medDocPublishers", medicalPartVersion.Content.DocumentPublisherId.Value) : null
            };

            return Ok(check);
        }
    }
}
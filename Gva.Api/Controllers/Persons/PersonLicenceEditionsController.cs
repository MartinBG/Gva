﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Api.UserContext;
using Common.Data;
using Common.Filters;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.FileRepository;
using Gva.Api.Repositories.PersonRepository;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Persons
{
    [RoutePrefix("api/persons/{lotId}/licences/{licencePartIndex}/licenceEditions")]
    [Authorize]
    public class PersonLicenceEditionsController : GvaCaseTypesPartController<PersonLicenceEditionDO>
    {
        private string path;
        private IUnitOfWork unitOfWork;
        private ILotRepository lotRepository;
        private IApplicationRepository applicationRepository;
        private ILotEventDispatcher lotEventDispatcher;
        private IFileRepository fileRepository;
        private ICaseTypeRepository caseTypeRepository;
        private IPersonRepository personRepository;
        private INomRepository nomRepository;
        private UserContext userContext;

        public PersonLicenceEditionsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            ICaseTypeRepository caseTypeRepository,
            IApplicationRepository applicationRepository,
            IPersonRepository personRepository,
            INomRepository nomRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("licenceEditions", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.path = "licenceEditions";
            this.unitOfWork = unitOfWork;
            this.lotRepository = lotRepository;
            this.applicationRepository = applicationRepository;
            this.lotEventDispatcher = lotEventDispatcher;
            this.fileRepository = fileRepository;
            this.caseTypeRepository = caseTypeRepository;
            this.personRepository = personRepository;
            this.nomRepository = nomRepository;
            this.userContext = userContext;
        }

        [Route("~/api/persons/{lotId}/licenceEditions/new")]
        public IHttpActionResult GetNewLicenceEdition(int lotId, int caseTypeId, int? appId = null, int? licencePartIndex = null)
        {
            var caseType = this.caseTypeRepository.GetCaseType(caseTypeId);
            List<CaseDO> cases = new List<CaseDO>() { 
                new CaseDO()
                {
                    CaseType = new NomValue
                    {
                        NomValueId = caseType.GvaCaseTypeId,
                        Name = caseType.Name,
                        Alias = caseType.Alias
                    }
                },
                new CaseDO()
                {
                    CaseType = new NomValue
                    {
                        NomValueId = caseType.GvaCaseTypeId,
                        Name = caseType.Name,
                        Alias = caseType.Alias
                    }
                }
            };
            if (appId.HasValue)
            {
                var app = this.applicationRepository.GetNomApplication(appId.Value);
                cases.First().Applications.Add(app);
                cases.Last().Applications.Add(app);
            }

            PersonLicenceEditionDO newLicenceEdition = new PersonLicenceEditionDO()
            {
                DocumentDateValidFrom = DateTime.Now,
                LicencePartIndex = licencePartIndex
            };

            return Ok(new CaseTypesPartDO<PersonLicenceEditionDO>(newLicenceEdition, cases));
        }

        [Route("")]
        public IHttpActionResult GetParts(int lotId, int licencePartIndex, int? caseTypeId = null)
        {
            var editionsPartVersions = this.lotRepository.GetLotIndex(lotId).Index.GetParts<PersonLicenceEditionDO>("licenceEditions")
                .Where(epv => epv.Content.LicencePartIndex == licencePartIndex)
                .OrderBy(epv => epv.Content.Index);

            List<PersonLicenceEditionViewDO> partVersionDOs = new List<PersonLicenceEditionViewDO>();
            foreach (var editionsPartVersion in editionsPartVersions)
            {
                var lotFiles = this.fileRepository.GetFileReferences(editionsPartVersion.PartId, caseTypeId);
                if (!caseTypeId.HasValue || lotFiles != null)
                {
                    string inspector = null;
                    if(editionsPartVersion.Content.InspectorId.HasValue)
                    {
                        var person = this.personRepository.GetPerson(editionsPartVersion.Content.InspectorId.Value);
                        inspector = string.Format("{0} {1}", person.Lin, person.Names);
                    }

                    var limitations = editionsPartVersion.Content.Limitations == null ? null : editionsPartVersion.Content.Limitations.Select(l => this.nomRepository.GetNomValue(l)).ToList();

                    PersonLicenceEditionViewDO edition = new PersonLicenceEditionViewDO()
                    {
                        PartIndex = editionsPartVersion.Part.Index,
                        LicencePartIndex = licencePartIndex,
                        Cases = lotFiles.Select(lf => new CaseDO(lf)).ToList(),
                        Notes = editionsPartVersion.Content.Notes,
                        NotesAlt = editionsPartVersion.Content.NotesAlt,
                        DocumentDateValidFrom = editionsPartVersion.Content.DocumentDateValidFrom,
                        DocumentDateValidTo = editionsPartVersion.Content.DocumentDateValidTo,
                        Index = editionsPartVersion.Content.Index,
                        Inspector = inspector,
                        Limitations = limitations,
                        StampNumber = editionsPartVersion.Content.StampNumber,
                        LicenceAction = editionsPartVersion.Content.LicenceActionId.HasValue ? this.nomRepository.GetNomValue("licenceActions", editionsPartVersion.Content.LicenceActionId.Value) : null,
                        HasNoNumber = editionsPartVersion.Content.HasNoNumber
                    };

                    partVersionDOs.Add(edition);
                }
            }
            
            return Ok(partVersionDOs);
        }

        [Route("")]
        [Validate]
        public IHttpActionResult PostNewPart(int lotId, int licencePartIndex, CaseTypesPartDO<PersonLicenceEditionDO> edition)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var lot = this.lotRepository.GetLotIndex(lotId);

                var editionsPartVersions = lot.Index.GetParts<PersonLicenceEditionDO>("licenceEditions")
                    .Where(epv => epv.Content.LicencePartIndex == licencePartIndex);
                var nextIndex = editionsPartVersions.Select(e => e.Content.Index).Max() + 1;
                edition.Part.Index = nextIndex;

                var licencePartVersion = lot.Index.GetPart<PersonLicenceDO>("licences/" + licencePartIndex);

                var partVersion = lot.CreatePart(this.path + "/*", edition.Part, this.userContext);
                this.fileRepository.AddFileReferences(partVersion.Part, edition.Cases);

                lot.Commit(this.userContext, this.lotEventDispatcher);

                this.unitOfWork.Save();

                this.lotRepository.ExecSpSetLotPartTokens(partVersion.PartId);

                transaction.Commit();

                return Ok(new CaseTypesPartDO<PersonLicenceEditionDO>(partVersion));
            }
        }

        [Route("{partIndex}")]
        [Validate]
        public IHttpActionResult PostPart(int lotId, int licencePartIndex, int partIndex, CaseTypesPartDO<PersonLicenceEditionDO> edition, int? caseTypeId = null)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var lot = this.lotRepository.GetLotIndex(lotId);
                var licencePartVersion = lot.Index.GetPart<PersonLicenceDO>("licences/" + licencePartIndex);

                var oldEditionPartVersion = lot.Index.GetPart<PersonLicenceEditionDO>(string.Format("{0}/{1}", this.path, partIndex));
                if (oldEditionPartVersion.Content.PrintedDocumentBlobKey.HasValue && !edition.Part.PrintedDocumentBlobKey.HasValue)
                {
                    edition.Part.PrintedDocumentBlobKey = oldEditionPartVersion.Content.PrintedDocumentBlobKey.Value;
                    edition.Part.PrintedFileId = oldEditionPartVersion.Content.PrintedFileId.Value;
                }

                if (oldEditionPartVersion.Content.PrintedRatingEditions.Count > 0 && edition.Part.PrintedRatingEditions.Count == 0)
                {
                    edition.Part.PrintedRatingEditions = oldEditionPartVersion.Content.PrintedRatingEditions;
                }

                var partVersion = lot.UpdatePart(string.Format("{0}/{1}", this.path, partIndex), edition.Part, this.userContext);
                this.fileRepository.AddFileReferences(partVersion.Part, edition.Cases);

                lot.Commit(this.userContext, this.lotEventDispatcher);

                this.unitOfWork.Save();

                this.lotRepository.ExecSpSetLotPartTokens(partVersion.PartId);

                transaction.Commit();

                var lotFiles = this.fileRepository.GetFileReferences(partVersion.PartId, caseTypeId);

                return Ok(new CaseTypesPartDO<PersonLicenceEditionDO>(partVersion, lotFiles));
            }
        }

        [Route("{partIndex}")]
        public IHttpActionResult DeletePart(int lotId, int licencePartIndex, int partIndex)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var lot = this.lotRepository.GetLotIndex(lotId);
                PartVersion licencePartVersion = null;
                var editionPartVersion = lot.DeletePart<PersonLicenceEditionDO>(string.Format("{0}/{1}", this.path, partIndex), this.userContext);
                var editionsPartVersions = lot.Index.GetParts<PersonLicenceEditionDO>("licenceEditions")
                    .Where(epv => epv.Content.LicencePartIndex == licencePartIndex);

                if (editionsPartVersions.Count() == 0)
                {
                    licencePartVersion = lot.DeletePart<PersonLicenceDO>(string.Format("{0}/{1}", "licences", licencePartIndex), this.userContext);
                    this.fileRepository.DeleteFileReferences(licencePartVersion.Part);
                }

                this.fileRepository.DeleteFileReferences(editionPartVersion.Part);
                lot.Commit(this.userContext, lotEventDispatcher);

                this.unitOfWork.Save();

                this.lotRepository.ExecSpSetLotPartTokens(editionPartVersion.PartId);
                if (licencePartVersion != null)
                {
                    this.lotRepository.ExecSpSetLotPartTokens(licencePartVersion.PartId);
                }

                transaction.Commit();

                return Ok();
            }
        }
    }
}
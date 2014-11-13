using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Common.Api.Models;
using Common.Api.UserContext;
using Common.Data;
using Common.Filters;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.FileRepository;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Persons
{
    [RoutePrefix("api/persons/{lotId}/licences/{licencePartIndex}/licenceEditions")]
    [Authorize]
    public class PersonLicenceEditionsController : GvaCaseTypePartController<PersonLicenceEditionDO>
    {
        private string path;
        private IUnitOfWork unitOfWork;
        private ILotRepository lotRepository;
        private IApplicationRepository applicationRepository;
        private ILotEventDispatcher lotEventDispatcher;
        private IFileRepository fileRepository;
        private ICaseTypeRepository caseTypeRepository;
        private UserContext userContext;

        public PersonLicenceEditionsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            ICaseTypeRepository caseTypeRepository,
            IApplicationRepository applicationRepository,
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
            this.userContext = userContext;
        }

        [Route("~/api/persons/{lotId}/licenceEditions/new")]
        public IHttpActionResult GetNewLicenceEdition(int lotId, int caseTypeId, int? appId = null, int? licencePartIndex = null)
        {
            var caseType = this.caseTypeRepository.GetCaseType(caseTypeId);
            CaseDO caseDO = new CaseDO()
            {
                CaseType = new NomValue
                {
                    NomValueId = caseType.GvaCaseTypeId,
                    Name = caseType.Name,
                    Alias = caseType.Alias
                }
            };

            if (appId.HasValue)
            {
                this.lotRepository.GetLotIndex(lotId);

                caseDO.Applications.Add(this.applicationRepository.GetInitApplication(appId));
            }

            PersonLicenceEditionDO newLicenceEdition = new PersonLicenceEditionDO()
            {
                DocumentDateValidFrom = DateTime.Now,
                LicencePartIndex = licencePartIndex
            };

            return Ok(new CaseTypePartDO<PersonLicenceEditionDO>(newLicenceEdition, caseDO));
        }

        [Route("")]
        public IHttpActionResult GetParts(int lotId, int licencePartIndex, int? caseTypeId = null)
        {
            var editionsPartVersions = this.lotRepository.GetLotIndex(lotId).Index.GetParts<PersonLicenceEditionDO>("licenceEditions")
                .Where(epv => epv.Content.LicencePartIndex == licencePartIndex)
                .OrderBy(epv => epv.Content.Index);

            List<CaseTypePartDO<PersonLicenceEditionDO>> partVersionDOs = new List<CaseTypePartDO<PersonLicenceEditionDO>>();
            foreach (var editionsPartVersion in editionsPartVersions)
            {
                var lotFile = this.fileRepository.GetFileReference(editionsPartVersion.PartId, caseTypeId);
                if (!caseTypeId.HasValue || lotFile != null)
                {
                    partVersionDOs.Add(new CaseTypePartDO<PersonLicenceEditionDO>(editionsPartVersion, lotFile));
                }
            }

            return Ok(partVersionDOs);
        }

        [Route("")]
        [Validate]
        public IHttpActionResult PostNewPart(int lotId, int licencePartIndex, CaseTypePartDO<PersonLicenceEditionDO> edition)
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
                this.fileRepository.AddFileReference(partVersion.Part, edition.Case);

                lot.Commit(this.userContext, this.lotEventDispatcher);

                this.unitOfWork.Save();

                this.lotRepository.ExecSpSetLotPartTokens(partVersion.PartId);

                transaction.Commit();

                return Ok(new CaseTypePartDO<PersonLicenceEditionDO>(partVersion));
            }
        }

        [Route("{partIndex}")]
        [Validate]
        public IHttpActionResult PostPart(int lotId, int licencePartIndex, int partIndex, CaseTypePartDO<PersonLicenceEditionDO> edition, int? caseTypeId = null)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var lot = this.lotRepository.GetLotIndex(lotId);
                var licencePartVersion = lot.Index.GetPart<PersonLicenceDO>("licences/" + licencePartIndex);

                var partVersion = lot.UpdatePart(string.Format("{0}/{1}", this.path, partIndex), edition.Part, this.userContext);
                this.fileRepository.AddFileReference(partVersion.Part, edition.Case);

                lot.Commit(this.userContext, this.lotEventDispatcher);

                this.unitOfWork.Save();

                this.lotRepository.ExecSpSetLotPartTokens(partVersion.PartId);

                transaction.Commit();

                var lotFile = this.fileRepository.GetFileReference(partVersion.PartId, caseTypeId);

                return Ok(new CaseTypePartDO<PersonLicenceEditionDO>(partVersion, lotFile));
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
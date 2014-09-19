using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Common.Api.Repositories.NomRepository;
using Common.Api.UserContext;
using Common.Data;
using Common.Filters;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.FileRepository;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Persons
{
    [RoutePrefix("api/persons/{lotId}/licences/{licencePartIndex}/licenceEditions")]
    [Authorize]
    public class PersonLicenceEditionsController : GvaFilePartController<PersonLicenceEditionDO>
    {
        private string path;
        private IUnitOfWork unitOfWork;
        private ILotRepository lotRepository;
        private IApplicationRepository applicationRepository;
        private ILotEventDispatcher lotEventDispatcher;
        private INomRepository nomRepository;
        private IFileRepository fileRepository;
        private UserContext userContext;

        public PersonLicenceEditionsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            IApplicationRepository applicationRepository,
            ILotEventDispatcher lotEventDispatcher,
            INomRepository nomRepository,
            UserContext userContext)
            : base("licenceEditions", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.path = "licenceEditions";
            this.unitOfWork = unitOfWork;
            this.lotRepository = lotRepository;
            this.applicationRepository = applicationRepository;
            this.lotEventDispatcher = lotEventDispatcher;
            this.nomRepository = nomRepository;
            this.fileRepository = fileRepository;
            this.userContext = userContext;
        }

        [Route("new")]
        public IHttpActionResult GetNewLicenceEdition(int lotId, int licencePartIndex, int? appId = null)
        {
            var files = new List<FileDO>();
            if (appId.HasValue)
            {
                this.lotRepository.GetLotIndex(lotId);
                files.Add(new FileDO()
                {
                    IsAdded = true,
                    Applications = new List<ApplicationNomDO>()
                    {
                        this.applicationRepository.GetInitApplication(appId)
                    }
                });
            }

            PersonLicenceEditionDO newLicenceEdition = new PersonLicenceEditionDO()
            {
                DocumentDateValidFrom = DateTime.Now,
                LicencePartIndex = licencePartIndex
            };

            return Ok(new FilePartVersionDO<PersonLicenceEditionDO>(newLicenceEdition, files));
        }

        [Route("")]
        public IHttpActionResult GetParts(int lotId, int licencePartIndex, int? caseTypeId = null)
        {
            var editionsPartVersions = this.lotRepository.GetLotIndex(lotId).Index.GetParts<PersonLicenceEditionDO>("licenceEditions")
                .Where(epv => epv.Content.LicencePartIndex == licencePartIndex)
                .OrderBy(epv => epv.Content.Index);

            List<FilePartVersionDO<PersonLicenceEditionDO>> partVersionDOs = new List<FilePartVersionDO<PersonLicenceEditionDO>>();
            foreach (var editionsPartVersion in editionsPartVersions)
            {
                var lotFiles = this.fileRepository.GetFileReferences(editionsPartVersion.PartId, caseTypeId);
                if (!caseTypeId.HasValue || lotFiles.Length != 0)
                {
                    partVersionDOs.Add(new FilePartVersionDO<PersonLicenceEditionDO>(editionsPartVersion, lotFiles));
                }
            }

            return Ok(partVersionDOs);
        }

        [Route("")]
        [Validate]
        public IHttpActionResult PostNewPart(int lotId, int licencePartIndex, FilePartVersionDO<PersonLicenceEditionDO> edition)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var lot = this.lotRepository.GetLotIndex(lotId);

                var editionsPartVersions = lot.Index.GetParts<PersonLicenceEditionDO>("licenceEditions")
                    .Where(epv => epv.Content.LicencePartIndex == licencePartIndex);
                var nextIndex = editionsPartVersions.Select(e => e.Content.Index).Max() + 1;
                edition.Part.Index = nextIndex;

                var licencePartVersion = lot.Index.GetPart<PersonLicenceDO>("licences/" + licencePartIndex);

                if (EditionDocDateValidToValidation(licencePartVersion.Content, edition))
                {
                    return BadRequest();
                }

                var partVersion = lot.CreatePart(this.path + "/*", edition.Part, this.userContext);
                this.fileRepository.AddFileReferences(partVersion.Part, edition.Files);

                lot.Commit(this.userContext, this.lotEventDispatcher);

                this.unitOfWork.Save();

                this.lotRepository.ExecSpSetLotPartTokens(partVersion.PartId);

                transaction.Commit();

                return Ok(new FilePartVersionDO<PersonLicenceEditionDO>(partVersion));
            }
        }

        [Route("{partIndex}")]
        [Validate]
        public IHttpActionResult PostPart(int lotId, int licencePartIndex, int partIndex, FilePartVersionDO<PersonLicenceEditionDO> edition, int? caseTypeId = null)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var lot = this.lotRepository.GetLotIndex(lotId);
                var licencePartVersion = lot.Index.GetPart<PersonLicenceDO>("licences/" + licencePartIndex);

                if (EditionDocDateValidToValidation(licencePartVersion.Content, edition))
                {
                    return BadRequest();
                }

                var partVersion = lot.UpdatePart(string.Format("{0}/{1}", this.path, partIndex), edition.Part, this.userContext);
                this.fileRepository.AddFileReferences(partVersion.Part, edition.Files);

                lot.Commit(this.userContext, this.lotEventDispatcher);

                this.unitOfWork.Save();

                this.lotRepository.ExecSpSetLotPartTokens(partVersion.PartId);

                transaction.Commit();

                var lotFiles = this.fileRepository.GetFileReferences(partVersion.PartId, caseTypeId);

                return Ok(new FilePartVersionDO<PersonLicenceEditionDO>(partVersion, lotFiles));
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

        private bool EditionDocDateValidToValidation(PersonLicenceDO licence, FilePartVersionDO<PersonLicenceEditionDO> edition)
        {
            if ((licence.StaffType.Alias != "flightCrew" ||
                    (licence.Fcl != null && licence.Fcl.Code != "Y" && licence.LicenceType.Code != "BG CCA"))
                && edition.Part.DocumentDateValidTo == null)
            {
                return true;
            }

            return false;
        }
    }
}
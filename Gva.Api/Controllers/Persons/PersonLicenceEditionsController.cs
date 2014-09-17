using System.Web.Http;
using Common.Data;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.ApplicationRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;
using Common.Api.Repositories.NomRepository;
using System.Collections.Generic;
using System;
using Common.Json;
using Common.Api.UserContext;
using Regs.Api.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Gva.Api.Repositories.FileRepository;
using Common.Filters;
using System.Linq;
using System.Web.Http.Results;
using System.Net;
using Common.Api.Models;

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
            var editionsPartVersions = this.lotRepository.GetLotIndex(lotId).Index.GetParts("licenceEditions")
                .Where(epv => epv.Content.Get<int>("licencePartIndex") == licencePartIndex)
                .OrderBy(epv => epv.Content.Get<int>("index"));

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

                var editionsPartVersions = lot.Index.GetParts("licenceEditions").Where(epv => epv.Content.Get<int>("licencePartIndex") == licencePartIndex);
                var nextIndex = editionsPartVersions.Select(e => e.Content.Get<int>("index")).Max() + 1;
                edition.Part.Index = nextIndex;

                var licencePartVersion = lot.Index.GetPart("licences/" + licencePartIndex);

                if (EditionDocDateValidToValidation(licencePartVersion.Content, edition))
                {
                    return BadRequest();
                }

                PartVersion partVersion = lot.CreatePart(this.path + "/*", JObject.FromObject(edition.Part), this.userContext);

                this.fileRepository.AddFileReferences(partVersion, edition.Files);

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

                var licencePartVersion = lot.Index.GetPart("licences/" + licencePartIndex);

                if (EditionDocDateValidToValidation(licencePartVersion.Content, edition))
                {
                    return BadRequest();
                }

                PartVersion partVersion = lot.UpdatePart(
                    string.Format("{0}/{1}", this.path, partIndex),
                    JObject.FromObject(edition.Part),
                    this.userContext);

                this.fileRepository.AddFileReferences(partVersion, edition.Files);

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
                var editionPartVersion = lot.DeletePart(string.Format("{0}/{1}", this.path, partIndex), this.userContext);
                var editionsPartVersions = lot.Index.GetParts("licenceEditions")
                    .Where(epv => epv.Content.Get<int>("licencePartIndex") == licencePartIndex);

                if (editionsPartVersions.Count() == 0)
                {
                    licencePartVersion = lot.DeletePart(string.Format("{0}/{1}", "licences", licencePartIndex), this.userContext);
                }

                this.fileRepository.DeleteFileReferences(editionPartVersion);
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

        public bool EditionDocDateValidToValidation(JObject licence, FilePartVersionDO<PersonLicenceEditionDO> edition)
        {
            if ((licence.Get<string>("staffType.alias") != "flightCrew" ||
                    (licence.Get<NomValue>("fcl") != null && (licence.Get<string>("fcl.code") != "Y" && licence.Get<string>("licenceType.code") != "BG CCA")))
                && edition.Part.DocumentDateValidTo == null)
            {
                return true;
            }

            return false;
        }
    }
}
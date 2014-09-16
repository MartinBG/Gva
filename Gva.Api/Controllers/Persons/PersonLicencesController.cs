using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Common.Api.UserContext;
using Common.Data;
using Common.Json;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.FileRepository;
using Newtonsoft.Json.Linq;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;
using Common.Api.Repositories.NomRepository;
using Common.Filters;
using Gva.Api.Repositories.PersonRepository;
using System.Net;

namespace Gva.Api.Controllers.Persons
{
    [RoutePrefix("api/persons/{lotId}/licences")]
    [Authorize]
    public class PersonLicencesController : GvaApplicationPartController<PersonLicenceDO>
    {
        private string path;
        private IUnitOfWork unitOfWork;
        private ILotRepository lotRepository;
        private IApplicationRepository applicationRepository;
        private IFileRepository fileRepository;
        private ILotEventDispatcher lotEventDispatcher;
        private INomRepository nomRepository;
        private UserContext userContext;
        private IPersonRepository personRepository;

        public PersonLicencesController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IApplicationRepository applicationRepository,
            IFileRepository fileRepository,
            ILotEventDispatcher lotEventDispatcher,
            INomRepository nomRepository,
            IPersonRepository personRepository,
            UserContext userContext)
            : base("licences", unitOfWork, lotRepository, applicationRepository, lotEventDispatcher, userContext)
        {
            this.path = "licences";
            this.unitOfWork = unitOfWork;
            this.lotRepository = lotRepository;
            this.applicationRepository = applicationRepository;
            this.fileRepository = fileRepository;
            this.lotEventDispatcher = lotEventDispatcher;
            this.nomRepository = nomRepository;
            this.userContext = userContext;
            this.personRepository = personRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewLicence(int lotId, int? appId = null)
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

            PersonLicenceDO licence = new PersonLicenceDO()
            {
                Valid = this.nomRepository.GetNomValue("boolean", "yes"),
                Publisher = this.nomRepository.GetNomValue("caa", "BG")
            };

            PersonLicenceEditionDO edition = new PersonLicenceEditionDO()
            {
                Index = 0,
                DocumentDateValidFrom = DateTime.Now
            };

            PersonLicenceNewDO newLicence = new PersonLicenceNewDO()
            {
                Licence = new ApplicationPartVersionDO<PersonLicenceDO>(licence),
                Edition = new FilePartVersionDO<PersonLicenceEditionDO>(edition, files)
            };

            return Ok(newLicence);
        }

        public override IHttpActionResult GetParts(int lotId, [FromUri] int[] partIndexes = null)
        {
            var licences = this.personRepository.GetLicences(lotId);

            return Ok(licences.Select(d => new GvaViewPersonLicenceEditionDO(d)));
        }

        [NonAction]
        public override IHttpActionResult PostNewPart(int lotId, ApplicationPartVersionDO<PersonLicenceDO> partVersionDO)
        {
            throw new NotSupportedException();
        }

        [Route("")]
        [Validate]
        public IHttpActionResult PostNewPart(int lotId, PersonLicenceNewDO newLicence)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                if ((newLicence.Licence.Part.StaffType.Alias != "flightCrew" ||
                    (newLicence.Licence.Part.Fcl != null && (newLicence.Licence.Part.Fcl.Code != "Y" && newLicence.Licence.Part.LicenceType.Code != "BG CCA")))
                        && newLicence.Edition.Part.DocumentDateValidTo == null)
                {
                    return BadRequest();
                }

                if (newLicence.Licence.Part.LicenceType.Code == "FOREIGN" &&
                    (newLicence.Licence.Part.ForeignLicenceNumber == null || newLicence.Licence.Part.ForeignPublisher == null || newLicence.Licence.Part.Employment == null))
                {
                    return BadRequest();
                }

                if (!newLicence.Licence.Part.LicenceNumber.HasValue)
                {
                    string licenceNumber = this.personRepository.GetLastLicenceNumber(lotId, newLicence.Licence.Part.LicenceType.Code);
                    if (licenceNumber == null)
                    {
                        newLicence.Licence.Part.LicenceNumber = 1;
                    }
                    else
                    {
                        int lastLicenceNumber;
                        if (Int32.TryParse(licenceNumber, out lastLicenceNumber))
                        {
                            newLicence.Licence.Part.LicenceNumber = lastLicenceNumber + 1;
                        }
                        else
                        {
                            return BadRequest();
                        }
                    }
                }

                var lot = this.lotRepository.GetLotIndex(lotId);

                PartVersion licencePartVersion = lot.CreatePart("licences/*", JObject.FromObject(newLicence.Licence.Part), this.userContext);

                newLicence.Edition.Part.LicencePartIndex = licencePartVersion.Part.Index;

                PartVersion editionPartVersion = lot.CreatePart("licenceEditions/*", JObject.FromObject(newLicence.Edition.Part), this.userContext);

                this.fileRepository.AddFileReferences(editionPartVersion, newLicence.Edition.Files);

                lot.Commit(this.userContext, this.lotEventDispatcher);

                this.unitOfWork.Save();

                this.lotRepository.ExecSpSetLotPartTokens(licencePartVersion.PartId);
                this.lotRepository.ExecSpSetLotPartTokens(editionPartVersion.PartId);

                transaction.Commit();

                return Ok(new PersonLicenceNewDO()
                    {
                        Licence = new ApplicationPartVersionDO<PersonLicenceDO>(licencePartVersion),
                        Edition = new FilePartVersionDO<PersonLicenceEditionDO>(editionPartVersion)
                    });
            }
        }

        [Route("lastLicenceNumber")]
        public IHttpActionResult GetLastLicenceNumber(int lotId, string licenceTypeCode)
        {
            string licenceNumber = this.personRepository.GetLastLicenceNumber(lotId, licenceTypeCode);

            return Ok(new JObject(new JProperty("number", licenceNumber)));
        }

        [Route("newStatus")]
        public IHttpActionResult GetNewLicenceStatus(int lotId)
        {
            return Ok(new PersonLicenceStatusDO());
        }
    }
}
using System;
using System.Linq;
using System.Web.Http;
using Common.Api.Repositories.NomRepository;
using Common.Api.UserContext;
using Common.Data;
using Common.Filters;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.FileRepository;
using Gva.Api.Repositories.PersonRepository;
using Newtonsoft.Json.Linq;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Persons
{
    [RoutePrefix("api/persons/{lotId}/licences")]
    [Authorize]
    public class PersonLicencesController : GvaCaseTypePartController<PersonLicenceDO>
    {
        private IUnitOfWork unitOfWork;
        private ILotRepository lotRepository;
        private IFileRepository fileRepository;
        private ILotEventDispatcher lotEventDispatcher;
        private INomRepository nomRepository;
        private UserContext userContext;
        private IPersonRepository personRepository;

        public PersonLicencesController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            ILotEventDispatcher lotEventDispatcher,
            INomRepository nomRepository,
            IPersonRepository personRepository,
            UserContext userContext)
            : base("licences", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.unitOfWork = unitOfWork;
            this.lotRepository = lotRepository;
            this.fileRepository = fileRepository;
            this.lotEventDispatcher = lotEventDispatcher;
            this.nomRepository = nomRepository;
            this.userContext = userContext;
            this.personRepository = personRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewLicence(int lotId)
        {
            PersonLicenceDO licence = new PersonLicenceDO()
            {
                Valid = this.nomRepository.GetNomValue("boolean", "yes"),
                Publisher = this.nomRepository.GetNomValue("caa", "BGR")
            };

            return Ok(new CaseTypePartDO<PersonLicenceDO>(licence));
        }

        public override IHttpActionResult GetParts(int lotId, int? caseTypeId = null)
        {
            var licences = this.personRepository.GetLicences(lotId, caseTypeId);

            return Ok(licences.Select(d => new GvaViewPersonLicenceEditionDO(d)));
        }

        [NonAction]
        public override IHttpActionResult PostNewPart(int lotId, CaseTypePartDO<PersonLicenceDO> partVersionDO)
        {
            throw new NotSupportedException();
        }

        [Route("")]
        [Validate]
        public IHttpActionResult PostNewPart(int lotId, PersonLicenceNewDO newLicence)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {

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

                var licencePartVersion = lot.CreatePart("licences/*", newLicence.Licence.Part, this.userContext);
                this.fileRepository.AddFileReference(licencePartVersion.Part, newLicence.Licence.Case);
                newLicence.Edition.Part.LicencePartIndex = licencePartVersion.Part.Index;

                var editionPartVersion = lot.CreatePart("licenceEditions/*", newLicence.Edition.Part, this.userContext);
                this.fileRepository.AddFileReferences(editionPartVersion.Part, newLicence.Edition.Cases);

                lot.Commit(this.userContext, this.lotEventDispatcher);

                this.unitOfWork.Save();

                this.lotRepository.ExecSpSetLotPartTokens(licencePartVersion.PartId);
                this.lotRepository.ExecSpSetLotPartTokens(editionPartVersion.PartId);

                transaction.Commit();

                return Ok(new PersonLicenceNewDO()
                    {
                        Licence = new CaseTypePartDO<PersonLicenceDO>(licencePartVersion),
                        Edition = new CaseTypesPartDO<PersonLicenceEditionDO>(editionPartVersion)
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
            PersonLicenceStatusDO licenceStatus = new PersonLicenceStatusDO();
            licenceStatus.Valid = this.nomRepository.GetNomValue("boolean", "yes");

            return Ok(licenceStatus);
        }

        [Route("{licencePartIndex}/lastEditionIndex")]
        public IHttpActionResult GetLastEditionIndex(int lotId, int licencePartIndex)
        {
            var lastLicenceEditionIndex = this.personRepository.GetLastLicenceEditionIndex(lotId, licencePartIndex);

            return Ok(new { LastIndex = lastLicenceEditionIndex });
        }

        [HttpGet]
        [Route("isUniqueLicenceNumber")]
        public IHttpActionResult IsUniqueLicenceNumber(string licenceTypeCode, int licenceNumber)
        {
            return Ok(
                new
                {
                    isUnique = this.personRepository.IsUniqueLicenceNumber(licenceTypeCode, licenceNumber)
                });
        }
    }
}
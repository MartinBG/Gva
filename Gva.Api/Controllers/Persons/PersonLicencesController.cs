using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Api.UserContext;
using Common.Data;
using Common.Filters;
using Gva.Api.Models.Views.Person;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.FileRepository;
using Gva.Api.Repositories.PersonRepository;
using Newtonsoft.Json.Linq;
using Regs.Api.LotEvents;
using Regs.Api.Models;
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
        private string path;

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
            this.path = "licences";
        }

        [Route("new")]
        public IHttpActionResult GetNewLicence(int lotId)
        {
            PersonLicenceDO licence = new PersonLicenceDO()
            {
                ValidId = this.nomRepository.GetNomValue("boolean", "yes").NomValueId,
                PublisherId = this.nomRepository.GetNomValue("caa", "BGR").NomValueId
            };

            return Ok(new CaseTypePartDO<PersonLicenceDO>(licence));
        }

        [Route("{licencePartIndex}/data")]
        public IHttpActionResult GetPart(int lotId, int licencePartIndex, int? caseTypeId = null)
        {
            var partVersion = this.lotRepository.GetLotIndex(lotId).Index.GetPart<PersonLicenceDO>(string.Format("{0}/{1}", path, licencePartIndex));
            var lotFile = this.fileRepository.GetFileReference(partVersion.PartId, caseTypeId);

            var statuses = partVersion.Content.Statuses == null ? null :
                partVersion.Content.Statuses.Select(s =>
                    {
                        NomValue inspector = null;
                        if(s.InspectorId.HasValue)
                        {
                            GvaViewPerson person = this.personRepository.GetPerson(s.InspectorId.Value);
                            inspector = new NomValue()
                            {
                                NomValueId = person.LotId,
                                Name = string.Format("{0} {1}", person.Lin, person.Names)
                            };
                        }

                        return new PersonLicenceStatusViewDO()
                        {
                            ChangeDate = s.ChangeDate,
                            Inspector = inspector,
                            ChangeReason = s.ChangeReasonId.HasValue ? this.nomRepository.GetNomValue(s.ChangeReasonId.Value) : null,
                            Valid = s.ValidId.HasValue ? this.nomRepository.GetNomValue(s.ValidId.Value) : null,
                            Notes = s.Notes
                        };
                    }).ToList();

            var licence = new PersonLicenceViewDO()
            {
                PartIndex = licencePartIndex,
                LicenceType = partVersion.Content.LicenceTypeId.HasValue ? this.nomRepository.GetNomValue(partVersion.Content.LicenceTypeId.Value) : null, 
                LicenceNumber = partVersion.Content.LicenceNumber,
                ForeignLicenceNumber = partVersion.Content.ForeignLicenceNumber,
                ForeignPublisher = partVersion.Content.ForeignPublisherId.HasValue ? this.nomRepository.GetNomValue(partVersion.Content.ForeignPublisherId.Value) : null, 
                Employment = partVersion.Content.EmploymentId.HasValue ? this.nomRepository.GetNomValue(partVersion.Content.EmploymentId.Value) : null, 
                Publisher = partVersion.Content.PublisherId.HasValue ? this.nomRepository.GetNomValue(partVersion.Content.PublisherId.Value) : null, 
                Valid = partVersion.Content.ValidId.HasValue ? this.nomRepository.GetNomValue(partVersion.Content.ValidId.Value) : null,
                Statuses = statuses,
                CaseTypeId = lotFile.GvaCaseType.GvaCaseTypeId
            };

            return Ok(licence);
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
                string LicenceTypeCode = null;
                if (newLicence.Licence.Part.LicenceTypeId.HasValue)
                {
                    LicenceTypeCode = this.nomRepository.GetNomValue(newLicence.Licence.Part.LicenceTypeId.Value).Code;
                }

                if (LicenceTypeCode == "FOREIGN" &&
                    (newLicence.Licence.Part.ForeignLicenceNumber == null || !newLicence.Licence.Part.ForeignPublisherId.HasValue || !newLicence.Licence.Part.EmploymentId.HasValue))
                {
                    return BadRequest();
                }

                if (!newLicence.Licence.Part.LicenceNumber.HasValue)
                {
                    string licenceNumber = this.personRepository.GetLastLicenceNumber(lotId, LicenceTypeCode);
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

        [HttpPost]
        [Route("{licencePartIndex}/status")]
        public IHttpActionResult UpdateLicenceStatus(int lotId, int licencePartIndex, PersonLicenceStatusDO status)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var lot = this.lotRepository.GetLotIndex(lotId);
                var licencePartVersion = this.lotRepository.GetLotIndex(lotId).Index.GetPart<PersonLicenceDO>(string.Format("{0}/{1}", this.path, licencePartIndex));
                if (licencePartVersion.Content.Statuses == null)
                {
                    licencePartVersion.Content.Statuses = new List<PersonLicenceStatusDO>();
                }

                licencePartVersion.Content.Statuses.Add(status);
                licencePartVersion.Content.ValidId = status.ValidId;

                PartVersion<PersonLicenceDO> partVersion = lot.UpdatePart(
                    string.Format("{0}/{1}", this.path, licencePartIndex),
                    licencePartVersion.Content,
                    this.userContext);

                lot.Commit(this.userContext, lotEventDispatcher);

                this.unitOfWork.Save();

                this.lotRepository.ExecSpSetLotPartTokens(partVersion.PartId);

                transaction.Commit();
            }
            return Ok();
        }

        [Route("lastLicenceNumber")]
        public IHttpActionResult GetLastLicenceNumber(int lotId, int licenceTypeId)
        {
            string licenceTypeCode = this.nomRepository.GetNomValue(licenceTypeId).Code;
            string licenceNumber = this.personRepository.GetLastLicenceNumber(lotId, licenceTypeCode);

            return Ok(new JObject(new JProperty("number", licenceNumber)));
        }

        [Route("newStatus")]
        public IHttpActionResult GetNewLicenceStatus(int lotId)
        {
            return Ok(new PersonLicenceStatusDO()
            {
                ValidId = this.nomRepository.GetNomValue("boolean", "yes").NomValueId
            });
        }

        [Route("{licencePartIndex}/lastEditionIndex")]
        public IHttpActionResult GetLastEditionIndex(int lotId, int licencePartIndex)
        {
            var lastLicenceEditionIndex = this.personRepository.GetLastLicenceEditionIndex(lotId, licencePartIndex);

            return Ok(new { LastIndex = lastLicenceEditionIndex });
        }

        [HttpGet]
        [Route("isUniqueLicenceNumber")]
        public IHttpActionResult IsUniqueLicenceNumber(int licenceTypeId, int licenceNumber)
        {
            string licenceTypeCode = this.nomRepository.GetNomValue(licenceTypeId).Code;

            return Ok(
                new
                {
                    isUnique = this.personRepository.IsUniqueLicenceNumber(licenceTypeCode, licenceNumber)
                });
        }
    }
}
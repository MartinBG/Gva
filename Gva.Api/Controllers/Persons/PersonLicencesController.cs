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
        private ILotEventDispatcher lotEventDispatcher;
        private INomRepository nomRepository;
        private UserContext userContext;

        public PersonLicencesController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            IApplicationRepository applicationRepository,
            ILotEventDispatcher lotEventDispatcher,
            INomRepository nomRepository,
            UserContext userContext)
            : base("licences", unitOfWork, lotRepository, applicationRepository, lotEventDispatcher, userContext)
        {
            this.path = "licences";
            this.unitOfWork = unitOfWork;
            this.lotRepository = lotRepository;
            this.applicationRepository = applicationRepository;
            this.lotEventDispatcher = lotEventDispatcher;
            this.nomRepository = nomRepository;
            this.userContext = userContext;
        }

        [Route("new")]
        public IHttpActionResult GetNewLicence(int lotId, int? appId = null)
        {
            var applications = new List<ApplicationNomDO>();
            if (appId.HasValue)
            {
                this.lotRepository.GetLotIndex(lotId);
                applications.Add(this.applicationRepository.GetInitApplication(appId));
            }

            PersonLicenceDO newLicence = new PersonLicenceDO()
            {
                NextIndex = 1,
                Valid = this.nomRepository.GetNomValue("boolean", "yes"),
                Editions = new[]
                {
                    new PersonLicenceEditionDO()
                    {
                        Index = 0,
                        DocumentDateValidFrom = DateTime.Now,
                        Applications = applications
                    }
                }
            };

            return Ok(new ApplicationPartVersionDO<PersonLicenceDO>(newLicence));
        }

        [Route("{partIndex}/newEdition")]
        public IHttpActionResult GetNewLicenceEdition(int lotId, int partIndex, int? appId = null)
        {
            var applications = new List<ApplicationNomDO>();
            if (appId.HasValue)
            {
                this.lotRepository.GetLotIndex(lotId);
                applications.Add(this.applicationRepository.GetInitApplication(appId));
            }

            PersonLicenceEditionDO newLicenceEdition = new PersonLicenceEditionDO()
            {
                DocumentDateValidFrom = DateTime.Now,
                Applications = applications
            };

            return Ok(newLicenceEdition);
        }

        public override IHttpActionResult PostNewPart(int lotId, ApplicationPartVersionDO<PersonLicenceDO> licence)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var lot = this.lotRepository.GetLotIndex(lotId);

                PartVersion partVersion = lot.CreatePart(this.path + "/*", JObject.FromObject(licence.Part), this.userContext);

                this.applicationRepository.AddApplicationRefs(partVersion.Part, licence.Part.Editions[0].Applications);

                lot.Commit(this.userContext, this.lotEventDispatcher);

                this.unitOfWork.Save();

                this.lotRepository.ExecSpSetLotPartTokens(partVersion.PartId);

                transaction.Commit();

                return Ok(new ApplicationPartVersionDO<PersonLicenceDO>(partVersion));
            }
        }

        public override IHttpActionResult PostPart(int lotId, int partIndex, ApplicationPartVersionDO<PersonLicenceDO> licence)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var lot = this.lotRepository.GetLotIndex(lotId);

                var lastEdition = licence.Part.Editions.Last();
                if (!lastEdition.Index.HasValue)
                {
                    lastEdition.Index = licence.Part.NextIndex;
                    licence.Part.NextIndex++;
                }

                var partVersion = lot.UpdatePart(
                    string.Format("{0}/{1}", this.path, partIndex),
                    JObject.FromObject(licence.Part),
                    this.userContext);

                this.applicationRepository.AddApplicationRefs(partVersion.Part, lastEdition.Applications);

                lot.Commit(this.userContext, this.lotEventDispatcher);

                this.unitOfWork.Save();

                this.lotRepository.ExecSpSetLotPartTokens(partVersion.PartId);

                transaction.Commit();

                return Ok(new ApplicationPartVersionDO<PersonLicenceDO>(partVersion));
            }
        }

        [Route("lastLicenceNumber")]
        public IHttpActionResult GetLastLicenceNumber(int lotId, string licenceType)
        {
            string licenceNumber = null;
            var lastLicence = this.lotRepository.GetLotIndex(lotId).Index.GetParts("licences")
                .Where(l => l.Content.Get<string>("licenceType.code") == licenceType)
                .OrderBy(l => l.Part.PartId)
                .LastOrDefault(l => l.Content.Get("licenceNumber") != null);

            if (lastLicence != null)
            {
                licenceNumber = lastLicence.Content.Get<string>("licenceNumber");
            }

            return Ok(new JObject(new JProperty("number", licenceNumber)));
        }

        [Route("newStatus")]
        public IHttpActionResult GetNewLicenceStatus(int lotId)
        {
            return Ok(new PersonLicenceStatusDO());
        }
    }
}
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
    [RoutePrefix("api/persons/{lotId}/ratings/{ratingPartIndex}/ratingEditions")]
    [Authorize]
    public class PersonRatingEditionsController : GvaApplicationPartController<PersonRatingEditionDO>
    {
        private string path;
        private IUnitOfWork unitOfWork;
        private ILotRepository lotRepository;
        private IApplicationRepository applicationRepository;
        private ILotEventDispatcher lotEventDispatcher;
        private INomRepository nomRepository;
        private IFileRepository fileRepository;
        private UserContext userContext;

        public PersonRatingEditionsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            IApplicationRepository applicationRepository,
            ILotEventDispatcher lotEventDispatcher,
            INomRepository nomRepository,
            UserContext userContext)
            : base("ratingEditions", unitOfWork, lotRepository, applicationRepository, lotEventDispatcher, userContext)
        {
            this.path = "ratingEditions";
            this.unitOfWork = unitOfWork;
            this.lotRepository = lotRepository;
            this.applicationRepository = applicationRepository;
            this.lotEventDispatcher = lotEventDispatcher;
            this.nomRepository = nomRepository;
            this.fileRepository = fileRepository;
            this.userContext = userContext;
        }

        [Route("new")]
        public IHttpActionResult GetNewRatingEdition(int lotId, int ratingPartIndex, int? appId = null)
        {
            var applications = new List<ApplicationNomDO>();
            if (appId.HasValue)
            {
                this.lotRepository.GetLotIndex(lotId);
                applications.Add(this.applicationRepository.GetInitApplication(appId));
            }

            PersonRatingEditionDO newRatingEdition = new PersonRatingEditionDO()
            {
                DocumentDateValidFrom = DateTime.Now,
                RatingPartIndex = ratingPartIndex
            };

            return Ok(new ApplicationPartVersionDO<PersonRatingEditionDO>(newRatingEdition, applications));
        }

        [Route("")]
        public IHttpActionResult GetParts(int lotId, int ratingPartIndex, int? caseTypeId = null)
        {
            var editionsPartVersions = this.lotRepository.GetLotIndex(lotId).Index.GetParts<PersonRatingEditionDO>("ratingEditions")
                .Where(epv => epv.Content.RatingPartIndex == ratingPartIndex)
                .OrderBy(epv => epv.Content.Index);

            List<ApplicationPartVersionDO<PersonRatingEditionDO>> partVersionDOs = new List<ApplicationPartVersionDO<PersonRatingEditionDO>>();
            foreach (var editionsPartVersion in editionsPartVersions)
            {
                var lotFiles = this.applicationRepository.GetApplicationRefs(editionsPartVersion.PartId);
                if (!caseTypeId.HasValue || lotFiles.Length != 0)
                {
                    partVersionDOs.Add(new ApplicationPartVersionDO<PersonRatingEditionDO>(editionsPartVersion, lotFiles));
                }
            }

            return Ok(partVersionDOs);
        }

        [Route("")]
        [Validate]
        public IHttpActionResult PostNewPart(int lotId, int ratingPartIndex, ApplicationPartVersionDO<PersonRatingEditionDO> edition)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var lot = this.lotRepository.GetLotIndex(lotId);

                var editionsPartVersions = lot.Index.GetParts<PersonRatingEditionDO>("ratingEditions").Where(epv => epv.Content.RatingPartIndex == ratingPartIndex);
                var nextIndex = editionsPartVersions.Select(e => e.Content.Index).Max() + 1;
                edition.Part.Index = nextIndex;

                var partVersion = lot.CreatePart(this.path + "/*", edition.Part, this.userContext);

                this.applicationRepository.AddApplicationRefs(partVersion.Part, edition.Applications);

                lot.Commit(this.userContext, this.lotEventDispatcher);

                this.unitOfWork.Save();

                this.lotRepository.ExecSpSetLotPartTokens(partVersion.PartId);

                transaction.Commit();

                return Ok(new ApplicationPartVersionDO<PersonRatingEditionDO>(partVersion));
            }
        }

        [Route("{partIndex}")]
        [Validate]
        public IHttpActionResult PostPart(int lotId, int ratingPartIndex, int partIndex, ApplicationPartVersionDO<PersonRatingEditionDO> edition)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var lot = this.lotRepository.GetLotIndex(lotId);

                var partVersion = lot.UpdatePart(string.Format("{0}/{1}", this.path, partIndex), edition.Part, this.userContext);

                this.applicationRepository.AddApplicationRefs(partVersion.Part, edition.Applications);

                lot.Commit(this.userContext, this.lotEventDispatcher);

                this.unitOfWork.Save();

                this.lotRepository.ExecSpSetLotPartTokens(partVersion.PartId);

                transaction.Commit();

                var lotFiles = this.applicationRepository.GetApplicationRefs(partVersion.PartId);

                return Ok(new ApplicationPartVersionDO<PersonRatingEditionDO>(partVersion, lotFiles));
            }
        }

        [Route("{partIndex}")]
        public IHttpActionResult DeletePart(int lotId, int ratingPartIndex, int partIndex)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var lot = this.lotRepository.GetLotIndex(lotId);
                PartVersion licencePartVersion = null;
                var editionPartVersion = lot.DeletePart<PersonRatingEditionDO>(string.Format("{0}/{1}", this.path, partIndex), this.userContext);
                var editionsPartVersions = lot.Index.GetParts<PersonRatingEditionDO>("ratingEditions")
                    .Where(epv => epv.Content.RatingPartIndex == ratingPartIndex);

                if (editionsPartVersions.Count() == 0)
                {
                    licencePartVersion = lot.DeletePart<PersonRatingDO>(string.Format("{0}/{1}", "ratings", ratingPartIndex), this.userContext);
                }

                this.applicationRepository.DeleteApplicationRefs(editionPartVersion.Part);

                lot.Commit(this.userContext, this.lotEventDispatcher);

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
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
using Gva.Api.Repositories.PersonRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Persons
{
    [RoutePrefix("api/persons/{lotId}/ratings")]
    [Authorize]
    public class PersonRatingsController : GvaApplicationPartController<PersonRatingDO>
    {
        private IUnitOfWork unitOfWork;
        private ILotRepository lotRepository;
        private IApplicationRepository applicationRepository;
        private IPersonRepository personRepository;
        private ILotEventDispatcher lotEventDispatcher;
        private INomRepository nomRepository;
        private UserContext userContext;

        public PersonRatingsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IApplicationRepository applicationRepository,
            IPersonRepository personRepository,
            ILotEventDispatcher lotEventDispatcher,
            INomRepository nomRepository,
            UserContext userContext)
            : base("ratings", unitOfWork, lotRepository, applicationRepository, lotEventDispatcher, userContext)
        {
            this.unitOfWork = unitOfWork;
            this.lotRepository = lotRepository;
            this.applicationRepository = applicationRepository;
            this.personRepository = personRepository;
            this.lotEventDispatcher = lotEventDispatcher;
            this.nomRepository = nomRepository;
            this.userContext = userContext;
        }

        [Route("new")]
        public IHttpActionResult GetNewRating(int lotId, int? appId = null)
        {
            var applications = new List<ApplicationNomDO>();
            if (appId.HasValue)
            {
                this.lotRepository.GetLotIndex(lotId);
                applications.Add(this.applicationRepository.GetInitApplication(appId));
            }

            PersonRatingEditionDO edition = new PersonRatingEditionDO()
            {
                Index = 0,
                DocumentDateValidFrom = DateTime.Now
            };

            PersonRatingNewDO newRating = new PersonRatingNewDO()
            {
                Rating = new ApplicationPartVersionDO<PersonRatingDO>(new PersonRatingDO()
                {
                    Caa = this.nomRepository.GetNomValue("caa", "BG")
                }),
                Edition = new ApplicationPartVersionDO<PersonRatingEditionDO>(edition, applications)
            };

            return Ok(newRating);
        }

        [NonAction]
        public override IHttpActionResult PostNewPart(int lotId, ApplicationPartVersionDO<PersonRatingDO> partVersionDO)
        {
            throw new NotSupportedException();
        }

        [Route("")]
        [Validate]
        public IHttpActionResult PostNewPart(int lotId, PersonRatingNewDO newRating)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var lot = this.lotRepository.GetLotIndex(lotId);

                var ratingPartVersion = lot.CreatePart("ratings/*", newRating.Rating.Part, this.userContext);

                newRating.Edition.Part.RatingPartIndex = ratingPartVersion.Part.Index;

                var editionPartVersion = lot.CreatePart("ratingEditions/*", newRating.Edition.Part, this.userContext);

                this.applicationRepository.AddApplicationRefs(editionPartVersion.Part, newRating.Edition.Applications);

                lot.Commit(this.userContext, this.lotEventDispatcher);

                this.unitOfWork.Save();

                this.lotRepository.ExecSpSetLotPartTokens(ratingPartVersion.PartId);
                this.lotRepository.ExecSpSetLotPartTokens(editionPartVersion.PartId);

                transaction.Commit();

                return Ok(new PersonRatingNewDO()
                {
                    Rating = new ApplicationPartVersionDO<PersonRatingDO>(ratingPartVersion),
                    Edition = new ApplicationPartVersionDO<PersonRatingEditionDO>(editionPartVersion)
                });
            }
        }

        public override IHttpActionResult GetParts(int lotId, [FromUri] int[] partIndexes = null)
        {
            var ratings = this.personRepository.GetRatings(lotId);

            return Ok(ratings.Select(d => new GvaViewPersonRatingDO(d)));
        }

        [Route("{ratingPartIndex}/lastEditionIndex")]
        public IHttpActionResult GetLastEditionIndex(int lotId, int ratingPartIndex)
        {
            var lastRatingEditionIndex = this.personRepository.GetLastRatingEditionIndex(lotId, ratingPartIndex);

            return Ok(new { LastIndex = lastRatingEditionIndex });
        }
    }
}
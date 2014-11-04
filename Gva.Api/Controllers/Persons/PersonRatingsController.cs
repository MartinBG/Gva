using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Common.Api.Repositories.NomRepository;
using Common.Api.UserContext;
using Common.Data;
using Common.Filters;
using Common.Linq;
using Gva.Api.Models.Views.Person;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.FileRepository;
using Gva.Api.Repositories.PersonRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Persons
{
    [RoutePrefix("api/persons/{lotId}/ratings")]
    [Authorize]
    public class PersonRatingsController : GvaCaseTypePartController<PersonRatingDO>
    {
        private IUnitOfWork unitOfWork;
        private ILotRepository lotRepository;
        private IFileRepository fileRepository;
        private IPersonRepository personRepository;
        private ILotEventDispatcher lotEventDispatcher;
        private INomRepository nomRepository;
        private UserContext userContext;

        public PersonRatingsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            IPersonRepository personRepository,
            ILotEventDispatcher lotEventDispatcher,
            INomRepository nomRepository,
            UserContext userContext)
            : base("ratings", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.unitOfWork = unitOfWork;
            this.lotRepository = lotRepository;
            this.fileRepository = fileRepository;
            this.personRepository = personRepository;
            this.lotEventDispatcher = lotEventDispatcher;
            this.nomRepository = nomRepository;
            this.userContext = userContext;
        }

        [Route("new")]
        public IHttpActionResult GetNewRating(int lotId)
        {
            PersonRatingDO rating = new PersonRatingDO()
            {
                Caa = this.nomRepository.GetNomValue("caa", "BG")
            };

            return Ok(new CaseTypePartDO<PersonRatingDO>(rating));
        }

        [NonAction]
        public override IHttpActionResult PostNewPart(int lotId, CaseTypePartDO<PersonRatingDO> partVersionDO)
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
                this.fileRepository.AddFileReference(ratingPartVersion.Part, newRating.Rating.Case);

                newRating.Edition.Part.RatingPartIndex = ratingPartVersion.Part.Index;
                var editionPartVersion = lot.CreatePart("ratingEditions/*", newRating.Edition.Part, this.userContext);
                this.fileRepository.AddFileReference(editionPartVersion.Part, newRating.Edition.Case);

                lot.Commit(this.userContext, this.lotEventDispatcher);

                this.unitOfWork.Save();

                this.lotRepository.ExecSpSetLotPartTokens(ratingPartVersion.PartId);
                this.lotRepository.ExecSpSetLotPartTokens(editionPartVersion.PartId);

                transaction.Commit();

                return Ok(new PersonRatingNewDO()
                {
                    Rating = new CaseTypePartDO<PersonRatingDO>(ratingPartVersion),
                    Edition = new CaseTypePartDO<PersonRatingEditionDO>(editionPartVersion)
                });
            }
        }

        public override IHttpActionResult GetParts(int lotId, int? caseTypeId = null)
        {
            var ratings = this.personRepository.GetRatings(lotId, caseTypeId);
            List<GvaViewPersonRatingDO> ratingDOs = ratings.Select(rating =>
                new GvaViewPersonRatingDO(rating, rating.Editions.OrderBy(e => e.Index).ToList()))
                .ToList();

            return Ok(ratingDOs);
        }

        [Route("{ratingPartIndex}/lastEditionIndex")]
        public IHttpActionResult GetLastEditionIndex(int lotId, int ratingPartIndex)
        {
            var lastRatingEditionIndex = this.personRepository.GetLastRatingEditionIndex(lotId, ratingPartIndex);

            return Ok(new { LastIndex = lastRatingEditionIndex });
        }

        [HttpPost]
        [Route("isValid")]
        public IHttpActionResult IsValidRating(int lotId, PersonRatingDO rating, int? ratingPartIndex = null)
        {
            var ratings = this.unitOfWork.DbContext.Set<GvaViewPersonRating>()
                .Where(r => r.LotId == lotId)
                .ToList();

            if (!string.IsNullOrEmpty(rating.Sector))
            {
                ratings = ratings.Where(r => string.Equals(r.Sector, rating.Sector)).ToList();
            }
            if (rating.RatingClass != null)
            {
                ratings = ratings.Where(r => r.RatingClassId == rating.RatingClass.NomValueId).ToList();
            }

            if (rating.RatingType != null)
            {
                ratings = ratings.Where(r => r.RatingTypeId == rating.RatingType.NomValueId).ToList();
            }

            if (rating.Authorization != null)
            {
                ratings = ratings.Where(r => r.AuthorizationId == rating.Authorization.NomValueId).ToList();
            }

            if (rating.AircraftTypeGroup != null)
            {
                ratings = ratings.Where(r => r.AircraftTypeGroupId == rating.AircraftTypeGroup.NomValueId).ToList();
            }

            if (rating.AircraftTypeCategory != null)
            {
                ratings = ratings.Where(r => r.AircraftTypeCategoryId == rating.AircraftTypeCategory.NomValueId).ToList();
            }

            if (rating.Caa != null)
            {
                ratings = ratings.Where(r => r.CaaId == rating.Caa.NomValueId).ToList();
            }

            if (ratingPartIndex.HasValue)
            {
                ratings = ratings.Where(r => r.PartIndex != ratingPartIndex).ToList();
            }

            return Ok(new {
                isValid = !ratings.Any()
            });
        }
    }
}
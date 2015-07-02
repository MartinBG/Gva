using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Api.UserContext;
using Common.Data;
using Common.Filters;
using Common.Linq;
using Gva.Api.Models;
using Gva.Api.Models.Views.Person;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.CaseTypeRepository;
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
        private ICaseTypeRepository caseTypeRepository;

        public PersonRatingsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            IPersonRepository personRepository,
            ILotEventDispatcher lotEventDispatcher,
            INomRepository nomRepository,
            ICaseTypeRepository caseTypeRepository,
            UserContext userContext)
            : base("ratings", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.unitOfWork = unitOfWork;
            this.lotRepository = lotRepository;
            this.fileRepository = fileRepository;
            this.personRepository = personRepository;
            this.lotEventDispatcher = lotEventDispatcher;
            this.nomRepository = nomRepository;
            this.caseTypeRepository = caseTypeRepository;
            this.userContext = userContext;
        }

        [Route("new")]
        public IHttpActionResult GetNewRating(int lotId, int? caseTypeId = null)
        {
            PersonRatingDO rating = new PersonRatingDO()
            {
                Caa = this.nomRepository.GetNomValue("caa", "BGR")
            };

            CaseDO caseDO = null;
            if (caseTypeId.HasValue)
            {
                GvaCaseType caseType = this.caseTypeRepository.GetCaseType(caseTypeId.Value);
                caseDO = new CaseDO()
                {
                    CaseType = new NomValue()
                    {
                        NomValueId = caseType.GvaCaseTypeId,
                        Name = caseType.Name,
                        Alias = caseType.Alias
                    }
                };
            }

            return Ok(new CaseTypePartDO<PersonRatingDO>(rating, caseDO));
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

        [Route("byValidity")]
        public IHttpActionResult GetRatingsByValidity(int lotId, int? caseTypeId = null, bool? valid = true, bool? showAllEditions = false)
        {
            var ratings = this.personRepository.GetRatings(lotId, caseTypeId);
            List<GvaViewPersonRatingDO> ratingDOs = new List<GvaViewPersonRatingDO>();
            foreach (GvaViewPersonRating rating in ratings)
            { 
                var editions = rating.Editions.OrderBy(e => e.Index).ToList();
                if (valid == true && !showAllEditions.Value)
                {
                    editions = editions.Where(e => !e.DocDateValidTo.HasValue || DateTime.Compare(e.DocDateValidTo.Value, DateTime.Now) >= 0).ToList();
                }

                if (editions.Count() > 0 && !showAllEditions.Value)
                {
                    ratingDOs.Add(new GvaViewPersonRatingDO(rating, editions.First(), editions.Last()));
                }
                else if (editions.Count() > 0 && showAllEditions.Value)
                {
                    rating.Editions.ForEach(edition =>
                    {
                        ratingDOs.Add(new GvaViewPersonRatingDO(rating, editions.First(), edition));
                    });
                }
            }

            return Ok(ratingDOs);
        }

        [Route("getRatingsWithAllEditions")]
        public IHttpActionResult GetRatingsWithAllEditions(int lotId, int? caseTypeId = null)
        {
            var ratings = this.personRepository.GetRatings(lotId, caseTypeId);
            List<GvaViewPersonRatingEditionDO> ratingEditionDOs = new List<GvaViewPersonRatingEditionDO>();
            foreach (var rating in ratings)
            { 
                rating.Editions.ForEach(e =>
                {
                    ratingEditionDOs.Add(new GvaViewPersonRatingEditionDO(rating, e));
                });
            }

            return Ok(ratingEditionDOs);
        }

        public override IHttpActionResult GetParts(int lotId, int? caseTypeId = null)
        {
            var ratings = this.personRepository.GetRatings(lotId, caseTypeId);
            List<GvaViewPersonRatingDO> ratingDOs = ratings.Select(rating =>
                {
                    var editions = rating.Editions.OrderBy(e => e.Index).ToList();
                    return new GvaViewPersonRatingDO(rating, editions.First(), editions.Last());
                })
                
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
            var predicate = PredicateBuilder.True<GvaViewPersonRating>()
                .And(r => r.LotId == lotId)
                .AndEquals(r => rating.Sector, rating.Sector)
                .AndEquals(r => r.RatingClassId, rating.RatingClass != null ? rating.RatingClass.NomValueId : (int?)null)
                .AndEquals(r => r.AuthorizationId, rating.Authorization != null ? rating.Authorization.NomValueId : (int?)null)
                .AndEquals(r => r.AircraftTypeGroupId, rating.AircraftTypeGroup != null ? rating.AircraftTypeGroup.NomValueId : (int?)null)
                .AndEquals(r => r.AircraftTypeCategoryId, rating.AircraftTypeCategory != null ? rating.AircraftTypeCategory.NomValueId : (int?)null)
                .AndEquals(r => r.CaaId, rating.Caa != null ? rating.Caa.NomValueId : (int?)null);

            string types = string.Join(", ", rating.RatingTypes.OrderBy(r => r.NomValueId).Select(r => r.Code));
            predicate = predicate.AndEquals(r => r.RatingTypes, types);

            if (ratingPartIndex.HasValue)
            {
                predicate = predicate.And(r => r.PartIndex != ratingPartIndex);
            }

            var ratings = this.unitOfWork.DbContext.Set<GvaViewPersonRating>().Where(predicate);

            return Ok(new {
                isValid = !ratings.Any()
            });
        }
    }
}
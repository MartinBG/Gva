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
        public IHttpActionResult GetNewRating(int lotId, int? appId = null)
        {
            PersonRatingDO rating = new PersonRatingDO()
            {
                Caa = this.nomRepository.GetNomValue("caa", "BG")
            };

            return Ok(new CaseTypePartDO<PersonRatingDO>(rating, new CaseDO()));
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

                newRating.Edition = new CaseTypePartDO<PersonRatingEditionDO>()
                {
                    Part = new PersonRatingEditionDO()
                    {
                        RatingPartIndex = ratingPartVersion.Part.Index,
                        DocumentDateValidFrom = DateTime.Now
                    },
                    Case = newRating.Rating.Case
                };

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
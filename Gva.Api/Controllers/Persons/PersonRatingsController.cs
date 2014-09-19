using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Common.Api.Repositories.NomRepository;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.FileRepository;
using Newtonsoft.Json.Linq;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Persons
{
    [RoutePrefix("api/persons/{lotId}/ratings")]
    [Authorize]
    public class PersonRatingsController : GvaApplicationPartController<PersonRatingDO>
    {
        private string path;
        private IUnitOfWork unitOfWork;
        private ILotRepository lotRepository;
        private IApplicationRepository applicationRepository;
        private ILotEventDispatcher lotEventDispatcher;
        private INomRepository nomRepository;
        private UserContext userContext;

        public PersonRatingsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            IApplicationRepository applicationRepository,
            ILotEventDispatcher lotEventDispatcher,
            INomRepository nomRepository,
            UserContext userContext)
            : base("ratings", unitOfWork, lotRepository, applicationRepository, lotEventDispatcher, userContext)
        {
            this.path = "ratings";
            this.unitOfWork = unitOfWork;
            this.lotRepository = lotRepository;
            this.applicationRepository = applicationRepository;
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

            PersonRatingDO newRating = new PersonRatingDO()
            {
                NextIndex = 1,
                Caa = this.nomRepository.GetNomValue("caa", "BG"),
                Editions = new[]
                {
                    new PersonRatingEditionDO()
                    {
                        Index = 0,
                        DocumentDateValidFrom = DateTime.Now,
                        Applications = applications
                    }
                }
            };

            return Ok(new ApplicationPartVersionDO<PersonRatingDO>(newRating));
        }

        [Route("{partIndex}/newEdition")]
        public IHttpActionResult GetNewRatingEdition(int lotId, int partIndex, int? appId = null)
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
                Applications = applications
            };

            return Ok(newRatingEdition);
        }

        public override IHttpActionResult PostNewPart(int lotId, ApplicationPartVersionDO<PersonRatingDO> rating)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var lot = this.lotRepository.GetLotIndex(lotId);
                var partVersion = lot.CreatePart(path + "/*", rating.Part, this.userContext);

                this.applicationRepository.AddApplicationRefs(partVersion.Part, rating.Part.Editions[0].Applications);

                lot.Commit(this.userContext, lotEventDispatcher);

                this.unitOfWork.Save();

                this.lotRepository.ExecSpSetLotPartTokens(partVersion.PartId);

                transaction.Commit();

                return Ok(new ApplicationPartVersionDO<PersonRatingDO>(partVersion));
            }
        }

        public override IHttpActionResult PostPart(int lotId, int partIndex, ApplicationPartVersionDO<PersonRatingDO> rating)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var lot = this.lotRepository.GetLotIndex(lotId);

                var lastEdition = rating.Part.Editions.Last();
                if (!lastEdition.Index.HasValue)
                {
                    lastEdition.Index = rating.Part.NextIndex;
                    rating.Part.NextIndex++;
                }

                var partVersion = lot.UpdatePart( string.Format("{0}/{1}", this.path, partIndex), rating.Part, this.userContext);

                this.applicationRepository.AddApplicationRefs(partVersion.Part, lastEdition.Applications);

                lot.Commit(this.userContext, lotEventDispatcher);

                this.unitOfWork.Save();

                this.lotRepository.ExecSpSetLotPartTokens(partVersion.PartId);

                transaction.Commit();

                return Ok(new ApplicationPartVersionDO<PersonRatingDO>(partVersion));
            }
        }
    }
}
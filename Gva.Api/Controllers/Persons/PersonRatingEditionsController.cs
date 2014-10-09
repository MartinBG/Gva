using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Api.UserContext;
using Common.Data;
using Common.Filters;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.FileRepository;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Persons
{
    [RoutePrefix("api/persons/{lotId}/ratings/{ratingPartIndex}/ratingEditions")]
    [Authorize]
    public class PersonRatingEditionsController : GvaCaseTypePartController<PersonRatingEditionDO>
    {
        private string path;
        private IUnitOfWork unitOfWork;
        private ILotRepository lotRepository;
        private IApplicationRepository applicationRepository;
        private ILotEventDispatcher lotEventDispatcher;
        private INomRepository nomRepository;
        private IFileRepository fileRepository;
        private ICaseTypeRepository caseTypeRepository;
        private UserContext userContext;

        public PersonRatingEditionsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IApplicationRepository applicationRepository,
            IFileRepository fileRepository,
            ICaseTypeRepository caseTypeRepository,
            ILotEventDispatcher lotEventDispatcher,
            INomRepository nomRepository,
            UserContext userContext)
            : base("ratingEditions", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.path = "ratingEditions";
            this.unitOfWork = unitOfWork;
            this.lotRepository = lotRepository;
            this.applicationRepository = applicationRepository;
            this.lotEventDispatcher = lotEventDispatcher;
            this.nomRepository = nomRepository;
            this.fileRepository = fileRepository;
            this.caseTypeRepository = caseTypeRepository;
            this.userContext = userContext;
        }

        [Route("~/api/persons/{lotId}/ratingEditions/new")]
        public IHttpActionResult GetNewRatingEdition(int lotId, int caseTypeId, int? appId = null, int? ratingPartIndex = null)
        {
            var caseType = this.caseTypeRepository.GetCaseType(caseTypeId);
            CaseDO caseDO = new CaseDO() 
            { 
                CaseType = new NomValue
                {
                    NomValueId = caseType.GvaCaseTypeId,
                    Name = caseType.Name,
                    Alias = caseType.Alias
                }
            };

            if (appId.HasValue)
            {
                this.lotRepository.GetLotIndex(lotId);

                caseDO.Applications.Add(this.applicationRepository.GetInitApplication(appId));
            }

            PersonRatingEditionDO newRatingEdition = new PersonRatingEditionDO()
            {
                DocumentDateValidFrom = DateTime.Now,
                RatingPartIndex = ratingPartIndex.HasValue ? ratingPartIndex.Value : 0,
            };

            return Ok(new CaseTypePartDO<PersonRatingEditionDO>(newRatingEdition, caseDO));
        }

        [Route("")]
        public IHttpActionResult GetParts(int lotId, int ratingPartIndex, int? caseTypeId = null)
        {
            var editionsPartVersions = this.lotRepository.GetLotIndex(lotId).Index.GetParts<PersonRatingEditionDO>("ratingEditions")
                .Where(epv => epv.Content.RatingPartIndex == ratingPartIndex)
                .OrderBy(epv => epv.Content.Index);

            List<CaseTypePartDO<PersonRatingEditionDO>> partVersionDOs = new List<CaseTypePartDO<PersonRatingEditionDO>>();
            foreach (var editionsPartVersion in editionsPartVersions)
            {
                var lotFile = this.fileRepository.GetFileReference(editionsPartVersion.PartId, caseTypeId);
                if (!caseTypeId.HasValue || lotFile != null)
                {
                    partVersionDOs.Add(new CaseTypePartDO<PersonRatingEditionDO>(editionsPartVersion, lotFile));
                }
            }

            return Ok(partVersionDOs);
        }

        [Route("")]
        [Validate]
        public IHttpActionResult PostNewPart(int lotId, int ratingPartIndex, CaseTypePartDO<PersonRatingEditionDO> edition)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var lot = this.lotRepository.GetLotIndex(lotId);

                var editionsPartVersions = lot.Index.GetParts<PersonRatingEditionDO>("ratingEditions").Where(epv => epv.Content.RatingPartIndex == ratingPartIndex);
                var nextIndex = editionsPartVersions.Select(e => e.Content.Index).Max() + 1;
                edition.Part.Index = nextIndex;

                var partVersion = lot.CreatePart(this.path + "/*", edition.Part, this.userContext);

                this.fileRepository.AddFileReference(partVersion.Part, edition.Case);

                lot.Commit(this.userContext, this.lotEventDispatcher);

                this.unitOfWork.Save();

                this.lotRepository.ExecSpSetLotPartTokens(partVersion.PartId);

                transaction.Commit();

                return Ok(new CaseTypePartDO<PersonRatingEditionDO>(partVersion));
            }
        }

        [Route("{partIndex}")]
        [Validate]
        public IHttpActionResult PostPart(int lotId, int licencePartIndex, int partIndex, CaseTypePartDO<PersonRatingEditionDO> edition, int? caseTypeId = null)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var lot = this.lotRepository.GetLotIndex(lotId);

                var partVersion = lot.UpdatePart(string.Format("{0}/{1}", this.path, partIndex), edition.Part, this.userContext);

                this.fileRepository.AddFileReference(partVersion.Part, edition.Case);

                lot.Commit(this.userContext, this.lotEventDispatcher);

                this.unitOfWork.Save();

                this.lotRepository.ExecSpSetLotPartTokens(partVersion.PartId);

                transaction.Commit();
                var lotFile = this.fileRepository.GetFileReference(partVersion.PartId, caseTypeId);

                return Ok(new CaseTypePartDO<PersonRatingEditionDO>(partVersion, lotFile));
            }
        }

        [Route("{partIndex}")]
        public IHttpActionResult DeletePart(int lotId, int ratingPartIndex, int partIndex)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var lot = this.lotRepository.GetLotIndex(lotId);
                PartVersion ratingPartVersion = null;
                var editionPartVersion = lot.DeletePart<PersonRatingEditionDO>(string.Format("{0}/{1}", this.path, partIndex), this.userContext);
                var editionsPartVersions = lot.Index.GetParts<PersonRatingEditionDO>("ratingEditions")
                    .Where(epv => epv.Content.RatingPartIndex == ratingPartIndex);

                if (editionsPartVersions.Count() == 0)
                {
                    ratingPartVersion = lot.DeletePart<PersonRatingDO>(string.Format("{0}/{1}", "ratings", ratingPartIndex), this.userContext);
                }

                this.applicationRepository.DeleteApplicationRefs(editionPartVersion.Part);

                lot.Commit(this.userContext, this.lotEventDispatcher);

                this.unitOfWork.Save();

                this.lotRepository.ExecSpSetLotPartTokens(editionPartVersion.PartId);
                if (ratingPartVersion != null)
                {
                    this.lotRepository.ExecSpSetLotPartTokens(ratingPartVersion.PartId);
                }

                transaction.Commit();

                return Ok();
            }
        }

    }
}
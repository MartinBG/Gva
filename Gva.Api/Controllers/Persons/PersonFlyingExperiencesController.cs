using System;
using System.Linq;
using System.Web.Http;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.FileRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Persons
{
    [RoutePrefix("api/persons/{lotId}/personFlyingExperiences")]
    [Authorize]
    public class PersonFlyingExperiencesController : GvaCaseTypePartController<PersonFlyingExperienceDO>
    {
        private ILotRepository lotRepository;
        private string path;

        public PersonFlyingExperiencesController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("personFlyingExperiences", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.path = "personFlyingExperiences";
            this.lotRepository = lotRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewFlyingExperience(int lotId)
        {
            PersonFlyingExperienceDO newFlyingExperience = new PersonFlyingExperienceDO()
            {
                DocumentDate = DateTime.Now
            };

            return Ok(new CaseTypePartDO<PersonFlyingExperienceDO>(newFlyingExperience));
        }

        [Route("sumAllFlightHours")]
        public IHttpActionResult GetSumOFAllFlightHours(int lotId, int locationIndicatorId, string sector, int? partIndex = null)
        {
            var allFlyingExperiences = this.lotRepository.GetLotIndex(lotId).Index.GetParts<PersonFlyingExperienceDO>(this.path).ToList();
            if (partIndex.HasValue)
            {
                allFlyingExperiences = allFlyingExperiences.Where(e => e.Part.Index != partIndex).ToList();
            }

            var total = allFlyingExperiences
                .Where(f => f.Content.TotalDoc.HasValue && 
                    DateTime.Compare(f.CreateDate, DateTime.Now) < 0 &&
                    f.Content.LocationIndicator.NomValueId == locationIndicatorId &&
                    f.Content.Sector.ToLower().Contains(sector.ToLower()))
                    .Select(s => s.Content.TotalDoc).Sum() ?? 0;

            return Ok(new
            {
                total = total
            });
        }
    }
}
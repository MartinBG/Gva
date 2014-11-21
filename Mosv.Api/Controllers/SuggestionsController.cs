using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using Common.Api.UserContext;
using Common.Data;
using Common.Filters;
using Mosv.Api.ModelsDO;
using Mosv.Api.ModelsDO.Suggestion;
using Mosv.Api.Repositories.SuggestionRepository;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

namespace Mosv.Api.Controllers
{
    [RoutePrefix("api/suggestion")]
    [Authorize]
    public class SuggestionsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private ILotRepository lotRepository;
        private ISuggestionRepository suggestionRepository;
        private ILotEventDispatcher lotEventDispatcher;

        public SuggestionsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            ISuggestionRepository suggestionRepository,
            ILotEventDispatcher lotEventDispatcher)
        {
            this.unitOfWork = unitOfWork;
            this.lotRepository = lotRepository;
            this.suggestionRepository = suggestionRepository;
            this.lotEventDispatcher = lotEventDispatcher;
        }

        [Route("")]
        public IHttpActionResult GetSuggestions(
            string incomingLot = null,
            string incomingNumber = null,
            DateTime? incomingDateFrom = null,
            DateTime? incomingDateТо = null,
            string applicant = null)
        {
            var admissions = this.suggestionRepository.GetSuggestions(
                incomingLot,
                incomingNumber,
                incomingDateFrom,
                incomingDateТо,
                applicant);

            return Ok(admissions.Select(s => new SuggestionViewDO(s)));
        }

        [Route("new")]
        public IHttpActionResult GetNewSuggestion()
        {
            return Ok(new SuggestionDO());
        }

        [Route("")]
        [Validate]
        public IHttpActionResult PostSuggestion(SuggestionDO suggestion)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                UserContext userContext = this.Request.GetUserContext();
                var newLot = this.lotRepository.CreateLot("Suggestion");

                newLot.CreatePart("suggestionData", suggestion, userContext);

                newLot.Commit(userContext, lotEventDispatcher);

                this.unitOfWork.Save();

                transaction.Commit();

                return Ok(new { id = newLot.LotId });
            }
        }

        [Route(@"{lotId}/{*path:regex(^suggestionData$)}")]
        public IHttpActionResult GetPart(int lotId, string path)
        {
            var part = this.lotRepository.GetLotIndex(lotId, true).Index.GetPart<SuggestionDO>(path);

            var suggestion = this.suggestionRepository.GetSuggestion(lotId);
            SuggestionViewDO suggestionDO = new SuggestionViewDO(suggestion);

            if (suggestionDO.ApplicationDocId.HasValue)
            {
                var dr = unitOfWork.DbContext.Set<Docs.Api.Models.DocRelation>()
                     .Include(e => e.Doc.DocCasePartType)
                     .Include(e => e.Doc.DocCasePartMovements.Select(dc => dc.User))
                     .Include(e => e.Doc.DocDirection)
                     .Include(e => e.Doc.DocType)
                     .Include(e => e.Doc.DocStatus)
                     .FirstOrDefault(e => e.DocId == suggestionDO.ApplicationDocId.Value);

                suggestionDO.ApplicationDocRelation = new Docs.Api.DataObjects.DocRelationDO(dr);
            }

            return Ok(new
            {
                data = suggestionDO,
                partData = new PartVersionDO<SuggestionDO>(part)
            });
        }

        [Route(@"{lotId}/{*path:regex(^suggestionData$)}")]
        public IHttpActionResult PostSuggestionData(int lotId, string path, PartVersionDO<SuggestionDO> partVersionDO)
        {
            UserContext userContext = this.Request.GetUserContext();

            var lot = this.lotRepository.GetLotIndex(lotId, true);
            PartVersion<SuggestionDO> partVersion = lot.UpdatePart(path, partVersionDO.Part, userContext);
            lot.Commit(userContext, lotEventDispatcher);

            this.unitOfWork.Save();

            return Ok();
        }

        [Route("{id}/fastSave")]
        [HttpPost]
        public IHttpActionResult FastSaveSignal(int id, SuggestionViewDO data)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var suggestion = this.suggestionRepository.GetSuggestion(id);
                suggestion.ApplicationDocId = data.ApplicationDocId;

                this.unitOfWork.Save();

                transaction.Commit();

                return Ok(new { id = id });
            }
        }

        [Route("{id}/loadData")]
        [HttpPost]
        public IHttpActionResult LoadSginalData(int id)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var admission = this.suggestionRepository.GetSuggestion(id);

                //todo load and write data

                //this.unitOfWork.Save();

                //transaction.Commit();

                return Ok(new { id = id });
            }
        }
    }
}
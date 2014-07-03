using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Common.Api.UserContext;
using Common.Data;
using Common.Json;
using Mosv.Api.Models;
using Mosv.Api.ModelsDO;
using Newtonsoft.Json.Linq;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;
using Mosv.Api.Repositories.SuggestionRepository;
using System.Data.Entity;

namespace Mosv.Api.Controllers
{
    [RoutePrefix("api/suggestion")]
    [Authorize]
    public class SuggestionsController : MosvLotsController
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
            : base(lotRepository, unitOfWork, lotEventDispatcher)
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

            return Ok(admissions.Select(s => new SuggestionDO(s)));
        }

        [Route("")]
        public IHttpActionResult PostSuggestion(JObject suggestion)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                UserContext userContext = this.Request.GetUserContext();
                var newLot = this.lotRepository.CreateLot("Suggestion", userContext);

                newLot.CreatePart("suggestionData", suggestion, userContext);

                newLot.Commit(userContext, lotEventDispatcher);

                this.unitOfWork.Save();

                transaction.Commit();

                return Ok(new { id = newLot.LotId });
            }
        }

        [Route(@"{lotId}/{*path:regex(^suggestionData$)}")]
        public override IHttpActionResult GetPart(int lotId, string path)
        {
            var part = this.lotRepository.GetLotIndex(lotId).Index.GetPart(path);

            var suggestion = this.suggestionRepository.GetSuggestion(lotId);
            SuggestionDO suggestionDO = new SuggestionDO(suggestion);

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
                partData = new PartVersionDO(part)
            });
        }

        [Route(@"{lotId}/{*path:regex(^suggestionData$)}")]
        public IHttpActionResult PostSuggestionData(int lotId, string path, JObject content)
        {
            var lot = this.lotRepository.GetLotIndex(lotId);

            return base.PostPart(lotId, path, content);
        }

        [Route("{id}/fastSave")]
        [HttpPost]
        public IHttpActionResult FastSaveSignal(int id, SignalDO data)
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
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
using Mosv.Api.Repositories.SignalRepository;
using System.Data.Entity;

namespace Mosv.Api.Controllers
{
    [RoutePrefix("api/signals")]
    [Authorize]
    public class SignalsController : MosvLotsController
    {
        private IUnitOfWork unitOfWork;
        private ILotRepository lotRepository;
        private ISignalRepository signalRepository;
        private ILotEventDispatcher lotEventDispatcher;

        public SignalsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            ISignalRepository signalRepository,
            ILotEventDispatcher lotEventDispatcher)
            : base(lotRepository, unitOfWork, lotEventDispatcher)
        {
            this.unitOfWork = unitOfWork;
            this.lotRepository = lotRepository;
            this.signalRepository = signalRepository;
            this.lotEventDispatcher = lotEventDispatcher;
        }

        [Route("")]
        public IHttpActionResult GetSignals(
            string incomingLot = null,
            string incomingNumber = null,
            DateTime? incomingDate = null,
            string applicant = null,
            string institution = null,
            string violation = null)
        {
            var signals = this.signalRepository.GetSignals(
                incomingLot,
                incomingNumber,
                incomingDate,
                applicant,
                institution,
                violation);

            return Ok(signals.Select(o => new SignalDO(o)));
        }

        [Route("")]
        public IHttpActionResult PostSignal(JObject signal)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                UserContext userContext = this.Request.GetUserContext();
                var newLot = this.lotRepository.CreateLot("Signal", userContext);

                newLot.CreatePart("signalData", signal.Get<JObject>("signalData"), userContext);

                newLot.Commit(userContext, lotEventDispatcher);

                this.unitOfWork.Save();

                transaction.Commit();

                return Ok(new { id = newLot.LotId });
            }
        }

        [Route(@"{lotId}/{*path:regex(^signalData$)}")]
        public override IHttpActionResult GetPart(int lotId, string path)
        {
            var part = this.lotRepository.GetLotIndex(lotId).Index.GetPart(path);

            var signal = this.signalRepository.GetSignal(lotId);
            SignalDO signalDO = new SignalDO(signal);

            if (signalDO.ApplicationDocId.HasValue)
            {
                var dr = unitOfWork.DbContext.Set<Docs.Api.Models.DocRelation>()
                     .Include(e => e.Doc.DocCasePartType)
                     .Include(e => e.Doc.DocCasePartMovements.Select(dc => dc.User))
                     .Include(e => e.Doc.DocDirection)
                     .Include(e => e.Doc.DocType)
                     .Include(e => e.Doc.DocStatus)
                     .FirstOrDefault(e => e.DocId == signalDO.ApplicationDocId.Value);

                signalDO.ApplicationDocRelation = new Docs.Api.DataObjects.DocRelationDO(dr);
            }

            return Ok(new
            {
                data = signalDO,
                partData = new PartVersionDO(part)
            });
        }

        [Route(@"{lotId}/{*path:regex(^signalData$)}")]
        public IHttpActionResult PostSignalData(int lotId, string path, JObject content)
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
                var signal = this.signalRepository.GetSignal(id);
                signal.ApplicationDocId = data.ApplicationDocId;

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
                var admission = this.signalRepository.GetSignal(id);

                //todo load and write data

                //this.unitOfWork.Save();

                //transaction.Commit();

                return Ok(new { id = id });
            }
        }
    }
}
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using Common.Api.UserContext;
using Common.Data;
using Common.Filters;
using Mosv.Api.Models;
using Mosv.Api.ModelsDO;
using Mosv.Api.ModelsDO.Admission;
using Mosv.Api.Repositories.AdmissionRepository;
using Newtonsoft.Json.Linq;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

namespace Mosv.Api.Controllers
{
    [RoutePrefix("api/admissions")]
    [Authorize]
    public class AdmissionsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private ILotRepository lotRepository;
        private IAdmissionRepository admissionRepository;
        private ILotEventDispatcher lotEventDispatcher;

        public AdmissionsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IAdmissionRepository admissionRepository,
            ILotEventDispatcher lotEventDispatcher)
        {
            this.unitOfWork = unitOfWork;
            this.lotRepository = lotRepository;
            this.admissionRepository = admissionRepository;
            this.lotEventDispatcher = lotEventDispatcher;
        }

        [Route("")]
        public IHttpActionResult GetAdmissions(
            string incomingLot = null,
            string incomingNumber = null,
            DateTime? incomingDate = null,
            string applicantType = null,
            string applicant = null)
        {
            var admissions = this.admissionRepository.GetAdmissions(
                incomingLot,
                incomingNumber,
                incomingDate,
                applicantType,
                applicant);

            return Ok(admissions.Select(o => new AdmissionViewDO(o)));
        }

        [Route("{lotId}")]
        public IHttpActionResult GetAdmission(int lotId)
        {
            var admission = this.admissionRepository.GetAdmission(lotId);
            AdmissionViewDO returnValue = new AdmissionViewDO(admission);

            return Ok(returnValue);
        }

        [Route("new")]
        public IHttpActionResult GetNewAdmission()
        {
            return Ok(new AdmissionDO());
        }

        [Route("")]
        [Validate]
        public IHttpActionResult PostAdmission(AdmissionDO admission)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                UserContext userContext = this.Request.GetUserContext();
                var newLot = this.lotRepository.CreateLot("Admission");

                newLot.CreatePart("admissionData", admission, userContext);

                newLot.Commit(userContext, lotEventDispatcher);

                this.unitOfWork.Save();

                transaction.Commit();

                return Ok(new { id = newLot.LotId });
            }
        }

        [Route(@"{lotId}/{*path:regex(^admissionData$)}")]
        public IHttpActionResult GetPart(int lotId, string path)
        {
            var part = this.lotRepository.GetLotIndex(lotId).Index.GetPart<AdmissionDO>(path);

            var admission = this.admissionRepository.GetAdmission(lotId);
            AdmissionViewDO admissionDO = new AdmissionViewDO(admission);

            if (admissionDO.ApplicationDocId.HasValue)
            {
                var dr = unitOfWork.DbContext.Set<Docs.Api.Models.DocRelation>()
                     .Include(e => e.Doc.DocCasePartType)
                     .Include(e => e.Doc.DocCasePartMovements.Select(dc => dc.User))
                     .Include(e => e.Doc.DocDirection)
                     .Include(e => e.Doc.DocType)
                     .Include(e => e.Doc.DocStatus)
                     .FirstOrDefault(e => e.DocId == admissionDO.ApplicationDocId.Value);

                admissionDO.ApplicationDocRelation = new Docs.Api.DataObjects.DocRelationDO(dr);
            }

            return Ok(new
            {
                data = admissionDO,
                partData = new PartVersionDO<AdmissionDO>(part)
            });
        }

        [Route(@"{lotId}/{*path:regex(^admissionData$)}")]
        [Validate]
        public IHttpActionResult PostAdmissionData(int lotId, string path, PartVersionDO<AdmissionDO> partVersionDO)
        {
            UserContext userContext = this.Request.GetUserContext();

            var lot = this.lotRepository.GetLotIndex(lotId);
            PartVersion<AdmissionDO> partVersion = lot.UpdatePart(path, partVersionDO.Part, userContext);
            lot.Commit(userContext, lotEventDispatcher);

            this.unitOfWork.Save();

            return Ok();
        }

        [Route("{id}/fastSave")]
        [HttpPost]
        public IHttpActionResult FastSaveAdmission(int id, AdmissionViewDO data)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var admission = this.admissionRepository.GetAdmission(id);
                admission.ApplicationDocId = data.ApplicationDocId;

                this.unitOfWork.Save();

                transaction.Commit();

                return Ok(new { id = id });
            }
        }

        [Route("{id}/loadData")]
        [HttpPost]
        public IHttpActionResult LoadAdmissionData(int id)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var admission = this.admissionRepository.GetAdmission(id);

                //todo load and write data

                //this.unitOfWork.Save();

                //transaction.Commit();

                return Ok(new { id = id });
            }
        }

        [Route("{id}/createLink")]
        [HttpPost]
        public IHttpActionResult CreateDocLotLink(int id, string lotType)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                UserContext userContext = this.Request.GetUserContext();
                JObject jObj = JObject.Parse("{}");

                if (lotType == "admission")
                {
                    var newLot = this.lotRepository.CreateLot("Admission");

                    newLot.CreatePart("admissionData", jObj, userContext);

                    newLot.Commit(userContext, lotEventDispatcher);

                    this.unitOfWork.Save();

                    var mosvViewAdmission = this.unitOfWork.DbContext.Set<MosvViewAdmission>().Single(e => e.LotId == newLot.LotId);

                    mosvViewAdmission.ApplicationDocId = id;

                    this.unitOfWork.Save();

                    transaction.Commit();

                    return Ok(new { id = newLot.LotId });
                }
                else if (lotType == "signal")
                {
                    var newLot = this.lotRepository.CreateLot("Signal");

                    newLot.CreatePart("signalData", jObj, userContext);

                    newLot.Commit(userContext, lotEventDispatcher);

                    this.unitOfWork.Save();

                    var mosvViewSignal = this.unitOfWork.DbContext.Set<MosvViewSignal>().Single(e => e.LotId == newLot.LotId);

                    mosvViewSignal.ApplicationDocId = id;

                    this.unitOfWork.Save();

                    transaction.Commit();

                    return Ok(new { id = newLot.LotId });
                }
                else if (lotType == "suggestion")
                {
                    var newLot = this.lotRepository.CreateLot("Suggestion");

                    newLot.CreatePart("suggestionData", jObj, userContext);

                    newLot.Commit(userContext, lotEventDispatcher);

                    this.unitOfWork.Save();

                    var mosvViewSuggestion = this.unitOfWork.DbContext.Set<MosvViewSuggestion>().Single(e => e.LotId == newLot.LotId);

                    mosvViewSuggestion.ApplicationDocId = id;

                    this.unitOfWork.Save();

                    transaction.Commit();

                    return Ok(new { id = newLot.LotId });
                }

                throw new Exception("LotType is not found");
            }
        }

        [Route("{id}/getDoc")]
        public IHttpActionResult GetDoc(int id)
        {
            int? lotId = null;

            var admission = this.unitOfWork.DbContext.Set<MosvViewAdmission>().FirstOrDefault(e => e.ApplicationDocId == id);

            if (admission != null)
            {
                return Ok(new
                {
                    lotType = "ДОИ",
                    lotId = admission.LotId
                });
            }

            var signal = this.unitOfWork.DbContext.Set<MosvViewSignal>().FirstOrDefault(e => e.ApplicationDocId == id);

            if (signal != null)
            {
                return Ok(new
                {
                    lotType = "Сигнал",
                    lotId = signal.LotId
                });
            }

            var suggestion = this.unitOfWork.DbContext.Set<MosvViewSuggestion>().FirstOrDefault(e => e.ApplicationDocId == id);

            if (suggestion != null)
            {
                return Ok(new
                {
                    lotType = "Предложение",
                    lotId = suggestion.LotId
                });
            }

            return Ok(new
            {
                lotId = lotId
            });
        }
    }
}
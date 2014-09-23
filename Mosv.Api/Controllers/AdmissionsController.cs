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
using Common.Api.Models;
using Docs.Api.Models;
using System.Collections.Generic;
using Docs.Api.DataObjects;
using System.Text;
using Mosv.Api.ModelsDO.Signal;
using Mosv.Api.ModelsDO.Suggestion;

namespace Mosv.Api.Controllers
{
    [Authorize]
    public class AdmissionsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private ILotRepository lotRepository;
        private IAdmissionRepository admissionRepository;
        private Docs.Api.Repositories.DocRepository.IDocRepository docRepository;
        private ILotEventDispatcher lotEventDispatcher;
        private UserContext userContext;

        public AdmissionsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IAdmissionRepository admissionRepository,
            Docs.Api.Repositories.DocRepository.IDocRepository docRepository,
            ILotEventDispatcher lotEventDispatcher)
        {
            this.unitOfWork = unitOfWork;
            this.lotRepository = lotRepository;
            this.admissionRepository = admissionRepository;
            this.docRepository = docRepository;
            this.lotEventDispatcher = lotEventDispatcher;
        }

        protected override void Initialize(System.Web.Http.Controllers.HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            try
            {
                this.userContext = this.Request.GetUserContext();
            }
            catch { }
        }

        [Route("api/admissions")]
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

        [Route("api/admissions/{lotId}")]
        public IHttpActionResult GetAdmission(int lotId)
        {
            var admission = this.admissionRepository.GetAdmission(lotId);
            AdmissionViewDO returnValue = new AdmissionViewDO(admission);

            return Ok(returnValue);
        }

        [Route("api/admissions/new")]
        public IHttpActionResult GetNewAdmission()
        {
            return Ok(new AdmissionDO());
        }

        [Route("api/admissions")]
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

        [Route(@"api/admissions/{lotId}/{*path:regex(^admissionData$)}")]
        public IHttpActionResult GetPart(int lotId, string path)
        {
            var part = this.lotRepository.GetLotIndex(lotId, true).Index.GetPart<AdmissionDO>(path);

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

        [Route(@"api/admissions/{lotId}/{*path:regex(^admissionData$)}")]
        [Validate]
        public IHttpActionResult PostAdmissionData(int lotId, string path, PartVersionDO<AdmissionDO> partVersionDO)
        {
            UserContext userContext = this.Request.GetUserContext();

            var lot = this.lotRepository.GetLotIndex(lotId, true);
            PartVersion<AdmissionDO> partVersion = lot.UpdatePart(path, partVersionDO.Part, userContext);
            lot.Commit(userContext, lotEventDispatcher);

            this.unitOfWork.Save();

            return Ok();
        }

        [Route("api/admissions/{id}/fastSave")]
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

        [Route("api/admissions/{id}/loadData")]
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

        [Route("api/admissions/{id}/createLink")]
        [HttpPost]
        public IHttpActionResult CreateDocLotLink(int id, string lotType)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                UserContext userContext = this.Request.GetUserContext();

                if (lotType == "admission")
                {
                    var newLot = this.lotRepository.CreateLot("Admission");

                    newLot.CreatePart("admissionData", new AdmissionDO(), userContext);

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

                    newLot.CreatePart("signalData", new SignalDO(), userContext);

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

                    newLot.CreatePart("suggestionData", new SuggestionDO(), userContext);

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

        [Route("api/admissions/{id}/getDoc")]
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

        [Route("api/admissions/docs")]
        [HttpGet]
        public IHttpActionResult GetDocsForSelect(
            int limit = 10,
            int offset = 0,
            string filter = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            string regUri = null,
            string docName = null,
            int? docTypeId = null,
            int? docStatusId = null,
            bool? hideRead = null,
            string corrs = null,
            string units = null,
            string ds = null,
            int? isChosen = null
            )
        {
            this.userContext = this.Request.GetUserContext();

            UnitUser unitUser = this.unitOfWork.DbContext.Set<UnitUser>().FirstOrDefault(e => e.UserId == this.userContext.UserId);
            ClassificationPermission readPermission = this.unitOfWork.DbContext.Set<ClassificationPermission>().SingleOrDefault(e => e.Alias == "Read");
            DocSourceType docSourceType = this.unitOfWork.DbContext.Set<DocSourceType>().SingleOrDefault(e => e.Alias == "Internet");
            DocCasePartType docCasePartType = this.unitOfWork.DbContext.Set<DocCasePartType>().SingleOrDefault(e => e.Alias == "Control");
            List<DocStatus> docStatuses = this.unitOfWork.DbContext.Set<DocStatus>().Where(e => e.IsActive).ToList();
            List<DocUnitRole> docUnitRoles = this.unitOfWork.DbContext.Set<DocUnitRole>()
                .Where(e => e.Alias.ToLower() == "incharge" || e.Alias.ToLower() == "controlling" || e.Alias.ToLower() == "to")
                .ToList();

            int totalCount = 0;
            List<Doc> docs = new List<Doc>();

            //? if possible implement to search for docs that are not connected to lots
            //if (isChosen == 1) //true
            //{
            //    List<AopApp> allAopApps = this.unitOfWork.DbContext.Set<AopApp>().ToList();
            //    List<int> aopAppDocIds =
            //        allAopApps.Where(e => e.STDocId.HasValue).Select(e => e.STDocId.Value)
            //        .Union(allAopApps.Where(e => e.STChecklistId.HasValue).Select(e => e.STChecklistId.Value))
            //        .Union(allAopApps.Where(e => e.STNoteId.HasValue).Select(e => e.STNoteId.Value))
            //        .Union(allAopApps.Where(e => e.NDDocId.HasValue).Select(e => e.NDDocId.Value))
            //        .Union(allAopApps.Where(e => e.NDChecklistId.HasValue).Select(e => e.NDChecklistId.Value))
            //        .Union(allAopApps.Where(e => e.NDReportId.HasValue).Select(e => e.NDReportId.Value))
            //        .ToList();

            //    for (int i = 0; i < returnValue.Count; i++)
            //    {
            //        if (returnValue[i].DocId.HasValue &&
            //            aopAppDocIds.Contains(returnValue[i].DocId.Value))
            //        {
            //            returnValue.RemoveAt(i);
            //        }
            //    }
            //}

            docs = this.docRepository.GetDocs(
                      fromDate,
                      toDate,
                      regUri,
                      docName,
                      docTypeId,
                      docStatusId,
                      hideRead,
                      null,
                      corrs,
                      units,
                      ds,
                      limit,
                      offset,
                      docCasePartType,
                      readPermission,
                      unitUser,
                      out totalCount);

            List<int> loadedDocIds = docs.Select(e => e.DocId).ToList();

            var docHasReads = this.unitOfWork.DbContext.Set<DocHasRead>()
                .Where(e => e.UnitId == unitUser.UnitId && loadedDocIds.Contains(e.DocId)).ToList();

            List<DocListItemDO> returnValue = docs.Select(e => new DocListItemDO(e, unitUser)).ToList();

            foreach (var item in returnValue)
            {
                var docCorrespondents = this.unitOfWork.DbContext.Set<DocCorrespondent>()
                    .Include(e => e.Correspondent.CorrespondentType)
                    .Where(e => e.DocId == item.DocId)
                    .ToList();

                item.DocCorrespondents.AddRange(docCorrespondents.Select(e => new DocCorrespondentDO(e)).ToList());
            }

            StringBuilder sb = new StringBuilder();

            if (totalCount >= 10000)
            {
                sb.Append("Има повече от 10000 резултата, моля, въведете допълнителни филтри.");
            }

            return Ok(new
            {
                documents = returnValue,
                documentCount = totalCount,
                msg = sb.ToString()
            });
        }
    }
}
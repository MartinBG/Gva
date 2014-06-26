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
using Mosv.Api.Repositories.AdmissionRepository;

namespace Mosv.Api.Controllers
{
    [RoutePrefix("api/admissions")]
    [Authorize]
    public class AdmissionsController : MosvLotsController
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
            : base(lotRepository, unitOfWork, lotEventDispatcher)
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

            return Ok(admissions.Select(o => new AdmissionDO(o)));
        }

        [Route("{lotId}")]
        public IHttpActionResult GetAdmission(int lotId)
        {
            var admission = this.admissionRepository.GetAdmission(lotId);
            AdmissionDO returnValue = new AdmissionDO(admission);

            return Ok(returnValue);
        }

        [Route("")]
        public IHttpActionResult PostAdmission(JObject admission)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                UserContext userContext = this.Request.GetUserContext();
                var newLot = this.lotRepository.CreateLot("Admission", userContext);

                newLot.CreatePart("admissionData", admission.Get<JObject>("admissionData"), userContext);

                newLot.Commit(userContext, lotEventDispatcher);

                this.unitOfWork.Save();

                transaction.Commit();

                return Ok(new { id = newLot.LotId });
            }
        }

        [Route(@"{lotId}/{*path:regex(^admissionData$)}")]
        public override IHttpActionResult GetPart(int lotId, string path)
        {
            return base.GetPart(lotId, path);
        }

        [Route(@"{lotId}/{*path:regex(^admissionData$)}")]
        public IHttpActionResult PostAdmissionData(int lotId, string path, JObject content)
        {
            var lot = this.lotRepository.GetLotIndex(lotId);

            return base.PostPart(lotId, path, content);
        }
    }
}
using System.Linq;
using System.Web.Http;
using Common.Api.Repositories.NomRepository;
using Common.Api.UserContext;
using Common.Data;
using Common.Filters;
using Gva.Api.Models.Views.Aircraft;
using Gva.Api.Models.Views.SModeCode;
using Gva.Api.ModelsDO.Aircrafts;
using Gva.Api.ModelsDO.SModeCodes;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.SModeCodeRepository;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.SModeCodes
{
    [RoutePrefix("api/sModeCodes")]
    [Authorize]
    public class SModeCodesController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private ILotRepository lotRepository;
        private INomRepository nomRepository;
        private ISModeCodeRepository sModeCodeRepository;
        private ICaseTypeRepository caseTypeRepository;
        private ILotEventDispatcher lotEventDispatcher;
        private UserContext userContext;

        public SModeCodesController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            INomRepository nomRepository,
            ISModeCodeRepository sModeCodeRepository,
            ICaseTypeRepository caseTypeRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
        {
            this.unitOfWork = unitOfWork;
            this.lotRepository = lotRepository;
            this.nomRepository = nomRepository;
            this.sModeCodeRepository = sModeCodeRepository;
            this.caseTypeRepository = caseTypeRepository;
            this.lotEventDispatcher = lotEventDispatcher;
            this.userContext = userContext;
        }

        [Route("new")]
        public IHttpActionResult GetNewSModeCode()
        {
            return Ok(new SModeCodeDO());
        }

        [Route("")]
        public IHttpActionResult GetSModeCodes(
            int? typeId = null,
            string codeHex = null,
            string regMark = null,
            string identifier = null,
            int? isConnectedToRegistrationId = null)
        {
            var sModeCodes = this.sModeCodeRepository.GetSModeCodes(
                typeId: typeId,
                codeHex: codeHex,
                regMark: regMark,
                identifier: identifier,
                isConnectedToRegistrationId: isConnectedToRegistrationId);

            return Ok(sModeCodes.Select(c => new SModeCodeViewDO(c)));
        }

        [Route("")]
        [Validate]
        public IHttpActionResult PostSModeCode(SModeCodeDO sModeCodeData)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var newLot = this.lotRepository.CreateLot("SModeCode");
                int smodeCodeCaseTypeId = this.caseTypeRepository.GetCaseTypesForSet("SModeCode").Single().GvaCaseTypeId;
                this.caseTypeRepository.AddCaseTypes(newLot, new int[] { smodeCodeCaseTypeId });
                var sModeCodeDataPart = newLot.CreatePart("sModeCodeData", sModeCodeData, this.userContext);

                newLot.Commit(this.userContext, this.lotEventDispatcher);

                this.unitOfWork.Save();

                this.lotRepository.ExecSpSetLotPartTokens(sModeCodeDataPart.PartId);

                transaction.Commit();

                return Ok(new { id = newLot.LotId });
            }
        }

        [HttpGet]
        [Route(@"{lotId}")]
        public IHttpActionResult GetSModeCodeData(int lotId)
        {
            var sModeCodeData = this.lotRepository.GetLotIndex(lotId).Index.GetPart<SModeCodeDO>("sModeCodeData");

            return Ok(sModeCodeData.Content);
        }

        [HttpPost]
        [Route(@"{lotId}")]
        [Validate]
        public IHttpActionResult PostSModeCode(int lotId, SModeCodeDO sModeCodeData)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var lot = this.lotRepository.GetLotIndex(lotId);

                var partVersion = lot.UpdatePart("sModeCodeData", sModeCodeData, this.userContext);

                this.unitOfWork.Save();

                lot.Commit(this.userContext, this.lotEventDispatcher);

                this.lotRepository.ExecSpSetLotPartTokens(partVersion.PartId);

                transaction.Commit();

                return Ok();
            }
        }

        [HttpGet]
        [Route("nextCode")]
        public IHttpActionResult GetNextHexCode(int typeId)
        {
            return Ok(new { code = this.sModeCodeRepository.GetNextHexCode(typeId) });
        }

        [HttpGet]
        [Route("perAircraft")]
        public IHttpActionResult GetSModeCodesPerAircraft(int aircraftId)
        {
            var results = this.unitOfWork.DbContext.Set<GvaViewSModeCode>()
                .Where(c => c.AircraftId == aircraftId).ToList();

            return Ok(results.Select(c => new AircraftCertSmodDO(c)));
        }

        [HttpGet]
        [Route("{lotId}/connectedRegistration")]
        public IHttpActionResult GetConnectedRegistrationToSModeCode(int lotId)
        {
            var sModeCodeData = this.unitOfWork.DbContext.Set<GvaViewSModeCode>()
                .Single(c => c.LotId == lotId);

            ConnectedRegistrationViewDO result = null;
            if (sModeCodeData.AircraftId.HasValue && sModeCodeData.RegistrationPartIndex.HasValue)
            { 
                var registrationLot = this.lotRepository.GetLotIndex(sModeCodeData.AircraftId.Value);
                string regPath = string.Format("aircraftCertRegistrationsFM/{0}", sModeCodeData.RegistrationPartIndex);
                PartVersion<AircraftCertRegistrationFMDO> registration = registrationLot.Index.GetPart<AircraftCertRegistrationFMDO>(regPath);

                if (registration != null)
                {
                    result = new ConnectedRegistrationViewDO(registration);
                }
            }

            return Ok(new { registration = result });
        }
    }
}
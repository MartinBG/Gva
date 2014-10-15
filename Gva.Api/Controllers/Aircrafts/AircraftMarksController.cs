using System.Linq;
using System.Web.Http;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Aircrafts;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.FileRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Aircrafts
{
    [RoutePrefix("api/aircrafts/{lotId}/aircraftCertMarks")]
    [Authorize]
    public class AircraftMarksController : GvaCaseTypePartController<AircraftCertMarkDO>
    {
        private IFileRepository fileRepository;
        private INomRepository nomRepository;
        private ICaseTypeRepository caseTypeRepository;

        public AircraftMarksController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            INomRepository nomRepository,
            ILotEventDispatcher lotEventDispatcher,
            ICaseTypeRepository caseTypeRepository,
            UserContext userContext)
            : base("aircraftCertMarks", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.fileRepository = fileRepository;
            this.nomRepository = nomRepository;
            this.caseTypeRepository = caseTypeRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewCertMark(int lotId)
        {
            AircraftCertMarkDO certificate = new AircraftCertMarkDO();
            certificate.Valid = this.nomRepository.GetNomValue("boolean", "yes");
            GvaCaseType caseType = this.caseTypeRepository.GetCaseTypesForSet("aircraft").Single();
            CaseDO caseDO = new CaseDO()
            {
                CaseType = new NomValue()
                {
                    NomValueId = caseType.GvaCaseTypeId,
                    Name = caseType.Name,
                    Alias = caseType.Alias
                },
                BookPageNumber = this.fileRepository.GetNextBPN(lotId, caseType.GvaCaseTypeId).ToString()
            };
            return Ok(new CaseTypePartDO<AircraftCertMarkDO>(certificate, caseDO));
        }
    }
}
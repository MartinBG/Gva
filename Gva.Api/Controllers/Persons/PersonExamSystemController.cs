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
    [RoutePrefix("api/persons/{lotId}/personExamSystData")]
    [Authorize]
    public class PersonExamSystemController : GvaCaseTypesPartController<PersonExamSystDataDO>
    {
        private ILotRepository lotRepository;
        private IFileRepository fileRepository;

        public PersonExamSystemController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("personExamSystData", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.lotRepository = lotRepository;
            this.fileRepository = fileRepository;
        }

        public override IHttpActionResult GetParts(int lotId, int? caseTypeId = null) 
        {
            var partVersion = this.lotRepository.GetLotIndex(lotId).Index.GetPart<PersonExamSystDataDO>("personExamSystData");
            var lotFiles = this.fileRepository.GetFileReferences(partVersion.PartId, caseTypeId);

            return Ok(new CaseTypesPartDO<PersonExamSystDataDO>(partVersion, lotFiles));
        }
    }
}
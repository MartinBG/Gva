using System;
using System.Web.Http;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.ExaminationSystemRepository;
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
        private IUnitOfWork unitOfWork;
        private IExaminationSystemRepository examinationSystemRepository;

        public PersonExamSystemController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            IExaminationSystemRepository examinationSystemRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("personExamSystData", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.lotRepository = lotRepository;
            this.fileRepository = fileRepository;
            this.unitOfWork = unitOfWork;
            this.examinationSystemRepository = examinationSystemRepository;
        }

        public override IHttpActionResult GetParts(int lotId, int? caseTypeId = null) 
        {
            var partVersion = this.lotRepository.GetLotIndex(lotId).Index.GetPart<PersonExamSystDataDO>("personExamSystData");
            var lotFiles = this.fileRepository.GetFileReferences(partVersion.PartId, caseTypeId);

            return Ok(new CaseTypesPartDO<PersonExamSystDataDO>(partVersion, lotFiles));
        }

        [Route("newState")]
        public IHttpActionResult GetNewManualState(int lotId)
        {
            PersonExamSystStateDO state = new PersonExamSystStateDO()
            {
                StateMethod = "Manually",
                FromDate = DateTime.Now,
                ToDate = DateTime.Now.AddMonths(18),
                State = "Started"
            };

            return Ok(state);
        }

        [Route("saveState")]
        public IHttpActionResult PostNewManualState(int lotId, PersonExamSystStateDO state)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                this.examinationSystemRepository.SaveNewState(lotId,state);

                transaction.Commit();

                return Ok();
            }
        }
    }
}
using System;
using System.Web.Http;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.Models.Enums;
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
        private IUnitOfWork unitOfWork;
        private ILotEventDispatcher lotEventDispatcher;
        private UserContext userContext;

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
            this.unitOfWork = unitOfWork;
            this.lotEventDispatcher = lotEventDispatcher;
            this.userContext = userContext;
        }

        public override IHttpActionResult GetParts(int lotId, int? caseTypeId = null) 
        {
            var partVersion = this.lotRepository.GetLotIndex(lotId).Index.GetPart<PersonExamSystDataDO>("personExamSystData");
            var lotFiles = this.fileRepository.GetFileReferences(partVersion.PartId, caseTypeId);

            return Ok(new CaseTypesPartDO<PersonExamSystDataDO>(partVersion, lotFiles));
        }

        [Route("updateInfo")]
        [HttpPost]
        public IHttpActionResult PostData(int lotId, PersonExamSystDataDO data)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var lot = this.lotRepository.GetLotIndex(lotId);

                var partVersion = lot.UpdatePart("personExamSystData", data, this.userContext);

                lot.Commit(this.userContext, this.lotEventDispatcher);

                this.unitOfWork.Save();

                this.lotRepository.ExecSpSetLotPartTokens(partVersion.PartId);

                transaction.Commit();

                return Ok();
            }
        }

        [Route("newState")]
        public IHttpActionResult GetNewManualState(int lotId)
        {
            PersonNewExamSystStateDO state = new PersonNewExamSystStateDO()
            {
                StateMethod = QualificationStateMethod.Manually.ToString(),
                FromDate = DateTime.Now,
                ToDate = DateTime.Now.AddMonths(18),
                State = QualificationState.Started.ToString()
            };

            return Ok(state);
        }
    }
}
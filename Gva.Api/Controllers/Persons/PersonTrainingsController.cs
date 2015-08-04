using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.FileRepository;
using Gva.Api.Repositories.PersonTrainingRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Persons
{
    [RoutePrefix("api/persons/{lotId}/personDocumentTrainings")]
    [Authorize]
    public class PersonTrainingsController : GvaCaseTypePartController<PersonTrainingDO>
    {
        private INomRepository nomRepository;
        private IPersonTrainingRepository personTrainingRepository;
        private ICaseTypeRepository caseTypeRepository;

        public PersonTrainingsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            INomRepository nomRepository,
            ICaseTypeRepository caseTypeRepository,
            IPersonTrainingRepository personTrainingRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("personDocumentTrainings", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.nomRepository = nomRepository;
            this.personTrainingRepository = personTrainingRepository;
            this.caseTypeRepository = caseTypeRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewTraining(int lotId, int? caseTypeId = null)
        {
            PersonTrainingDO newTraining = new PersonTrainingDO()
            {
                DocumentDateValidFrom = DateTime.Now,
                ValidId = this.nomRepository.GetNomValue("boolean", "yes").NomValueId
            };

            CaseDO caseDO = null;
            if (caseTypeId.HasValue)
            {
                GvaCaseType caseType = this.caseTypeRepository.GetCaseType(caseTypeId.Value);
                caseDO = new CaseDO()
                {
                    CaseType = new NomValue()
                    {
                        NomValueId = caseType.GvaCaseTypeId,
                        Name = caseType.Name,
                        Alias = caseType.Alias
                    }
                };
            }

            return Ok(new CaseTypePartDO<PersonTrainingDO>(newTraining, caseDO));
        }

        [Route("exams")]
        public IHttpActionResult GetExams(int lotId, int? caseTypeId = null)
        {
            int roleExamId = this.nomRepository.GetNomValue("documentRoles", "exam").NomValueId;
            var examViewDOs = this.personTrainingRepository.GetTrainings(lotId, caseTypeId, roleExamId);

            return Ok(examViewDOs);
        }

        public override IHttpActionResult GetParts(int lotId, int? caseTypeId = null)
        {
            var trainingViewDOs = this.personTrainingRepository.GetTrainings(lotId, caseTypeId);
            return Ok(trainingViewDOs);
        }
    }
}
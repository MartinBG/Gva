using System;
using System.Collections.Generic;
using System.Web.Http;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.FileRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Persons
{
    [RoutePrefix("api/persons/{lotId}/personExams")]
    [Authorize]
    public class PersonExamsController : GvaCaseTypesPartController<PersonExamDO>
    {
        private IApplicationRepository applicationRepository;
        private ILotRepository lotRepository;

        public PersonExamsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            IApplicationRepository applicationRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("personExams", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.applicationRepository = applicationRepository;
            this.lotRepository = lotRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewExam(int lotId, int? appId = null)
        {
            PersonExamDO newExam = new PersonExamDO()
            {
                ExamDate = DateTime.Now,
                SuccessThreshold = 60
            };

            var cases = new List<CaseDO>();
            if (appId.HasValue)
            {
                this.lotRepository.GetLotIndex(lotId);
                cases.Add(new CaseDO()
                {
                    IsAdded = true,
                    Applications = new List<ApplicationNomDO>()
                    {
                        this.applicationRepository.GetInitApplication(appId)
                    }
                });
            }

            return Ok(new CaseTypesPartDO<PersonExamDO>(newExam, cases));
        }

        [Route("~/api/persons/personExams/new")]
        public IHttpActionResult GetNewExam()
        {
            PersonExamDO newExam = new PersonExamDO()
            {
                ExamDate = DateTime.Now,
                SuccessThreshold = 60
            };

            var cases = new List<CaseDO>() { new CaseDO() };

            return Ok(new CaseTypesPartDO<PersonExamDO>(newExam, cases));
        }
    }
}
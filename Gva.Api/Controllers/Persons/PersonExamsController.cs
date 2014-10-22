using System;
using System.Collections.Generic;
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
    [RoutePrefix("api/persons/{lotId}/personExams")]
    [Authorize]
    public class PersonExamsController : GvaCaseTypesPartController<PersonExamDO>
    {
        public PersonExamsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("personExams", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
        }

        [Route("new")]
        public IHttpActionResult GetNewExam(int lotId)
        {
            PersonExamDO newExam = new PersonExamDO()
            {
                ExamDate = DateTime.Now,
                SuccessThreshold = 60
            };

            return Ok(new CaseTypesPartDO<PersonExamDO>(newExam, new List<CaseDO>()));
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
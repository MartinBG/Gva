using System;
using System.Collections.Generic;
using System.Web.Http;
using Common.Api.Repositories.NomRepository;
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
    [RoutePrefix("api/persons/{lotId}/personDocumentExams")]
    [Authorize]
    public class PersonDocumentExamsController : GvaCaseTypePartController<PersonDocumentExamDO>
    {
        private IApplicationRepository applicationRepository;
        private ILotRepository lotRepository;
        private INomRepository nomRepository;

        public PersonDocumentExamsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            IApplicationRepository applicationRepository,
            INomRepository nomRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("personDocumentExams", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.applicationRepository = applicationRepository;
            this.nomRepository = nomRepository;
            this.lotRepository = lotRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewDocumentExam(int lotId, int? appId = null)
        {
            PersonDocumentExamDO newDocumentExam = new PersonDocumentExamDO()
            {
                DocumentDateValidFrom = DateTime.Now
            };

            newDocumentExam.Valid = this.nomRepository.GetNomValue("boolean", "yes");

            CaseDO caseDO = null;
            if (appId.HasValue)
            {
                this.lotRepository.GetLotIndex(lotId);
                caseDO = new CaseDO()
                {
                    IsAdded = true,
                    Applications = new List<ApplicationNomDO>()
                    {
                        this.applicationRepository.GetInitApplication(appId)
                    }
                };
            }

            return Ok(new CaseTypePartDO<PersonDocumentExamDO>(newDocumentExam, caseDO));
        }
    }
}
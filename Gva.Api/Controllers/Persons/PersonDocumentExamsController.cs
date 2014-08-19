using System;
using System.Collections.Generic;
using System.Web.Http;
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
    public class PersonDocumentExamsController : GvaFilePartController<PersonDocumentExamDO>
    {
        private IApplicationRepository applicationRepository;
        private ILotRepository lotRepository;

        public PersonDocumentExamsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            IApplicationRepository applicationRepository,
            ILotEventDispatcher lotEventDispatcher)
            : base("personDocumentExams", unitOfWork, lotRepository, fileRepository, lotEventDispatcher)
        {
            this.applicationRepository = applicationRepository;
            this.lotRepository = lotRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewDocumentExam(int lotId, int? appId = null)
        {
            PersonDocumentExamDO newDocumentExam = new PersonDocumentExamDO()
            {
                DocumentDateValidFrom = DateTime.Now
            };

            var files = new List<FileDO>();
            if (appId.HasValue)
            {
                this.lotRepository.GetLotIndex(lotId);
                files.Add(new FileDO()
                {
                    IsAdded = true,
                    Applications = new List<ApplicationNomDO>()
                    {
                        this.applicationRepository.GetInitApplication(appId)
                    }
                });
            }

            return Ok(new FilePartVersionDO<PersonDocumentExamDO>(newDocumentExam, files));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Common.Api.Repositories.NomRepository;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.FileRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Persons
{
    [RoutePrefix("api/persons/{lotId}/personDocumentTrainings")]
    [Authorize]
    public class PersonTrainingsController : GvaCaseTypePartController<PersonTrainingDO>
    {
        private INomRepository nomRepository;
        private IFileRepository fileRepository;
        private ILotRepository lotRepository;
        private string path = "personDocumentTrainings";

        public PersonTrainingsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            INomRepository nomRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("personDocumentTrainings", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.nomRepository = nomRepository;
            this.fileRepository = fileRepository;
            this.lotRepository = lotRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewTraining(int lotId)
        {
            PersonTrainingDO newTraining = new PersonTrainingDO()
            {
                DocumentDateValidFrom = DateTime.Now,
                Valid = this.nomRepository.GetNomValue("boolean", "yes")
            };

            return Ok(new CaseTypePartDO<PersonTrainingDO>(newTraining));
        }

        [Route("exams")]
        public IHttpActionResult GetExams(int lotId, int? caseTypeId = null)
        {
            var partVersions = this.lotRepository.GetLotIndex(lotId).Index.GetParts<PersonTrainingDO>(this.path)
                .Where(d => d.Content.DocumentRole.Alias == "exam");

            List<CaseTypePartDO<PersonTrainingDO>> partVersionDOs = new List<CaseTypePartDO<PersonTrainingDO>>();
            foreach (var partVersion in partVersions)
            {
                var lotFile = this.fileRepository.GetFileReference(partVersion.PartId, caseTypeId);
                if (!caseTypeId.HasValue || lotFile != null)
                {
                    partVersionDOs.Add(new CaseTypePartDO<PersonTrainingDO>(partVersion, lotFile));
                }
            }

            return Ok(partVersionDOs);
        }
    }
}
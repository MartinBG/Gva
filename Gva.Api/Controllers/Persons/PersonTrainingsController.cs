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
        private ICaseTypeRepository caseTypeRepository;
        private string path = "personDocumentTrainings";

        public PersonTrainingsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            INomRepository nomRepository,
            ICaseTypeRepository caseTypeRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("personDocumentTrainings", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.nomRepository = nomRepository;
            this.fileRepository = fileRepository;
            this.lotRepository = lotRepository;
            this.caseTypeRepository = caseTypeRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewTraining(int lotId, int? caseTypeId = null)
        {
            PersonTrainingDO newTraining = new PersonTrainingDO()
            {
                DocumentDateValidFrom = DateTime.Now,
                Valid = this.nomRepository.GetNomValue("boolean", "yes")
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
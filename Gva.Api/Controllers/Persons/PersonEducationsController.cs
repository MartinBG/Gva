using System.Linq;
using System.Collections.Generic;
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
    [RoutePrefix("api/persons/{lotId}/personDocumentEducations")]
    [Authorize]
    public class PersonEducationsController : GvaCaseTypesPartController<PersonEducationDO>
    {
        private ILotRepository lotRepository;
        private IFileRepository fileRepository;
        private INomRepository nomRepository;
        private string path;

        public PersonEducationsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            INomRepository nomRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("personDocumentEducations", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.lotRepository = lotRepository;
            this.fileRepository = fileRepository;
            this.nomRepository = nomRepository;
            this.path = "personDocumentEducations";
        }

        [Route("new")]
        public IHttpActionResult GetNewEducation(int lotId)
        {
            PersonEducationDO newEducation = new PersonEducationDO();

            return Ok(new CaseTypesPartDO<PersonEducationDO>(newEducation, new List<CaseDO>()));
        }


        public override IHttpActionResult GetParts(int lotId, int? caseTypeId = null)
        {
            var educations = this.lotRepository.GetLotIndex(lotId).Index.GetParts<PersonEducationDO>(this.path);

            List<PersonEducationViewDO> educationViewDOs = new List<PersonEducationViewDO>();
            foreach (var educationPartVersion in educations)
            {
                var lotFiles = this.fileRepository.GetFileReferences(educationPartVersion.PartId, caseTypeId);
                if (!caseTypeId.HasValue || lotFiles.Length != 0)
                {
                    educationViewDOs.Add(new PersonEducationViewDO()
                    {
                        Cases = lotFiles.Select(lf => new CaseDO(lf)).ToList(),
                        PartIndex = educationPartVersion.Part.Index,
                        PartId = educationPartVersion.PartId,
                        DocumentNumber = educationPartVersion.Content.DocumentNumber,
                        Notes = educationPartVersion.Content.Notes,
                        School = educationPartVersion.Content.SchoolId.HasValue ? this.nomRepository.GetNomValue("schools", educationPartVersion.Content.SchoolId.Value) : null,
                        CompletionDate = educationPartVersion.Content.CompletionDate,
                        Graduation = educationPartVersion.Content.GraduationId.HasValue ? this.nomRepository.GetNomValue("graduations", educationPartVersion.Content.GraduationId.Value) : null,
                        Speciality = educationPartVersion.Content.Speciality
                    });
                }
            }
            return Ok(educationViewDOs);
        }
    }
}
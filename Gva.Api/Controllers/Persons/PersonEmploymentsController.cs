using System.Collections.Generic;
using System.Web.Http;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.FileRepository;
using Gva.Api.Repositories.OrganizationRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Persons
{
    [RoutePrefix("api/persons/{lotId}/personDocumentEmployments")]
    [Authorize]
    public class PersonEmploymentsController : GvaCaseTypePartController<PersonEmploymentDO>
    {
        private INomRepository nomRepository;
        private ILotRepository lotRepository;
        private IOrganizationRepository organizationRepository;
        private IFileRepository fileRepository;
        private string path;

        public PersonEmploymentsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            INomRepository nomRepository,
            IOrganizationRepository organizationRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("personDocumentEmployments", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.nomRepository = nomRepository;
            this.lotRepository = lotRepository;
            this.organizationRepository = organizationRepository;
            this.fileRepository = fileRepository;
            this.path = "personDocumentEmployments";
        }

        [Route("new")]
        public IHttpActionResult GetNewEmployment(int lotId)
        {
            PersonEmploymentDO newEmployment = new PersonEmploymentDO()
            {
                ValidId = this.nomRepository.GetNomValue("boolean", "yes").NomValueId,
                CountryId = this.nomRepository.GetNomValue("countries", "BG").NomValueId
            };

            return Ok(new CaseTypePartDO<PersonEmploymentDO>(newEmployment));
        }

        public override IHttpActionResult GetParts(int lotId, int? caseTypeId = null)
        {
            var employments = this.lotRepository.GetLotIndex(lotId).Index.GetParts<PersonEmploymentDO>(this.path);

            List<PersonEmploymentViewDO> employmentViewDOs = new List<PersonEmploymentViewDO>();
            foreach (var employmentPartVersion in employments)
            {
                var lotFile = this.fileRepository.GetFileReference(employmentPartVersion.PartId, caseTypeId);
                if (!caseTypeId.HasValue || lotFile != null)
                {
                    var organization = employmentPartVersion.Content.OrganizationId.HasValue ?
                        this.organizationRepository.GetOrganization(employmentPartVersion.Content.OrganizationId.Value) : null;
                    NomValue organizationNom = organization != null ?
                        new NomValue()
                        {
                            NomValueId = organization.LotId,
                            Name = organization.Name,
                            NameAlt = organization.NameAlt
                        } : null;
                    employmentViewDOs.Add(new PersonEmploymentViewDO()
                    {
                        Case = lotFile != null ? new CaseDO(lotFile) : null,
                        PartIndex = employmentPartVersion.Part.Index,
                        PartId = employmentPartVersion.PartId,
                        Hiredate = employmentPartVersion.Content.Hiredate,
                        Country = employmentPartVersion.Content.CountryId.HasValue ? this.nomRepository.GetNomValue("countries", employmentPartVersion.Content.CountryId.Value) : null,
                        Notes = employmentPartVersion.Content.Notes,
                        Organization = organizationNom,
                        Valid = employmentPartVersion.Content.ValidId.HasValue ? this.nomRepository.GetNomValue("boolean", employmentPartVersion.Content.ValidId.Value) : null,
                        EmploymentCategory = employmentPartVersion.Content.EmploymentCategoryId.HasValue ? this.nomRepository.GetNomValue("employmentCategories", employmentPartVersion.Content.EmploymentCategoryId.Value) : null,
                    });
                }
            }
            return Ok(employmentViewDOs);
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Organizations;
using Gva.Api.Repositories.FileRepository;
using Gva.Api.Repositories.OrganizationRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Organizations
{
    [RoutePrefix("api/organizations/{lotId}/organizationInspections")]
    [Authorize]
    public class OrganizationInspectionsController : GvaCaseTypePartController<OrganizationInspectionDO>
    {
        private ILotRepository lotRepository;
        private IOrganizationRepository organizationRepository;
        private IFileRepository fileRepository;
        private string path;

        public OrganizationInspectionsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            ILotEventDispatcher lotEventDispatcher,
            IOrganizationRepository organizationRepository,
            UserContext userContext)
            : base("organizationInspections", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.path = "organizationInspections";
            this.lotRepository = lotRepository;
            this.organizationRepository = organizationRepository;
            this.fileRepository = fileRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewInspection(int lotId)
        {
            OrganizationInspectionDO newInspection = new OrganizationInspectionDO();

            return Ok(new CaseTypePartDO<OrganizationInspectionDO>(newInspection));
        }

        [Route("{inspectionPartIndex}/recommendations")]
        public IHttpActionResult GetInspectionRecommendations(int lotId, int inspectionPartIndex)
        {
            return Ok(this.organizationRepository.GetInspectionRecommendations(lotId, inspectionPartIndex));
        }

        [Route("data")]
        public IHttpActionResult GetInspections(int lotId, int? caseTypeId = null, [FromUri] int?[] partIndexes = null)
        {
            var partVersions = this.lotRepository.GetLotIndex(lotId).Index.GetParts<OrganizationInspectionDO>(this.path);

            if (partIndexes != null)
            {
                partVersions = partVersions.Where(pv => partIndexes.Contains(pv.Part.Index)).ToArray();
            }

            List<CaseTypePartDO<OrganizationInspectionDO>> partVersionDOs = new List<CaseTypePartDO<OrganizationInspectionDO>>();
            foreach (var partVersion in partVersions)
            {
                var lotFile = this.fileRepository.GetFileReference(partVersion.PartId, caseTypeId);
                if (!caseTypeId.HasValue || lotFile != null)
                {
                    partVersionDOs.Add(new CaseTypePartDO<OrganizationInspectionDO>(partVersion, lotFile));
                }
            }

            return Ok(partVersionDOs);
        }
    }
}
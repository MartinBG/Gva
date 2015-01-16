using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Api.UserContext;
using Common.Data;
using Common.Filters;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.FileRepository;
using Gva.Api.Repositories.PersonRepository;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Persons
{
    [RoutePrefix("api/persons/{lotId}/personReports")]
    [Authorize]
    public class PersonReportsController : GvaCaseTypePartController<PersonReportDO>
    {
        private ICaseTypeRepository caseTypeRepository;
        private ILotRepository lotRepository;
        private IFileRepository fileRepository;
        private IPersonRepository personRepository;
        private string path;

        public PersonReportsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            INomRepository nomRepository,
            ICaseTypeRepository caseTypeRepository,
            IPersonRepository personRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("personReports", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.caseTypeRepository = caseTypeRepository;
            this.lotRepository = lotRepository;
            this.fileRepository = fileRepository;
            this.personRepository = personRepository;
            this.path = "personReports";
        }

        [Route("new")]
        public IHttpActionResult GetNewReport(int lotId)
        {
            return Ok(new CaseTypePartDO<PersonReportDO>(new PersonReportDO()));
        }

        public override IHttpActionResult GetParts(int lotId, int? caseTypeId = null)
        {
            var partVersions = this.lotRepository.GetLotIndex(lotId).Index.GetParts<PersonReportViewDO>(this.path);
           
            List<CaseTypePartDO<PersonReportViewDO>> partVersionDOs = new List<CaseTypePartDO<PersonReportViewDO>>();
            foreach (var partVersion in partVersions)
            {
                partVersion.Content.IncludedPersonChecks = this.personRepository.GetChecksForReport(partVersion.Content.IncludedChecks)
                    .GroupBy(c => c.LotId)
                    .Select(c => new IncludedPersonDO() {
                        LotId = c.First().LotId,
                        Lin = c.First().PersonLin
                    })
                    .ToList();

                var lotFile = this.fileRepository.GetFileReference(partVersion.PartId, caseTypeId);
                if (!caseTypeId.HasValue || lotFile != null)
                {
                    partVersionDOs.Add(new CaseTypePartDO<PersonReportViewDO>(partVersion, lotFile));
                }
            }

            return Ok(partVersionDOs);
        }
    }
}
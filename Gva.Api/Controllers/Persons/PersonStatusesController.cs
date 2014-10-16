using System;
using System.Linq;
using System.Web.Http;
using Common.Api.Models;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.FileRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Persons
{
    [RoutePrefix("api/persons/{lotId}/personStatuses")]
    [Authorize]
    public class PersonStatusesController : GvaCaseTypesPartController<PersonStatusDO>
    {
        private ICaseTypeRepository caseTypeRepository;

        public PersonStatusesController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            ICaseTypeRepository caseTypeRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("personStatuses", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.caseTypeRepository = caseTypeRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewStatus(int lotId)
        {
            var cases = this.caseTypeRepository.GetCaseTypesForSet("person")
                .Select(ct => new CaseDO()
                    {
                        CaseType = new NomValue()
                        {
                            NomValueId = ct.GvaCaseTypeId,
                            Name = ct.Name,
                            Alias = ct.Alias
                        },
                        IsAdded = true
                    })
                .ToList();

            PersonStatusDO newStatus = new PersonStatusDO()
            {
                DocumentDateValidFrom = DateTime.Now
            };

            return Ok(new CaseTypesPartDO<PersonStatusDO>(newStatus, cases));
        }
    }
}
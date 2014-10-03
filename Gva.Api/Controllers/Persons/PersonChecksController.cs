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
    [RoutePrefix("api/persons/{lotId}/personDocumentChecks")]
    [Authorize]
    public class PersonChecksController : GvaFilePartController<PersonCheckDO>
    {
        private IApplicationRepository applicationRepository;
        private ILotRepository lotRepository;
        private INomRepository nomRepository;

        public PersonChecksController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            IApplicationRepository applicationRepository,
            INomRepository nomRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("personDocumentChecks", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.applicationRepository = applicationRepository;
            this.lotRepository = lotRepository;
            this.nomRepository = nomRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewCheck(int lotId, int? appId = null)
        {
            PersonCheckDO newCheck = new PersonCheckDO()
            {
                DocumentDateValidFrom = DateTime.Now
            };

            newCheck.Valid = this.nomRepository.GetNomValue("boolean", "yes");

            var cases = new List<CaseDO>();
            if (appId.HasValue)
            {
                this.lotRepository.GetLotIndex(lotId);
                cases.Add(new CaseDO()
                {
                    IsAdded = true,
                    Applications = new List<ApplicationNomDO>()
                    {
                        this.applicationRepository.GetInitApplication(appId)
                    }
                });
            }

            return Ok(new FilePartVersionDO<PersonCheckDO>(newCheck, cases));
        }
    }
}
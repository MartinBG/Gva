using System;
using System.Collections.Generic;
using System.Web.Http;
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
    [RoutePrefix("api/persons/{lotId}/personDocumentOthers")]
    [Authorize]
    public class PersonDocumentOthersController : GvaFilePartController<PersonDocumentOtherDO>
    {
        private IApplicationRepository applicationRepository;
        private ILotRepository lotRepository;

        public PersonDocumentOthersController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            IApplicationRepository applicationRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("personDocumentOthers", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.applicationRepository = applicationRepository;
            this.lotRepository = lotRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewDocumentOther(int lotId, int? appId = null)
        {
            PersonDocumentOtherDO newDocumentOther = new PersonDocumentOtherDO()
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

            return Ok(new FilePartVersionDO<PersonDocumentOtherDO>(newDocumentOther, files));
        }
    }
}
using System;
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
    [RoutePrefix("api/persons/{lotId}/personDocumentOthers")]
    [Authorize]
    public class PersonDocumentOthersController : GvaCaseTypePartController<PersonDocumentOtherDO>
    {
        private INomRepository nomRepository;

        public PersonDocumentOthersController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            INomRepository nomRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("personDocumentOthers", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.nomRepository = nomRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewDocumentOther(int lotId)
        {
            PersonDocumentOtherDO newDocumentOther = new PersonDocumentOtherDO()
            {
                DocumentDateValidFrom = DateTime.Now,
                Valid = this.nomRepository.GetNomValue("boolean", "yes")
            };

            return Ok(new CaseTypePartDO<PersonDocumentOtherDO>(newDocumentOther, null));
        }
    }
}
using System;
using System.Web.Http;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.FileRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Persons
{
    [RoutePrefix("api/persons/{lotId}/personDocumentMedicals")]
    [Authorize]
    public class PersonMedicalsController : GvaCaseTypePartController<PersonMedicalDO>
    {
        public PersonMedicalsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("personDocumentMedicals", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        { }

        [Route("new")]
        public IHttpActionResult GetNewMedical(int lotId)
        {
            PersonMedicalDO newMedical = new PersonMedicalDO()
            {
                DocumentNumberPrefix = "MED BG",
                DocumentDateValidFrom = DateTime.Now
            };

            return Ok(new CaseTypePartDO<PersonMedicalDO>(newMedical));
        }
    }
}
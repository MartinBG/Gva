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
    [RoutePrefix("api/persons/{lotId}/personDocumentLangCertificates")]
    [Authorize]
    public class PersonLanguageCertificatesController : GvaCaseTypePartController<PersonLangCertDO>
    {
        private INomRepository nomRepository;

        public PersonLanguageCertificatesController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            INomRepository nomRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("personDocumentLangCertificates", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.nomRepository = nomRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewLangCert(int lotId)
        {
            PersonLangCertDO newLangCert = new PersonLangCertDO()
            {
                DocumentDateValidFrom = DateTime.Now,
                Valid = this.nomRepository.GetNomValue("boolean", "yes")
            };

            return Ok(new CaseTypePartDO<PersonLangCertDO>(newLangCert));
        }
    }
}
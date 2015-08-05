using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Http;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.FileRepository;
using Gva.Api.Repositories.PersonLangCertRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Persons
{
    [RoutePrefix("api/persons/{lotId}/personDocumentLangCertificates")]
    [Authorize]
    public class PersonLanguageCertificatesController : GvaCaseTypePartController<PersonLangCertDO>
    {
        private INomRepository nomRepository;
        private ICaseTypeRepository caseTypeRepository;
        private IPersonLangCertRepository personLangCertRepository;
        private ILotRepository lotRepository;

        public PersonLanguageCertificatesController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            INomRepository nomRepository,
            ICaseTypeRepository caseTypeRepository,
            IPersonLangCertRepository personLangCertRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("personDocumentLangCertificates", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.nomRepository = nomRepository;
            this.caseTypeRepository = caseTypeRepository;
            this.lotRepository = lotRepository;
            this.personLangCertRepository = personLangCertRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewLangCert(int lotId, int? caseTypeId = null)
        {
            PersonLangCertDO newLangCert = new PersonLangCertDO()
            {
                DocumentDateValidFrom = DateTime.Now,
                ValidId = this.nomRepository.GetNomValue("boolean", "yes").NomValueId,
                LangLevelEntries = new List<PersonLangLevelDO>()
            };

            CaseDO caseDO = null;
            if (caseTypeId.HasValue)
            {
                GvaCaseType caseType = this.caseTypeRepository.GetCaseType(caseTypeId.Value);
                caseDO = new CaseDO()
                {
                    CaseType = new NomValue()
                    {
                        NomValueId = caseType.GvaCaseTypeId,
                        Name = caseType.Name,
                        Alias = caseType.Alias
                    }
                };
            }

            return Ok(new CaseTypePartDO<PersonLangCertDO>(newLangCert, caseDO));
        }

        [Route("newLangLevel")]
        public IHttpActionResult GetNewLangLevel(int lotId)
        {
            PersonLangLevelDO langLevel = new PersonLangLevelDO()
            {
                ChangeDate = DateTime.Now
            };

            return Ok(langLevel);
        }

        [Route("byValidity")]
        public IHttpActionResult GetLangCertsByValidity(int lotId, int? caseTypeId = null, bool? valid = true)
        {
            var langCertViewDOs = this.personLangCertRepository.GetLangCerts(lotId, caseTypeId, valid);
            return Ok(langCertViewDOs);
        }

        public override IHttpActionResult GetParts(int lotId, int? caseTypeId = null)
        {
            var langCertViewDOs = this.personLangCertRepository.GetLangCerts(lotId, caseTypeId);
            return Ok(langCertViewDOs);
        }

        [Route("{partIndex}/langLevelHistory")]
        public IHttpActionResult GetLangLevelHistory(int lotId, int partIndex)
        {
            var partVersion = this.lotRepository.GetLotIndex(lotId).Index.GetPart<PersonLangCertDO>(string.Format("personDocumentLangCertificates/{0}", partIndex));
            List<PersonLangLevelViewDO> langLevelHistoryDOs = partVersion.Content.LangLevelEntries
                .Select(l => new PersonLangLevelViewDO()
                {
                    ChangeDate = l.ChangeDate,
                    LangLevel = l.LangLevelId.HasValue ? this.nomRepository.GetNomValue("langLevels", l.LangLevelId.Value) : null
                })
                .ToList();

            return Ok(langLevelHistoryDOs);
        }
    }
}
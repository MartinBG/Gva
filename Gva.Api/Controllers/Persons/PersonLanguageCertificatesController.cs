using System;
using System.Collections.Generic;
using System.Linq;
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
        private ILotRepository lotRepository;

        public PersonLanguageCertificatesController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            INomRepository nomRepository,
            ICaseTypeRepository caseTypeRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("personDocumentLangCertificates", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.nomRepository = nomRepository;
            this.caseTypeRepository = caseTypeRepository;
            this.lotRepository = lotRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewLangCert(int lotId, int? caseTypeId = null)
        {
            PersonLangCertDO newLangCert = new PersonLangCertDO()
            {
                DocumentDateValidFrom = DateTime.Now,
                Valid = this.nomRepository.GetNomValue("boolean", "yes"),
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
            var langCerts = this.lotRepository.GetLotIndex(lotId).Index.GetParts<PersonLangCertDO>("personDocumentLangCertificates").ToList();
            if (valid == true)
            {
                langCerts = langCerts
                    .Where(e => !e.Content.DocumentDateValidTo.HasValue || DateTime.Compare(e.Content.DocumentDateValidTo.Value, DateTime.Now) >= 0)
                    .ToList();
            }

            List<CaseTypePartDO<PersonLangCertDO>> langCertDOs = new List<CaseTypePartDO<PersonLangCertDO>>();
            langCerts.ForEach(l => langCertDOs.Add(new CaseTypePartDO<PersonLangCertDO>(l)));

            return Ok(langCertDOs);
        }
    }
}
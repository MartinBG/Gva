using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Common.Api.Models;
using Common.Data;
using Common.Linq;
using Gva.Api.Models.Views.Aircraft;
using Gva.Api.ModelsDO.Aircrafts;
using Regs.Api.Repositories.LotRepositories;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Models;
using Gva.Api.ModelsDO.Persons;
using Regs.Api.Models;
using Common.Api.UserContext;
using Regs.Api.LotEvents;
using Docs.Api.Repositories.CorrespondentRepository;
using Docs.Api.DataObjects;
using Docs.Api.Models;
using R_0009_000015;

namespace Gva.Api.Repositories.IntegrationRepository
{
    public class IntegrationRepository : IIntegrationRepository
    {
        private IUnitOfWork unitOfWork;
        private ICaseTypeRepository caseTypeRepository;
        private ILotRepository lotRepository;
        private ILotEventDispatcher lotEventDispatcher;
        private ICorrespondentRepository correspondentRepository;

        public IntegrationRepository(
            IUnitOfWork unitOfWork,
            ICaseTypeRepository caseTypeRepository,
            ILotRepository lotRepository,
            ILotEventDispatcher lotEventDispatcher,
            ICorrespondentRepository correspondentRepository)
        {
            this.unitOfWork = unitOfWork;
            this.caseTypeRepository = caseTypeRepository;
            this.lotRepository = lotRepository;
            this.lotEventDispatcher = lotEventDispatcher;
            this.correspondentRepository = correspondentRepository;
        }

        public void UpdatePersonDataCaseTypes(GvaCaseType caseType, PersonDataDO personData, Lot lot, UserContext userContext)
        {
            NomValue caseTypeNom = new NomValue()
            {
                NomValueId = caseType.GvaCaseTypeId,
                Name = caseType.Name,
                Alias = caseType.Alias
            };

            personData.CaseTypes.Add(caseTypeNom);

            this.caseTypeRepository.AddCaseTypes(lot, personData.CaseTypes.Select(ct => ct.NomValueId));

            var personDataPart = lot.UpdatePart("personData", personData, userContext);

            lot.Commit(userContext, this.lotEventDispatcher);

            this.unitOfWork.Save();

            this.lotRepository.ExecSpSetLotPartTokens(personDataPart.PartId);
        }

        public List<int> GetCorrespondentIdsPerPersonLot(PersonDataDO personData, UserContext userContext)
        {
            string personNames = string.Format("{0} {1} {2}", personData.FirstName, personData.MiddleName, personData.LastName);
            List<Correspondent> correspondents = this.correspondentRepository.GetCorrespondents(personNames, personData.Email, 10, 0);

            List<int> correspondentIds = correspondents.Select(c => c.CorrespondentId).ToList() ?? new List<int>();
            if (correspondentIds.Count() == 0)
            {
                CorrespondentDO correspondent = this.correspondentRepository.GetNewCorrespondent();
                if (personData.Country.Code == "BG")
                {
                    correspondent.BgCitizenFirstName = personData.FirstName;
                    correspondent.BgCitizenLastName = personData.LastName;
                    correspondent.BgCitizenUIN = personData.Uin;

                }
                else
                {
                    correspondent.ForeignerFirstName = personData.FirstName;
                    correspondent.ForeignerLastName = personData.LastName;
                }

                correspondent.Email = personData.Email;
                CorrespondentDO corr = this.correspondentRepository.CreateCorrespondent(correspondent, userContext);
                correspondentIds.Add(corr.CorrespondentId.Value);
            }

            return correspondentIds;
        }

        public List<int> GetCorrespondentIdFromCorrespondent(CorrespondentDO correspondent, UserContext userContext)
        {
            string displayName = "";
            if (correspondent.CorrespondentTypeAlias == "ForeignLegalEntity")
            {
                displayName = correspondent.FLegalEntityName;
            }
            else if (correspondent.CorrespondentTypeAlias == "LegalEntity")
            {
                displayName = correspondent.LegalEntityName;
            }
            else if (correspondent.CorrespondentTypeAlias == "Foreigner")
            {
                displayName = string.Format("{0} {1}", correspondent.ForeignerFirstName, correspondent.ForeignerLastName);
            }
            else
            {
                displayName = string.Format("{0} {1}", correspondent.BgCitizenFirstName, correspondent.BgCitizenLastName);
            }

            List<Correspondent> correspondents = this.correspondentRepository.GetCorrespondents(displayName, null, 10, 0);

            List<int> correspondentIds = correspondents.Select(c => c.CorrespondentId).ToList() ?? new List<int>();
            if (correspondentIds.Count() == 0)
            {
                CorrespondentDO newCorrespondent = this.correspondentRepository.CreateCorrespondent(correspondent, userContext);
                correspondentIds.Add(newCorrespondent.CorrespondentId.Value);
            }

            return correspondentIds;
        }
    }
}

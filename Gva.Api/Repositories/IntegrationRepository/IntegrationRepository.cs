﻿using System.Collections.Generic;
using System.Linq;
using Common.Api.Models;
using Common.Data;
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
using Gva.Api.ModelsDO.Organizations;

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

        public void UpdateLotCaseTypes(string set, GvaCaseType caseType, Lot lot, UserContext userContext)
        {
            NomValue caseTypeNom = new NomValue()
            {
                NomValueId = caseType.GvaCaseTypeId,
                Name = caseType.Name,
                Alias = caseType.Alias
            };

            Part updatedPart = null;
            int? partId = null;
            if (set == "Person")
            {
                PersonDataDO personData = lot.Index.GetPart<PersonDataDO>("personData").Content;
                personData.CaseTypes.Add(caseTypeNom);
                this.caseTypeRepository.AddCaseTypes(lot, personData.CaseTypes.Select(ct => ct.NomValueId));
                updatedPart = lot.UpdatePart("personData", personData, userContext).Part;
            }
            else if (set == "Organization")
            {
                OrganizationDataDO organizationData = lot.Index.GetPart<OrganizationDataDO>("organizationData").Content;
                organizationData.CaseTypes.Add(caseTypeNom);
                this.caseTypeRepository.AddCaseTypes(lot, organizationData.CaseTypes.Select(ct => ct.NomValueId));
                updatedPart = lot.UpdatePart("organizationData", organizationData, userContext).Part;
            }

            lot.Commit(userContext, this.lotEventDispatcher);

            this.unitOfWork.Save();

            this.lotRepository.ExecSpSetLotPartTokens(updatedPart.PartId);
        }

        public List<int> GetCorrespondentIdsPerPersonLot(PersonDataDO personData, UserContext userContext)
        {
            string personNames = string.Format("{0} {1}", personData.FirstName, personData.LastName);
            List<Correspondent> correspondents = this.correspondentRepository.GetCorrespondents(personNames, personData.Email, 10, 0);

            List<int> correspondentIds = correspondents.Select(c => c.CorrespondentId).ToList() ?? new List<int>();
            if (correspondentIds.Count() == 0)
            {
                CorrespondentDO correspondent = this.correspondentRepository.GetNewCorrespondent();

                var correspondentType = this.unitOfWork.DbContext.Set<CorrespondentType>()
                    .SingleOrDefault(e => e.Alias.ToLower() == "BulgarianCitizen".ToLower());

                correspondent.CorrespondentTypeAlias = correspondentType.Alias;
                correspondent.CorrespondentTypeId = correspondentType.CorrespondentTypeId;
                correspondent.CorrespondentTypeName = correspondentType.Name;

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

        public List<int> CreateCorrespondent(CorrespondentDO correspondent, UserContext userContext)
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

using System.Collections.Generic;
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
using R_0009_000015;
using R_4012;
using Gva.Api.Repositories.PersonRepository;
using Common.Api.Repositories.NomRepository;
using R_0009_000008;
using R_0009_000011;

namespace Gva.Api.Repositories.IntegrationRepository
{
    public class IntegrationRepository : IIntegrationRepository
    {
        private IUnitOfWork unitOfWork;
        private ICaseTypeRepository caseTypeRepository;
        private ILotRepository lotRepository;
        private ICorrespondentRepository correspondentRepository;
        private IPersonRepository personRepository;
        private INomRepository nomRepository;
        private ILotEventDispatcher lotEventDispatcher;

        public IntegrationRepository(
            IUnitOfWork unitOfWork,
            ICaseTypeRepository caseTypeRepository,
            ILotRepository lotRepository,
            ICorrespondentRepository correspondentRepository,
            IPersonRepository personRepository,
            INomRepository nomRepository,
            ILotEventDispatcher lotEventDispatcher)
        {
            this.unitOfWork = unitOfWork;
            this.caseTypeRepository = caseTypeRepository;
            this.lotRepository = lotRepository;
            this.correspondentRepository = correspondentRepository;
            this.personRepository = personRepository;
            this.nomRepository = nomRepository;
            this.lotEventDispatcher = lotEventDispatcher;
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

        public CorrespondentDO ConvertElServiceRecipientToCorrespondent(ElectronicServiceRecipient applicant)
        {
            CorrespondentGroup correspondentGroup = this.unitOfWork.DbContext.Set<CorrespondentGroup>()
                    .SingleOrDefault(e => e.Alias.ToLower() == "Applicants".ToLower());

            CorrespondentDO newCorrespondent = new CorrespondentDO()
            {
                CorrespondentGroupId = correspondentGroup.CorrespondentGroupId,
                IsActive = true
            };

            CorrespondentType correspondentType = null;

            if (applicant.Entity != null)
            {
                newCorrespondent.LegalEntityName = applicant.Entity.Name;
                newCorrespondent.LegalEntityBulstat = applicant.Entity.Identifier;
                correspondentType = this.unitOfWork.DbContext.Set<CorrespondentType>()
                    .SingleOrDefault(e => e.Alias.ToLower() == "LegalEntity".ToLower());
            }
            else if (applicant.ForeignEntity != null)
            {
                newCorrespondent.FLegalEntityName = applicant.ForeignEntity.ForeignEntityName;
                correspondentType = this.unitOfWork.DbContext.Set<CorrespondentType>()
                    .SingleOrDefault(e => e.Alias.ToLower() == "ForeignLegalEntity".ToLower());
            }
            else if (applicant.ForeignPerson != null)
            {
                newCorrespondent.ForeignerFirstName = applicant.ForeignPerson.Names.FirstCyrillic;
                newCorrespondent.ForeignerLastName = applicant.ForeignPerson.Names.LastCyrillic;
                correspondentType = this.unitOfWork.DbContext.Set<CorrespondentType>()
                    .SingleOrDefault(e => e.Alias.ToLower() == "Foreigner".ToLower());
            }
            else
            {
                newCorrespondent.BgCitizenFirstName = applicant.Person.Names.First;
                newCorrespondent.BgCitizenLastName = applicant.Person.Names.Last;
                newCorrespondent.BgCitizenUIN = applicant.Person.Identifier.EGN;
                correspondentType = this.unitOfWork.DbContext.Set<CorrespondentType>()
                    .SingleOrDefault(e => e.Alias.ToLower() == "BulgarianCitizen".ToLower());
            }

            newCorrespondent.CorrespondentTypeId = correspondentType != null ? correspondentType.CorrespondentTypeId : (int?)null;
            newCorrespondent.CorrespondentTypeAlias = correspondentType != null ? correspondentType.Alias : string.Empty;
            newCorrespondent.CorrespondentTypeName = correspondentType != null ? correspondentType.Name : string.Empty;

            newCorrespondent.SetupFlags();

            return newCorrespondent;
        }

        public PersonDataDO ConvertAppWithFlightCrewDataToPersonData(FlightCrewPersonalData FlightCrewlData, GvaCaseType caseType)
        {
            PersonDataDO personData = new PersonDataDO();

            if (!string.IsNullOrEmpty(FlightCrewlData.CAAPersonalIdentificationNumber))
            {
                int lin = 0;
                if (int.TryParse(FlightCrewlData.CAAPersonalIdentificationNumber, out lin))
                {
                    personData.Lin = lin;
                    personData.LinType = this.nomRepository.GetNomValues("linTypes").Where(l => l.Code == "none").Single();
                }
            }

            string countryName = FlightCrewlData.Citizenship.CountryName;
            string countryCode = FlightCrewlData.Citizenship.CountryGRAOCode;
            if (!string.IsNullOrEmpty(countryName))
            {
                personData.Country = this.nomRepository.GetNomValues("countries").Where(c => c.Name == countryName).FirstOrDefault();
            }

            if (countryCode == "BG")
            {
                personData.Country = this.nomRepository.GetNomValue("countries", "BG");
                personData.FirstName = FlightCrewlData.BulgarianCitizen.PersonNames.First;
                personData.MiddleName = FlightCrewlData.BulgarianCitizen.PersonNames.Middle;
                personData.LastName = FlightCrewlData.BulgarianCitizen.PersonNames.Last;
                personData.DateOfBirth = FlightCrewlData.BulgarianCitizen.BirthDate;
            }
            else
            {
                personData.FirstName = FlightCrewlData.ForeignCitizen.ForeignCitizenNames.FirstCyrillic;
                personData.LastName = FlightCrewlData.ForeignCitizen.ForeignCitizenNames.LastCyrillic;
                personData.DateOfBirth = FlightCrewlData.ForeignCitizen.BirthDate;
            }

            personData.FirstNameAlt = FlightCrewlData.PersonNamesLatin.PersonFirstNameLatin;
            personData.MiddleNameAlt = FlightCrewlData.PersonNamesLatin.PersonMiddleNameLatin;
            personData.LastNameAlt = FlightCrewlData.PersonNamesLatin.PersonLastNameLatin;

            personData.Email = FlightCrewlData.ContactData.EmailAddress;

            personData.CaseTypes = new List<NomValue>() {
                                        new NomValue()
                                        {
                                            NomValueId = caseType.GvaCaseTypeId,
                                            Name = caseType.Name,
                                            Alias = caseType.Alias
                                        }
                                    };

            return personData;
        }

        public PersonDataDO ConvertAppWithPersonAndForeignCitizenBasicDataToPersonData(
            string caaPersonIdentificator,
            string email,
            PersonBasicData personBasicData,
            ForeignCitizenBasicData foreignCitizenBasicData,
            GvaCaseType caseType)
        {
            PersonDataDO personData = new PersonDataDO();

            if (!string.IsNullOrEmpty(caaPersonIdentificator))
            {
                int lin = -1;
                if (int.TryParse(caaPersonIdentificator, out lin))
                {
                    personData.Lin = lin;
                    personData.LinType = this.nomRepository.GetNomValues("linTypes").Where(l => l.Code == "none").Single();
                }
            }

            if (personBasicData != null)
            {
                personData.Country = this.nomRepository.GetNomValue("countries", "BG");
                personData.FirstName = personBasicData.Names.First;
                personData.MiddleName = personBasicData.Names.Middle;
                personData.LastName = personBasicData.Names.Last;
                personData.FirstNameAlt = personBasicData.Names.First;
                personData.MiddleNameAlt = personBasicData.Names.Middle;
                personData.LastNameAlt = personBasicData.Names.Last;
                personData.Uin = personBasicData.Identifier.EGN;
            }
            else if (foreignCitizenBasicData != null)
            {
                personData.FirstName = foreignCitizenBasicData.Names.FirstCyrillic;
                personData.LastName = foreignCitizenBasicData.Names.LastCyrillic;
                personData.DateOfBirth = foreignCitizenBasicData.BirthDate;
                personData.FirstNameAlt = foreignCitizenBasicData.Names.FirstLatin;
                personData.LastNameAlt = foreignCitizenBasicData.Names.LastLatin;
                if (!string.IsNullOrEmpty(foreignCitizenBasicData.IdentityDocument.CountryCode))
                {
                    personData.Country = this.nomRepository.GetNomValues("countries").Where(c => c.Code == foreignCitizenBasicData.IdentityDocument.CountryCode).FirstOrDefault();
                }
            }

            personData.Email = email;

            personData.CaseTypes = new List<NomValue>() {
                                        new NomValue()
                                        {
                                            NomValueId = caseType.GvaCaseTypeId,
                                            Name = caseType.Name,
                                            Alias = caseType.Alias
                                        }
                                    };

            return personData;
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using Common.Api.Models;
using Common.Data;
using Common.Json;
using System.Data.Entity;
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
using Gva.Api.Models.Views.Person;

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

        public CorrespondentDO ConvertOrganizationDataToCorrespondent(OrganizationDataDO organizationData)
        {
            CorrespondentGroup correspondentGroup = this.unitOfWork.DbContext.Set<CorrespondentGroup>()
                    .SingleOrDefault(e => e.Alias.ToLower() == "Applicants".ToLower());

            var correspondentType = this.unitOfWork.DbContext.Set<CorrespondentType>()
                .SingleOrDefault(e => e.Alias.ToLower() == "LegalEntity".ToLower());

            return new CorrespondentDO()
            {
                CorrespondentGroupId = correspondentGroup.CorrespondentGroupId,
                IsActive = true,
                CorrespondentTypeId = correspondentType.CorrespondentTypeId,
                CorrespondentTypeName = correspondentType.Name,
                CorrespondentTypeAlias = correspondentType.Alias,
                LegalEntityName = organizationData.Name,
                LegalEntityBulstat = organizationData.Uin
            };
        }

        public CorrespondentDO ConvertPersonDataToCorrespondent(PersonDataDO personData)
        {
            CorrespondentDO correspondent = this.correspondentRepository.GetNewCorrespondent();
            CorrespondentType correspondentType = null;

            if (personData.Country.Code == "BG")
            {
                correspondent.BgCitizenFirstName = personData.FirstName;
                correspondent.BgCitizenLastName = personData.LastName;
                correspondent.BgCitizenUIN = personData.Uin;
                correspondentType = this.unitOfWork.DbContext.Set<CorrespondentType>()
                    .SingleOrDefault(e => e.Alias.ToLower() == "BulgarianCitizen".ToLower());
            }
            else
            {
                correspondent.ForeignerFirstName = personData.FirstName;
                correspondent.ForeignerLastName = personData.LastName;
                correspondent.ForeignerBirthDate = personData.DateOfBirth;

                correspondentType = this.unitOfWork.DbContext.Set<CorrespondentType>()
                    .SingleOrDefault(e => e.Alias.ToLower() == "Foreigner".ToLower());
                
                Country country = this.unitOfWork.DbContext.Set<Country>()
                    .Where(s => s.Code == personData.Country.Code)
                    .FirstOrDefault();

                correspondent.ForeignerCountryId = country != null ? country.CountryId : (int?)null;
            }

            correspondent.CorrespondentTypeAlias = correspondentType.Alias;
            correspondent.CorrespondentTypeId = correspondentType.CorrespondentTypeId;
            correspondent.CorrespondentTypeName = correspondentType.Name;

            correspondent.ContactPhone = personData.Phone1;
            correspondent.Email = personData.Email;
            correspondent.ContactFax = personData.Fax;

            GvaViewPerson person = this.personRepository.GetPersons(
                lin: personData.Lin,
                uin: personData.Uin,
                names: string.Format("{0} {1}", personData.FirstName, personData.LastName))
                .FirstOrDefault();

            if (person != null)
            {
                int lotId = person.LotId;
                var addressPart = this.lotRepository.GetLotIndex(lotId).Index.GetParts<PersonAddressDO>("personAddresses")
                    .OrderByDescending(a => a.CreateDate)
                    .Where(a => a.Content.Valid.Code == "Y")
                    .FirstOrDefault();
                if (addressPart != null)
                {
                    correspondent.ContactAddress = addressPart.Content.Address;
                    correspondent.ContactPostCode = addressPart.Content.PostalCode;

                    Dictionary<string, string> settlementTypes = new Dictionary<string, string>(){
                    {"T", "гр."},
                    {"V", "с."},
                    {"М", "ман."},
                };

                    NomValue settlement = this.nomRepository.GetNomValue("cities", addressPart.Content.Settlement.NomValueId);
                    string type = settlement.TextContent.Get<string>("type");
                    var settlementType = settlementTypes.ContainsKey(type) ? settlementTypes[type] : "";

                    Settlement settlementResult = this.unitOfWork.DbContext.Set<Settlement>()
                        .Where(s => s.SettlementName == addressPart.Content.Settlement.Name && s.TypeName == settlementType)
                        .FirstOrDefault();

                    correspondent.ContactSettlementId = settlementResult != null ? settlementResult.SettlementId : (int?)null;
                }
            }

            return correspondent;
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

using System.Collections.Generic;
using System.Linq;
using Common.Api.Repositories.NomRepository;
using Common.Json;
using Gva.Api.ModelsDO.Persons;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.WordTemplates
{
    public class CoordinatorLicence : IDataGenerator
    {
        private ILotRepository lotRepository;
        private INomRepository nomRepository;

        public CoordinatorLicence(
            ILotRepository lotRepository,
            INomRepository nomRepository)
        {
            this.lotRepository = lotRepository;
            this.nomRepository = nomRepository;
        }

        public string[] TemplateNames
        {
            get
            {
                return new string[] { "coordinator" };
            }
        }

        public object GetData(int lotId, string path)
        {
            var lot = this.lotRepository.GetLotIndex(lotId);
            var personData = lot.Index.GetPart<PersonDataDO>("personData").Content;
            var personAddressPart = lot.Index.GetParts<PersonAddressDO>("personAddresses")
                .FirstOrDefault(a => a.Content.Valid.Code == "Y");
            var personAddress = personAddressPart == null ?
                new PersonAddressDO() :
                personAddressPart.Content;

            var licencePart = lot.Index.GetPart<PersonLicenceDO>(path);
            var licence = licencePart.Content;
            var editions = lot.Index.GetParts<PersonLicenceEditionDO>("licenceEditions")
                .Where(e => e.Content.LicencePartIndex == licencePart.Part.Index)
                .OrderBy(e => e.Content.Index)
                .Select(e => e.Content);

            var firstEdition = editions.First();
            var lastEdition = editions.Last();

            var includedRatings = lastEdition.IncludedRatings
                .Select(i => lot.Index.GetPart<PersonRatingDO>("ratings/" + i).Content);
            var includedTrainings = lastEdition.IncludedTrainings
                .Select(i => lot.Index.GetPart<PersonTrainingDO>("personDocumentTrainings/" + i).Content);

            var licenceType = this.nomRepository.GetNomValue("licenceTypes", licence.LicenceType.NomValueId);
            var licenceCaCode = licenceType.TextContent.Get<string>("codeCA");
            var licenceNumber = string.Format(
                "BG {0} - {1} - {2}",
                licenceType.Code,
                licence.LicenceNumber,
                personData.Lin);

            var documents = this.GetDocuments(licenceType.Code, includedTrainings);
            var endorsements2 = this.GetEndorsements2(includedRatings);

            var json = new
            {
                root = new
                {
                    L_NAME = licenceType.Name.ToUpper(),
                    L_NAME_TRANS = licenceType.NameAlt == null ? string.Empty : licenceType.NameAlt.ToUpper(),
                    L_LICENCE_TYPE_CA_CODE = licenceCaCode,
                    L_NAME1 = licenceType.Name.ToUpper(),
                    L_NAME1_TRANS = licenceType.NameAlt == null ? string.Empty : licenceType.NameAlt.ToUpper(),
                    L_LICENCE_TYPE_CA_CODE2 = licenceCaCode,
                    L_LICENCE_NO = licenceNumber,
                    L_LICENCE_HOLDER = this.GetPersonData(personData, personAddress),
                    L_LICENCE_PRIV = this.GetLicencePrivileges(),
                    L_FIRST_ISSUE_DATE = firstEdition.DocumentDateValidFrom,
                    L_ISSUE_DATE = lastEdition.DocumentDateValidFrom,
                    ENDORSEMENT = this.GetEndorsements(includedRatings),
                    T_LICENCE_HOLDER = this.GetLicenceHolder(personData, personAddress),
                    T_LICENCE_CODE = licenceCaCode,
                    T_LICENCE_NO = licenceNumber,
                    T_FIRST_ISSUE_DATE = firstEdition.DocumentDateValidFrom,
                    T_ACTION = lastEdition.LicenceAction.Name.ToUpper(),
                    T_ISSUE_DATE = lastEdition.DocumentDateValidFrom,
                    T_DOCUMENTS = documents.Take(documents.Length / 2),
                    T_DOCUMENTS2 = documents.Skip(documents.Length / 2),
                    T_ENDORSEMENT = endorsements2,
                    L_ENDORSEMENT = endorsements2,
                    L_ABBREVIATION = this.GetAbbreviations()
                }
            };

            return json;
        }

        private object GetPersonData(PersonDataDO personData, PersonAddressDO personAddress)
        {
            var placeOfBirth = personData.PlaceOfBirth;
            var country = this.nomRepository.GetNomValue("countries", placeOfBirth.ParentValueId.Value);
            var nationality = this.nomRepository.GetNomValue("countries", personData.Country.NomValueId);

            return new
            {
                FAMILY_BG = personData.LastName.ToUpper(),
                FAMILY_TRANS = personData.LastNameAlt.ToUpper(),
                FIRST_NAME_BG = personData.FirstName.ToUpper(),
                FIRST_NAME_TRANS = personData.FirstNameAlt.ToUpper(),
                SURNAME_BG = personData.MiddleName.ToUpper(),
                SURNAME_TRANS = personData.MiddleNameAlt.ToUpper(),
                DATE_PLACE_OF_BIRTH = new
                {
                    DATE_OF_BIRTH = personData.DateOfBirth,
                    PLACE_OF_BIRTH = new
                    {
                        COUNTRY_NAME = country.Name,
                        TOWN_VILLAGE_NAME = placeOfBirth.Name
                    },
                    PLACE_OF_BIRTH_TRANS = new
                    {
                        COUNTRY_NAME = country.NameAlt,
                        TOWN_VILLAGE_NAME = placeOfBirth.NameAlt
                    }
                },
                ADDRESS = string.Format(
                    "{0}, {1}",
                    personAddress.Settlement.Name,
                    personAddress.Address),
                ADDRESS_TRANS = string.Format(
                    "{0}, {1}",
                    personAddress.AddressAlt,
                    personAddress.Settlement.NameAlt),
                NATIONALITY = new
                {
                    COUNTRY_NAME_BG = nationality.Name,
                    COUNTRY_CODE = nationality.TextContent.Get<string>("nationalityCodeCA")
                }
            };
        }

        private List<object> GetLicencePrivileges()
        {
            return new List<object>()
            {
                LicenceDictionary.LicencePrivilege["CATML"],
                LicenceDictionary.LicencePrivilege["idDoc3"]
            };
        }

        private List<object> GetEndorsements(IEnumerable<PersonRatingDO> includedRatings)
        {
            return includedRatings
                .Where(r => r.Authorization != null)
                .GroupBy(r => r.Authorization.Name)
                .Select(g => new
                    {
                        NAME = g.Key,
                        DATE = g.Min(r => r.Editions.Last().DocumentDateValidFrom)
                    }).ToList<object>();
        }

        private object GetLicenceHolder(PersonDataDO personData, PersonAddressDO personAddress)
        {
            return new
            {
                NAME = string.Format(
                    "{0} {1} {2}",
                    personData.FirstName,
                    personData.MiddleName,
                    personData.LastName).ToUpper(),
                LIN = personData.Lin,
                EGN = personData.Uin,
                ADDRESS = string.Format(
                    "{0}, {1}",
                    personAddress.Settlement.Name,
                    personAddress.Address),
                TELEPHONE = personData.Phone1 ??
                            personData.Phone2 ??
                            personData.Phone3 ??
                            personData.Phone4 ??
                            personData.Phone5
            };
        }

        private object[] GetDocuments(string licenceTypeCode, IEnumerable<PersonTrainingDO> includedTrainings)
        {
            string[] documentRoleCodes;
            bool hasRoles = LicenceDictionary.LicenceRole.TryGetValue(licenceTypeCode, out documentRoleCodes);

            if (!hasRoles)
            {
                return new object[0];
            }

            return includedTrainings
                .Where(t => t.Valid.Code == "Y" && documentRoleCodes.Contains(t.DocumentRole.Code))
                .OrderBy(t => t.DocumentDateValidFrom)
                .Select((t, i) =>
                    new
                    {
                        DOC = new
                        {
                            DOC_ROLE = string.Format("{0}. {1}", i + 1, t.DocumentRole.Name),
                            SUB_DOC = new
                            {
                                DOC_TYPE = t.DocumentType.Name,
                                DOC_NO = t.DocumentNumber,
                                DATE = t.DocumentDateValidFrom,
                                DOC_PUBLISHER = t.DocumentPublisher
                            }
                        }
                    }).ToArray<object>();
        }

        private List<object> GetEndorsements2(IEnumerable<PersonRatingDO> includedRatings)
        {
            return includedRatings
                .Where(r => r.StaffType.Alias == "ovd")
                .Select(r =>
                {
                    {
                        PersonRatingEditionDO lastEdition = r.Editions.Last();

                        return new
                        {
                            ICAO = r.LocationIndicator == null ? null : r.LocationIndicator.Name,
                            SECTOR = r.Sector,
                            AUTH = r.Authorization == null ? null : r.Authorization.Name,
                            ISSUE_DATE = lastEdition.DocumentDateValidFrom,
                            VALID_DATE = lastEdition.DocumentDateValidTo
                        };
                    }
                }).ToList<object>();
        }

        private List<object> GetAbbreviations()
        {
            return new List<object>()
            {
                LicenceDictionary.LicenceAbbreviation["AFIS"],
                LicenceDictionary.LicenceAbbreviation["ASM"],
                LicenceDictionary.LicenceAbbreviation["ATFM"],
                LicenceDictionary.LicenceAbbreviation["FDA"],
                LicenceDictionary.LicenceAbbreviation["FIS"],
                LicenceDictionary.LicenceAbbreviation["OJTI"],
                LicenceDictionary.LicenceAbbreviation["SAR"],
                LicenceDictionary.LicenceAbbreviation["SIMI"]
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Json;
using System.Threading.Tasks;
using Common.Api.Repositories.NomRepository;
using Newtonsoft.Json.Linq;
using Regs.Api.Repositories.LotRepositories;
using Common.Api.Models;

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

        public JObject GetData(int lotId, string path, int index)
        {
            var lot = this.lotRepository.GetLotIndex(lotId);
            var personData = lot.GetPart("personData").Content;
            var personAddressPart = lot.GetParts("personAddresses")
                .FirstOrDefault(a => a.Content.Get<string>("valid.code") == "Y");
            var personAddress = personAddressPart == null ?
                new JObject() :
                personAddressPart.Content;
            var licence = lot.GetPart(path).Content;
            var edition = licence.Get<JObject>(string.Format("editions[{0}]", index));
            var firstEdition = licence.Get<JObject>("editions[0]");

            var includedRatings = edition.GetItems<int>("includedRatings")
                .Select(i => lot.GetPart("ratings/" + i).Content);
            var includedTrainings = edition.GetItems<int>("includedTrainings")
                .Select(i => lot.GetPart("personDocumentTrainings/" + i).Content);

            var licenceType = this.nomRepository.GetNomValue("licenceTypes", licence.Get<int>("licenceType.nomValueId"));
            var licenceCaCode = licenceType.TextContent.Get<string>("codeCA");
            var licenceNumber = string.Format(
                "BG {0} - {1} - {2}",
                licenceType.Code,
                licence.Get<string>("licenceNumber"),
                personData.Get<string>("lin"));

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
                    L_FIRST_ISSUE_DATE = firstEdition.Get<DateTime>("documentDateValidFrom"),
                    L_ISSUE_DATE = edition.Get<DateTime>("documentDateValidFrom"),
                    ENDORSEMENT = this.GetEndorsements(includedRatings),
                    T_LICENCE_HOLDER = this.GetLicenceHolder(personData, personAddress),
                    T_LICENCE_CODE = licenceCaCode,
                    T_LICENCE_NO = licenceNumber,
                    T_FIRST_ISSUE_DATE = firstEdition.Get<DateTime>("documentDateValidFrom"),
                    T_ACTION = edition.Get<string>("licenceAction.name").ToUpper(),
                    T_ISSUE_DATE = edition.Get<DateTime>("documentDateValidFrom"),
                    T_DOCUMENTS = documents.Take(documents.Length / 2),
                    T_DOCUMENTS2 = documents.Skip(documents.Length / 2),
                    T_ENDORSEMENT = endorsements2,
                    L_ENDORSEMENT = endorsements2,
                    L_ABBREVIATION = this.GetAbbreviations()
                }
            };

            return JObject.FromObject(json);
        }

        private object GetPersonData(JObject personData, JObject personAddress)
        {
            var placeOfBirth = personData.Get<NomValue>("placeOfBirth");
            var country = this.nomRepository.GetNomValue("countries", placeOfBirth.ParentValueId.Value);
            var nationality = this.nomRepository.GetNomValue("countries", personData.Get<int>("country.nomValueId"));

            return new
            {
                FAMILY_BG = personData.Get<string>("lastName").ToUpper(),
                FAMILY_TRANS = personData.Get<string>("lastNameAlt").ToUpper(),
                FIRST_NAME_BG = personData.Get<string>("firstName").ToUpper(),
                FIRST_NAME_TRANS = personData.Get<string>("firstNameAlt").ToUpper(),
                SURNAME_BG = personData.Get<string>("middleName").ToUpper(),
                SURNAME_TRANS = personData.Get<string>("middleNameAlt").ToUpper(),
                DATE_PLACE_OF_BIRTH = new
                {
                    DATE_OF_BIRTH = personData.Get<DateTime>("dateOfBirth"),
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
                    personAddress.Get<string>("settlement.name"),
                    personAddress.Get<string>("address")),
                ADDRESS_TRANS = string.Format(
                    "{0}, {1}",
                    personAddress.Get<string>("addressAlt"),
                    personAddress.Get<string>("settlement.nameAlt")),
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

        private List<object> GetEndorsements(IEnumerable<JObject> includedRatings)
        {
            return includedRatings
                .Where(r => r.Get<NomValue>("authorization") != null)
                .GroupBy(r => r.Get<string>("authorization.name"))
                .Select(g =>
                {
                    return new
                    {
                        NAME = g.Key,
                        DATE = g.Min(r => r.GetItems<JObject>("editions").Last().Get<DateTime>("documentDateValidFrom"))
                    };
                }).ToList<object>();
        }

        private object GetLicenceHolder(JObject personData, JObject personAddress)
        {
            return new
            {
                NAME = string.Format(
                    "{0} {1} {2}",
                    personData.Get<string>("firstName"),
                    personData.Get<string>("middleName"),
                    personData.Get<string>("lastName")).ToUpper(),
                LIN = personData.Get<string>("lin"),
                EGN = personData.Get<string>("uin"),
                ADDRESS = string.Format(
                    "{0}, {1}",
                    personAddress.Get<string>("settlement.name"),
                    personAddress.Get<string>("address")),
                TELEPHONE = personData.Get<string>("phone1") ??
                            personData.Get<string>("phone2") ??
                            personData.Get<string>("phone3") ??
                            personData.Get<string>("phone4") ??
                            personData.Get<string>("phone5")
            };
        }

        private object[] GetDocuments(string licenceTypeCode, IEnumerable<JObject> includedTrainings)
        {
            string[] documentRoleCodes;
            bool hasRoles = LicenceDictionary.LicenceRole.TryGetValue(licenceTypeCode, out documentRoleCodes);

            if (!hasRoles)
            {
                return new object[0];
            }

            return includedTrainings
                .Where(t => t.Get<string>("valid.code") == "Y" && documentRoleCodes.Contains(t.Get<string>("documentRole.code")))
                .OrderBy(t => t.Get<DateTime>("documentDateValidFrom"))
                .Select((t, i) =>
                    new
                    {
                        DOC = new
                        {
                            DOC_ROLE = string.Format("{0}. {1}", i + 1, t.Get<string>("documentRole.name")),
                            SUB_DOC = new
                            {
                                DOC_TYPE = t.Get<string>("documentType.name"),
                                DOC_NO = t.Get<string>("documentNumber"),
                                DATE = t.Get<DateTime>("documentDateValidFrom"),
                                DOC_PUBLISHER = t.Get<string>("documentPublisher")
                            }
                        }
                    }).ToArray<object>();
        }

        private List<object> GetEndorsements2(IEnumerable<JObject> includedRatings)
        {
            return includedRatings
                .Where(r => r.Get<string>("staffType.alias") == "ovd")
                .Select(r =>
                {
                    {
                        JObject lastEdition = r.GetItems<JObject>("editions").Last();
                        var ratingType = r.Get<string>("ratingType.name");
                        var ratingClass = r.Get<string>("ratingClass.name");
                        var authorization = r.Get<string>("authorization.name");

                        return new
                        {
                            ICAO = r.Get<string>("locationIndicator.name"),
                            SECTOR = r.Get<string>("sector"),
                            AUTH = authorization,
                            ISSUE_DATE = lastEdition.Get<DateTime>("documentDateValidFrom"),
                            VALID_DATE = lastEdition.Get<DateTime>("documentDateValidTo")
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

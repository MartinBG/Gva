using System;
using System.Collections.Generic;
using System.Linq;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.WordTemplates
{
    public class CoordinatorSimi : IDataGenerator
    {
        private ILotRepository lotRepository;
        private INomRepository nomRepository;

        public CoordinatorSimi(
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
                return new string[] { "coordinator_simi" };
            }
        }

        public object GetData(int lotId, string path)
        {
            var lot = this.lotRepository.GetLotIndex(lotId);
            var personData = lot.Index.GetPart("personData").Content;
            var personAddressPart = lot.Index.GetParts("personAddresses")
               .FirstOrDefault(a => a.Content.Get<string>("valid.code") == "Y");
            var personAddress = personAddressPart == null ?
                new JObject() :
                personAddressPart.Content;

            var licencePart = lot.Index.GetPart(path);
            var licence = licencePart.Content;
            var editions = lot.Index.GetParts("licenceEditions")
                .Where(e => e.Content.Get<int>("licencePartIndex") == licencePart.Part.Index)
                .OrderBy(e => e.Content.Get<int>("index"))
                .Select(e => e.Content);

            var firstEdition = editions.First();
            var lastEdition = editions.Last();

            var licenceType = this.nomRepository.GetNomValue("licenceTypes", licence.Get<int>("licenceType.nomValueId"));

            var includedTrainings = lastEdition.GetItems<int>("includedTrainings")
                .Select(i => lot.Index.GetPart("personDocumentTrainings/" + i).Content);
            var includedRatings = lastEdition.GetItems<int>("includedRatings")
                .Select(i => lot.Index.GetPart("ratings/" + i).Content);
            var includedExams = lastEdition.GetItems<int>("includedExams")
                .Select(i => lot.Index.GetPart("personDocumentExams/" + i).Content);
            var documents = this.GetDocuments(licenceType.Code, includedTrainings, includedExams);
            var licenceCodeCa = licenceType.TextContent.Get<string>("codeCA");

            IEnumerable<JObject> engLevels = includedTrainings.Where(t => t.Get<string>("documentRole.alias") == "engTraining");

            dynamic lLangLevel = null;
            dynamic tLangLevel = null;
            if (engLevels.Count() > 0)
            {
                lLangLevel = engLevels.Select(l => new
                {
                    LEVEL = l.Get<string>("engLangLevel.name"),
                    VALID_DATE = l.Get<DateTime>("documentDateValidTo")
                });

                 tLangLevel = engLevels.Select(l => new
                 {
                    LEVEL = l.Get<string>("engLangLevel.name"),
                    ISSUE_DATE = l.Get<DateTime>("documentDateValidFrom"),
                    VALID_DATE = l.Get<DateTime>("documentDateValidTo")
                });
            }

            var endorsements = includedRatings
                .Select(r =>
                    new {
                        NAME = r.Get<string>("authorization.code"),
                        DATE = r.GetItems<JObject>("editions").First().Get<DateTime>("documentDateValidFrom")
                    });
            var tEndorsements = includedRatings
                .Select(r =>
                    new {
                        ICAO = r.Get<string>("locationIndicator.code"),
                        AUTH = r.Get<string>("authorization.code"),
                        SECTOR = r.Get<string>("sector"),
                        ISSUE_DATE = r.GetItems<JObject>("editions").Last().Get<DateTime>("documentDateValidFrom")
                    });
            var lEndorsements = includedRatings
                .Select(r =>
                    new
                    {
                        ICAO = r.Get<string>("locationIndicator.code"),
                        AUTH = r.Get<string>("authorization.code"),
                        SECTOR = r.Get<string>("sector"),
                        VALID_DATE = r.GetItems<JObject>("editions").Last().Get<DateTime>("documentDateValidTo")
                    });
            var licenceNumber = string.Format(
                "BG {0} - {1} - {2}",
                licenceType.Code,
                licence.Get<string>("licenceNumber"),
                personData.Get<string>("lin"));

            List<dynamic> licencePrivileges = this.GetLicencePrivileges();

            var json = new
            {
                root = new
                {
                    L_NAME = licenceType.Name,
                    L_NAME_TRANS = licenceType.NameAlt,
                    L_LICENCE_TYPE_CA_CODE = licenceCodeCa,
                    L_NAME1 = licenceType.Name,
                    L_NAME1_TRANS = licenceType.NameAlt,
                    L_LICENCE_TYPE_CA_CODE2 = licenceCodeCa,
                    L_LICENCE_NO = licenceNumber,
                    L_LICENCE_HOLDER = this.GetPersonData(personData, personAddress),
                    L_LICENCE_PRIV = licencePrivileges.Select(p => p.NAME_BG),
                    L_LICENCE_PRIV_TRANS = licencePrivileges.Select(p => p.NAME_TRANS),
                    ENDORSEMENT = endorsements,
                    L_ENDORSEMENT = lEndorsements,
                    L_LANG_LEVEL = lLangLevel,
                    L_FIRST_ISSUE_DATE = firstEdition.Get<DateTime>("documentDateValidFrom"),
                    L_ISSUE_DATE = lastEdition.Get<DateTime>("documentDateValidFrom"),
                    T_ENDORSEMENT = tEndorsements,
                    T_LICENCE_HOLDER = this.GetLicenceHolder(personData, personAddress),
                    T_LICENCE_NO = licenceNumber,
                    T_LICENCE_CODE = licenceType.Code,
                    T_ACTION = firstEdition.Get<string>("licenceAction.name").ToUpper(),
                    T_FIRST_ISSUE_DATE = firstEdition.Get<DateTime>("documentDateValidFrom"),
                    T_ISSUE_DATE = lastEdition.Get<DateTime>("documentDateValidFrom"),
                    T_LANG_LEVEL = tLangLevel,
                    T_DOCUMENTS = documents.Take(documents.Length / 2),
                    T_DOCUMENTS2 = documents.Skip(documents.Length / 2),
                    L_ABBREVIATION = this.GetAbbreviations()
                }
            };

            return json;
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
                ADDRESS = personAddress.Get<string>("address"),
                ADDRESS_TRANS = personAddress.Get<string>("addressAlt"),
                NATIONALITY = new
                {
                    COUNTRY_NAME_BG = nationality.Name,
                    COUNTRY_CODE = nationality.TextContent.Get<string>("nationalityCodeCA")
                }
            };
        }

        private object GetLicenceHolder(JObject personData, JObject personAddress)
        {
            return new
            {
                NAME = string.Format(
                    "{0} {1} {2}",
                    personData.Get<string>("firstName"),
                    personData.Get<string>("middleName"),
                    personData.Get<string>("lastName")),
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

        private object[] GetDocuments(string licenceTypeCode, IEnumerable<JObject> includedTrainings, IEnumerable<JObject> includedExams)
        {
            string[] documentRoleCodes;
            bool hasRoles = LicenceDictionary.LicenceRole.TryGetValue(licenceTypeCode, out documentRoleCodes);

            if (!hasRoles)
            {
                return new object[0];
            }

            var trainings = includedTrainings
                .Where(e => e.Get<string>("valid.code") == "Y")
                .Select(t =>
                    new
                    {
                        DOC = new
                        {
                            DOC_ROLE = t.Get<string>("documentRole.name"),
                            SUB_DOC = new
                            {
                                CLASS = t.Get<string>("documentType.name"),
                                DOC_NO = t.Get<string>("documentNumber"),
                                DATE = t.Get<DateTime>("documentDateValidFrom")
                            }
                        }
                    }).ToArray<dynamic>();

            var exams = includedExams
                .Where(e => e.Get<string>("valid.code") == "Y")
                .Select(e =>
                    new
                    {
                        DOC = new
                        {
                            DOC_ROLE = e.Get<string>("documentRole.name"),
                            SUB_DOC = new
                            {
                                CLASS = e.Get<string>("documentType.name"),
                                DOC_NO = e.Get<string>("documentNumber"),
                                DATE = e.Get<DateTime>("documentDateValidFrom")
                            }
                        }
                    }).ToArray<dynamic>();

            return trainings
                .Union(exams)
                .OrderBy(d => d.DOC.SUB_DOC.DATE)
                .ToArray<object>();
        }

        private IEnumerable<object> GetAbbreviations()
        {
            return new[]
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

        private List<object> GetLicencePrivileges()
        {
            return new List<object>()
            {
                LicenceDictionary.LicencePrivilege["coordinatorSimi1"],
                LicenceDictionary.LicencePrivilege["coordinatorSimi2"],
                LicenceDictionary.LicencePrivilege["coordinatorSimi3"],
                LicenceDictionary.LicencePrivilege["coordinatorSimi4"],
                LicenceDictionary.LicencePrivilege["coordinatorSimi5"]
            };
        }
    }
}

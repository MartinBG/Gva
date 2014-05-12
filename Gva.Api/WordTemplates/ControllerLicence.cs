using System;
using System.Collections.Generic;
using System.Linq;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Json;
using Newtonsoft.Json.Linq;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.WordTemplates
{
    public class ControllerLicence : IDataGenerator
    {
        private static Dictionary<string, List<object>> licencePrivileges = new Dictionary<string, List<object>>
        {
            {
                "ATCL",
                new List<object>()
                {
                    LicenceDictionary.LicencePrivilege["ATCLratings"]
                }
            },
            {
                "SATCL",
                new List<object>()
                {
                    LicenceDictionary.LicencePrivilege["medCertClass3"],
                    LicenceDictionary.LicencePrivilege["ATCLstudent"]
                }
            }
        };

        private static Dictionary<string, List<object>> licenceAbbreviations = new Dictionary<string, List<object>>
        {
            {
                "ATCL",
                new List<object>()
                {
                    LicenceDictionary.LicenceAbbreviation["AFIS"],
                    LicenceDictionary.LicenceAbbreviation["ASM"],
                    LicenceDictionary.LicenceAbbreviation["ATFM"],
                    LicenceDictionary.LicenceAbbreviation["FDA"],
                    LicenceDictionary.LicenceAbbreviation["FIS"],
                    LicenceDictionary.LicenceAbbreviation["OJTI"],
                    LicenceDictionary.LicenceAbbreviation["SAR"],
                    LicenceDictionary.LicenceAbbreviation["SIMI"]
                }
            },
            {
                "SATCL",
                new List<object>()
                {
                    LicenceDictionary.LicenceAbbreviation["ACS"],
                    LicenceDictionary.LicenceAbbreviation["ADI"]
                }
            }
        };

        private ILotRepository lotRepository;
        private INomRepository nomRepository;
        private int number;

        public ControllerLicence(
            ILotRepository lotRepository,
            INomRepository nomRepository)
        {
            this.lotRepository = lotRepository;
            this.nomRepository = nomRepository;
            this.number = 1;
        }

        public string[] TemplateNames
        {
            get
            {
                return new string[] { "atcl1" };
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
            var includedMedicals = edition.GetItems<int>("includedMedicals")
                .Select(i => lot.GetPart("personDocumentMedicals/" + i).Content);

            var licenceType = this.nomRepository.GetNomValue("licenceTypes", licence.Get<int>("licenceType.nomValueId"));
            var licenceCaCode = JObject.Parse(licenceType.TextContent).Get<string>("codeCA");
            var licenceNumber = string.Format(
                "BG {0} - {1} - {2}",
                licenceType.Code,
                licence.Get<string>("licenceNumber"),
                personData.Get<string>("lin"));
            var placeOfBirth = personData.Get<NomValue>("placeOfBirth");
            var country = this.nomRepository.GetNomValue("countries", placeOfBirth.ParentValueId.Value);
            var nationality = this.nomRepository.GetNomValue("countries", personData.Get<int>("country.nomValueId"));
            var address = string.Format(
                "{0}, {1}",
                personAddress.Get<string>("settlement.name"),
                personAddress.Get<string>("address"));

            var documents = this.GetDocuments(licenceType.Code, includedTrainings);
            var langLevel = this.GetEngLevel(includedTrainings);
            var endorsements2 = this.GetEndorsements2(includedRatings);
            var abbreviations = this.GetAbbreviations(licenceType.Code);

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
                    FAMILY_BG = personData.Get<string>("lastName").ToUpper(),
                    FAMILY_TRANS = personData.Get<string>("lastNameAlt").ToUpper(),
                    FIRST_NAME_BG = personData.Get<string>("firstName").ToUpper(),
                    FIRST_NAME_TRANS = personData.Get<string>("firstNameAlt").ToUpper(),
                    SURNAME_BG = personData.Get<string>("middleName").ToUpper(),
                    SURNAME_TRANS = personData.Get<string>("middleNameAlt").ToUpper(),
                    DATE_OF_BIRTH = personData.Get<DateTime>("dateOfBirth"),
                    COUNTRY = country.Name,
                    CITY = placeOfBirth.Name,
                    COUNTRY_EN = country.NameAlt,
                    CITY_EN = placeOfBirth.NameAlt,
                    ADDRESS = address,
                    ADDRESS_EN = string.Format(
                        "{0}, {1}",
                        personAddress.Get<string>("addressAlt"),
                        personAddress.Get<string>("settlement.nameAlt")),
                    NATIONALITY = nationality.Name,
                    NATIONALITY_EN = JObject.Parse(nationality.TextContent).Get<string>("nationalityCodeCA"),
                    L_LICENCE_PRIV = this.GetLicencePrivileges(licenceType.Code),
                    L_RATINGS = this.GetRatings(includedRatings),
                    ENDORSEMENT = this.GetEndorsements(includedRatings),
                    L_LANG_LEVEL = langLevel,
                    L_FIRST_ISSUE_DATE = firstEdition.Get<DateTime>("documentDateValidFrom"),
                    L_ISSUE_DATE = edition.Get<DateTime>("documentDateValidFrom"),
                    NAME = string.Format(
                        "{0} {1} {2}",
                        personData.Get<string>("firstName"),
                        personData.Get<string>("middleName"),
                        personData.Get<string>("lastName")).ToUpper(),
                    LIN = personData.Get<string>("lin"),
                    EGN = personData.Get<string>("uin"),
                    ADDRESS1 = address,
                    TELEPHONE = personData.Get<string>("phone1") ??
                        personData.Get<string>("phone2") ??
                        personData.Get<string>("phone3") ??
                        personData.Get<string>("phone4") ??
                        personData.Get<string>("phone5"),
                    T_LICENCE_CODE = " РП ",
                    T_LICENCE_NO = licenceNumber,
                    T_FIRST_ISSUE_DATE = firstEdition.Get<DateTime>("documentDateValidFrom"),
                    T_ACTION = edition.Get<string>("licenceAction.name").ToUpper(),
                    T_ISSUE_DATE = edition.Get<DateTime>("documentDateValidFrom"),
                    T_DOCUMENTS = documents.Take(documents.Length / 2),
                    T_DOCUMENTS2 = documents.Skip(documents.Length / 2),
                    T_LANG_LEVEL_NO = number++,
                    T_LANG_LEVEL = langLevel,
                    T_MED_CERT = this.GetMedCerts(includedMedicals, personData),
                    T_ACTIVE_NO = number++,
                    T_ENDORSEMENT = endorsements2,
                    L_ENDORSEMENT = endorsements2,
                    L_ENDORSEMENT1 = endorsements2,
                    L_ABBREVIATION1 = abbreviations,
                    L_ABBREVIATION2 = abbreviations,
                    L_ABBREVIATION = abbreviations
                }
            };

            return JObject.FromObject(json);
        }

        private List<object> GetLicencePrivileges(string licenceTypeCode)
        {
            List<dynamic> privileges;
            if (licencePrivileges.TryGetValue(licenceTypeCode, out privileges))
            {
                return privileges
                    .OrderBy(p => p.NO)
                    .ToList<object>();
            }

            return new object[0].ToList();
        }

        private List<object> GetRatings(IEnumerable<JObject> includedRatings)
        {
            return includedRatings
                .Where(r => r.Get<NomValue>("ratingClass") != null || r.Get<NomValue>("ratingType") != null)
                .GroupBy(r => string.Format(
                    "{0} {1}",
                    r.Get<string>("ratingClass.name"),
                    r.Get<string>("ratingType.name")).Trim())
                .Select(g =>
                {
                    return new
                    {
                        NAME = g.Key,
                        DATE = g.Min(r => r.GetItems<JObject>("editions").Last().Get<DateTime>("documentDateValidFrom"))
                    };
                }).ToList<object>();
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

        private List<object> GetEndorsements2(IEnumerable<JObject> includedRatings)
        {
            return includedRatings
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
                            AUTH = ratingClass == null && ratingType == null ?
                                authorization :
                                string.Format(
                                    "{0} {1} {2}",
                                    ratingType,
                                    ratingClass,
                                    authorization == null ? string.Empty : " - " + authorization).Trim(),
                            ISSUE_DATE = lastEdition.Get<DateTime>("documentDateValidFrom"),
                            VALID_DATE = lastEdition.Get<DateTime>("documentDateValidTo")
                        };
                    }
                }).ToList<object>();
        }

        private object GetEngLevel(IEnumerable<JObject> includedTrainings)
        {
            var engTrainings = includedTrainings
                .Where(t => t.Get<string>("documentRole.alias") == "engTraining");

            JObject result = new JObject();
            int currentSeqNumber = 0;
            foreach (var engTraining in engTrainings)
            {
                int? engLangLevelId = engTraining.Get<int>("engLangLevel.nomValueId");
                if (!engLangLevelId.HasValue)
                {
                    continue;
                }

                var engLevel = this.nomRepository.GetNomValue("engLangLevels", engLangLevelId.Value);
                int? seqNumber = JObject.Parse(engLevel.TextContent).Get<int?>("seqNumber");
                if (!seqNumber.HasValue)
                {
                    continue;
                }

                if (currentSeqNumber < seqNumber)
                {
                    result = engTraining;
                    currentSeqNumber = seqNumber.Value;
                }
                else if (currentSeqNumber == seqNumber &&
                    DateTime.Compare(result.Get<DateTime>("documentDateValidFrom"), engTraining.Get<DateTime>("documentDateValidFrom")) < 0)
                {
                    result = engTraining;
                }
            }

            return new
            {
                LEVEL = result.Get<string>("engLangLevel.name"),
                ISSUE_DATE = result.Get<DateTime?>("documentDateValidFrom"),
                VALID_DATE = result.Get<DateTime?>("documentDateValidTo")
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
                .Select(t =>
                    new
                    {
                        DOC = new
                        {
                            DOC_ROLE = string.Format("{0}. {1}", number++, t.Get<string>("documentRole.name")),
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

        private List<object> GetMedCerts(IEnumerable<JObject> includedMedicals, JObject personData)
        {
            var result = includedMedicals.Select(m =>
                new
                {
                    ORDER_NO = number++,
                    NO = string.Format(
                        "{0}-{1}-{2}-{3}",
                        m.Get<string>("documentNumberPrefix"),
                        m.Get<string>("documentNumber"),
                        personData.Get<string>("lin"),
                        m.Get<string>("documentNumberSuffix")),
                    ISSUE_DATE = m.Get<DateTime>("documentDateValidFrom"),
                    VALID_DATE = m.Get<DateTime>("documentDateValidTo"),
                    CLASS = m.Get<string>("medClass.name"),
                    PUBLISHER = m.Get<string>("documentPublisher.name"),
                    LIMITATION = string.Join(",", m.GetItems<JObject>("limitations").Select(l => l.Get<string>("name")))
                }).ToList<object>();
            return result;
        }

        private IEnumerable<object> GetAbbreviations(string licenceTypeCode)
        {
            List<object> abbreviations;
            if (licenceAbbreviations.TryGetValue(licenceTypeCode, out abbreviations))
            {
                return abbreviations;
            }

            return new object[0].ToList();
        }
    }
}

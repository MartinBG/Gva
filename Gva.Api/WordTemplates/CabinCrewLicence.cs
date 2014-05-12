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
    public class CabinCrewLicence : IDataGenerator
    {
        private static Dictionary<string, List<object>> licencePrivileges = new Dictionary<string, List<object>>
        {
            {
                "C/AL",
                new List<object>()
                {
                    LicenceDictionary.LicencePrivilege["steward"],
                    LicenceDictionary.LicencePrivilege["medCert2"],
                    LicenceDictionary.LicencePrivilege["idDoc"],
                }
            }
        };

        private ILotRepository lotRepository;
        private INomRepository nomRepository;

        public CabinCrewLicence(
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
                return new string[] { "caa_steward" };
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

            var includedTrainings = edition.GetItems<int>("includedTrainings")
                .Select(i => lot.GetPart("personDocumentTrainings/" + i).Content);
            var includedExams = edition.GetItems<int>("includedExams")
                .Select(i => lot.GetPart("personDocumentExams/" + i).Content);
            var includedMedicals = edition.GetItems<int>("includedMedicals")
                .Select(i => lot.GetPart("personDocumentMedicals/" + i).Content);
            var includedRatings = edition.GetItems<int>("includedRatings")
                .Select(i => lot.GetPart("ratings/" + i).Content);

            var licenceType = this.nomRepository.GetNomValue("licenceTypes", licence.Get<int>("licenceType.nomValueId"));
            var licenceCaCode = JObject.Parse(licenceType.TextContent).Get<string>("codeCA");
            var licenceNumber = string.Format(
                "BG {0} - {1} - {2}",
                licenceType.Code,
                licence.Get<string>("licenceNumber"),
                personData.Get<string>("lin"));

            var documents = this.GetDocuments(includedTrainings, includedExams, licenceType.Code);
            var ratings = this.GetRatings(includedRatings);

            var json = new
            {
                root = new
                {
                    L_LICENCE_TYPE_CA_CODE = licenceCaCode,
                    L_LICENCE_TYPE_NAME = licenceType.Name.ToUpper(),
                    L_LICENCE_TYPE_NAME_TRANS = licenceType.NameAlt == null ? string.Empty : licenceType.NameAlt.ToUpper(),
                    L_LICENCE_CA_CODE = licenceCaCode,
                    L_LICENCE_NO = licenceNumber,
                    L_LICENCE_HOLDER = this.GetPersonData(personData, personAddress),
                    L_FIRST_ISSUE_DATE = firstEdition.Get<DateTime>("documentDateValidFrom"),
                    L_PRIVILEGE = this.GetLicencePrivileges(licenceType.Code, edition),
                    L_ISSUE_DATE = edition.Get<DateTime>("documentDateValidFrom"),
                    T_LICENCE_HOLDER = this.GetLicenceHolder(personData, personAddress),
                    T_LICENCE_TYPE_NAME = licenceType.Name.ToLower(),
                    T_LICENCE_NO = licenceNumber,
                    T_FIRST_ISSUE_DATE = firstEdition.Get<DateTime>("documentDateValidFrom"),
                    T_VALID_DATE = edition.Get<DateTime>("documentDateValidTo"),
                    T_ACTION = edition.Get<string>("licenceAction.name").ToUpper(),
                    T_ISSUE_DATE = edition.Get<DateTime>("documentDateValidFrom"),
                    T_DOCUMENTS = documents.Take(documents.Length / 2),
                    T_DOCUMENTS2 = documents.Skip(documents.Length / 2),
                    MED_CERT = this.GetMedCerts(licenceType.Code, includedMedicals, personData),
                    T_RATING = ratings,
                    L_RATING2 = ratings,
                    L_ABBREV = this.GetAbbreviations()
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
                DATEPLACE_OF_BIRTH = new
                {
                    DATE_OF_BIRTH = personData.Get<DateTime>("dateOfBirth"),
                    PLACE_OF_BIRTH = new
                    {
                        COUNTRY_NAME = country.Name,
                        TOWN_VILLAGE_NAME = placeOfBirth.Name
                    },
                    PLACEBIRTH_TRANS = new
                    {
                        COUNTRY_NAME = country.NameAlt,
                        TOWN_VILLAGE_NAME = placeOfBirth.NameAlt
                    }
                },
                ADDRESS_BG = string.Format(
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
                    COUNTRY_CODE = JObject.Parse(nationality.TextContent).Get<string>("nationalityCodeCA")
                }
            };
        }

        private List<object> GetLicencePrivileges(string licenceTypeCode, JObject edition)
        {
            List<object> privileges;
            List<dynamic> result = new object[0].ToList();

            if (licencePrivileges.TryGetValue(licenceTypeCode, out privileges))
            {
                if (licenceTypeCode == "C/AL")
                {
                    dynamic dateValidPrivilege = LicenceDictionary.LicencePrivilege["dateValid"];
                    string dateValid = edition.Get<DateTime>("documentDateValidTo").ToString("dd.MM.yyyy");

                    result = new List<object>(privileges);
                    result.Add(new
                    {
                        NAME_BG = string.Format(dateValidPrivilege.NAME_BG, dateValid),
                        NAME_TRANS = string.Format(dateValidPrivilege.NAME_TRANS, dateValid)
                    });
                }
            }

            return result.OrderBy(p => p.NO).ToList<object>();
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

        private object[] GetDocuments(
            IEnumerable<JObject> includedTrainings,
            IEnumerable<JObject> includedExams,
            string licenceTypeCode)
        {
            string[] documentRoleCodes;
            bool hasRoles = LicenceDictionary.LicenceRole.TryGetValue(licenceTypeCode, out documentRoleCodes);

            if (!hasRoles)
            {
                return new object[0];
            }

            var trainings = includedTrainings
                .Where(t => documentRoleCodes.Contains(t.Get<string>("documentRole.code")))
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
                .Where(e => documentRoleCodes.Contains(e.Get<string>("documentRole.code")))
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

        private object[] GetMedCerts(string licenceTypeCode, IEnumerable<JObject> includedMedicals, JObject personData)
        {
            return includedMedicals.Select(m =>
                new
                {
                    ORDER_NO = 9,
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
                }).ToArray<object>();
        }

        private object[] GetRatings(IEnumerable<JObject> includedRatings)
        {
            return includedRatings
                .Select(r =>
                {
                    JObject lastEdition = r.GetItems<JObject>("editions").Last();
                    var ratingType = r.Get<NomValue>("ratingType");
                    var ratingClass = r.Get<NomValue>("ratingClass");
                    var authorization = r.Get<NomValue>("authorization");

                    return new
                    {
                        CLASS_AUTH = ratingClass == null && ratingType == null ?
                            authorization.Name:
                            string.Format(
                                "{0} {1} {2}",
                                ratingType == null ? string.Empty : ratingType.Name,
                                ratingClass == null ? string.Empty : ratingClass.Name,
                                authorization == null ? string.Empty : "/ " + authorization.Name).Trim(),
                        ISSUE_DATE = lastEdition.Get<DateTime>("documentDateValidFrom"),
                        VALID_DATE = lastEdition.Get<DateTime>("documentDateValidTo")
                    };
                }).ToArray<object>();
        }

        private IEnumerable<object> GetAbbreviations()
        {
            return new[]
            {
                LicenceDictionary.LicenceAbbreviation["C/AL"],
                LicenceDictionary.LicenceAbbreviation["FE"],
                LicenceDictionary.LicenceAbbreviation["INS-C/AL"],
                LicenceDictionary.LicenceAbbreviation["SEN-C/AL"],
                LicenceDictionary.LicenceAbbreviation["Types"]
            };
        }
    }
}

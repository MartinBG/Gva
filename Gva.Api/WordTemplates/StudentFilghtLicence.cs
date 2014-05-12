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
    public class StudentFilghtLicence : IDataGenerator
    {
        private static Dictionary<string, List<object>> licencePrivileges = new Dictionary<string, List<object>>
        {
            {
                "SP(H)",
                new List<object>()
                {
                    LicenceDictionary.LicencePrivilege["instr"],
                    LicenceDictionary.LicencePrivilege["medCertClass1or2"],
                    LicenceDictionary.LicencePrivilege["idDoc"]
                }
            },
            {
                "SP(A)",
                new List<object>()
                {
                    LicenceDictionary.LicencePrivilege["instr"],
                    LicenceDictionary.LicencePrivilege["medCertClass1or2"],
                    LicenceDictionary.LicencePrivilege["idDoc"]
                }
            }
        };

        private ILotRepository lotRepository;
        private INomRepository nomRepository;

        public StudentFilghtLicence(
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
                return new string[] { "student_flight_licence" };
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
            var includedMedicals = edition.GetItems<int>("includedMedicals")
                .Select(i => lot.GetPart("personDocumentMedicals/" + i).Content);
            var includedRatings = edition.GetItems<int>("includedRatings")
                .Select(i => lot.GetPart("ratings/" + i).Content);

            var licenceNumber = string.Format(
                "BGR {0} - {1} - {2}",
                licence.Get<string>("licenceType.code"),
                licence.Get<string>("licenceNumber"),
                personData.Get<string>("lin"));
            var licenceCaCode = JObject.Parse(this.nomRepository.GetNomValue(
                    "licenceTypes",
                    licence.Get<int>("licenceType.nomValueId")).TextContent)
                .Get<string>("codeCA");
            var licenceTypeCode = licence.Get<string>("licenceType.code");

            var documents = this.GetDocuments(licenceTypeCode, includedTrainings);

            var json = new
            {
                root = new
                {
                    L_LICENCE_TYPE_CA_CODE = licenceCaCode,
                    L_LICENCE_TYPE_CA_CODE2 = licenceCaCode,
                    L_LICENCE_NO = licenceNumber,
                    L_LICENCE_HOLDER = this.GetPersonData(personData, personAddress),
                    L_LICENCE_PRIV = this.GetLicencePrivileges(licenceTypeCode, edition),
                    L_FIRST_ISSUE_DATE = firstEdition.Get<DateTime>("documentDateValidFrom"),
                    L_ISSUE_DATE = edition.Get<DateTime>("documentDateValidFrom"),
                    T_LICENCE_HOLDER = this.GetLicenceHolder(personData, personAddress),
                    T_LICENCE_NO = licenceNumber,
                    T_FIRST_ISSUE_DATE = firstEdition.Get<DateTime>("documentDateValidFrom"),
                    T_VALID_DATE = edition.Get<DateTime>("documentDateValidTo"),
                    T_ACTION = edition.Get<string>("licenceAction.name").ToUpper(),
                    T_ISSUE_DATE = edition.Get<DateTime>("documentDateValidFrom"),
                    T_DOCUMENTS = documents.Take(documents.Length / 2),
                    T_DOCUMENTS2 = documents.Skip(documents.Length / 2),
                    T_MED_CERT = this.GetMedCerts(licenceTypeCode, includedMedicals, personData),
                    T_RATING = this.GetRaitings(includedRatings),
                    L_RATING = this.GetScools(includedRatings),
                    L_ABBREVIATION = this.GetAbbreviations()
                }
            };

            return JObject.FromObject(json);
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
                if (licenceTypeCode == "SP(H)" || licenceTypeCode == "SP(A)")
                {
                    dynamic dateValidPrivilege = LicenceDictionary.LicencePrivilege["dateValid"];
                    string dateValid = edition.Get<DateTime>("documentDateValidTo").ToString("dd.MM.yyyy");

                    result = new List<object>(privileges);
                    result.Add(new
                    {
                        NO = dateValidPrivilege.NO,
                        NAME_BG = string.Format(dateValidPrivilege.NAME_BG, dateValid),
                        NAME_TRANS = string.Format(dateValidPrivilege.NAME_TRANS, dateValid)
                    });
                }
            }

            return result
                .OrderBy(p => p.NO)
                .ToList<object>();
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
                .Where(t => documentRoleCodes.Contains(t.Get<string>("documentRole.code")))
                .OrderBy(t => t.Get<DateTime>("documentDateValidFrom"))
                .Select(t =>
                    new
                    {
                        DOC = new
                        {
                            DOC_ROLE = t.Get<string>("documentRole.name"),
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

        private object[] GetMedCerts(string licenceTypeCode, IEnumerable<JObject> includedMedicals, JObject personData)
        {
            int orderNumber = licenceTypeCode == "SP(A)" ? 8 : 1;

            return includedMedicals.Select(m =>
                new
                {
                    ORDER_NO = orderNumber,
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

        private object[] GetRaitings(IEnumerable<JObject> includedRatings)
        {
            return includedRatings
                .Select(r =>
                    {
                        JObject lastEdition = r.GetItems<JObject>("editions").Last();

                        return new
                        {
                            TYPE = string.Format(
                                "{0} {1}",
                                r.Get<string>("ratingClass.name"),
                                r.Get<string>("ratingType.name")),
                            AUTH_NOTES = string.Format(
                                "{0} {1}",
                                r.Get<string>("authorization.name"),
                                lastEdition.Get<string>("notes")),
                            ISSUE_DATE = lastEdition.Get<DateTime>("documentDateValidFrom"),
                            VALID_DATE = lastEdition.Get<DateTime>("documentDateValidTo")
                        };
                    }).ToArray<object>();
        }

        private object[] GetScools(IEnumerable<JObject> includedRatings)
        {
            return includedRatings
                .Where(r => r.Get<NomValue>("ratingType") != null && r.Get<NomValue>("ratingClass") != null)
                .Select(r =>
                    {
                        JObject lastEdition = r.GetItems<JObject>("editions").Last();

                        return new
                        {
                            SCHOOL = lastEdition.Get<string>("notes"),
                            TYPE = string.Format(
                                "{0} {1}",
                                r.Get<string>("ratingClass.name"),
                                r.Get<string>("ratingType.name")),
                            ISSUE_DATE = lastEdition.Get<DateTime>("documentDateValidFrom"),
                            VALID_DATE = lastEdition.Get<DateTime>("documentDateValidTo")
                        };
                    }).ToArray<object>();
        }

        private IEnumerable<object> GetAbbreviations()
        {
            return new[]
            {
                LicenceDictionary.LicenceAbbreviation["ATP"],
                LicenceDictionary.LicenceAbbreviation["SP"],
            };
        }
    }
}

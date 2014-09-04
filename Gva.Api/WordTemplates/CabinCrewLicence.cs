using System;
using System.Collections.Generic;
using System.Linq;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Json;
using Gva.Api.ModelsDO.Persons;
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

            var includedTrainings = lastEdition.IncludedTrainings
                .Select(i => lot.Index.GetPart<PersonTrainingDO>("personDocumentTrainings/" + i).Content);
            //TODO - includedExams! var includedExams = lastEdition.GetItems<int>("includedExams")
            //TODO - includedExams!     .Select(i => lot.Index.GetPart("personDocumentExams/" + i).Content);
            var includedMedicals = lastEdition.IncludedMedicals
                .Select(i => lot.Index.GetPart<PersonMedicalDO>("personDocumentMedicals/" + i).Content);
            var includedRatings = lastEdition.IncludedRatings
                .Select(i => lot.Index.GetPart<PersonRatingDO>("ratings/" + i).Content);

            var licenceType = this.nomRepository.GetNomValue("licenceTypes", licence.LicenceType.NomValueId);
            var licenceCaCode = licenceType.TextContent.Get<string>("codeCA");
            var licenceNumber = string.Format(
                "BG {0} - {1} - {2}",
                licenceType.Code,
                Utils.PadLicenceNumber(licence.LicenceNumber),
                personData.Lin);

            //TODO - includedExams! var documents = this.GetDocuments(includedTrainings, includedExams, licenceType.Code);
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
                    L_FIRST_ISSUE_DATE = firstEdition.DocumentDateValidFrom,
                    L_PRIVILEGE = this.GetLicencePrivileges(licenceType.Code, lastEdition),
                    L_ISSUE_DATE = lastEdition.DocumentDateValidFrom,
                    T_LICENCE_HOLDER = Utils.GetLicenceHolder(personData, personAddress),
                    T_LICENCE_TYPE_NAME = licenceType.Name.ToLower(),
                    T_LICENCE_NO = licenceNumber,
                    T_FIRST_ISSUE_DATE = firstEdition.DocumentDateValidFrom,
                    T_VALID_DATE = lastEdition.DocumentDateValidTo,
                    T_ACTION = lastEdition.LicenceAction.Name.ToUpper(),
                    T_ISSUE_DATE = lastEdition.DocumentDateValidFrom,
                    //TODO - includedExams! T_DOCUMENTS = documents.Take(documents.Length / 2),
                    //TODO - includedExams! T_DOCUMENTS2 = documents.Skip(documents.Length / 2),
                    MED_CERT = this.GetMedCerts(licenceType.Code, includedMedicals, personData),
                    T_RATING = ratings,
                    L_RATING2 = ratings,
                    L_ABBREV = this.GetAbbreviations()
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
                DATEPLACE_OF_BIRTH = new
                {
                    DATE_OF_BIRTH = personData.DateOfBirth,
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

        private List<object> GetLicencePrivileges(string licenceTypeCode, PersonLicenceEditionDO edition)
        {
            List<object> privileges;
            List<dynamic> result = new object[0].ToList();

            if (licencePrivileges.TryGetValue(licenceTypeCode, out privileges))
            {
                if (licenceTypeCode == "C/AL")
                {
                    dynamic dateValidPrivilege = LicenceDictionary.LicencePrivilege["dateValid"];
                    string dateValid = edition.DocumentDateValidTo.Value.ToString("dd.MM.yyyy");
                    string dateValidTrans = edition.DocumentDateValidTo.Value.ToString("dd MMMM yyyy");

                    result = new List<object>(privileges);
                    result.Add(new
                    {
                        NO = dateValidPrivilege.NO,
                        NAME_BG = string.Format(dateValidPrivilege.NAME_BG, dateValid),
                        NAME_TRANS = string.Format(dateValidPrivilege.NAME_TRANS, dateValidTrans)
                    });
                }
            }

            return result.OrderBy(p => p.NO).ToList<object>();
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

        private object[] GetMedCerts(string licenceTypeCode, IEnumerable<PersonMedicalDO> includedMedicals, PersonDataDO personData)
        {
            return includedMedicals.Select(m =>
                new
                {
                    ORDER_NO = 9,
                    NO = string.Format(
                        "{0}-{1}-{2}-{3}",
                        m.DocumentNumberPrefix,
                        m.DocumentNumber,
                        personData.Lin,
                        m.DocumentNumberSuffix),
                    ISSUE_DATE = m.DocumentDateValidFrom,
                    VALID_DATE = m.DocumentDateValidTo,
                    CLASS = m.MedClass.Name,
                    PUBLISHER = m.DocumentPublisher.Name,
                    LIMITATION = string.Join(",", m.Limitations.Select(l => l.Name))
                }).ToArray<object>();
        }

        private object[] GetRatings(IEnumerable<PersonRatingDO> includedRatings)
        {
            return includedRatings
                .Select(r =>
                {
                    PersonRatingEditionDO lastEdition = r.Editions.Last();
                    var ratingTypeName = r.RatingType == null ? null : r.RatingType.Name;
                    var ratingClassName = r.RatingClass == null ? null : r.RatingClass.Name;
                    var authorizationName = r.Authorization == null ? null : r.Authorization.Name;

                    return new
                    {
                        CLASS_AUTH = string.IsNullOrEmpty(ratingClassName) && string.IsNullOrEmpty(ratingTypeName) ?
                            authorizationName :
                            string.Format(
                                "{0} {1} {2}",
                                ratingTypeName,
                                ratingClassName,
                                string.IsNullOrEmpty(authorizationName) ? string.Empty : "/ " + authorizationName).Trim(),
                        ISSUE_DATE = lastEdition.DocumentDateValidFrom,
                        VALID_DATE = lastEdition.DocumentDateValidTo
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

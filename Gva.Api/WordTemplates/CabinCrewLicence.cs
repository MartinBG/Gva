using System.Collections.Generic;
using System.Linq;
using Common.Api.Repositories.NomRepository;
using Common.Json;
using Gva.Api.ModelsDO.Persons;
using Regs.Api.Repositories.LotRepositories;
using Regs.Api.Models;
using Common.Api.Models;

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
            var includedExams = lastEdition.IncludedExams
                .Select(i => lot.Index.GetPart<PersonTrainingDO>("personDocumentTrainings/" + i).Content);
            var includedMedicals = lastEdition.IncludedMedicals
                .Select(i => lot.Index.GetPart<PersonMedicalDO>("personDocumentMedicals/" + i).Content);
            var includedChecks = lastEdition.IncludedChecks
                .Select(i => lot.Index.GetPart<PersonCheckDO>("personDocumentChecks/" + i).Content);
            var includedRatings = lastEdition.IncludedRatings.Select(i => i.Ind).Distinct()
                .Select(ind => lot.Index.GetPart<PersonRatingDO>("ratings/" + ind));
            var ratingEditions = lastEdition.IncludedRatings.Select(i => lot.Index.GetPart<PersonRatingEditionDO>("ratingEditions/" + i.Index));
            
            var licenceType = this.nomRepository.GetNomValue("licenceTypes", licence.LicenceType.NomValueId);
            var licenceCaCode = licenceType.TextContent.Get<string>("codeCA");
            var licenceNumber = string.Format(
                "BG {0} - {1} - {2}",
                licenceType.Code,
                Utils.PadLicenceNumber(licence.LicenceNumber),
                personData.Lin);

            string[] documentRoleCodes;
            bool hasRoles = LicenceDictionary.LicenceRole.TryGetValue(licenceType.Code, out documentRoleCodes);
            dynamic trainings = null;
            dynamic exams = null;
            dynamic simulators = null;
            if (hasRoles)
            {
                trainings = this.GetTrainings(includedTrainings, documentRoleCodes);

                NomValue examRole = this.nomRepository.GetNomValue("documentRoles", "exam");
                NomValue simulatorRole = this.nomRepository.GetNomValue("documentRoles", "simulator");
                NomValue practicalCheckRole = this.nomRepository.GetNomValue("documentRoles", "practicalCheck");

                if (documentRoleCodes.Contains(examRole.Code) || documentRoleCodes.Contains(examRole.Code))
                {
                    exams = this.GetExams(includedExams, includedChecks, examRole);
                }

                if (documentRoleCodes.Contains(simulatorRole.Code))
                {
                    simulators = this.GetSimulators(includedChecks, simulatorRole);
                }
            }

            var ratings = this.GetRatings(includedRatings, ratingEditions);

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
                    T_DOCUMENTS = new { SUB_DOC = trainings },
                    T_DOCUMENTS2 = new { SUB_DOC = exams },
                    T_DOCUMENTS3 = new { SUB_DOC = simulators },
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
            NomValue country = null;
            NomValue nationality = null;
            if (placeOfBirth != null)
            {
                country = this.nomRepository.GetNomValue("countries", placeOfBirth.ParentValueId.Value);
                nationality = this.nomRepository.GetNomValue("countries", personData.Country.NomValueId);
            }

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
                        COUNTRY_NAME = country != null ? country.Name : null,
                        TOWN_VILLAGE_NAME = placeOfBirth != null? placeOfBirth.Name : null
                    },
                    PLACEBIRTH_TRANS = new
                    {
                        COUNTRY_NAME = country != null ? country.NameAlt : null,
                        TOWN_VILLAGE_NAME = placeOfBirth != null ? placeOfBirth.NameAlt : null
                    }
                },
                ADDRESS_BG = string.Format(
                    "{0}, {1}",
                    personAddress.Settlement != null ? personAddress.Settlement.Name : null,
                    personAddress.Address),
                ADDRESS_TRANS = string.Format(
                    "{0}, {1}",
                    personAddress.AddressAlt,
                    personAddress.Settlement != null ? personAddress.Settlement.NameAlt : null),
                NATIONALITY = new
                {
                    COUNTRY_NAME_BG = nationality != null? nationality.Name : null,
                    COUNTRY_CODE = nationality != null? nationality.TextContent.Get<string>("nationalityCodeCA") : null
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
                    string dateValid = edition.DocumentDateValidTo.HasValue ? edition.DocumentDateValidTo.Value.ToString("dd.MM.yyyy") : null;

                    result = new List<object>(privileges);
                    result.Add(new
                    {
                        NO = dateValidPrivilege.NO,
                        NAME_BG = string.Format(dateValidPrivilege.NAME_BG, dateValid),
                        NAME_TRANS = string.Format(dateValidPrivilege.NAME_TRANS, dateValid)
                    });
                }
            }

            return result.OrderBy(p => p.NO).ToList<object>();
        }

        private List<object> GetTrainings(
            IEnumerable<PersonTrainingDO> includedTrainings,
            string[] documentRoleCodes)
        {
            var trainings = includedTrainings
                .Where(t => documentRoleCodes.Contains(t.DocumentRole.Code))
                .Select(t => new
                {
                    CLASS = t.RatingType != null ? t.RatingType.Code : "",
                    DOC_NO = t.DocumentNumber,
                    DATE = t.DocumentDateValidFrom
                })
                .OrderBy(d => d.DATE)
                .ToList<object>();

            return Utils.FillBlankData(trainings, 4);
        }

        private List<object> GetExams(
            IEnumerable<PersonTrainingDO> includedExams,
            IEnumerable<PersonCheckDO> includedChecks,
            NomValue practicalCheckRole)
        {
            var exams  = includedExams
                .Select(t => new
                    {
                        CLASS = t.RatingType != null ? t.RatingType.Code : "",
                        DOC_NO = t.DocumentNumber,
                        DATE = t.DocumentDateValidFrom
                    })
                    .OrderBy(d => d.DATE)
                    .ToList<object>();

           var checks =  includedChecks.Where(d => d.DocumentRole.Code == practicalCheckRole.Code)
                .Select(t => new
                {
                    CLASS = t.RatingType != null ? t.RatingType.Code : "",
                    DOC_NO = t.DocumentNumber,
                    DATE = t.DocumentDateValidFrom
                })
                    .OrderBy(d => d.DATE)
                    .ToList<object>();

           return Utils.FillBlankData(exams.Union(checks).ToList<object>(), 4);
        }

        private List<object> GetSimulators(
            IEnumerable<PersonCheckDO> includedChecks,
            NomValue simulatorRole)
        {
            var simulators = includedChecks.Where(t => t.DocumentRole.Code == simulatorRole.Code)
                .Select(t => new
                {
                    CLASS = t.RatingType != null ? t.RatingType.Code : "",
                    DOC_NO = t.DocumentNumber,
                    DATE = t.DocumentDateValidFrom
                })
                .OrderBy(d => d.DATE)
                .ToList<object>();

            return Utils.FillBlankData(simulators, 4);
        }

        private List<object> GetMedCerts(string licenceTypeCode, IEnumerable<PersonMedicalDO> includedMedicals, PersonDataDO personData)
        {
            var medicals = includedMedicals.Select(m =>
                new
                {
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
                    LIMITATION = m.Limitations.Count > 0 ? string.Join(",", m.Limitations.Select(l => l.Name)) : string.Empty
                }).ToList<object>();

            return Utils.FillBlankData(medicals, 1);
        }

        private List<object> GetRatings(IEnumerable<PartVersion<PersonRatingDO>> includedRatings, IEnumerable<PartVersion<PersonRatingEditionDO>> editions)
        {
            List<object> ratingEditions = new List<object>();
            foreach (var edition in editions.OrderBy(r => r.Content.DocumentDateValidFrom))
            {
                var rating = includedRatings.Where(r => r.Part.Index == edition.Content.RatingPartIndex).Single();
                var ratingTypeCode = rating.Content.RatingType == null ? null : rating.Content.RatingType.Code;
                var ratingClassName = rating.Content.RatingClass == null ? null : rating.Content.RatingClass.Name;
                var authorizationCode = rating.Content.Authorization == null ? null : rating.Content.Authorization.Code;
                ratingEditions.Add(new
                {
                    CLASS_AUTH = string.IsNullOrEmpty(ratingClassName) && string.IsNullOrEmpty(ratingTypeCode) ?
                        authorizationCode :
                        string.Format(
                            "{0} {1} {2}",
                            ratingTypeCode,
                            ratingClassName,
                            string.IsNullOrEmpty(authorizationCode) ? string.Empty : " / " + authorizationCode).Trim(),
                    ISSUE_DATE = edition.Content.DocumentDateValidFrom,
                    VALID_DATE = edition.Content.DocumentDateValidTo
                });
            }

            ratingEditions = Utils.FillBlankData(ratingEditions, 11);
            return ratingEditions;
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

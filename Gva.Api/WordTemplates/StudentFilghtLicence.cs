using System.Collections.Generic;
using System.Linq;
using Common.Api.Repositories.NomRepository;
using Common.Json;
using Gva.Api.ModelsDO.Persons;
using Regs.Api.Repositories.LotRepositories;
using Regs.Api.Models;
using Common.Api.Models;
using System;

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
        private int number;

        public StudentFilghtLicence(
            ILotRepository lotRepository,
            INomRepository nomRepository)
        {
            this.lotRepository = lotRepository;
            this.nomRepository = nomRepository;
            this.number = 6;
        }


        public string[] TemplateNames
        {
            get
            {
                return new string[] { "student_flight_licence" };
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
            var includedMedicals = lastEdition.IncludedMedicals
                .Select(i => lot.Index.GetPart<PersonMedicalDO>("personDocumentMedicals/" + i).Content);
             var includedRatings = lastEdition.IncludedRatings
                .Select(i => lot.Index.GetPart<PersonRatingDO>("ratings/" + i.Ind));
            var ratingEditions = lastEdition.IncludedRatings.Select(i => lot.Index.GetPart<PersonRatingEditionDO>("ratingEditions/" + i.Index));
   
            var licenceCaCode = this.nomRepository.GetNomValue("licenceTypes", licence.LicenceType.NomValueId).TextContent.Get<string>("codeCA");
            var licenceTypeCode = licence.LicenceType.Code;
            var licenceNumber = string.Format(
                "BGR {0} - {1} - {2}",
                licenceTypeCode,
                Utils.PadLicenceNumber(licence.LicenceNumber),
                personData.Lin);
            
            string[] documentRoleCodes;
            bool hasRoles = LicenceDictionary.LicenceRole.TryGetValue(licenceTypeCode, out documentRoleCodes);
            dynamic trainings = null;
            dynamic educations = null;
            if (hasRoles)
            {
                trainings = this.GetTrainings(includedTrainings, documentRoleCodes);
                educations = this.GetEducations(includedTrainings, documentRoleCodes);
            }

            var json = new
            {
                root = new
                {
                    L_LICENCE_TYPE_CA_CODE = licenceCaCode,
                    L_LICENCE_TYPE_CA_CODE2 = licenceCaCode,
                    L_LICENCE_NO = licenceNumber,
                    L_LICENCE_HOLDER = this.GetPersonData(personData, personAddress),
                    L_LICENCE_PRIV = this.GetLicencePrivileges(licenceTypeCode, lastEdition),
                    L_FIRST_ISSUE_DATE = firstEdition.DocumentDateValidFrom,
                    L_ISSUE_DATE = lastEdition.DocumentDateValidFrom,
                    T_LICENCE_HOLDER = Utils.GetLicenceHolder(personData, personAddress),
                    T_LICENCE_NO = licenceNumber,
                    T_FIRST_ISSUE_DATE = firstEdition.DocumentDateValidFrom,
                    T_VALID_DATE = lastEdition.DocumentDateValidTo,
                    T_ACTION = lastEdition.LicenceAction.Name.ToUpper(),
                    T_ISSUE_DATE = lastEdition.DocumentDateValidFrom,
                    T_DOCUMENTS =  trainings,
                    T_DOCUMENTS2 = educations,
                    T_MED_CERT = this.GetMedCerts(licenceTypeCode, includedMedicals, personData),
                    T_RATING = this.GetRatings(includedRatings, ratingEditions),
                    L_RATING = this.GetSchools(includedRatings, ratingEditions),
                    L_ABBREVIATION = this.GetAbbreviations()
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
                DATE_PLACE_OF_BIRTH = new
                {
                    DATE_OF_BIRTH = personData.DateOfBirth,
                    PLACE_OF_BIRTH = new
                        {
                            COUNTRY_NAME = country != null? country.Name : null,
                            TOWN_VILLAGE_NAME = placeOfBirth != null ? placeOfBirth.Name : null
                        },
                    PLACE_OF_BIRTH_TRANS = new
                        {
                            COUNTRY_NAME = country != null ? country.NameAlt : null,
                            TOWN_VILLAGE_NAME = placeOfBirth != null ? placeOfBirth.NameAlt : null
                        }
                },
                ADDRESS = string.Format(
                    "{0}, {1}",
                    personAddress.Settlement != null ? personAddress.Settlement.Name : null,
                    personAddress.Address),
                ADDRESS_TRANS = string.Format(
                    "{0}, {1}",
                    personAddress.AddressAlt,
                    personAddress.Settlement != null ? personAddress.Settlement.NameAlt : null),
                NATIONALITY = new
                {
                    COUNTRY_NAME_BG = nationality != null ? nationality.Name : null,
                    COUNTRY_CODE = nationality != null ? nationality.TextContent.Get<string>("nationalityCodeCA") : null
                }
            };
        }

        private List<object> GetLicencePrivileges(string licenceTypeCode, PersonLicenceEditionDO edition)
        {
            List<object> privileges;
            List<dynamic> result = new object[0].ToList();

            if (licencePrivileges.TryGetValue(licenceTypeCode, out privileges))
            {
                if (licenceTypeCode == "SP(H)" || licenceTypeCode == "SP(A)")
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

            return result
                .OrderBy(p => p.NO)
                .ToList<object>();
        }

        private List<object> GetEducations(
            IEnumerable<PersonTrainingDO> includedTrainings,
            string[] documentRoleCodes)
        {
            var educationRole = this.nomRepository.GetNomValue("documentRoles", "diploma");
            var trainings = includedTrainings
                .Where(t => t.Valid.Code == "Y" && documentRoleCodes.Contains(t.DocumentRole.Code) && t.DocumentRole.Code == educationRole.Code)
                .OrderBy(t => t.DocumentDateValidFrom)
                .Select(t =>
                    new
                    {
                        DOC_TYPE = t.DocumentType.Name.ToLower(),
                        DOC_NO = t.DocumentNumber,
                        DATE = t.DocumentDateValidFrom,
                        DOC_PUBLISHER = t.DocumentPublisher
                    }).ToList<object>();

            return new List<object>()
            {
                new
                {
                    DOC = new
                    {
                        DOC_ROLE = String.Format("{0}. {1}", this.number++, LicenceDictionary.DocumentTitle["Diploma"]),
                        SUB_DOC = Utils.FillBlankData(trainings, 1)
                    }
                }
            };
        }

        private List<object> GetTrainings(
            IEnumerable<PersonTrainingDO> includedTrainings,
            string[] documentRoleCodes)
        {
            var trainingRole = this.nomRepository.GetNomValue("documentRoles", "theoreticalTraining");

            var trainings = includedTrainings
                .Where(t => t.Valid.Code == "Y" && documentRoleCodes.Contains(t.DocumentRole.Code) && t.DocumentRole.Code == trainingRole.Code)
                .OrderBy(t => t.DocumentDateValidFrom)
                .Select(t =>
                    new
                    {
                        DOC_TYPE = t.DocumentType.Name.ToLower(),
                        DOC_NO = t.DocumentNumber,
                        DATE = t.DocumentDateValidFrom,
                        DOC_PUBLISHER = t.DocumentPublisher
                    }).ToList<object>();

            return new List<object>()
            {
                new
                {
                    DOC = new
                    {
                        DOC_ROLE = String.Format("{0}. {1}", this.number++, LicenceDictionary.DocumentTitle["TheoreticalTraining"]),
                        SUB_DOC = Utils.FillBlankData(trainings, 1)
                    }
                }
            };
        }

        private List<object> GetMedCerts(string licenceTypeCode, IEnumerable<PersonMedicalDO> includedMedicals, PersonDataDO personData)
        {
            var medicals = includedMedicals.Select(m =>
                new
                {
                    ORDER_NO = this.number++,
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

            if (medicals.Count() == 0)
            { 
                medicals.Add(new
                {
                    ORDER_NO = this.number++
                });
            }

            return medicals;
        }

        private List<object> GetRatings(IEnumerable<PartVersion<PersonRatingDO>> includedRatings, IEnumerable<PartVersion<PersonRatingEditionDO>> ratingEditions)
        {
            List<object> ratings = new List<object>();
            foreach (var edition in ratingEditions)
            {
                var rating = includedRatings.Where(r => r.Part.Index == edition.Content.RatingPartIndex).Single();
                ratings.Add(new
                {
                    TYPE = string.Format(
                           "{0} {1}",
                           rating.Content.RatingClass == null ? null : rating.Content.RatingClass.Name,
                           rating.Content.RatingType == null ? null : rating.Content.RatingType.Code),
                    AUTH_NOTES = string.Format(
                        "{0} {1}",
                        rating.Content.Authorization == null ? null : rating.Content.Authorization.Code,
                        edition.Content.Notes),
                    ISSUE_DATE = edition.Content.DocumentDateValidFrom,
                    VALID_DATE = edition.Content.DocumentDateValidTo
                });
            }

            ratings = Utils.FillBlankData(ratings, 11);
            return ratings;
        }

        private object[] GetSchools(IEnumerable<PartVersion<PersonRatingDO>> includedRatings, IEnumerable<PartVersion<PersonRatingEditionDO>> ratingEditions)
        {
            List<object> schools = new List<object>();
            foreach (var edition in ratingEditions)
            {
                var rating = includedRatings.Where(r => r.Part.Index == edition.Content.RatingPartIndex).Single();
                schools.Add(new
                {
                    SCHOOL = edition.Content.Notes,
                    TYPE = string.Format(
                        "{0} {1}",
                        rating.Content.RatingClass != null ? rating.Content.RatingClass.Name : "",
                        rating.Content.RatingType != null ? rating.Content.RatingType.Code : ""),
                    ISSUE_DATE = edition.Content.DocumentDateValidFrom,
                    VALID_DATE = edition.Content.DocumentDateValidTo
                });
            }

            return schools.ToArray();
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

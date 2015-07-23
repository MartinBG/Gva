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

        public string GeneratorCode
        {
            get
            {
                return "studentFlightLicence";
            }
        }

        public string GeneratorName
        {
            get
            {
                return "Лиценз за ученик пилот";
            }
        }

        public object GetData(int lotId, string path)
        {
            var lot = this.lotRepository.GetLotIndex(lotId);
            var personData = lot.Index.GetPart<PersonDataDO>("personData").Content;
            var personAddressPart = lot.Index.GetParts<PersonAddressDO>("personAddresses")
               .FirstOrDefault(a => this.nomRepository.GetNomValue("boolean", a.Content.ValidId.Value).Code == "Y");
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
                .Select(i => i.Ind)
                .Distinct()
                .Select(i => lot.Index.GetPart<PersonRatingDO>("ratings/" + i.Value));
            var ratingEditions = lastEdition.IncludedRatings.Select(i => lot.Index.GetPart<PersonRatingEditionDO>("ratingEditions/" + i.Index));

            var licenceType = this.nomRepository.GetNomValue("licenceTypes", licence.LicenceTypeId.Value);
            var licenceCaCode = licenceType.TextContent.Get<string>("codeCA");
            var licenceTypeCode = licenceType.Code;
            var licenceNumber = string.Format(
                "BGR {0} - {1} - {2}",
                licenceTypeCode.Replace("(", "").Replace(")", ""),
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

            string licenceAction = lastEdition.LicenceActionId.HasValue ? this.nomRepository.GetNomValue("licenceActions", lastEdition.LicenceActionId.Value).Name.ToUpper() : null;

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
                    T_LICENCE_HOLDER = Utils.GetLicenceHolder(personData, personAddress, this.nomRepository),
                    T_LICENCE_NO = licenceNumber,
                    T_FIRST_ISSUE_DATE = firstEdition.DocumentDateValidFrom,
                    T_VALID_DATE = lastEdition.DocumentDateValidTo,
                    T_ACTION = licenceAction,
                    T_ISSUE_DATE = lastEdition.DocumentDateValidFrom,
                    T_DOCUMENTS =  trainings,
                    T_DOCUMENTS2 = educations,
                    T_MED_CERT = Utils.GetMedCerts(this.number++, includedMedicals, personData),
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

            NomValue settlement = null;
            if (personAddress.SettlementId.HasValue)
            {
                settlement = this.nomRepository.GetNomValue("cities", personAddress.SettlementId.Value);
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
                    settlement != null ? settlement.Name : null,
                    personAddress.Address),
                ADDRESS_TRANS = string.Format(
                    "{0}, {1}",
                    personAddress.AddressAlt,
                    settlement != null ? settlement.NameAlt : null),
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
            var trainings = Utils.GetTrainingsByCode(includedTrainings, educationRole.Code, documentRoleCodes);

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
            var trainings = Utils.GetTrainingsByCode(includedTrainings, trainingRole.Code, documentRoleCodes);

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

        private List<object> GetRatings(IEnumerable<PartVersion<PersonRatingDO>> includedRatings, IEnumerable<PartVersion<PersonRatingEditionDO>> ratingEditions)
        {
            List<object> ratings = new List<object>();
            foreach (var edition in ratingEditions)
            {
                var rating = includedRatings.Where(r => r.Part.Index == edition.Content.RatingPartIndex).Single();
                var ratingTypesCodes = rating.Content.RatingTypes.Count() > 0 ? string.Join(", ", rating.Content.RatingTypes.Select(rt => rt.Code)) : "";
                var ratingClassCodes = rating.Content.RatingClass == null ? null : rating.Content.RatingClass.Code;
                var authorizationCode = rating.Content.Authorization == null ? null : rating.Content.Authorization.Code;
                var firstRatingEdition = this.lotRepository.GetLotIndex(rating.Part.LotId)
                        .Index.GetParts<PersonRatingEditionDO>("ratingEditions")
                        .Where(epv => epv.Content.RatingPartIndex == rating.Part.Index)
                        .OrderByDescending(epv => epv.Content.Index)
                        .Last();

                ratings.Add(new
                {
                    TYPE = string.Format(
                           "{0} {1}",
                           ratingClassCodes,
                           ratingTypesCodes),
                    AUTH_NOTES = string.Format(
                        "{0} {1}",
                        authorizationCode,
                        edition.Content.Notes),
                    ISSUE_DATE = firstRatingEdition.Content.DocumentDateValidFrom,
                    VALID_DATE = edition.Content.DocumentDateValidTo
                });
            }

            return Utils.FillBlankData(ratings, 11);
        }

        private object[] GetSchools(IEnumerable<PartVersion<PersonRatingDO>> includedRatings, IEnumerable<PartVersion<PersonRatingEditionDO>> ratingEditions)
        {
            List<object> schools = new List<object>();
            foreach (var edition in ratingEditions)
            {
                var rating = includedRatings.Where(r => r.Part.Index == edition.Content.RatingPartIndex).Single();
                var firstRatingEdition = this.lotRepository.GetLotIndex(rating.Part.LotId)
                        .Index.GetParts<PersonRatingEditionDO>("ratingEditions")
                        .Where(epv => epv.Content.RatingPartIndex == rating.Part.Index)
                        .OrderByDescending(epv => epv.Content.Index)
                        .Last();

                schools.Add(new
                {
                    SCHOOL = edition.Content.Notes,
                    TYPE = string.Format(
                        "{0} {1}",
                        rating.Content.RatingClass != null ? rating.Content.RatingClass.Name : "",
                        rating.Content.RatingTypes.Count() > 0 ? string.Join(", ", rating.Content.RatingTypes.Select(rt => rt.Code)) : ""),
                    ISSUE_DATE = firstRatingEdition.Content.DocumentDateValidFrom,
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

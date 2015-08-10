using System.Collections.Generic;
using System.Linq;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Json;
using Gva.Api.ModelsDO.Persons;
using Regs.Api.Models;
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

        public string GeneratorCode
        {
            get
            {
                return "coordinatorSimi";
            }
        }

        public string GeneratorName
        {
            get
            {
                return "Лиценз за координатор по УВД (SIMI)";
            }
        }

        public object GetData(int lotId, string path)
        {
            var lot = this.lotRepository.GetLotIndex(lotId);
            var personData = lot.Index.GetPart<PersonDataDO>("personData").Content;
            int validTrueId = this.nomRepository.GetNomValue("boolean", "yes").NomValueId;
            var personAddressPart = lot.Index.GetParts<PersonAddressDO>("personAddresses")
                .FirstOrDefault(a => a.Content.ValidId == validTrueId);
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

            var licenceType = this.nomRepository.GetNomValue("licenceTypes", licence.LicenceTypeId.Value);

            var includedTrainings = lastEdition.IncludedTrainings
                .Select(i => lot.Index.GetPart<PersonTrainingDO>("personDocumentTrainings/" + i).Content);
            var includedExams = lastEdition.IncludedExams
                .Select(i => lot.Index.GetPart<PersonTrainingDO>("personDocumentTrainings/" + i).Content);
            var includedChecks = lastEdition.IncludedChecks
               .Select(i => lot.Index.GetPart<PersonCheckDO>("personDocumentChecks/" + i).Content);
            var includedLangCerts = lastEdition.IncludedLangCerts
                .Select(i => lot.Index.GetPart<PersonLangCertDO>("personDocumentLangCertificates/" + i).Content);
            var includedRatings = lastEdition.IncludedRatings
                .Select(i => i.Ind)
                .Distinct()
                .Select(i => lot.Index.GetPart<PersonRatingDO>("ratings/" + i.Value));
            var ratingEditions = lastEdition.IncludedRatings.Select(i => lot.Index.GetPart<PersonRatingEditionDO>("ratingEditions/" + i.Index));
            var licenceCodeCa = licenceType.TextContent.Get<string>("codeCA");

            string[] documentRoleCodes;
            int[] documentRoleIds;
            bool hasRoles = LicenceDictionary.LicenceRole.TryGetValue(licenceType.Code, out documentRoleCodes);
            documentRoleIds = documentRoleCodes
                .Select(c =>
                    this.nomRepository.GetNomValues("documentRoles").Where(r => r.Code == c).SingleOrDefault().NomValueId)
                    .ToArray();

            dynamic theoreticalExams = null;
            dynamic practicalExams = null;
            dynamic accessOrderPractEducation = null;
            dynamic accessOrderWorkAlone = null;
            dynamic checksAtWork = null;

            if (hasRoles)
            {
                NomValue theoreticalExamRole = this.nomRepository.GetNomValue("documentRoles", "exam");
                NomValue practicalExamRole = this.nomRepository.GetNomValue("documentRoles", "practicalExams");
                NomValue accessOrderPractEducationRole = this.nomRepository.GetNomValue("documentRoles", "accessOrderPractEduc");
                NomValue accessOrderWorkAloneRole = this.nomRepository.GetNomValue("documentRoles", "accessOrderWorkAlone");
                NomValue checkAtWorkRole = this.nomRepository.GetNomValue("documentRoles", "checkAtWork");

                practicalExams = Utils.GetExamsById(includedExams, includedChecks, includedTrainings, practicalExamRole.NomValueId, documentRoleIds, this.nomRepository);
                theoreticalExams = Utils.GetExamsById(includedExams, includedChecks, includedTrainings, theoreticalExamRole.NomValueId, documentRoleIds, this.nomRepository);
                checksAtWork = Utils.GetChecksById(includedChecks, checkAtWorkRole.NomValueId, documentRoleIds, this.nomRepository);
                accessOrderWorkAlone = Utils.GetTrainingsById(includedTrainings, accessOrderWorkAloneRole.NomValueId, documentRoleIds, this.nomRepository);
                accessOrderPractEducation = Utils.GetTrainingsById(includedTrainings, accessOrderPractEducationRole.NomValueId, documentRoleIds, this.nomRepository);
            }

            IEnumerable<PersonLangCertDO> langLevels = includedLangCerts.Where(t => t.LangLevelId.HasValue);

            List<object> lLangLevel = new List<object>();
            List<object> tLangLevel = new List<object>();
            if (langLevels.Count() > 0)
            {
                lLangLevel = langLevels
                    .Select(l => new
                {
                    LEVEL = this.nomRepository.GetNomValue("langLevels", l.LangLevelId.Value).Name,
                    VALID_DATE = l.DocumentDateValidTo.HasValue ? l.DocumentDateValidTo.Value.ToString("dd/MM/yyyy") : "unlimited"
                })
                .ToList<object>();

                tLangLevel = langLevels.Select(l => new
                {
                    LEVEL = this.nomRepository.GetNomValue("langLevels", l.LangLevelId.Value).Name,
                    ISSUE_DATE = l.DocumentDateValidFrom,
                    VALID_DATE = l.DocumentDateValidTo.HasValue ? l.DocumentDateValidTo.Value.ToString("dd/MM/yyyy") : "unlimited"
                })
                .ToList<object>();
            }

            var endorsements = Utils.GetEndorsements(includedRatings, ratingEditions, this.lotRepository, this.nomRepository);
            var tEndorsements = this.GetTEndorsements(includedRatings, ratingEditions);
            var lEndorsements = this.GetLEndorsements(includedRatings, ratingEditions, langLevels);

            var licenceNumber = string.Format(
                "BGR. {0} - {1} - {2}",
                licenceType.Code,
                Utils.PadLicenceNumber(licence.LicenceNumber),
                personData.Lin);

            string licenceAction = lastEdition.LicenceActionId.HasValue ? this.nomRepository.GetNomValue("licenceActions", lastEdition.LicenceActionId.Value).Name.ToUpper() : null;

            var json = new
            {
                root = new
                {
                    L_NAME = licenceType.Name.ToUpper(),
                    L_NAME_TRANS = licenceType.NameAlt.ToUpper(),
                    L_LICENCE_TYPE_CA_CODE = licenceCodeCa,
                    L_NAME1 = licenceType.Name.ToUpper(),
                    L_NAME1_TRANS = licenceType.NameAlt.ToUpper(),
                    L_LICENCE_TYPE_CA_CODE2 = licenceCodeCa,
                    L_LICENCE_NO = licenceNumber,
                    L_LICENCE_HOLDER = this.GetPersonData(personData, personAddress),
                    L_LICENCE_PRIV = this.GetLicencePrivileges(),
                    ENDORSEMENT = Utils.FillBlankData(endorsements, 2),
                    L_ENDORSEMENT = lEndorsements,
                    L_LANG_LEVEL = Utils.FillBlankData(lLangLevel, 1),
                    L_FIRST_ISSUE_DATE = firstEdition.DocumentDateValidFrom,
                    L_ISSUE_DATE = lastEdition.DocumentDateValidFrom,
                    T_ENDORSEMENT = tEndorsements,
                    T_LICENCE_HOLDER = Utils.GetLicenceHolder(personData, personAddress, this.nomRepository),
                    T_LICENCE_NO = licenceNumber,
                    T_LICENCE_CODE = licenceType.Code,
                    T_ACTION = licenceAction,
                    T_FIRST_ISSUE_DATE = firstEdition.DocumentDateValidFrom,
                    T_ISSUE_DATE = lastEdition.DocumentDateValidFrom,
                    T_LANG_LEVEL = Utils.FillBlankData(tLangLevel, 1),
                    T_THEORETICAL_EXAM = Utils.FillBlankData(theoreticalExams, 1),
                    T_ACCESS_ORDER_PRACTICAL_EDUC = Utils.FillBlankData(accessOrderPractEducation, 1),
                    T_PRACTICAL_EXAM = Utils.FillBlankData(practicalExams, 1),
                    T_CHECK_AT_WORK = Utils.FillBlankData(checksAtWork, 1),
                    T_ACCESS_ORDER_WORK_ALONE = Utils.FillBlankData(accessOrderWorkAlone, 1),
                    L_ABBREVIATION = this.GetAbbreviations()
                }
            };

            return json;
        }

        private object GetPersonData(PersonDataDO personData, PersonAddressDO personAddress)
        {
            NomValue placeOfBirth = nomRepository.GetNomValue("cities", personData.PlaceOfBirthId.Value);
            NomValue country = null;
            NomValue nationality = null;
            if (placeOfBirth != null)
            {
                country = this.nomRepository.GetNomValue("countries", placeOfBirth.ParentValueId.Value);
                nationality = this.nomRepository.GetNomValue("countries", personData.CountryId.Value);
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
                        COUNTRY_NAME = country != null ? country.Name : null,
                        TOWN_VILLAGE_NAME = placeOfBirth != null ? placeOfBirth.Name : null
                    },
                    PLACE_OF_BIRTH_TRANS = new
                    {
                        COUNTRY_NAME = country != null ? country.NameAlt : null,
                        TOWN_VILLAGE_NAME = placeOfBirth != null ? placeOfBirth.NameAlt : null
                    }
                },
                ADDRESS = personAddress.Address,
                ADDRESS_TRANS = personAddress.AddressAlt,
                NATIONALITY = new
                {
                    COUNTRY_NAME_BG = nationality != null? nationality.Name : null,
                    COUNTRY_CODE = nationality != null? nationality.TextContent.Get<string>("nationalityCodeCA") : null
                }
            };
        }

        private List<object> GetTEndorsements(IEnumerable<PartVersion<PersonRatingDO>> includedRatings, IEnumerable<PartVersion<PersonRatingEditionDO>> ratingEditions)
        {
            List<object> endosments = new List<object>();
            foreach (var edition in ratingEditions)
            {
                var rating = includedRatings.Where(r => r.Part.Index == edition.Content.RatingPartIndex).Single();
                if (rating.Content.AuthorizationId.HasValue)
                {
                    var firstRatingEdition = this.lotRepository.GetLotIndex(rating.Part.LotId)
                        .Index.GetParts<PersonRatingEditionDO>("ratingEditions")
                        .Where(epv => epv.Content.RatingPartIndex == rating.Part.Index)
                        .OrderByDescending(epv => epv.Content.Index)
                        .Last();

                    endosments.Add(new
                    {
                        ICAO = rating.Content.LocationIndicatorId.HasValue ? this.nomRepository.GetNomValue("locationIndicators", rating.Content.LocationIndicatorId.Value).Code : null,
                        AUTH = rating.Content.AuthorizationId.HasValue ? this.nomRepository.GetNomValue("authorizations", rating.Content.AuthorizationId.Value).Code : null,
                        SECTOR = rating.Content.Sector,
                        ISSUE_DATE = firstRatingEdition.Content.DocumentDateValidFrom,
                        VALID_DATE = edition.Content.DocumentDateValidTo
                    });
                }
            };

            return Utils.FillBlankData(endosments, 11);
        }


        private List<object> GetLEndorsements(
            IEnumerable<PartVersion<PersonRatingDO>> includedRatings,
            IEnumerable<PartVersion<PersonRatingEditionDO>> ratingEditions,
            IEnumerable<PersonLangCertDO> langLevels)
        {
            List<object> endorsements = new List<object>();
            foreach (var edition in ratingEditions)
            {
                var rating = includedRatings.Where(r => r.Part.Index == edition.Content.RatingPartIndex).Single();
                if (rating.Content.AuthorizationId.HasValue)
                {
                    endorsements.Add(new
                    {
                        ICAO = rating.Content.LocationIndicatorId.HasValue ? this.nomRepository.GetNomValue("locationIndicators", rating.Content.LocationIndicatorId.Value).Code : null,
                        AUTH = rating.Content.AuthorizationId.HasValue ? this.nomRepository.GetNomValue("authorizations", rating.Content.AuthorizationId.Value).Code : null,
                        SECTOR = rating.Content.Sector,
                        VALID_DATE = edition.Content.DocumentDateValidTo.HasValue ? edition.Content.DocumentDateValidTo.Value.ToString("dd/MM/yyyy") : "unlimited"
                    });
                }
            };

            endorsements = endorsements
                .Union(langLevels
                    .Where(l => l.LangLevelId.HasValue)
                    .Select(l => new
                    {
                        AUTH = this.nomRepository.GetNomValue("langLevels", l.LangLevelId.Value).Name.ToUpper(),
                        VALID_DATE = l.DocumentDateValidTo
                    }))
                .ToList<object>();

            return Utils.FillBlankData(endorsements, 11);
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

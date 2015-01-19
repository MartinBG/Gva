using System.Collections.Generic;
using System.Linq;
using Common.Api.Repositories.NomRepository;
using Common.Json;
using Gva.Api.ModelsDO.Persons;
using Regs.Api.Repositories.LotRepositories;
using Regs.Api.Models;
using Gva.Api.Repositories.FileRepository;
using Gva.Api.Repositories.CaseTypeRepository;
using Common.Api.Models;

namespace Gva.Api.WordTemplates
{
    public class CoordinatorLicence : IDataGenerator
    {
        private ILotRepository lotRepository;
        private INomRepository nomRepository;
        private IFileRepository fileRepository;
        private ICaseTypeRepository caseTypeRepository;

        public CoordinatorLicence(
            ILotRepository lotRepository,
            INomRepository nomRepository,
            IFileRepository fileRepository,
            ICaseTypeRepository caseTypeRepository)
        {
            this.lotRepository = lotRepository;
            this.nomRepository = nomRepository;
            this.fileRepository = fileRepository;
            this.caseTypeRepository = caseTypeRepository;
        }

        public string[] TemplateNames
        {
            get
            {
                return new string[] { "coordinator" };
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

            var includedRatings = lastEdition.IncludedRatings.Select(i => i.Ind).Distinct()
                .Select(ind => lot.Index.GetPart<PersonRatingDO>("ratings/" + ind));
            var ratingEditions = lastEdition.IncludedRatings.Select(i => lot.Index.GetPart<PersonRatingEditionDO>("ratingEditions/" + i.Index));

            var includedTrainings = lastEdition.IncludedTrainings
                .Select(i => lot.Index.GetPart<PersonTrainingDO>("personDocumentTrainings/" + i).Content);
            var includedExams = lastEdition.IncludedExams
                .Select(i => lot.Index.GetPart<PersonTrainingDO>("personDocumentTrainings/" + i).Content);
            var includedChecks = lastEdition.IncludedChecks
               .Select(i => lot.Index.GetPart<PersonCheckDO>("personDocumentChecks/" + i).Content);

            var licenceType = this.nomRepository.GetNomValue("licenceTypes", licence.LicenceType.NomValueId);
            var licenceCaCode = licenceType.TextContent.Get<string>("codeCA");
            var licenceNumber = string.Format(
                "BGR. {0} - {1} - {2}",
                licenceType.TextContent.Get<string>("licenceCode"),
                Utils.PadLicenceNumber(licence.LicenceNumber),
                personData.Lin);

            var endorsements2 = this.GetEndorsements2(includedRatings, ratingEditions);

            string[] documentRoleCodes;
            bool hasRoles = LicenceDictionary.LicenceRole.TryGetValue(licenceType.Code, out documentRoleCodes);

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

                if (documentRoleCodes.Contains(theoreticalExamRole.Code) || documentRoleCodes.Contains(practicalExamRole.Code))
                {
                    practicalExams = this.GetExams(includedExams, includedChecks, includedTrainings, practicalExamRole);
                    theoreticalExams = this.GetExams(includedExams, includedChecks, includedTrainings, theoreticalExamRole);
                }

                if (documentRoleCodes.Contains(checkAtWorkRole.Code))
                {
                    checksAtWork = this.GetChecks(includedChecks, checkAtWorkRole);
                }

                if (documentRoleCodes.Contains(accessOrderWorkAloneRole.Code))
                {
                    accessOrderWorkAlone = this.GetTrainings(includedTrainings, accessOrderWorkAloneRole);
                }

                if (documentRoleCodes.Contains(accessOrderPractEducationRole.Code))
                {
                    accessOrderPractEducation = this.GetTrainings(includedTrainings, accessOrderPractEducationRole);
                }
            }

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
                    L_LICENCE_PRIV = this.GetLicencePrivileges(licenceType.TextContent.Get<string>("licenceCode")),
                    L_FIRST_ISSUE_DATE = firstEdition.DocumentDateValidFrom,
                    L_ISSUE_DATE = lastEdition.DocumentDateValidFrom,
                    ENDORSEMENT = this.GetEndorsements(includedRatings, ratingEditions),
                    T_LICENCE_HOLDER = Utils.GetLicenceHolder(personData, personAddress),
                    T_LICENCE_CODE = licenceCaCode,
                    T_LICENCE_NO = licenceNumber,
                    T_FIRST_ISSUE_DATE = firstEdition.DocumentDateValidFrom,
                    T_ACTION = lastEdition.LicenceAction.Name.ToUpper(),
                    T_ISSUE_DATE = lastEdition.DocumentDateValidFrom,
                    T_THEORETICAL_EXAM = theoreticalExams,
                    T_ACCESS_ORDER_PRACTICAL_EDUC = accessOrderPractEducation,
                    T_PRACTICAL_EXAM = practicalExams,
                    T_CHECK_AT_WORK = checksAtWork,
                    T_ACCESS_ORDER_WORK_ALONE = accessOrderWorkAlone,
                    T_ENDORSEMENT = endorsements2,
                    L_ENDORSEMENT = endorsements2,
                    L_ABBREVIATION = this.GetAbbreviations()
                }
            };

            return json;
        }

        private object GetPersonData(PersonDataDO personData, PersonAddressDO personAddress)
        {
            var placeOfBirth = personData.PlaceOfBirth;
            dynamic country = null;
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
                        COUNTRY_NAME = country != null ? country.Name : null,
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
                    COUNTRY_NAME_BG = nationality != null? nationality.Name : null,
                    COUNTRY_CODE = nationality != null? nationality.TextContent.Get<string>("nationalityCodeCA") : null
                }
            };
        }

        private List<object> GetLicencePrivileges(string licenceCode)
        {
            return new List<object>()
            {
                LicenceDictionary.LicencePrivilege[licenceCode],
                LicenceDictionary.LicencePrivilege["idDoc4"]
            };
        }

        private List<object> GetEndorsements(IEnumerable<PartVersion<PersonRatingDO>> includedRatings, IEnumerable<PartVersion<PersonRatingEditionDO>> ratingEditions)
        {
            var result = includedRatings
                .Where(r => r.Content.Authorization != null)
                .GroupBy(r => r.Content.Authorization.Code)
                .Select(g => new
                    {
                        NAME = g.Key,
                        DATE = g.Min(r => ratingEditions.Where(e => e.Content.RatingPartIndex == r.Part.Index).OrderBy(e => e.Content.Index).Last().Content.DocumentDateValidFrom)
                    }).ToList<object>();

            return Utils.FillBlankData(result, 3);
        }

        private List<object> GetExams(
            IEnumerable<PersonTrainingDO> includedExams,
            IEnumerable<PersonCheckDO> includedChecks,
            IEnumerable<PersonTrainingDO> includedTrainings,
            NomValue examRole)
        {
            var exams = includedExams.Where(d => d.DocumentRole.Code == examRole.Code)
               .Select(t => new
                {
                    DOC_TYPE = t.DocumentType.Name.ToLower(),
                    DOC_NO = t.DocumentNumber,
                    DATE = t.DocumentDateValidFrom,
                    DOC_PUBLISHER = t.DocumentPublisher
               })
               .ToList<object>();

            var trainings = this.GetTrainings(includedTrainings, examRole);
            var checks = this.GetChecks(includedChecks, examRole);

            return Utils.FillBlankData(exams.Union(checks).Union(trainings).ToList<object>(), 1);
        }

        private List<object> GetChecks(
            IEnumerable<PersonCheckDO> includedChecks,
            NomValue checkRole)
        {
            var checks = includedChecks.Where(d => d.DocumentRole.Code == checkRole.Code)
                 .Select(t => new
                 {
                     DOC_TYPE = t.DocumentType.Name.ToLower(),
                     DOC_NO = t.DocumentNumber,
                     DATE = t.DocumentDateValidFrom,
                     DOC_PUBLISHER = t.DocumentPublisher
                 })
                 .ToList<object>();

            return Utils.FillBlankData(checks, 1);
        }

        private List<object> GetTrainings(
            IEnumerable<PersonTrainingDO> includedTrainings,
            NomValue trainingRole)
        {
            var trainings = includedTrainings.Where(d => d.DocumentRole.Code == trainingRole.Code)
                 .Select(t => new
                 {
                     DOC_TYPE = t.DocumentType.Name.ToLower(),
                     DOC_NO = t.DocumentNumber,
                     DATE = t.DocumentDateValidFrom,
                     DOC_PUBLISHER = t.DocumentPublisher
                 })
                 .ToList<object>();

            return Utils.FillBlankData(trainings, 1);
        }

        private List<object> GetEndorsements2(IEnumerable<PartVersion<PersonRatingDO>> includedRatings, IEnumerable<PartVersion<PersonRatingEditionDO>> editions)
        {
            int caseTypeId = this.caseTypeRepository.GetCaseType("ovd").GvaCaseTypeId;
            List<object> ratingEditions = new List<object>();
            foreach (var edition in editions.OrderBy(e => e.Content.DocumentDateValidFrom))
            {
                var rating = includedRatings.Where(r => r.Part.Index == edition.Content.RatingPartIndex).Single();
                var ratingTypes = rating.Content.RatingTypes.Count() > 0 ? string.Join(", ", rating.Content.RatingTypes.Select(rt => rt.Code)) : "";
                var ratingClass = rating.Content.RatingClass == null ? null : rating.Content.RatingClass.Code;
                var authorization = rating.Content.Authorization == null ? null : rating.Content.Authorization.Code;
                var firstRatingEdition = this.lotRepository.GetLotIndex(rating.Part.LotId)
                        .Index.GetParts<PersonRatingEditionDO>("ratingEditions")
                        .Where(epv => epv.Content.RatingPartIndex == rating.Part.Index)
                        .OrderByDescending(epv => epv.Content.Index)
                        .Last();

                ratingEditions.Add(new
                {
                    ICAO = rating.Content.LocationIndicator == null ? null : rating.Content.LocationIndicator.Code,
                    SECTOR = rating.Content.Sector,
                    AUTH = string.IsNullOrEmpty(ratingClass) && string.IsNullOrEmpty(ratingTypes) ?
                        authorization :
                        string.Format(
                            "{0} {1} {2}",
                            ratingTypes,
                            ratingClass,
                            string.IsNullOrEmpty(authorization) ? string.Empty : " - " + authorization).Trim(),
                    ISSUE_DATE = firstRatingEdition.Content.DocumentDateValidFrom,
                    VALID_DATE = edition.Content.DocumentDateValidTo
                });
            }

            return Utils.FillBlankData(ratingEditions, 11);
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

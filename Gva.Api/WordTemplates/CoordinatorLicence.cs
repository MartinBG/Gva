﻿using System.Collections.Generic;
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

        public string GeneratorCode
        {
            get
            {
                return "coordinator";
            }
        }

        public string GeneratorName
        {
            get
            {
                return "Лиценз за координатор по УВД (CATML)";
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

            var includedRatings = lastEdition.IncludedRatings
                .Select(i => i.Ind)
                .Distinct()
                .Select(i => lot.Index.GetPart<PersonRatingDO>("ratings/" + i.Value));
            var ratingEditions = lastEdition.IncludedRatings.Select(i => lot.Index.GetPart<PersonRatingEditionDO>("ratingEditions/" + i.Index));

            var includedTrainings = lastEdition.IncludedTrainings
                .Select(i => lot.Index.GetPart<PersonTrainingDO>("personDocumentTrainings/" + i).Content);
            var includedExams = lastEdition.IncludedExams
                .Select(i => lot.Index.GetPart<PersonTrainingDO>("personDocumentTrainings/" + i).Content);
            var includedChecks = lastEdition.IncludedChecks
               .Select(i => lot.Index.GetPart<PersonCheckDO>("personDocumentChecks/" + i).Content);

            var licenceType = this.nomRepository.GetNomValue("licenceTypes", licence.LicenceTypeId.Value);
            var licenceCaCode = licenceType.TextContent.Get<string>("codeCA");
            var licenceNumber = string.Format(
                "BGR. {0} - {1} - {2}",
                licenceType.TextContent.Get<string>("codeCA"),
                Utils.PadLicenceNumber(licence.LicenceNumber),
                personData.Lin);

            var endorsements2 = this.GetEndorsements2(includedRatings, ratingEditions);

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

            string licenceAction = lastEdition.LicenceActionId.HasValue ? this.nomRepository.GetNomValue("licenceActions", lastEdition.LicenceActionId.Value).Name.ToUpper() : null;

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
                    L_LICENCE_PRIV = this.GetLicencePrivileges(licenceType.TextContent.Get<string>("codeCA")),
                    L_FIRST_ISSUE_DATE = firstEdition.DocumentDateValidFrom,
                    L_ISSUE_DATE = lastEdition.DocumentDateValidFrom,
                    ENDORSEMENT = Utils.FillBlankData(Utils.GetEndorsements(includedRatings, ratingEditions, this.lotRepository, this.nomRepository), 3),
                    T_LICENCE_HOLDER = Utils.GetLicenceHolder(personData, personAddress, this.nomRepository),
                    T_LICENCE_CODE = licenceCaCode,
                    T_LICENCE_NO = licenceNumber,
                    T_FIRST_ISSUE_DATE = firstEdition.DocumentDateValidFrom,
                    T_ACTION = licenceAction,
                    T_ISSUE_DATE = lastEdition.DocumentDateValidFrom,
                    T_THEORETICAL_EXAM = Utils.FillBlankData(theoreticalExams, 1),
                    T_ACCESS_ORDER_PRACTICAL_EDUC = Utils.FillBlankData(accessOrderPractEducation, 1),
                    T_PRACTICAL_EXAM = Utils.FillBlankData(practicalExams, 1),
                    T_CHECK_AT_WORK = Utils.FillBlankData(checksAtWork, 1),
                    T_ACCESS_ORDER_WORK_ALONE = Utils.FillBlankData(accessOrderWorkAlone, 1),
                    T_ENDORSEMENT = endorsements2,
                    L_ENDORSEMENT = endorsements2,
                    L_ABBREVIATION = this.GetAbbreviations()
                }
            };

            return json;
        }

        private object GetPersonData(PersonDataDO personData, PersonAddressDO personAddress)
        {
            NomValue placeOfBirth = null;
            NomValue country = null;
            NomValue nationality = null;
            if (personData.PlaceOfBirthId.HasValue)
            {
                placeOfBirth = this.nomRepository.GetNomValue("cities", personData.PlaceOfBirthId.Value);
                country = this.nomRepository.GetNomValue("countries", placeOfBirth.ParentValueId.Value);
                nationality = this.nomRepository.GetNomValue("countries", personData.CountryId.Value);
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
                    settlement != null ? settlement.Name : null,
                    personAddress.Address),
                ADDRESS_TRANS = string.Format(
                    "{0}, {1}",
                    personAddress.AddressAlt,
                    settlement != null ? settlement.NameAlt : null),
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

        private List<object> GetEndorsements2(IEnumerable<PartVersion<PersonRatingDO>> includedRatings, IEnumerable<PartVersion<PersonRatingEditionDO>> editions)
        {
            int caseTypeId = this.caseTypeRepository.GetCaseType("ovd").GvaCaseTypeId;
            List<object> ratingEditions = new List<object>();
            foreach (var edition in editions)
            {
                var rating = includedRatings.Where(r => r.Part.Index == edition.Content.RatingPartIndex).Single();
                var ratingTypes = rating.Content.RatingTypes.Count() > 0 ? string.Join(", ", this.nomRepository.GetNomValues("ratingTypes", rating.Content.RatingTypes.ToArray()).Select(rt => rt.Code)) : "";
                var ratingClass = rating.Content.RatingClassId.HasValue ? this.nomRepository.GetNomValue("ratingClasses", rating.Content.RatingClassId.Value).Code : null;
                var authorization = rating.Content.AuthorizationId.HasValue ? this.nomRepository.GetNomValue("authorizations", rating.Content.AuthorizationId.Value).Code : null;
                var firstRatingEdition = this.lotRepository.GetLotIndex(rating.Part.LotId)
                        .Index.GetParts<PersonRatingEditionDO>("ratingEditions")
                        .Where(epv => epv.Content.RatingPartIndex == rating.Part.Index)
                        .OrderByDescending(epv => epv.Content.Index)
                        .Last();

                ratingEditions.Add(new
                {
                    ICAO = rating.Content.LocationIndicatorId.HasValue ? this.nomRepository.GetNomValue("locationIndicators", rating.Content.LocationIndicatorId.Value).Code : null,
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

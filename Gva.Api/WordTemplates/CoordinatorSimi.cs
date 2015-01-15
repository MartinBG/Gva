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

        public string[] TemplateNames
        {
            get
            {
                return new string[] { "coordinator_simi" };
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

            var licenceType = this.nomRepository.GetNomValue("licenceTypes", licence.LicenceType.NomValueId);

            var includedTrainings = lastEdition.IncludedTrainings
                .Select(i => lot.Index.GetPart<PersonTrainingDO>("personDocumentTrainings/" + i).Content);
            var includedExams = lastEdition.IncludedExams
                .Select(i => lot.Index.GetPart<PersonTrainingDO>("personDocumentTrainings/" + i).Content);
            var includedChecks = lastEdition.IncludedChecks
               .Select(i => lot.Index.GetPart<PersonCheckDO>("personDocumentChecks/" + i).Content);
            var includedLangCerts = lastEdition.IncludedLangCerts
                .Select(i => lot.Index.GetPart<PersonLangCertDO>("personDocumentLangCertificates/" + i).Content);
            var includedRatings = lastEdition.IncludedRatings.Select(i => i.Ind).Distinct()
                .Select(ind => lot.Index.GetPart<PersonRatingDO>("ratings/" + ind));
            var ratingEditions = lastEdition.IncludedRatings.Select(i => lot.Index.GetPart<PersonRatingEditionDO>("ratingEditions/" + i.Index));
            var licenceCodeCa = licenceType.TextContent.Get<string>("codeCA");

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
                    checksAtWork = Utils.FillBlankData(this.GetChecks(includedChecks, checkAtWorkRole), 1);
                }

                if (documentRoleCodes.Contains(accessOrderWorkAloneRole.Code))
                {
                    accessOrderWorkAlone = Utils.FillBlankData(this.GetTrainings(includedTrainings, accessOrderWorkAloneRole), 1);
                }

                if (documentRoleCodes.Contains(accessOrderPractEducationRole.Code))
                {
                    accessOrderPractEducation = Utils.FillBlankData(this.GetTrainings(includedTrainings, accessOrderPractEducationRole), 1);
                }
            }

            IEnumerable<PersonLangCertDO> engLevels = includedLangCerts.Where(t => t.DocumentRole.Alias == "engCert" && t.LangLevel != null);

            dynamic lLangLevel = null;
            dynamic tLangLevel = null;
            if (engLevels.Count() > 0)
            {
                lLangLevel = engLevels.Select(l => new
                {
                    LEVEL = l.LangLevel.Name,
                    VALID_DATE = l.DocumentDateValidTo
                });

                tLangLevel = engLevels.Select(l => new
                {
                    LEVEL = l.LangLevel.Name,
                    ISSUE_DATE = l.DocumentDateValidFrom,
                    VALID_DATE = l.DocumentDateValidTo
                });
            }

            var endorsements = this.GetEndorsements(includedRatings, ratingEditions);
            var tEndorsements = this.GetTEndorsements(includedRatings, ratingEditions);
            var lEndorsements = this.GetLEndorsements(includedRatings, ratingEditions, engLevels);

            var licenceNumber = string.Format(
                "BGR. {0} - {1} - {2}",
                licenceType.Code,
                Utils.PadLicenceNumber(licence.LicenceNumber),
                personData.Lin);

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
                    ENDORSEMENT = endorsements,
                    L_ENDORSEMENT = lEndorsements,
                    L_LANG_LEVEL = lLangLevel,
                    L_FIRST_ISSUE_DATE = firstEdition.DocumentDateValidFrom,
                    L_ISSUE_DATE = lastEdition.DocumentDateValidFrom,
                    T_ENDORSEMENT = tEndorsements,
                    T_LICENCE_HOLDER = Utils.GetLicenceHolder(personData, personAddress),
                    T_LICENCE_NO = licenceNumber,
                    T_LICENCE_CODE = licenceType.Code,
                    T_ACTION = firstEdition.LicenceAction.Name.ToUpper(),
                    T_FIRST_ISSUE_DATE = firstEdition.DocumentDateValidFrom,
                    T_ISSUE_DATE = lastEdition.DocumentDateValidFrom,
                    T_LANG_LEVEL = tLangLevel,
                    T_THEORETICAL_EXAM = theoreticalExams,
                    T_ACCESS_ORDER_PRACTICAL_EDUC = accessOrderPractEducation,
                    T_PRACTICAL_EXAM = practicalExams,
                    T_CHECK_AT_WORK = checksAtWork,
                    T_ACCESS_ORDER_WORK_ALONE = accessOrderWorkAlone,
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
            return includedChecks.Where(d => d.Valid.Code == "Y" && d.DocumentRole.Code == checkRole.Code)
                 .Select(t => new
                 {
                    DOC_TYPE = t.DocumentType.Name.ToLower(),
                    DOC_NO = t.DocumentNumber,
                    DATE = t.DocumentDateValidFrom,
                    DOC_PUBLISHER = t.DocumentPublisher
                 })
                 .ToList<object>();
        }

        private List<object> GetTrainings(
            IEnumerable<PersonTrainingDO> includedTrainings,
            NomValue trainingRole)
        {
            return includedTrainings.Where(d => d.Valid.Code == "Y" && d.DocumentRole.Code == trainingRole.Code)
                 .Select(t => new
                 {
                     DOC_TYPE = t.DocumentType.Name.ToLower(),
                     DOC_NO = t.DocumentNumber,
                     DATE = t.DocumentDateValidFrom,
                     DOC_PUBLISHER = t.DocumentPublisher
                 })
                 .ToList<object>();
        }

        private List<object> GetEndorsements(IEnumerable<PartVersion<PersonRatingDO>> includedRatings, IEnumerable<PartVersion<PersonRatingEditionDO>> ratingEditions)
        {
            List<object> endosments = new List<object>();
            foreach(var edition in ratingEditions)
            {
                var rating = includedRatings.Where(r => r.Part.Index == edition.Content.RatingPartIndex).Single();
                if(rating.Content.Authorization != null)
                {
                    endosments.Add(new {
                        NAME = rating.Content.Authorization.Code,
                        DATE = edition.Content.DocumentDateValidFrom
                    });
                }
            };

            return Utils.FillBlankData(endosments, 2);
        }

        private List<object> GetTEndorsements(IEnumerable<PartVersion<PersonRatingDO>> includedRatings, IEnumerable<PartVersion<PersonRatingEditionDO>> ratingEditions)
        {
            List<object> endosments = new List<object>();
            foreach (var edition in ratingEditions)
            {
                var rating = includedRatings.Where(r => r.Part.Index == edition.Content.RatingPartIndex).Single();
                if (rating.Content.Authorization != null)
                {
                    endosments.Add(new
                    {
                        ICAO = rating.Content.LocationIndicator == null ? null : rating.Content.LocationIndicator.Code,
                        AUTH = rating.Content.Authorization.Code,
                        SECTOR = rating.Content.Sector,
                        ISSUE_DATE = edition.Content.DocumentDateValidFrom,
                        VALID_DATE = edition.Content.DocumentDateValidTo
                    });
                }
            };

            return Utils.FillBlankData(endosments, 11);
        }


        private List<object> GetLEndorsements(
            IEnumerable<PartVersion<PersonRatingDO>> includedRatings,
            IEnumerable<PartVersion<PersonRatingEditionDO>> ratingEditions,
            IEnumerable<PersonLangCertDO> engLevels)
        {
            List<object> endosments = new List<object>();
            foreach (var edition in ratingEditions)
            {
                var rating = includedRatings.Where(r => r.Part.Index == edition.Content.RatingPartIndex).Single();
                if (rating.Content.Authorization != null)
                {
                    endosments.Add(new
                    {
                        ICAO = rating.Content.LocationIndicator == null ? null : rating.Content.LocationIndicator.Code,
                        AUTH = rating.Content.Authorization.Code,
                        SECTOR = rating.Content.Sector,
                        VALID_DATE = edition.Content.DocumentDateValidTo
                    });
                }
            };

            endosments = endosments
                .Union(engLevels.Select(l => new
                {
                    AUTH = l.LangLevel.Name.ToUpper(),
                    VALID_DATE = l.DocumentDateValidTo
                }))
                .ToList<object>();

            return Utils.FillBlankData(endosments, 11);
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

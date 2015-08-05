using System;
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
    public class SAtcl1Licence : IDataGenerator
    {
        private ILotRepository lotRepository;
        private INomRepository nomRepository;
        private IFileRepository fileRepository;
        private ICaseTypeRepository caseTypeRepository;
        private int number;

        public SAtcl1Licence(
            ILotRepository lotRepository,
            INomRepository nomRepository,
            IFileRepository fileRepository,
            ICaseTypeRepository caseTypeRepository)
        {
            this.lotRepository = lotRepository;
            this.nomRepository = nomRepository;
            this.fileRepository = fileRepository;
            this.caseTypeRepository = caseTypeRepository;
            this.number = 5;
        }

        public string GeneratorCode
        {
            get
            {
                return "studentController";
            }
        }

        public string GeneratorName
        {
            get
            {
                return "SAtcl за ученик ръководител полети";
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
            var includedLangCerts = lastEdition.IncludedLangCerts
                .Select(i => lot.Index.GetPart<PersonLangCertDO>("personDocumentLangCertificates/" + i).Content);
            var includedMedicals = lastEdition.IncludedMedicals
                .Select(i => lot.Index.GetPart<PersonMedicalDO>("personDocumentMedicals/" + i).Content);

            var licenceType = this.nomRepository.GetNomValue("licenceTypes", licence.LicenceTypeId.Value);
            var licenceNumber = string.Format(
                "BGR - SATCO - {0} - {1} Student ATCO licence",
                Utils.PadLicenceNumber(licence.LicenceNumber),
                personData.Lin);

            NomValue placeOfBirth = null;
            NomValue country = null;
            NomValue nationality = null;

            if (personData.PlaceOfBirth != null)
            {
                placeOfBirth = this.nomRepository.GetNomValue("cities", personData.PlaceOfBirth.NomValueId);
                country = this.nomRepository.GetNomValue("countries", placeOfBirth.ParentValueId.Value);
            }

            if (personData.Country != null)
            {
                nationality = this.nomRepository.GetNomValue("countries", personData.Country.NomValueId);
            }

            NomValue settlement = null;
            if (personAddress.SettlementId.HasValue)
            {
                settlement = this.nomRepository.GetNomValue("cities", personAddress.SettlementId.Value);
            }

            var address = string.Format(
                "{0}, {1}",
                settlement != null? settlement.Name : null,
                personAddress.Address);

            var documents = this.GetDocuments(licenceType.Code, includedTrainings, includedLangCerts);
            var langLevel = Utils.GetATCLLangCerts(includedLangCerts, nomRepository);
            var lEndorsements = this.GetEndorsements2(includedRatings, ratingEditions, false, false);
            var tEndorsements = this.GetEndorsements2(includedRatings, ratingEditions, true, true);
            var endorsementsAndOtherEndorsements = this.GetEndorsements(includedRatings, ratingEditions);

            string licenceAction = lastEdition.LicenceActionId.HasValue ? this.nomRepository.GetNomValue("licenceActions", lastEdition.LicenceActionId.Value).Name.ToUpper() : null;

            var json = new
            {
                root = new
                {
                    L_NAME = "УЧЕНИК-РЪКОВОДИТЕЛ НА ПОЛЕТИ",
                    L_NAME_TRANS = "STUDENT AIR TRAFFIC CONTROLLER",
                    L_NAME1 = "УЧЕНИК-РЪКОВОДИТЕЛ НА ПОЛЕТИ",
                    L_NAME1_TRANS = "STUDENT AIR TRAFFIC CONTROLLER",
                    L_LICENCE_NO = licenceNumber,
                    FAMILY_BG = personData.LastName.ToUpper(),
                    FAMILY_TRANS = personData.LastNameAlt.ToUpper(),
                    FIRST_NAME_BG = personData.FirstName.ToUpper(),
                    FIRST_NAME_TRANS = personData.FirstNameAlt.ToUpper(),
                    SURNAME_BG = personData.MiddleName.ToUpper(),
                    SURNAME_TRANS = personData.MiddleNameAlt.ToUpper(),
                    DATE_OF_BIRTH = personData.DateOfBirth,
                    COUNTRY = country != null ? country.Name : null,
                    CITY = placeOfBirth != null ? placeOfBirth.Name : null,
                    COUNTRY_EN = country != null ? country.NameAlt : null,
                    CITY_EN = placeOfBirth != null ? placeOfBirth.NameAlt : null,
                    ADDRESS = address,
                    ADDRESS_EN = string.Format(
                        "{0}, {1}",
                        personAddress.AddressAlt,
                        personAddress.SettlementId.HasValue ? this.nomRepository.GetNomValue("cities", personAddress.SettlementId.Value).NameAlt : null),
                    NATIONALITY = nationality != null ? nationality.Name : null,
                    NATIONALITY_EN = nationality != null ? nationality.TextContent.Get<string>("nationalityCodeCA") : null,
                    L_LICENCE_PRIV = this.GetLicencePrivileges(),
                    L_RATINGS = this.GetRatings(includedRatings, ratingEditions),
                    ENDORSEMENT = Utils.FillBlankData(endorsementsAndOtherEndorsements.Item1, 3),
                    L_LANG_LEVEL = Utils.FillBlankData(langLevel, 1),
                    L_FIRST_ISSUE_DATE = firstEdition.DocumentDateValidFrom,
                    L_ISSUE_DATE = lastEdition.DocumentDateValidFrom,
                    NAME = string.Format(
                        "{0} {1} {2}",
                        personData.FirstName,
                        personData.MiddleName,
                        personData.LastName).ToUpper(),
                    LIN = personData.Lin,
                    EGN = personData.Uin,
                    ADDRESS1 = address,
                    TELEPHONE = Utils.GetPhonesString(personData),
                    T_LICENCE_CODE = " РП ",
                    T_LICENCE_NO = licenceNumber,
                    T_FIRST_ISSUE_DATE = firstEdition.DocumentDateValidFrom,
                    T_ACTION = licenceAction,
                    T_ISSUE_DATE = lastEdition.DocumentDateValidFrom,
                    T_DOCUMENTS = documents.Take(6),
                    T_DOCUMENTS2 = documents.Skip(6),
                    T_LANG_LEVEL_NO = number++,
                    T_LANG_LEVEL = Utils.FillBlankData(langLevel, 1),
                    T_MED_CERT = Utils.GetMedCerts(this.number++, includedMedicals, personData),
                    L_ENDORSEMENT1 = Utils.FillBlankData(lEndorsements, 15),
                    L_ENDORSEMENT = Utils.FillBlankData(endorsementsAndOtherEndorsements.Item2, 4),
                    T_ENDORSEMENT = Utils.FillBlankData(tEndorsements, 8)
                }
            };

            return json;
        }

        private List<object> GetLangCertsForEndosement(IEnumerable<PersonLangCertDO> includedLangCerts)
        {
            return includedLangCerts
                .Where(c => c.LangLevelId.HasValue)
                .Select(c => 
                    {   
                        var langLevel = this.nomRepository.GetNomValue("langLevels", c.LangLevelId.Value);
                        return new
                        {
                            AUTH = langLevel.Name.ToUpper(),
                            VALID_DATE = !langLevel.Code.Contains("6") && c.DocumentDateValidTo.HasValue ? c.DocumentDateValidTo.Value.ToString("dd.MM.yyyy") : "unlimited"

                        };
                    })
                    .ToList<object>();
        }

        private List<object> GetLicencePrivileges()
        {
            return new List<object>()
            {
                LicenceDictionary.LicencePrivilege["ATCLratings"]
            };
        }

        private Tuple<List<object>, List<object>> GetEndorsements(
            IEnumerable<PartVersion<PersonRatingDO>> includedRatings,
            IEnumerable<PartVersion<PersonRatingEditionDO>> ratingEditions)
        {
            List<object> endorsments = new List<object>();
            List<object> otherEndorsments = new List<object>();
            List<string> otherEndorsementCodes = new List<string>() { "OJTI", "STDI", "Assessor" };

            foreach (var edition in ratingEditions)
            {
                var rating = includedRatings.Where(r => r.Part.Index == edition.Content.RatingPartIndex).Single();
                if (rating.Content.Authorization != null)
                {
                    if (otherEndorsementCodes.Any(oe => rating.Content.Authorization.Code.Contains(oe)))
                    {
                        string name = otherEndorsementCodes
                            .Where(oe => rating.Content.Authorization.Code.Contains(oe))
                            .First();

                        var lastRatingEdition = this.lotRepository.GetLotIndex(rating.Part.LotId)
                            .Index.GetParts<PersonRatingEditionDO>("ratingEditions")
                            .Where(epv => epv.Content.RatingPartIndex == rating.Part.Index)
                            .OrderByDescending(epv => epv.Content.Index)
                            .First();

                        otherEndorsments.Add(new
                        {
                            NAME = name,
                            VALID_DATE = lastRatingEdition.Content.DocumentDateValidTo
                        });
                    }
                    else
                    {
                        var firstRatingEdition = this.lotRepository.GetLotIndex(rating.Part.LotId)
                            .Index.GetParts<PersonRatingEditionDO>("ratingEditions")
                            .Where(epv => epv.Content.RatingPartIndex == rating.Part.Index)
                            .OrderByDescending(epv => epv.Content.Index)
                            .Last();

                        endorsments.Add(new
                        {
                            NAME = rating.Content.Authorization.Code,
                            DATE = firstRatingEdition.Content.DocumentDateValidFrom
                        });
                    }
                }
            };

            List<object> otherEndorsmentsResult = (from endorsment in otherEndorsments
                    group endorsment by ((dynamic)endorsment).NAME into newGroup
                    let d = newGroup.OrderBy(g => ((dynamic)g).VALID_DATE).FirstOrDefault()
                    select d)
                 .ToList();

            List<object> endorsmentsResult = (from endorsment in endorsments
                    group endorsment by ((dynamic)endorsment).NAME into newGroup
                    let d = newGroup.OrderBy(g => ((dynamic)g).DATE).FirstOrDefault()
                    select d)
                 .ToList();

            return new Tuple<List<object>, List<object>>(endorsmentsResult, otherEndorsmentsResult);
        }

        private List<object> GetRatings(IEnumerable<PartVersion<PersonRatingDO>> includedRatings, IEnumerable<PartVersion<PersonRatingEditionDO>> ratingEditions)
        {
            var result = includedRatings
                .Where(r => r.Content.RatingClass != null || r.Content.RatingTypes.Count() > 0)
                .GroupBy(r => string.Format(
                    "{0} {1}",
                    r.Content.RatingClass == null ? string.Empty : r.Content.RatingClass.Code,
                    r.Content.RatingTypes.Count() == 0 ? string.Empty : string.Join(", ", r.Content.RatingTypes.Select(rt => rt.Code))).Trim())
                .Select(g =>
                {
                    var rating = g.Select(r => r.Part).First();
                    var firstRatingEdition = lotRepository.GetLotIndex(rating.LotId)
                        .Index.GetParts<PersonRatingEditionDO>("ratingEditions")
                        .Where(e => e.Content.RatingPartIndex == rating.Index).OrderByDescending(e => e.Content.Index).Last();

                    return new
                    {
                        NAME = g.Key,
                        DATE = firstRatingEdition.Content.DocumentDateValidFrom
                    };
                }).ToList<object>();

            return Utils.FillBlankData(result, 3);
        }

        private List<object> GetEndorsements2(
            IEnumerable<PartVersion<PersonRatingDO>> includedRatings,
            IEnumerable<PartVersion<PersonRatingEditionDO>> editions,
            bool withIssueDate,
            bool includeOtherEndorsements)
        {
            int caseTypeId = this.caseTypeRepository.GetCaseType("ovd").GvaCaseTypeId;
            List<string> otherEndorsementCodes = new List<string>() { "OJTI", "STDI", "Assessor" };

            List<object> ratingEditions = new List<object>();
            foreach (var edition in editions)
            {
                var rating = includedRatings.Where(r => r.Part.Index == edition.Content.RatingPartIndex).Single();
                var authorization = rating.Content.Authorization == null ? null : rating.Content.Authorization.Code;
                string sector = !string.IsNullOrEmpty(rating.Content.Sector) ? rating.Content.Sector.ToUpper() : null;

                if (!includeOtherEndorsements && authorization != null)
                {
                    if(otherEndorsementCodes.Any(oe => authorization.Contains(oe)))
                    {
                        continue;
                    }
                }

                var firstRatingEdition = this.lotRepository.GetLotIndex(rating.Part.LotId)
                    .Index.GetParts<PersonRatingEditionDO>("ratingEditions")
                    .Where(epv => epv.Content.RatingPartIndex == rating.Part.Index)
                    .OrderByDescending(epv => epv.Content.Index)
                    .Last();
                var ratingTypes = rating.Content.RatingTypes.Count() == 0 ? string.Empty : string.Join(", ", rating.Content.RatingTypes.Select(rt => rt.Code));
                var ratingClass = rating.Content.RatingClass == null ? null : rating.Content.RatingClass.Code;


                object result = null;
                if (withIssueDate)
                {
                    result = new
                    {
                        ICAO = rating.Content.LocationIndicator == null ? null : rating.Content.LocationIndicator.Code,
                        SECTOR = sector,
                        AUTH = string.IsNullOrEmpty(ratingClass) && string.IsNullOrEmpty(ratingTypes) ?
                            authorization :
                            string.Format(
                                "{0} {1} {2}",
                                ratingTypes,
                                ratingClass,
                                string.IsNullOrEmpty(authorization) ? string.Empty : " - " + authorization).Trim(),
                        ISSUE_DATE = firstRatingEdition.Content.DocumentDateValidFrom,
                        VALID_DATE = edition.Content.DocumentDateValidTo
                    };
                }
                else
                {
                    result = new
                    {
                        AUTH = string.IsNullOrEmpty(ratingClass) && string.IsNullOrEmpty(ratingTypes) ?
                            authorization :
                            string.Format(
                                "{0} {1} {2}",
                                ratingTypes,
                                ratingClass,
                                string.IsNullOrEmpty(authorization) ? string.Empty : " - " + authorization).Trim()
                    };
                }

                ratingEditions.Add(result);
            }

            return ratingEditions;
        }

        private List<object> GetDocuments(
            string licenceTypeCode,
            IEnumerable<PersonTrainingDO> includedTrainings,
            IEnumerable<PersonLangCertDO> includedLangCerts)
        {
            IEnumerable<object> result = new List<object>();
            string[] documentRoleCodes;
            int[] documentRoleIds;
            bool hasRoles = LicenceDictionary.LicenceRole.TryGetValue(licenceTypeCode, out documentRoleCodes);
            documentRoleIds = documentRoleCodes
                .Select(c =>
                    this.nomRepository.GetNomValues("documentRoles").Where(r => r.Code == c).SingleOrDefault().NomValueId)
                    .ToArray();

            if (!hasRoles)
            {
                return null;
            }

            NomValue bgCertRole = this.nomRepository.GetNomValue("documentRoles", "bgCert");
            NomValue engCertRole = this.nomRepository.GetNomValue("documentRoles", "engCert");
            NomValue theoreticalExamTransitionalEducationRole = this.nomRepository.GetNomValue("documentRoles", "theorExamTransEducation");
            NomValue practicalExamPreliminaryEducationRole = this.nomRepository.GetNomValue("documentRoles", "practExamPrelimEduc");
            NomValue accessOrderPracticalEducationRole = this.nomRepository.GetNomValue("documentRoles", "accessOrderPractEduc");
            NomValue accessOrderWorkAloneRole = this.nomRepository.GetNomValue("documentRoles", "accessOrderWorkAlone");
            NomValue practExamToGainAccessRole = this.nomRepository.GetNomValue("documentRoles", "practExamToGainAccess");
            NomValue RPcertRole = this.nomRepository.GetNomValue("documentRoles", "RPcert");

            var theoreticalExamsTransitionalEducation = Utils.GetTrainingsById(includedTrainings, theoreticalExamTransitionalEducationRole.NomValueId, documentRoleIds, this.nomRepository);
            var practicalExamsPreliminaryEducation = Utils.GetTrainingsById(includedTrainings, practicalExamPreliminaryEducationRole.NomValueId, documentRoleIds, this.nomRepository);
            var accessOrdersPracticalEducation = Utils.GetTrainingsById(includedTrainings, accessOrderPracticalEducationRole.NomValueId, documentRoleIds, this.nomRepository);
            var practicalExamsToGainAccess = Utils.GetTrainingsById(includedTrainings, practExamToGainAccessRole.NomValueId, documentRoleIds, this.nomRepository);
            var accessOrdersToWorkAlone = Utils.GetTrainingsById(includedTrainings, accessOrderWorkAloneRole.NomValueId, documentRoleIds, this.nomRepository);
            var RPcerts = Utils.GetTrainingsById(includedTrainings, RPcertRole.NomValueId, documentRoleIds, this.nomRepository);
            var bgLangCerts = Utils.GetLangCertsById(includedLangCerts, bgCertRole.NomValueId, documentRoleIds, this.nomRepository);
            var engLangCerts = Utils.GetLangCertsById(includedLangCerts, engCertRole.NomValueId, documentRoleIds, this.nomRepository);

            return new List<object>()
            {
                new 
                {
                    DOC = new
                    {
                        DOC_ROLE = string.Format("{0}. {1}", number++, LicenceDictionary.DocumentTitle["TheoreticalExamTransitionalEducation"]),
                        SUB_DOC = Utils.FillBlankData(theoreticalExamsTransitionalEducation, 1)
                    }
                },
                new 
                {
                    DOC = new
                    {
                        DOC_ROLE = string.Format("{0}. {1}", number++, LicenceDictionary.DocumentTitle["PracticalExamPriliminaryEducation"]),
                        SUB_DOC = Utils.FillBlankData(practicalExamsPreliminaryEducation, 1)
                    }
                },
                new 
                {
                    DOC = new
                    {
                        DOC_ROLE = string.Format("{0}. {1}", number++, LicenceDictionary.DocumentTitle["RPcert"]),
                        SUB_DOC = Utils.FillBlankData(RPcerts, 1)
                    }
                },
                new 
                {
                    DOC = new
                    {
                        DOC_ROLE = string.Format("{0}. {1}", number++, LicenceDictionary.DocumentTitle["AccessOrderPracticalEducation"]),
                        SUB_DOC = Utils.FillBlankData(accessOrdersPracticalEducation, 1)
                    }
                },
                new 
                {
                    DOC = new
                    {
                        DOC_ROLE = string.Format("{0}. {1}", number++, LicenceDictionary.DocumentTitle["PracticalExamToGainAccess"]),
                        SUB_DOC = Utils.FillBlankData(practicalExamsToGainAccess, 1)
                    }
                },
                new 
                {
                    DOC = new
                    {
                        DOC_ROLE = string.Format("{0}. {1}", number++, LicenceDictionary.DocumentTitle["AccessOrderWorkAlone"]),
                        SUB_DOC = Utils.FillBlankData(accessOrdersToWorkAlone, 1)
                    }
                },
                new 
                {
                    DOC = new
                    {
                        DOC_ROLE = string.Format("{0}. {1}", number++, LicenceDictionary.DocumentTitle["EngLangCert"]),
                        SUB_DOC = Utils.FillBlankData(engLangCerts, 1)
                    }
                },
                new 
                {
                    DOC = new
                    {
                        DOC_ROLE = string.Format("{0}. {1}", number++, LicenceDictionary.DocumentTitle["BgLangCert"]),
                        SUB_DOC = Utils.FillBlankData(bgLangCerts, 1)
                    }
                }
            };
        }
    }
}

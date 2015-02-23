﻿using System;
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
    public class Atcl1Licence : IDataGenerator
    {
        private ILotRepository lotRepository;
        private INomRepository nomRepository;
        private IFileRepository fileRepository;
        private ICaseTypeRepository caseTypeRepository;
        private int number;

        public Atcl1Licence(
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

        public string[] TemplateNames
        {
            get
            {
                return new string[] { "atcl1" };
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

            var licenceType = this.nomRepository.GetNomValue("licenceTypes", licence.LicenceType.NomValueId);
            var licenceCaCode = licenceType.TextContent.Get<string>("codeCA");
            var licenceNumber = string.Format(
                "BGR {0} - {1} - {2}",
                licenceType.Code,
                Utils.PadLicenceNumber(licence.LicenceNumber),
                personData.Lin);
            var placeOfBirth = personData.PlaceOfBirth;
            NomValue country = null;
            NomValue nationality = null;
            if (placeOfBirth != null)
            {
                country = this.nomRepository.GetNomValue("countries", placeOfBirth.ParentValueId.Value);
                nationality = this.nomRepository.GetNomValue("countries", personData.Country.NomValueId);
            }
            var address = string.Format(
                "{0}, {1}",
                personAddress.Settlement != null? personAddress.Settlement.Name : null,
                personAddress.Address);

            var documents = this.GetDocuments(licenceType.Code, includedTrainings, includedLangCerts);
            var langLevel = Utils.FillBlankData(Utils.GetLangCerts(includedLangCerts), 1);
            var langCertsInLEndorsments = this.GetLangCertsForEndosement(includedLangCerts);
            var lEndorsements = Utils.FillBlankData(this.GetEndorsements2(includedRatings, ratingEditions, false).Union(langCertsInLEndorsments).ToList(), 9);

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
                    FAMILY_BG = personData.LastName.ToUpper(),
                    FAMILY_TRANS = personData.LastNameAlt.ToUpper(),
                    FIRST_NAME_BG = personData.FirstName.ToUpper(),
                    FIRST_NAME_TRANS = personData.FirstNameAlt.ToUpper(),
                    SURNAME_BG = personData.MiddleName.ToUpper(),
                    SURNAME_TRANS = personData.MiddleNameAlt.ToUpper(),
                    DATE_OF_BIRTH = personData.DateOfBirth,
                    COUNTRY = country != null ? country.Name : null,
                    CITY = placeOfBirth.Name,
                    COUNTRY_EN = country != null ? country.NameAlt : null,
                    CITY_EN = placeOfBirth != null ? placeOfBirth.NameAlt : null,
                    ADDRESS = address,
                    ADDRESS_EN = string.Format(
                        "{0}, {1}",
                        personAddress.AddressAlt,
                        personAddress.Settlement != null ? personAddress.Settlement.Name : null),
                    NATIONALITY = nationality != null ? nationality.Name : null,
                    NATIONALITY_EN = nationality != null ? nationality.TextContent.Get<string>("nationalityCodeCA") : null,
                    L_LICENCE_PRIV = this.GetLicencePrivileges(),
                    L_RATINGS = this.GetRatings(includedRatings, ratingEditions),
                    ENDORSEMENT = Utils.FillBlankData(Utils.GetEndorsements(includedRatings, ratingEditions, this.lotRepository), 2),
                    L_LANG_LEVEL = langLevel,
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
                    T_ACTION = lastEdition.LicenceAction.Name.ToUpper(),
                    T_ISSUE_DATE = lastEdition.DocumentDateValidFrom,
                    T_DOCUMENTS = documents.Take(6),
                    T_DOCUMENTS2 = documents.Skip(6),
                    T_LANG_LEVEL_NO = number++,
                    T_LANG_LEVEL = langLevel,
                    T_MED_CERT = Utils.GetMedCerts(this.number++, includedMedicals, personData),
                    T_ACTIVE_NO = number++,
                    L_ENDORSEMENT = lEndorsements,
                    T_ENDORSEMENT = Utils.FillBlankData(this.GetEndorsements2(includedRatings, ratingEditions, true), 9),
                    L_ENDORSEMENT1 = Utils.FillBlankData(new List<object>(), 9)
                }
            };

            return json;
        }

        private List<object> GetLangCertsForEndosement(IEnumerable<PersonLangCertDO> includedLangCerts)
        {
            return includedLangCerts
                .Where(c => c.LangLevel != null)
                .Select(c => new
                {
                    AUTH = c.LangLevel.Name.ToUpper(),
                    VALID_DATE = c.DocumentDateValidTo.HasValue ? c.DocumentDateValidTo.Value.ToShortDateString() : "unlimited"

                }).ToList<object>();
        }

        private List<object> GetLicencePrivileges()
        {
            return new List<object>()
            {
                LicenceDictionary.LicencePrivilege["ATCLratings"]
            };
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
                    return new
                    {
                        NAME = g.Key,
                        DATE = g.Min(r => ratingEditions.Where(e => e.Content.RatingPartIndex == r.Part.Index).OrderBy(e => e.Content.Index).Last().Content.DocumentDateValidFrom)
                    };
                }).ToList<object>();

            return Utils.FillBlankData(result, 3);
        }

        private List<object> GetEndorsements2(IEnumerable<PartVersion<PersonRatingDO>> includedRatings, IEnumerable<PartVersion<PersonRatingEditionDO>> editions, bool withIssueDate)
        {
            int caseTypeId = this.caseTypeRepository.GetCaseType("ovd").GvaCaseTypeId;

            List<object> ratingEditions = new List<object>();
            foreach (var edition in editions)
            {
                var rating = includedRatings.Where(r => r.Part.Index == edition.Content.RatingPartIndex).Single();
                var firstRatingEdition = this.lotRepository.GetLotIndex(rating.Part.LotId)
                    .Index.GetParts<PersonRatingEditionDO>("ratingEditions")
                    .Where(epv => epv.Content.RatingPartIndex == rating.Part.Index)
                    .OrderByDescending(epv => epv.Content.Index)
                    .Last();

                var ratingTypes = rating.Content.RatingTypes.Count() == 0 ? string.Empty : string.Join(", ", rating.Content.RatingTypes.Select(rt => rt.Code));
                var ratingClass = rating.Content.RatingClass == null ? null : rating.Content.RatingClass.Code;
                var authorization = rating.Content.Authorization == null ? null : rating.Content.Authorization.Code;
                object result = null;
                if (withIssueDate)
                {
                    result = new
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
                    };
                }
                else
                {
                    result = new
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
                        VALID_DATE = edition.Content.DocumentDateValidTo
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
            bool hasRoles = LicenceDictionary.LicenceRole.TryGetValue(licenceTypeCode, out documentRoleCodes);

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

            var theoreticalExamsTransitionalEducation = Utils.GetTrainingsByCode(includedTrainings, theoreticalExamTransitionalEducationRole.Code, documentRoleCodes);
            var practicalExamsPreliminaryEducation = Utils.GetTrainingsByCode(includedTrainings, practicalExamPreliminaryEducationRole.Code, documentRoleCodes);
            var accessOrdersPracticalEducation = Utils.GetTrainingsByCode(includedTrainings, accessOrderPracticalEducationRole.Code, documentRoleCodes);
            var practicalExamsToGainAccess = Utils.GetTrainingsByCode(includedTrainings, practExamToGainAccessRole.Code, documentRoleCodes);
            var accessOrdersToWorkAlone = Utils.GetTrainingsByCode(includedTrainings, accessOrderWorkAloneRole.Code, documentRoleCodes);
            var RPcerts = Utils.GetTrainingsByCode(includedTrainings, RPcertRole.Code, documentRoleCodes);
            var bgLangCerts = Utils.GetLangCertsByCode(includedLangCerts, bgCertRole.Code, documentRoleCodes);
            var engLangCerts = Utils.GetLangCertsByCode(includedLangCerts, engCertRole.Code, documentRoleCodes);

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

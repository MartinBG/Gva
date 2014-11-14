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
    public class ControllerLicence : IDataGenerator
    {
        private static Dictionary<string, List<object>> licencePrivileges = new Dictionary<string, List<object>>
        {
            {
                "ATCL",
                new List<object>()
                {
                    LicenceDictionary.LicencePrivilege["ATCLratings"]
                }
            },
            {
                "SATCL",
                new List<object>()
                {
                    LicenceDictionary.LicencePrivilege["medCertClass3"],
                    LicenceDictionary.LicencePrivilege["ATCLstudent"]
                }
            }
        };

        private static Dictionary<string, List<object>> licenceAbbreviations = new Dictionary<string, List<object>>
        {
            {
                "ATCL",
                new List<object>()
                {
                    LicenceDictionary.LicenceAbbreviation["AFIS"],
                    LicenceDictionary.LicenceAbbreviation["ASM"],
                    LicenceDictionary.LicenceAbbreviation["ATFM"],
                    LicenceDictionary.LicenceAbbreviation["FDA"],
                    LicenceDictionary.LicenceAbbreviation["FIS"],
                    LicenceDictionary.LicenceAbbreviation["OJTI"],
                    LicenceDictionary.LicenceAbbreviation["SAR"],
                    LicenceDictionary.LicenceAbbreviation["SIMI"]
                }
            },
            {
                "SATCL",
                new List<object>()
                {
                    LicenceDictionary.LicenceAbbreviation["ACS"],
                    LicenceDictionary.LicenceAbbreviation["ADI"]
                }
            }
        };

        private ILotRepository lotRepository;
        private INomRepository nomRepository;
        private IFileRepository fileRepository;
        private ICaseTypeRepository caseTypeRepository;
        private int number;

        public ControllerLicence(
            ILotRepository lotRepository,
            INomRepository nomRepository,
            IFileRepository fileRepository,
            ICaseTypeRepository caseTypeRepository)
        {
            this.lotRepository = lotRepository;
            this.nomRepository = nomRepository;
            this.fileRepository = fileRepository;
            this.caseTypeRepository = caseTypeRepository;
            this.number = 1;
        }

        public string[] TemplateNames
        {
            get
            {
                return new string[] { "atcl1", "student_controller" };
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
                .Select(i => lot.Index.GetPart<PersonRatingDO>("ratings/" + i.Ind));
            var ratingEditions = lot.Index.GetParts<PersonRatingEditionDO>("ratingEditions");
            var includedTrainings = lastEdition.IncludedTrainings
                .Select(i => lot.Index.GetPart<PersonTrainingDO>("personDocumentTrainings/" + i).Content);
            var includedLangCerts = lastEdition.IncludedLangCerts
                .Select(i => lot.Index.GetPart<PersonLangCertDO>("personDocumentLangCertificates/" + i).Content);
            var includedMedicals = lastEdition.IncludedMedicals
                .Select(i => lot.Index.GetPart<PersonMedicalDO>("personDocumentMedicals/" + i).Content);

            var licenceType = this.nomRepository.GetNomValue("licenceTypes", licence.LicenceType.NomValueId);
            var licenceCaCode = licenceType.TextContent.Get<string>("codeCA");
            var licenceNumber = string.Format(
                "BGR. {0} - {1} - {2}",
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

            var documents = this.GetDocuments(licenceType.Code, includedTrainings);
            var langLevel = this.GetEngLevel(includedLangCerts);
            var endorsements2 = this.GetEndorsements2(includedRatings, ratingEditions);
            var abbreviations = this.GetAbbreviations(licenceType.Code);

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
                    COUNTRY = country != null? country.Name : null,
                    CITY = placeOfBirth.Name,
                    COUNTRY_EN = country != null ? country.NameAlt : null,
                    CITY_EN = placeOfBirth != null? placeOfBirth.NameAlt : null,
                    ADDRESS = address,
                    ADDRESS_EN = string.Format(
                        "{0}, {1}",
                        personAddress.AddressAlt,
                        personAddress.Settlement != null ? personAddress.Settlement.Name : null),
                    NATIONALITY = nationality != null ? nationality.Name : null,
                    NATIONALITY_EN = nationality != null ? nationality.TextContent.Get<string>("nationalityCodeCA") : null ,
                    L_LICENCE_PRIV = this.GetLicencePrivileges(licenceType.Code),
                    L_RATINGS = this.GetRatings(includedRatings, ratingEditions),
                    ENDORSEMENT = this.GetEndorsements(includedRatings, ratingEditions),
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
                    T_DOCUMENTS = documents.Take(documents.Length / 2),
                    T_DOCUMENTS2 = documents.Skip(documents.Length / 2),
                    T_LANG_LEVEL_NO = number++,
                    T_LANG_LEVEL = langLevel,
                    T_MED_CERT = this.GetMedCerts(includedMedicals, personData),
                    T_ACTIVE_NO = number++,
                    T_ENDORSEMENT = endorsements2,
                    L_ENDORSEMENT = endorsements2,
                    L_ENDORSEMENT1 = endorsements2,
                    L_ABBREVIATION1 = abbreviations,
                    L_ABBREVIATION2 = abbreviations,
                    L_ABBREVIATION = abbreviations
                }
            };

            return json;
        }

        private List<object> GetLicencePrivileges(string licenceTypeCode)
        {
            List<dynamic> privileges;
            if (licencePrivileges.TryGetValue(licenceTypeCode, out privileges))
            {
                return privileges
                    .OrderBy(p => p.NO)
                    .ToList<object>();
            }

            return new object[0].ToList();
        }

        private List<object> GetRatings(IEnumerable<PartVersion<PersonRatingDO>> includedRatings, IEnumerable<PartVersion<PersonRatingEditionDO>> ratingEditions)
        {
            var result = includedRatings
                .Where(r => r.Content.RatingClass != null || r.Content.RatingType != null)
                .GroupBy(r => string.Format(
                    "{0} {1}",
                    r.Content.RatingClass == null ? string.Empty : r.Content.RatingClass.Name,
                    r.Content.RatingType == null ? string.Empty : r.Content.RatingType.Name).Trim())
                .Select(g =>
                {
                    return new
                    {
                        NAME = g.Key,
                        DATE = g.Min(r => ratingEditions.Where(e => e.Content.RatingPartIndex == r.Part.Index).OrderBy(e => e.Content.Index).Last().Content.DocumentDateValidFrom)
                    };
                }).ToList<object>();

            result = Utils.FillBlankData(result, 3);
            return result;
        }

        private List<object> GetEndorsements(IEnumerable<PartVersion<PersonRatingDO>> includedRatings, IEnumerable<PartVersion<PersonRatingEditionDO>> ratingEditions)
        {
            var result = includedRatings
                .Where(r => r.Content.Authorization != null)
                .GroupBy(r => r.Content.Authorization.Name)
                .Select(g =>
                {
                    return new
                    {
                        NAME = g.Key,
                        DATE = g.Min(r => ratingEditions.Where(e => e.Content.RatingPartIndex == r.Part.Index).OrderBy(e => e.Content.Index).Last().Content.DocumentDateValidFrom)
                    };
                }).ToList<object>();

            result = Utils.FillBlankData(result, 3);
            return result;
        }

        private List<object> GetEndorsements2(IEnumerable<PartVersion<PersonRatingDO>> includedRatings, IEnumerable<PartVersion<PersonRatingEditionDO>> ratingEditions)
        {
            int caseTypeId = this.caseTypeRepository.GetCaseType("ovd").GvaCaseTypeId;
                
            var result = includedRatings
                .Where(r => this.fileRepository.GetFileReference(r.PartId, caseTypeId) != null)
                .Select(r =>
                {
                    {
                        var lastEdition = ratingEditions.Where(e => e.Content.RatingPartIndex == r.Part.Index).OrderBy(e => e.Content.Index).Last();
                        var ratingType = r.Content.RatingType == null ? null : r.Content.RatingType.Name;
                        var ratingClass = r.Content.RatingClass == null ? null : r.Content.RatingClass.Name;
                        var authorization = r.Content.Authorization == null ? null : r.Content.Authorization.Name;

                        return new
                        {
                            ICAO = r.Content.LocationIndicator == null ? null : r.Content.LocationIndicator.Name,
                            SECTOR = r.Content.Sector,
                            AUTH = string.IsNullOrEmpty(ratingClass) && string.IsNullOrEmpty(ratingType) ?
                                authorization :
                                string.Format(
                                    "{0} {1} {2}",
                                    ratingType,
                                    ratingClass,
                                    string.IsNullOrEmpty(authorization) ? string.Empty : " - " + authorization).Trim(),
                            ISSUE_DATE = lastEdition.Content.DocumentDateValidFrom,
                            VALID_DATE = lastEdition.Content.DocumentDateValidTo
                        };
                    }
                }).ToList<object>();

            result = Utils.FillBlankData(result, 9);

            return result;
        }

        private object GetEngLevel(IEnumerable<PersonLangCertDO> includedLangCerts)
        {
            var engCerts = includedLangCerts
                .Where(t => t.DocumentRole.Alias == "engCert" && t.LangLevel != null);

            if (engCerts.Count() == 0)
            {
                return null;
            }

            PersonLangCertDO result = new PersonLangCertDO();
            int currentSeqNumber = 0;
            foreach (var engCert in engCerts)
            {
                var engLevel = this.nomRepository.GetNomValue("langLevels", engCert.LangLevel.NomValueId);
                int? seqNumber = engLevel.TextContent.Get<int?>("seqNumber");
                if (!seqNumber.HasValue)
                {
                    continue;
                }

                if (currentSeqNumber < seqNumber)
                {
                    result = engCert;
                    currentSeqNumber = seqNumber.Value;
                }
                else if (currentSeqNumber == seqNumber &&
                    DateTime.Compare(result.DocumentDateValidFrom.Value, engCert.DocumentDateValidFrom.Value) < 0)
                {
                    result = engCert;
                }
            }

            return new
            {
                LEVEL = result.LangLevel.Name,
                ISSUE_DATE = result.DocumentDateValidFrom,
                VALID_DATE = result.DocumentDateValidTo
            };
        }

        private object[] GetDocuments(string licenceTypeCode, IEnumerable<PersonTrainingDO> includedTrainings)
        {
            string[] documentRoleCodes;
            bool hasRoles = LicenceDictionary.LicenceRole.TryGetValue(licenceTypeCode, out documentRoleCodes);

            if (!hasRoles)
            {
                return new object[0];
            }

            return includedTrainings
                .Where(t => t.Valid.Code == "Y" && documentRoleCodes.Contains(t.DocumentRole.Code))
                .OrderBy(t => t.DocumentDateValidFrom)
                .Select(t =>
                    new
                    {
                        DOC = new
                        {
                            DOC_ROLE = string.Format("{0}. {1}", number++, t.DocumentRole.Name),
                            SUB_DOC = new
                            {
                                DOC_TYPE = t.DocumentType.Name,
                                DOC_NO = t.DocumentNumber,
                                DATE = t.DocumentDateValidFrom,
                                DOC_PUBLISHER = t.DocumentPublisher
                            }
                        }
                    }).ToArray<object>();
        }

        private List<object> GetMedCerts(IEnumerable<PersonMedicalDO> includedMedicals, PersonDataDO personData)
        {
            return includedMedicals.Select(m =>
                new
                {
                    ORDER_NO = number++,
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
        }

        private IEnumerable<object> GetAbbreviations(string licenceTypeCode)
        {
            List<object> abbreviations;
            if (licenceAbbreviations.TryGetValue(licenceTypeCode, out abbreviations))
            {
                return abbreviations;
            }

            return new object[0].ToList();
        }
    }
}

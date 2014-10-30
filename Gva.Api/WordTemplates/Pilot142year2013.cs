using System;
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
    public class Pilot142year2013 : IDataGenerator
    {
        private static string publisherCaaCode = "BG";

        private static Dictionary<string, List<object>> licencePrivileges = new Dictionary<string, List<object>>
        {
            {
                "FCL/ATPA",
                new List<object>()
                {
                    LicenceDictionary.LicencePrivilege["medCert"],
                    LicenceDictionary.LicencePrivilege["photo"]
                }
            },
            {
                "FCL/CPA",
                new List<object>()
                {
                    LicenceDictionary.LicencePrivilege["medCert"],
                    LicenceDictionary.LicencePrivilege["photo"]
                }
            },
            {
                "FCL/ATPH",
                new List<object>()
                {
                    LicenceDictionary.LicencePrivilege["medCert"],
                    LicenceDictionary.LicencePrivilege["photo"]
                }
            }
        };

        private static Dictionary<string, List<object>> licenceAbbreviations = new Dictionary<string, List<object>>
        {
            {
                "FCL/ATPA",
                new List<object>()
                {
                    LicenceDictionary.LicenceAbbreviation["Aeroplane"],
                    LicenceDictionary.LicenceAbbreviation["ATPL"],
                    LicenceDictionary.LicenceAbbreviation["Co-pilot"],
                    LicenceDictionary.LicenceAbbreviation["CPL"],
                    LicenceDictionary.LicenceAbbreviation["CRI"],
                    LicenceDictionary.LicenceAbbreviation["flightInstr"],
                    LicenceDictionary.LicenceAbbreviation["instrumentRating"],
                    LicenceDictionary.LicenceAbbreviation["IRI"],
                    LicenceDictionary.LicenceAbbreviation["MEP"],
                    LicenceDictionary.LicenceAbbreviation["PIC"],
                    LicenceDictionary.LicenceAbbreviation["PPL"],
                    LicenceDictionary.LicenceAbbreviation["R/T"],
                    LicenceDictionary.LicenceAbbreviation["SEP"],
                    LicenceDictionary.LicenceAbbreviation["TMG"],
                    LicenceDictionary.LicenceAbbreviation["TRI"]
                }
            },
            {
                "FCL/ATPH",
                new List<object>()
                {
                    LicenceDictionary.LicenceAbbreviation["Aeroplane"],
                    LicenceDictionary.LicenceAbbreviation["ATPL"],
                    LicenceDictionary.LicenceAbbreviation["Co-pilot"],
                    LicenceDictionary.LicenceAbbreviation["CPL"],
                    LicenceDictionary.LicenceAbbreviation["CRI"],
                    LicenceDictionary.LicenceAbbreviation["flightInstr"],
                    LicenceDictionary.LicenceAbbreviation["instrumentRating"],
                    LicenceDictionary.LicenceAbbreviation["IRI"],
                    LicenceDictionary.LicenceAbbreviation["MEP"],
                    LicenceDictionary.LicenceAbbreviation["PIC"],
                    LicenceDictionary.LicenceAbbreviation["PPL"],
                    LicenceDictionary.LicenceAbbreviation["R/T"],
                    LicenceDictionary.LicenceAbbreviation["SEP"],
                    LicenceDictionary.LicenceAbbreviation["TMG"],
                    LicenceDictionary.LicenceAbbreviation["TRI"]
                }
            },
            {
                "FCL/CPA",
                new List<object>()
                {
                    LicenceDictionary.LicenceAbbreviation["Aeroplane"],
                    LicenceDictionary.LicenceAbbreviation["ATPL"],
                    LicenceDictionary.LicenceAbbreviation["Co-pilot"],
                    LicenceDictionary.LicenceAbbreviation["CPL"],
                    LicenceDictionary.LicenceAbbreviation["CRI"],
                    LicenceDictionary.LicenceAbbreviation["flightInstr"],
                    LicenceDictionary.LicenceAbbreviation["instrumentRating"],
                    LicenceDictionary.LicenceAbbreviation["IRI"],
                    LicenceDictionary.LicenceAbbreviation["MEP"],
                    LicenceDictionary.LicenceAbbreviation["PIC"],
                    LicenceDictionary.LicenceAbbreviation["PPL"],
                    LicenceDictionary.LicenceAbbreviation["R/T"],
                    LicenceDictionary.LicenceAbbreviation["SEP"],
                    LicenceDictionary.LicenceAbbreviation["TMG"],
                    LicenceDictionary.LicenceAbbreviation["TRI"]
                }
            }
        };

        private ILotRepository lotRepository;
        private INomRepository nomRepository;

        public Pilot142year2013(
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
                return new string[] { "Pilot142_2013" };
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
            var lastEdition = lot.Index.GetParts<PersonLicenceEditionDO>("licenceEditions")
                .Where(e => e.Content.LicencePartIndex == licencePart.Part.Index)
                .OrderBy(e => e.Content.Index)
                .Last()
                .Content;

            var includedTrainings = lastEdition.IncludedTrainings
                .Select(i => lot.Index.GetPart<PersonTrainingDO>("personDocumentTrainings/" + i).Content);
            var includedLangCerts = lastEdition.IncludedLangCerts
                .Select(i => lot.Index.GetPart<PersonLangCertDO>("personDocumentLangCertificates/" + i).Content);
            var includedRatings = lastEdition.IncludedRatings
                .Select(i => lot.Index.GetPart<PersonRatingDO>("ratings/" + i));
            var ratingEditions = lot.Index.GetParts<PersonRatingEditionDO>("ratingEditions");
            var includedLicences = lastEdition.IncludedLicences
                .Select(i => lot.Index.GetPart<PersonLicenceDO>("licences/" + i));
            var includedMedicals = lastEdition.IncludedMedicals
                .Select(i => lot.Index.GetPart<PersonMedicalDO>("personDocumentMedicals/" + i).Content);
            var includedExams = lastEdition.IncludedExams
                .Select(i => lot.Index.GetPart<PersonTrainingDO>("personDocumentTrainings/" + i).Content);

            var inspectorId = lastEdition.Inspector == null ? (int?)null : lastEdition.Inspector.NomValueId;
            object[] instructorData = new object[0];
            object[] examinerData = new object[0];
            if (inspectorId.HasValue)
            {
                var inspectorLot = this.lotRepository.GetLotIndex(inspectorId.Value);
                var inspectorRatings = inspectorLot.Index.GetParts<PersonRatingDO>("ratings");
                var inspectorRatingEditions = inspectorLot.Index.GetParts<PersonRatingEditionDO>("ratingEditions");

                instructorData = this.GetInstructorData(inspectorRatings, inspectorRatingEditions);
                examinerData = this.GetExaminerData(inspectorRatings, inspectorRatingEditions);
            }

            object instrNoEntries = instructorData.Length == 0 ?
                new
                {
                    LABEL = "Инструктори/Instructor",
                    NO_ENTRIES = "No Entries"
                } :
                null;

            object examinerNoData = examinerData.Length == 0 ?
                new
                {
                    LABEL = "Проверяващи/Examiner",
                    NO_ENTRIES = "No Entries"
                } :
                null;

            var licenceType = this.nomRepository.GetNomValue("licenceTypes", licence.LicenceType.NomValueId);
            var licenceCaCode = licenceType.TextContent.Get<string>("codeCA");
            var otherLicences = this.GetOtherLicences(licenceCaCode, lot, lastEdition, includedLicences);
            var rtoRating = this.GetRtoRating(includedRatings, ratingEditions);
            var engLevel = this.GetEngLevel(includedLangCerts);
            var limitations = this.GetLimitations(lastEdition, includedMedicals, includedExams);
            var ratings = this.GetRaitings(includedRatings, ratingEditions);
            var country = this.GetCountry(personAddress);
            var licenceNumber = string.Format(
                "BGR {0} - {1} - {2}",
                licenceType.Code,
                Utils.PadLicenceNumber(licence.LicenceNumber),
                personData.Lin);

            var json = new
            {
                root = new
                {
                    L_LICENCE_NO = licenceNumber,
                    L_LICENCE_HOLDER = this.GetPersonData(personData, personAddress),
                    COUNTRY_NAME_BG = country.Name,
                    COUNTRY_CODE = country.TextContent.Get<string>("nationalityCodeCA"),
                    ISSUE_DATE = lastEdition.DocumentDateValidFrom.Value,
                    OTHER_LICENCE = otherLicences,
                    T_DOCUMENTS = new object[0],
                    L_LICENCE_PRIV = this.GetLicencePrivileges(licence),
                    RTO_NOTES = rtoRating.Notes,
                    RTO_NOTES_EN = rtoRating.NotesAlt,
                    ENG_LEVEL = engLevel,
                    LIMITS = limitations,
                    T_RATING = ratings,
                    INSTR_DATA = instructorData.Select(id => new { INSTRUCTOR = id }),
                    INSTRUCTOR_NO_ENTRIES = instrNoEntries,
                    EXAMINER_DATA = examinerData.Select(ed => new { EXAMINER = ed }),
                    EXAMINER_NO_ENTRIES = examinerNoData,
                    T_LICENCE_HOLDER = Utils.GetLicenceHolder(personData, personAddress),
                    T_LICENCE_TYPE_NAME = licenceType.Name.ToLower(),
                    T_LICENCE_NO = licenceNumber,
                    T_ISSUE_DATE = lastEdition.DocumentDateValidFrom.Value,
                    OTHER_LICENCE2 = otherLicences,
                    T_MED_CERT = this.GetMedCerts(includedMedicals, personData),
                    T_DOCUMENTS2 = this.GetDocuments2(licence, includedTrainings),
                    L_RATING = ratings,
                    INSTR_DATA1 = instructorData.Select(id => new { INSTRUCTOR1 = id }),
                    INSTRUCTOR1_NO_ENTRIES = instrNoEntries,
                    RTO_NOTES2 = rtoRating.Notes,
                    RTO_NOTES2_EN = rtoRating.NotesAlt,
                    ENG_LEVEL1 = engLevel,
                    T_LIMITS = limitations,
                    EXAMINER_DATA1 = examinerData.Select(ed => new { EXAMINER1 = ed }),
                    EXAMINER1_NO_ENTRIES = examinerNoData,
                    L_ABBREVIATION = this.GetAbbreviations(licenceType.Code)
                }
            };

            return json;
        }

        private object GetPersonData(PersonDataDO personData, PersonAddressDO personAddress)
        {
            var placeOfBirth = personData.PlaceOfBirth;
            var country = this.nomRepository.GetNomValue("countries", placeOfBirth.ParentValueId.Value);

            return new
            {
                FAMILY_BG = personData.LastName.ToUpper(),
                FAMILY_TRANS = personData.LastNameAlt.ToUpper(),
                FIRST_NAME_BG = personData.FirstName.ToUpper(),
                FIRST_NAME_TRANS = personData.FirstNameAlt.ToUpper(),
                SURNAME_BG = personData.MiddleName.ToUpper(),
                SURNAME_TRANS = personData.MiddleNameAlt.ToUpper(),
                DATE_OF_BIRTH = personData.DateOfBirth,
                PLACE_OF_BIRTH = string.Format(
                    "{0} {1}",
                    country.Name,
                    placeOfBirth.Name),
                PLACE_OF_BIRTH_TRAN = string.Format(
                    "{0} {1}",
                    country.NameAlt,
                    placeOfBirth.NameAlt),
                ADDRESS = string.Format(
                    "{0}, {1}",
                    personAddress.Settlement.Name,
                    personAddress.Address),
                ADDRESS_TRANS = string.Format(
                    "{0}, {1}",
                    personAddress.AddressAlt,
                    personAddress.Settlement.NameAlt)
            };
        }

        private NomValue GetCountry(PersonAddressDO personAddress)
        {
            int? countryId = personAddress.Settlement.ParentValueId;

            NomValue country = countryId.HasValue ?
                this.nomRepository.GetNomValue("countries", countryId.Value) :
                new NomValue
                {
                    Name = null,
                    TextContentString = string.Empty
                };

            return country;
        }

        private List<object> GetLicencePrivileges(PersonLicenceDO licence)
        {
            List<dynamic> privileges;
            if (licencePrivileges.TryGetValue(licence.LicenceType.Code, out privileges))
            {
                return privileges
                    .OrderBy(p => p.NO)
                    .ToList<object>();
            }

            return new object[0].ToList();
        }

        private List<object> GetOtherLicences(
            string licenceCaCode,
            Lot lot,
            PersonLicenceEditionDO edition,
            IEnumerable<PartVersion<PersonLicenceDO>> includedLicences)
        {
            var otherLicences = new List<object>()
            {
                new
                {
                    LIC_NO = licenceCaCode,
                    ISSUE_DATE = edition.DocumentDateValidFrom,
                    C_CODE = publisherCaaCode
                }
            };

            otherLicences = otherLicences.Concat(includedLicences.Select(l =>
                {
                    var lastEdition = lot.Index.GetParts<PersonLicenceEditionDO>("licenceEditions")
                        .Where(e => e.Content.LicencePartIndex == l.Part.Index)
                        .OrderBy(e => e.Content.Index)
                        .Last()
                        .Content;

                    return new
                    {
                        LIC_NO = this.nomRepository.GetNomValue("licenceTypes", l.Content.LicenceType.NomValueId).TextContent.Get<string>("codeCA"),
                        ISSUE_DATE = lastEdition.DocumentDateValidFrom,
                        C_CODE = publisherCaaCode
                    };
                })).ToList();

            return otherLicences;
        }

        private PersonRatingEditionDO GetRtoRating(IEnumerable<PartVersion<PersonRatingDO>> includedRatings, IEnumerable<PartVersion<PersonRatingEditionDO>> ratingEditions)
        {
            var rtoRatingPart = includedRatings.FirstOrDefault(r => r.Content.Authorization != null && r.Content.Authorization.Code == "RTO");
            PersonRatingEditionDO rtoRatingEd = new PersonRatingEditionDO();

            if (rtoRatingPart != null)
            {
                rtoRatingEd = ratingEditions.Where(e => e.Content.RatingPartIndex == rtoRatingPart.Part.Index).OrderBy(e => e.Content.Index).Last().Content;
            }

            return rtoRatingEd;
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

        private List<object> GetLimitations(
            PersonLicenceEditionDO edition,
            IEnumerable<PersonMedicalDO> includedMedicals,
            IEnumerable<PersonTrainingDO> includedExams)
        {
            if (edition.Limitations == null)
            {
                return new List<object>();
            }

            var limitations = edition.Limitations.Select(l => new { LIMIT_NAME = l.Name });

            limitations = limitations.Union(includedMedicals
                .Where(m => m.Limitations.Count > 0)
                .SelectMany(m => m.Limitations)
                .Select(l => new { LIMIT_NAME = l.Name }));

            limitations = limitations.Union(includedExams
                .Select(e => new
                {
                    LIMIT_NAME = string.Format(
                        "{0} {1} {2}",
                        e.DocumentNumber,
                        e.DocumentDateValidFrom.Value.ToString("dd.MM.yyyy"),
                        e.DocumentDateValidTo.HasValue ? e.DocumentDateValidTo.Value.ToString("dd.MM.yyyy") : null).Trim()
                }));

            return limitations.ToList<object>();
        }

        private List<object> GetRaitings(IEnumerable<PartVersion<PersonRatingDO>> includedRatings, IEnumerable<PartVersion<PersonRatingEditionDO>> ratingEditions)
        {
            var authorizationGroupIds = this.nomRepository.GetNomValues("authorizationGroups")
                .Where(nv => nv.Code == "FT" || nv.Code == "FC")
                .Select(nv => nv.NomValueId);

            return includedRatings
                .Where(r =>
                    r.Content.Authorization != null &&
                    r.Content.Authorization.Code != "RTO" &&
                    !authorizationGroupIds.Contains(r.Content.Authorization.ParentValueId.Value))
                .Select(r =>
                    {
                        var lastEdition = ratingEditions.Where(e => e.Content.RatingPartIndex == r.Part.Index).OrderBy(e => e.Content.Index).Last();

                        return new
                        {
                            TYPE = string.Format(
                                "{0} {1}",
                                r.Content.RatingClass == null ? string.Empty : r.Content.RatingClass.Name,
                                r.Content.RatingType == null ? string.Empty : r.Content.RatingType.Name).Trim(),
                            AUTH_NOTES = string.Format(
                                "{0} {1}",
                                r.Content.Authorization.Name,
                                lastEdition.Content.NotesAlt).Trim(),
                            VALID_DATE = lastEdition.Content.DocumentDateValidTo
                        };
                    }).ToList<object>();
        }

        private object[] GetInstructorData(IEnumerable<PartVersion<PersonRatingDO>> inspectorRatings, IEnumerable<PartVersion<PersonRatingEditionDO>> inspectorRatingEditions)
        {
            var authorizationGroup = this.nomRepository.GetNomValues("authorizationGroups")
                .First(nv => nv.Code == "FT");

            return inspectorRatings
                .Where(p =>
                    p.Content.Authorization != null &&
                    p.Content.Authorization.Code != "RTO" &&
                    p.Content.Authorization.ParentValueId == authorizationGroup.NomValueId)
                .Select(p =>
                    {
                        var instrRatingEdPart = inspectorRatingEditions.Where(e => e.Content.RatingPartIndex == p.Part.Index).OrderBy(e => e.Content.Index).Last();
                        return new
                        {
                            TYPE = string.Format(
                                "{0} {1} {2}",
                                p.Content.RatingClass == null ? string.Empty : p.Content.RatingClass.Code,
                                p.Content.RatingType == null ? string.Empty : p.Content.RatingType.Code,
                                p.Content.Authorization.Code).Trim(),
                            VALID_DATE = instrRatingEdPart.Content.DocumentDateValidFrom,
                            AUTH_NOTES = instrRatingEdPart.Content.NotesAlt
                        };
                    }).ToArray<object>();
        }

        private object[] GetExaminerData(IEnumerable<PartVersion<PersonRatingDO>> inspectorRatings, IEnumerable<PartVersion<PersonRatingEditionDO>> inspectorRatingEditions)
        {
            var authorizationGroup = this.nomRepository.GetNomValues("authorizationGroups")
                .First(nv => nv.Code == "FC");

            return inspectorRatings
                .Where(p =>
                    p.Content.Authorization != null &&
                    p.Content.Authorization.Code != "RTO" &&
                    p.Content.Authorization.ParentValueId == authorizationGroup.NomValueId)
                .Select(p =>
                    {
                        var exRatingEdPart = inspectorRatingEditions.Where(e => e.Content.RatingPartIndex == p.Part.Index).OrderBy(e => e.Content.Index).Last();

                        return new
                        {
                            TYPE = string.Format(
                                "{0} {1} {2}",
                                p.Content.RatingClass == null ? string.Empty : p.Content.RatingClass.Code,
                                p.Content.RatingType == null ? string.Empty : p.Content.RatingType.Code,
                                p.Content.Authorization.Code).Trim(),
                            VALID_DATE = exRatingEdPart.Content.DocumentDateValidFrom,
                            AUTH_NOTES = exRatingEdPart.Content.NotesAlt
                        };
                    }).ToArray<object>();
        }

        private List<object> GetMedCerts(IEnumerable<PersonMedicalDO> includedMedicals, PersonDataDO personData)
        {
            var result = includedMedicals.Select(m =>
                new
                {
                    ORDER_NO = "10",
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
            return result;
        }

        private List<object> GetDocuments2(PersonLicenceDO licence, IEnumerable<PersonTrainingDO> includedTrainings)
        {
            string[] documentRoleCodes;
            bool hasRoles = LicenceDictionary.LicenceRole.TryGetValue(
                licence.LicenceType.Code,
                out documentRoleCodes);

            if (!hasRoles)
            {
                return null;
            }

            return includedTrainings
                .Where(t => documentRoleCodes.Contains(t.DocumentRole.Code))
                .OrderBy(t => t.DocumentDateValidFrom.Value)
                .Select(t =>
                    new
                    {
                        DOC = new
                        {
                            DOC_ROLE = t.DocumentRole.Name,
                            SUB_DOC = new
                            {
                                DOC_TYPE = t.DocumentType.Name,
                                DOC_NO = t.DocumentNumber,
                                DATE = t.DocumentDateValidFrom,
                                DOC_PUBLISHER = t.DocumentPublisher
                            }
                        }
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

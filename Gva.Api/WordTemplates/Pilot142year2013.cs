using System;
using System.Collections.Generic;
using System.Linq;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            var personData = lot.Index.GetPart("personData").Content;
            var personAddressPart = lot.Index.GetParts("personAddresses")
               .FirstOrDefault(a => a.Content.Get<string>("valid.code") == "Y");
            var personAddress = personAddressPart == null ?
                new JObject() :
                personAddressPart.Content;
            var licence = lot.Index.GetPart(path).Content;
            var lastEdition = licence.GetItems<JObject>("editions").Last();

            var includedTrainings = lastEdition.GetItems<int>("includedTrainings")
                .Select(i => lot.Index.GetPart("personDocumentTrainings/" + i).Content);
            var includedRatings = lastEdition.GetItems<int>("includedRatings")
                .Select(i => lot.Index.GetPart("ratings/" + i).Content);
            var includedLicences = lastEdition.GetItems<int>("includedLicences")
                .Select(i => lot.Index.GetPart("licences/" + i).Content);
            var includedMedicals = lastEdition.GetItems<int>("includedMedicals")
                .Select(i => lot.Index.GetPart("personDocumentMedicals/" + i).Content);
            var includedExams = lastEdition.GetItems<int>("includedExams")
                .Select(i => lot.Index.GetPart("personDocumentExams/" + i).Content);

            var inspectorId = lastEdition.Get<int?>("inspector.nomValueId");
            object[] instructorData = new object[0];
            object[] examinerData = new object[0];
            if (inspectorId.HasValue)
            {
                var inspectorRatings = this.lotRepository.GetLotIndex(inspectorId.Value)
                    .Index.GetParts("ratings");

                instructorData = this.GetInstructorData(inspectorRatings);
                examinerData = this.GetExaminerData(inspectorRatings);
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

            var licenceType = this.nomRepository.GetNomValue("licenceTypes", licence.Get<int>("licenceType.nomValueId"));
            var licenceCaCode = licenceType.TextContent.Get<string>("codeCA");
            var otherLicences = this.GetOtherLicences(licenceCaCode, lastEdition, includedLicences);
            var rtoRating = this.GetRtoRating(includedRatings);
            var engLevel = this.GetEngLevel(includedTrainings);
            var limitations = this.GetLimitations(lastEdition, includedMedicals, includedExams);
            var ratings = this.GetRaitings(includedRatings);
            var country = this.GetCountry(personAddress);
            var licenceNumber = string.Format(
                "BGR {0} - {1} - {2}",
                licenceType.Code,
                licence.Get<string>("licenceNumber"),
                personData.Get<string>("lin"));

            var json = new
            {
                root = new
                {
                    L_LICENCE_NO = licenceNumber,
                    L_LICENCE_HOLDER = this.GetPersonData(personData, personAddress),
                    COUNTRY_NAME_BG = country.Name,
                    COUNTRY_CODE = country.TextContent.Get<string>("nationalityCodeCA"),
                    ISSUE_DATE = lastEdition.Get<DateTime>("documentDateValidFrom"),
                    OTHER_LICENCE = otherLicences,
                    T_DOCUMENTS = new object[0],
                    L_LICENCE_PRIV = this.GetLicencePrivileges(licence),
                    RTO_NOTES = rtoRating.Get<string>("notes"),
                    RTO_NOTES_EN = rtoRating.Get<string>("notesAlt"),
                    ENG_LEVEL = engLevel,
                    LIMITS = limitations,
                    T_RATING = ratings,
                    INSTR_DATA = instructorData.Select(id => new { INSTRUCTOR = id }),
                    INSTRUCTOR_NO_ENTRIES = instrNoEntries,
                    EXAMINER_DATA = examinerData.Select(ed => new { EXAMINER = ed }),
                    EXAMINER_NO_ENTRIES = examinerNoData,
                    T_LICENCE_HOLDER = this.GetLicenceHolder(personData, personAddress),
                    T_LICENCE_TYPE_NAME = licenceType.Name.ToLower(),
                    T_LICENCE_NO = licenceNumber,
                    T_ISSUE_DATE = lastEdition.Get<DateTime>("documentDateValidFrom"),
                    OTHER_LICENCE2 = otherLicences,
                    T_MED_CERT = this.GetMedCerts(includedMedicals, personData),
                    T_DOCUMENTS2 = this.GetDocuments2(licence, includedTrainings),
                    L_RATING = ratings,
                    INSTR_DATA1 = instructorData.Select(id => new { INSTRUCTOR1 = id }),
                    INSTRUCTOR1_NO_ENTRIES = instrNoEntries,
                    RTO_NOTES2 = rtoRating.Get<string>("notes"),
                    RTO_NOTES2_EN = rtoRating.Get<string>("notesAlt"),
                    ENG_LEVEL1 = engLevel,
                    T_LIMITS = limitations,
                    EXAMINER_DATA1 = examinerData.Select(ed => new { EXAMINER1 = ed }),
                    EXAMINER1_NO_ENTRIES = examinerNoData,
                    L_ABBREVIATION = this.GetAbbreviations(licenceType.Code)
                }
            };

            return json;
        }

        private object GetPersonData(JObject personData, JObject personAddress)
        {
            var placeOfBirth = personData.Get<NomValue>("placeOfBirth");
            var country = this.nomRepository.GetNomValue("countries", placeOfBirth.ParentValueId.Value);

            return new
            {
                FAMILY_BG = personData.Get<string>("lastName").ToUpper(),
                FAMILY_TRANS = personData.Get<string>("lastNameAlt").ToUpper(),
                FIRST_NAME_BG = personData.Get<string>("firstName").ToUpper(),
                FIRST_NAME_TRANS = personData.Get<string>("firstNameAlt").ToUpper(),
                SURNAME_BG = personData.Get<string>("middleName").ToUpper(),
                SURNAME_TRANS = personData.Get<string>("middleNameAlt").ToUpper(),
                DATE_OF_BIRTH = personData.Get<DateTime>("dateOfBirth"),
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
                    personAddress.Get<string>("settlement.name"),
                    personAddress.Get<string>("address")),
                ADDRESS_TRANS = string.Format(
                    "{0}, {1}",
                    personAddress.Get<string>("addressAlt"),
                    personAddress.Get<string>("settlement.nameAlt"))
            };
        }

        private NomValue GetCountry(JObject personAddress)
        {
            int? countryId = personAddress.Get<int?>("settlement.parentValueId");
            NomValue country = countryId.HasValue ?
                this.nomRepository.GetNomValue("countries", countryId.Value) :
                new NomValue
                {
                    Name = null,
                    TextContentString = string.Empty
                };

            return country;
        }

        private List<object> GetLicencePrivileges(JObject licence)
        {
            List<dynamic> privileges;
            if (licencePrivileges.TryGetValue(licence.Get<string>("licenceType.code"), out privileges))
            {
                return privileges
                    .OrderBy(p => p.NO)
                    .ToList<object>();
            }

            return new object[0].ToList();
        }

        private List<object> GetOtherLicences(
            string licenceCaCode,
            JObject edition,
            IEnumerable<JObject> includedLicences)
        {
            var otherLicences = new List<object>()
            {
                new
                {
                    LIC_NO = licenceCaCode,
                    ISSUE_DATE = edition.Get<DateTime>("documentDateValidFrom"),
                    C_CODE = publisherCaaCode
                }
            };

            otherLicences = otherLicences.Concat(includedLicences.Select(l =>
                new
                {
                    LIC_NO = this.nomRepository.GetNomValue("licenceTypes", l.Get<int>("licenceType.nomValueId")).TextContent.Get<string>("codeCA"),
                    ISSUE_DATE = l.GetItems<JObject>("editions")
                        .LastOrDefault()
                        .Get<DateTime>("documentDateValidFrom"),
                    C_CODE = publisherCaaCode
                }))
                .ToList();

            return otherLicences;
        }

        private JObject GetRtoRating(IEnumerable<JObject> includedRatings)
        {
            var rtoRatingPart = includedRatings.FirstOrDefault(r => r.Get<string>("authorization.code") == "RTO");
            JObject rtoRatingEd = new JObject();

            if (rtoRatingPart != null)
            {
                rtoRatingEd = rtoRatingPart.GetItems<JObject>("editions").Last();
            }

            return rtoRatingEd;
        }

        private object GetEngLevel(IEnumerable<JObject> includedTrainings)
        {
            var engTrainings = includedTrainings
                .Where(t => t.Get<string>("documentRole.alias") == "engTraining");

            JObject result = new JObject();
            int currentSeqNumber = 0;
            foreach (var engTraining in engTrainings)
            {
                int? engLangLevelId = engTraining.Get<int>("engLangLevel.nomValueId");
                if (!engLangLevelId.HasValue)
                {
                    continue;
                }

                var engLevel = this.nomRepository.GetNomValue("engLangLevels", engLangLevelId.Value);
                int? seqNumber = engLevel.TextContent.Get<int?>("seqNumber");
                if (!seqNumber.HasValue)
                {
                    continue;
                }

                if (currentSeqNumber < seqNumber)
                {
                    result = engTraining;
                    currentSeqNumber = seqNumber.Value;
                }
                else if (currentSeqNumber == seqNumber &&
                    DateTime.Compare(result.Get<DateTime>("documentDateValidFrom"), engTraining.Get<DateTime>("documentDateValidFrom")) < 0) {
                    result = engTraining;
                }
            }

            return new
                {
                    LEVEL = result.Get<string>("engLangLevel.name"),
                    ISSUE_DATE = result.Get<DateTime?>("documentDateValidFrom"),
                    VALID_DATE = result.Get<DateTime?>("documentDateValidTo")
                };
        }

        private List<object> GetLimitations(
            JObject edition,
            IEnumerable<JObject> includedMedicals,
            IEnumerable<JObject> includedExams)
        {
            var limitations = edition.GetItems<NomValue>("limitations")
                .Select(l => new { LIMIT_NAME = l.Name });

            limitations = limitations.Concat(includedMedicals
                .SelectMany(m => m.GetItems<JObject>("limitations"))
                .Select(l => new { LIMIT_NAME = l.Get<string>("name") }));

            limitations = limitations.Concat(includedExams
                .Select(e => new
                {
                    LIMIT_NAME = string.Format(
                        "{0} {1} {2}",
                        e.Get<string>("documentNumber"),
                        e.Get<string>("documentDateValidFrom"),
                        e.Get<string>("documentDateValidTo"))
                }));

            return limitations.ToList<object>();
        }

        private List<object> GetRaitings(IEnumerable<JObject> includedRatings)
        {
            var authorizationGroupIds = this.nomRepository.GetNomValues("authorizationGroups")
                .Where(nv => nv.Code == "FT" || nv.Code == "FC")
                .Select(nv => nv.NomValueId);

            return includedRatings
                .Where(r => r.Get<string>("authorization.code") != "RTO" &&
                        !authorizationGroupIds.Contains(r.Get<int>("authorization.parentValueId")))
                .Select(r =>
                    {
                        JObject lastEdition = r.GetItems<JObject>("editions").Last();

                        return new
                        {
                            TYPE = string.Format(
                                "{0} {1}",
                                r.Get<string>("ratingClass.name"),
                                r.Get<string>("ratingType.name")),
                            AUTH_NOTES = string.Format(
                                "{0} {1}",
                                r.Get<string>("authorization.name"),
                                lastEdition.Get<string>("notesAlt")),
                            VALID_DATE = lastEdition.Get<DateTime>("documentDateValidTo")
                        };
                    }).ToList<object>();
        }

        private object[] GetInstructorData(IEnumerable<PartVersion> inspectorRatings)
        {
            var authorizationGroup = this.nomRepository.GetNomValues("authorizationGroups")
                .First(nv => nv.Code == "FT");

            return inspectorRatings
                .Where(
                    p => p.Content.Get<string>("authorization.code") != "RTO" &&
                    p.Content.Get<int>("authorization.parentValueId") == authorizationGroup.NomValueId)
                .Select(p =>
                    {
                        var instrRatingEdPart = p.Content.GetItems<JObject>("editions").Last();
                        return new
                        {
                            TYPE = string.Format(
                                "{0} {1} {2}",
                                p.Content.Get<string>("ratingClass.code"),
                                p.Content.Get<string>("ratingType.code"),
                                p.Content.Get<string>("authorization.code")).Trim(),
                            VALID_DATE = instrRatingEdPart.Get<DateTime>("documentDateValidFrom"),
                            AUTH_NOTES = instrRatingEdPart.Get<string>("notesAlt")
                        };
                    }).ToArray<object>();
        }

        private object[] GetExaminerData(IEnumerable<PartVersion> inspectorRatings)
        {
            var authorizationGroup = this.nomRepository.GetNomValues("authorizationGroups")
                .First(nv => nv.Code == "FC");

            return inspectorRatings
                .Where(
                    p => p.Content.Get<string>("authorization.code") != "RTO" &&
                    p.Content.Get<int>("authorization.parentValueId") == authorizationGroup.NomValueId)
                .Select(p =>
                    {
                        var exRatingEdPart = p.Content.GetItems<JObject>("editions").Last();

                        return new
                        {
                            TYPE = string.Format(
                                "{0} {1} {2}",
                                p.Content.Get<string>("ratingClass.code"),
                                p.Content.Get<string>("ratingType.code"),
                                p.Content.Get<string>("authorization.code")).Trim(),
                            VALID_DATE = exRatingEdPart.Get<DateTime>("documentDateValidFrom"),
                            AUTH_NOTES = exRatingEdPart.Get<string>("notesAlt")
                        };
                    }).ToArray<object>();
        }

        private object GetLicenceHolder(JObject personData, JObject personAddress)
        {
            return new
                {
                    NAME = string.Format(
                        "{0} {1} {2}",
                        personData.Get<string>("firstName"),
                        personData.Get<string>("middleName"),
                        personData.Get<string>("lastName")).ToUpper(),
                    LIN = personData.Get<string>("lin"),
                    EGN = personData.Get<string>("uin"),
                    ADDRESS = string.Format(
                        "{0}, {1}",
                        personAddress.Get<string>("settlement.name"),
                        personAddress.Get<string>("address")),
                    TELEPHONE = personData.Get<string>("phone1") ??
                                personData.Get<string>("phone2") ??
                                personData.Get<string>("phone3") ??
                                personData.Get<string>("phone4") ??
                                personData.Get<string>("phone5")
                };
        }

        private List<object> GetMedCerts(IEnumerable<JObject> includedMedicals, JObject personData)
        {
            var result = includedMedicals.Select(m =>
                new
                {
                    ORDER_NO = "10",
                    NO = string.Format(
                        "{0}-{1}-{2}-{3}",
                        m.Get<string>("documentNumberPrefix"),
                        m.Get<string>("documentNumber"),
                        personData.Get<string>("lin"),
                        m.Get<string>("documentNumberSuffix")),
                    ISSUE_DATE = m.Get<DateTime>("documentDateValidFrom"),
                    VALID_DATE = m.Get<DateTime>("documentDateValidTo"),
                    CLASS = m.Get<string>("medClass.name"),
                    PUBLISHER = m.Get<string>("documentPublisher.name"),
                    LIMITATION = string.Join(",", m.GetItems<JObject>("limitations").Select(l => l.Get<string>("name")))
                }).ToList<object>();
            return result;
        }

        private List<object> GetDocuments2(JObject licence, IEnumerable<JObject> includedTrainings)
        {
            string[] documentRoleCodes;
            bool hasRoles = LicenceDictionary.LicenceRole.TryGetValue(
                licence.Get<string>("licenceType.code"),
                out documentRoleCodes);

            if (!hasRoles)
            {
                return null;
            }

            return includedTrainings
                .Where(t => documentRoleCodes.Contains(t.Get<string>("documentRole.code")))
                .OrderBy(t => t.Get<DateTime>("documentDateValidFrom"))
                .Select(t =>
                    new
                    {
                        DOC = new
                        {
                            DOC_ROLE = t.Get<string>("documentRole.name"),
                            SUB_DOC = new
                            {
                                DOC_TYPE = t.Get<string>("documentType.name"),
                                DOC_NO = t.Get<string>("documentNumber"),
                                DATE = t.Get<DateTime>("documentDateValidFrom"),
                                DOC_PUBLISHER = t.Get<string>("documentPublisher")
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

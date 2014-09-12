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
    public class Pilot142 : IDataGenerator
    {
        private static string publisherCaaCode = "BG";

        private ILotRepository lotRepository;
        private INomRepository nomRepository;

        public Pilot142(
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
                return new string[] { "Pilot142" };
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

            var licencePart = lot.Index.GetPart(path);
            var licence = licencePart.Content;
            var editions = lot.Index.GetParts("licenceEditions")
                .Where(e => e.Content.Get<int>("licencePartIndex") == licencePart.Part.Index)
                .OrderBy(e => e.Content.Get<int>("index"))
                .Select(e => e.Content);

            var firstEdition = editions.First();
            var lastEdition = editions.Last();

            var includedTrainings = lastEdition.GetItems<int>("includedTrainings")
                .Select(i => lot.Index.GetPart("personDocumentTrainings/" + i).Content);
            var includedRatings = lastEdition.GetItems<int>("includedRatings")
                .Select(i => lot.Index.GetPart("ratings/" + i).Content);
            var includedLicences = lastEdition.GetItems<int>("includedLicences")
                .Select(i => lot.Index.GetPart("licences/" + i).Content);
            var includedMedicals = lastEdition.GetItems<int>("includedMedicals")
                .Select(i => lot.Index.GetPart("personDocumentMedicals/" + i).Content);

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

            var licenceType = this.nomRepository.GetNomValue("licenceTypes", licence.Get<int>("licenceType.nomValueId"));
            var licenceCaCode = licenceType.TextContent.Get<string>("codeCA");
            var otherLicences = this.GetOtherLicences(licenceCaCode, lastEdition, includedLicences);
            var rtoRating = this.GetRtoRating(includedRatings);
            var engLevel = this.GetEngLevel(includedTrainings);
            var ratings = this.GetRaitings(includedRatings);
            var country = this.GetCountry(personAddress);
            var licenceNumber = string.Format(
                "BG {0} - {1} - {2}",
                licenceType.Code,
                licence.Get<string>("licenceNumber"),
                personData.Get<string>("lin"));
            var documents = this.GetDocuments(licence, includedTrainings);
            var nationality = this.nomRepository.GetNomValue("countries", personData.Get<int>("country.nomValueId"));

            var json = new
            {
                root = new
                {
                    L_LICENCE_NO = licenceNumber,
                    L_LICENCE_HOLDER = this.GetPersonData(personData, personAddress),
                    COUNTRY_NAME_BG = country.Name,
                    COUNTRY_CODE = nationality.TextContent.Get<string>("nationalityCodeCA"),
                    ISSUE_DATE = lastEdition.Get<DateTime>("documentDateValidFrom"),
                    OTHER_LICENCE = otherLicences,
                    L_LICENCE_PRIV = this.GetLicencePrivileges(licence),
                    RTO_NOTES = rtoRating.Get<string>("notes"),
                    RTO_NOTES_EN = rtoRating.Get<string>("notesAlt"),
                    ENG_LEVEL = engLevel,
                    T_RATING = ratings,
                    INSTRUCTOR = instructorData,
                    EXAMINER = examinerData,
                    T_LICENCE_HOLDER = this.GetLicenceHolder(personData, personAddress),
                    T_LICENCE_TYPE_NAME = licenceType.Name.ToLower(),
                    T_LICENCE_NO = licenceNumber,
                    T_FIRST_ISSUE_DATE = firstEdition.Get<DateTime>("documentDateValidFrom"),
                    T_VALID_DATE = lastEdition.Get<DateTime>("documentDateValidTo"),
                    T_ACTION = lastEdition.Get<string>("licenceAction.name"),
                    T_ISSUE_DATE = lastEdition.Get<DateTime>("documentDateValidFrom"),
                    OTHER_LICENCE2 = otherLicences,
                    T_DOCUMENTS = documents.Take(documents.Length / 2),
                    T_MED_CERT = this.GetMedCerts(includedMedicals, personData),
                    T_DOCUMENTS2 = documents.Skip(documents.Length / 2),
                    RTO_NOTES2 = rtoRating.Get<string>("notes"),
                    RTO_NOTES2_EN = rtoRating.Get<string>("notesAlt"),
                    ENG_LEVEL1 = engLevel,
                    L_RATING = ratings,
                    INSTRUCTOR1 = instructorData,
                    EXAMINER1 = examinerData,
                    REVAL = new object[0],
                    REVAL2 = new object[0],
                    REVAL3 = new object[0],
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
            return new List<object>()
            {
                LicenceDictionary.LicencePrivilege["medCert"],
                LicenceDictionary.LicencePrivilege["photo"]
            };
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

        private object[] GetDocuments(JObject licence, IEnumerable<JObject> includedTrainings)
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
                    }).ToArray<object>();
        }

        private IEnumerable<object> GetAbbreviations(string licenceTypeCode)
        {
            return new List<object>()
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
                    LicenceDictionary.LicenceAbbreviation["TRI"],
                };
        }
    }
}

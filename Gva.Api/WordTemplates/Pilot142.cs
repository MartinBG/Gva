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

            var includedTrainings = lastEdition.IncludedTrainings
                .Select(i => lot.Index.GetPart<PersonTrainingDO>("personDocumentTrainings/" + i).Content);
            var includedRatings = lastEdition.IncludedRatings
                .Select(i => lot.Index.GetPart<PersonRatingDO>("ratings/" + i).Content);
            var includedLicences = lastEdition.IncludedLicences
                .Select(i => lot.Index.GetPart<PersonLicenceDO>("licences/" + i));
            var includedMedicals = lastEdition.IncludedMedicals
                .Select(i => lot.Index.GetPart<PersonMedicalDO>("personDocumentMedicals/" + i).Content);

            var inspectorId = lastEdition.Inspector == null ? (int?)null : lastEdition.Inspector.NomValueId;

            object[] instructorData = new object[0];
            object[] examinerData = new object[0];
            if (inspectorId.HasValue)
            {
                var inspectorRatings = this.lotRepository.GetLotIndex(inspectorId.Value)
                    .Index.GetParts<PersonRatingDO>("ratings")
                    .Select(r => r.Content);

                instructorData = this.GetInstructorData(inspectorRatings);
                examinerData = this.GetExaminerData(inspectorRatings);
            }

            var licenceType = this.nomRepository.GetNomValue("licenceTypes", licence.LicenceType.NomValueId);
            var licenceCaCode = licenceType.TextContent.Get<string>("codeCA");
            var otherLicences = this.GetOtherLicences(licenceCaCode, lot, lastEdition, includedLicences);
            var rtoRating = this.GetRtoRating(includedRatings);
            var engLevel = this.GetEngLevel(includedTrainings);
            var ratings = this.GetRaitings(includedRatings);
            var country = this.GetCountry(personAddress);
            var licenceNumber = string.Format(
                "BG {0} - {1} - {2}",
                licenceType.Code,
                Utils.PadLicenceNumber(licence.LicenceNumber),
                personData.Lin);
            var documents = this.GetDocuments(licence, includedTrainings);
            var nationality = this.nomRepository.GetNomValue("countries", personData.Country.NomValueId);

            var json = new
            {
                root = new
                {
                    L_LICENCE_NO = licenceNumber,
                    L_LICENCE_HOLDER = this.GetPersonData(personData, personAddress),
                    COUNTRY_NAME_BG = country.Name,
                    COUNTRY_CODE = nationality.TextContent.Get<string>("nationalityCodeCA"),
                    ISSUE_DATE = lastEdition.DocumentDateValidFrom,
                    OTHER_LICENCE = otherLicences,
                    L_LICENCE_PRIV = this.GetLicencePrivileges(),
                    RTO_NOTES = rtoRating.Notes,
                    RTO_NOTES_EN = rtoRating.NotesAlt,
                    ENG_LEVEL = engLevel,
                    T_RATING = ratings,
                    INSTRUCTOR = instructorData,
                    EXAMINER = examinerData,
                    T_LICENCE_HOLDER = Utils.GetLicenceHolder(personData, personAddress),
                    T_LICENCE_TYPE_NAME = licenceType.Name.ToLower(),
                    T_LICENCE_NO = licenceNumber,
                    T_FIRST_ISSUE_DATE = firstEdition.DocumentDateValidFrom,
                    T_VALID_DATE = lastEdition.DocumentDateValidTo,
                    T_ACTION = lastEdition.LicenceAction.Name,
                    T_ISSUE_DATE = lastEdition.DocumentDateValidFrom,
                    OTHER_LICENCE2 = otherLicences,
                    T_DOCUMENTS = documents.Take(documents.Length / 2),
                    T_MED_CERT = this.GetMedCerts(includedMedicals, personData),
                    T_DOCUMENTS2 = documents.Skip(documents.Length / 2),
                    RTO_NOTES2 = rtoRating.Notes,
                    RTO_NOTES2_EN = rtoRating.NotesAlt,
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

        private List<object> GetLicencePrivileges()
        {
            return new List<object>()
            {
                LicenceDictionary.LicencePrivilege["medCert"],
                LicenceDictionary.LicencePrivilege["photo"]
            };
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
                }))
                .ToList();

            return otherLicences;
        }

        private PersonRatingEditionDO GetRtoRating(IEnumerable<PersonRatingDO> includedRatings)
        {
            var rtoRatingPart = includedRatings.FirstOrDefault(r => r.Authorization != null && r.Authorization.Code == "RTO");
            PersonRatingEditionDO rtoRatingEd = new PersonRatingEditionDO();

            if (rtoRatingPart != null)
            {
                rtoRatingEd = rtoRatingPart.Editions.Last();
            }

            return rtoRatingEd;
        }

        private object GetEngLevel(IEnumerable<PersonTrainingDO> includedTrainings)
        {
            var engTrainings = includedTrainings
                .Where(t => t.DocumentRole.Alias == "engTraining");

            PersonTrainingDO result = new PersonTrainingDO();
            int currentSeqNumber = 0;
            foreach (var engTraining in engTrainings)
            {
                int? engLangLevelId = engTraining.EngLangLevel == null ? (int?)null : engTraining.EngLangLevel.NomValueId;
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
                    DateTime.Compare(result.DocumentDateValidFrom.Value, engTraining.DocumentDateValidFrom.Value) < 0) {
                    result = engTraining;
                }
            }

            return new
                {
                    LEVEL = result.EngLangLevel == null ? null : result.EngLangLevel.Name,
                    ISSUE_DATE = result.DocumentDateValidFrom,
                    VALID_DATE = result.DocumentDateValidTo
                };
        }

        private List<object> GetRaitings(IEnumerable<PersonRatingDO> includedRatings)
        {
            var authorizationGroupIds = this.nomRepository.GetNomValues("authorizationGroups")
                .Where(nv => nv.Code == "FT" || nv.Code == "FC")
                .Select(nv => nv.NomValueId);

            return includedRatings
                .Where(r => r.Authorization != null && r.Authorization.Code != "RTO" && !authorizationGroupIds.Contains(r.Authorization.ParentValueId.Value))
                .Select(r =>
                    {
                        PersonRatingEditionDO lastEdition = r.Editions.Last();

                        return new
                        {
                            TYPE = string.Format(
                                "{0} {1}",
                                r.RatingClass == null ? string.Empty : r.RatingClass.Name,
                                r.RatingType == null ? string.Empty : r.RatingType.Name).Trim(),
                            AUTH_NOTES = string.Format(
                                "{0} {1}",
                                r.Authorization.Name,
                                lastEdition.NotesAlt).Trim(),
                            VALID_DATE = lastEdition.DocumentDateValidTo
                        };
                    }).ToList<object>();
        }

        private object[] GetInstructorData(IEnumerable<PersonRatingDO> inspectorRatings)
        {
            var authorizationGroupId = this.nomRepository.GetNomValues("authorizationGroups")
                .First(nv => nv.Code == "FT")
                .NomValueId;

            return inspectorRatings
                .Where(p => p.Authorization != null && p.Authorization.Code != "RTO" && p.Authorization.ParentValueId == authorizationGroupId)
                .Select(p =>
                    {
                        var instrRatingEdPart = p.Editions.Last();
                        return new
                        {
                            TYPE = string.Format(
                                "{0} {1} {2}",
                                p.RatingClass == null ? string.Empty : p.RatingClass.Code,
                                p.RatingType == null ? string.Empty : p.RatingType.Code,
                                p.Authorization.Code).Trim(),
                            VALID_DATE = instrRatingEdPart.DocumentDateValidFrom,
                            AUTH_NOTES = instrRatingEdPart.NotesAlt
                        };
                    }).ToArray<object>();
        }

        private object[] GetExaminerData(IEnumerable<PersonRatingDO> inspectorRatings)
        {
            var authorizationGroupId = this.nomRepository.GetNomValues("authorizationGroups")
                .First(nv => nv.Code == "FC")
                .NomValueId;

            return inspectorRatings
                .Where(p => p.Authorization != null && p.Authorization.Code != "RTO" && p.Authorization.ParentValueId == authorizationGroupId)
                .Select(p =>
                    {
                        var exRatingEdPart = p.Editions.Last();

                        return new
                        {
                            TYPE = string.Format(
                                "{0} {1} {2}",
                                p.RatingClass == null ? string.Empty : p.RatingClass.Code,
                                p.RatingType == null ? string.Empty : p.RatingType.Code,
                                p.Authorization.Code).Trim(),
                            VALID_DATE = exRatingEdPart.DocumentDateValidFrom,
                            AUTH_NOTES = exRatingEdPart.NotesAlt
                        };
                    }).ToArray<object>();
        }

        private List<object> GetMedCerts(IEnumerable<PersonMedicalDO> includedMedicals, PersonDataDO personData)
        {
            return includedMedicals.Select(m =>
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
                    LIMITATION = string.Join(",", m.Limitations.Select(l => l.Name))
                }).ToList<object>();
        }

        private object[] GetDocuments(PersonLicenceDO licence, IEnumerable<PersonTrainingDO> includedTrainings)
        {
            string[] documentRoleCodes;
            bool hasRoles = LicenceDictionary.LicenceRole.TryGetValue(licence.LicenceType.Code, out documentRoleCodes);

            if (!hasRoles)
            {
                return null;
            }

            return includedTrainings
                .Where(t => documentRoleCodes.Contains(t.DocumentRole.Code))
                .OrderBy(t => t.DocumentDateValidFrom)
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

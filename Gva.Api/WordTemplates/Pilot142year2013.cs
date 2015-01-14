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
                .Select(i => lot.Index.GetPart<PersonRatingDO>("ratings/" + i.Ind));
            var ratingEditions = lastEdition.IncludedRatings.Select(i => lot.Index.GetPart<PersonRatingEditionDO>("ratingEditions/" + i.Index));
            var includedLicences = lastEdition.IncludedLicences
                .Select(i => lot.Index.GetPart<PersonLicenceDO>("licences/" + i));
            var includedMedicals = lastEdition.IncludedMedicals
                .Select(i => lot.Index.GetPart<PersonMedicalDO>("personDocumentMedicals/" + i).Content);
            var includedExams = lastEdition.IncludedExams
                .Select(i => lot.Index.GetPart<PersonTrainingDO>("personDocumentTrainings/" + i).Content);

            var inspectorId = lastEdition.Inspector == null ? (int?)null : lastEdition.Inspector.NomValueId;
            List<object> instructorData = this.GetRatingsDataByCode(includedRatings, ratingEditions, "FT");
            List<object> examinerData = this.GetRatingsDataByCode(includedRatings, ratingEditions, "FC");

            object instrNoEntries = instructorData.Count == 0 ?
                new
                {
                    LABEL = "Инструктори/Instructor",
                    NO_ENTRIES = "No Entries"
                } :
                null;

            object examinerNoData = examinerData.Count == 0 ?
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
            var langLevel = this.GetLangLevels(includedLangCerts);
            var limitations = this.GetLimitations(lastEdition, includedMedicals, includedExams);
            var ratings = this.GetRatings(includedRatings, ratingEditions);
            var country = this.GetCountry(personAddress);
            var licenceNumber = string.Format(
                "BGR. {0} - {1} - {2}",
                licenceType.Code,
                Utils.PadLicenceNumber(licence.LicenceNumber),
                personData.Lin);

            var json = new
            {
                root = new
                {
                    L_LICENCE_NO = licenceNumber,
                    L_LICENCE_HOLDER = this.GetPersonData(personData, personAddress),
                    COUNTRY_NAME_BG = country != null ? country.Name : null,
                    COUNTRY_CODE = country != null ? (country.TextContent != null ? country.TextContent.Get<string>("nationalityCodeCA") : null) : null,
                    ISSUE_DATE = lastEdition.DocumentDateValidFrom.Value,
                    OTHER_LICENCE = otherLicences,
                    T_DOCUMENTS = new object[0],
                    L_LICENCE_PRIV = this.GetLicencePrivileges(),
                    RTO_NOTES = rtoRating != null ? rtoRating.Notes : null,
                    RTO_NOTES_EN = rtoRating != null ? rtoRating.NotesAlt : null,
                    ENG_LEVEL = langLevel,
                    LIMITS = limitations,
                    T_RATING = ratings,
                    INSTR_DATA = instructorData.Count > 0 ? new { INSTRUCTOR = instructorData } : null,
                    INSTRUCTOR_NO_ENTRIES = instrNoEntries,
                    EXAMINER_DATA = examinerData.Count > 0 ? new { EXAMINER = examinerData } : null,
                    EXAMINER_NO_ENTRIES = examinerNoData,
                    T_LICENCE_HOLDER = Utils.GetLicenceHolder(personData, personAddress),
                    T_LICENCE_TYPE_NAME = licenceType.Name.ToLower(),
                    T_LICENCE_NO = licenceNumber,
                    T_ISSUE_DATE = lastEdition.DocumentDateValidFrom.Value,
                    OTHER_LICENCE2 = otherLicences,
                    T_MED_CERT = this.GetMedCerts(includedMedicals, personData),
                    T_DOCUMENTS2 = this.GetDocuments2(licence, includedTrainings),
                    L_RATING = ratings,
                    INSTR_DATA1 = instructorData.Count > 0 ? new { INSTRUCTOR1 = instructorData } : null,
                    INSTRUCTOR1_NO_ENTRIES = instrNoEntries,
                    RTO_NOTES2 = rtoRating != null ? rtoRating.Notes : null,
                    RTO_NOTES2_EN = rtoRating != null ? rtoRating.NotesAlt : null,
                    ENG_LEVEL1 = langLevel,
                    T_LIMITS = limitations,
                    EXAMINER_DATA1 = examinerData.Count > 0 ? new { EXAMINER1 = examinerData } : null,
                    EXAMINER1_NO_ENTRIES = examinerNoData,
                    L_ABBREVIATION = this.GetAbbreviations()
                }
            };

            return json;
        }

        private object GetPersonData(PersonDataDO personData, PersonAddressDO personAddress)
        {
            var placeOfBirth = personData.PlaceOfBirth;
            dynamic country = null;
            if (placeOfBirth != null)
            {
                country = this.nomRepository.GetNomValue("countries", placeOfBirth.ParentValueId.Value);
            }

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
                    country != null ? country.Name : null,
                    placeOfBirth != null ? placeOfBirth.Name : null),
                PLACE_OF_BIRTH_TRAN = string.Format(
                    "{0} {1}",
                    country != null ? country.NameAlt : null,
                    placeOfBirth != null ? placeOfBirth.NameAlt : null),
                ADDRESS = string.Format(
                    "{0}, {1}",
                    personAddress.Settlement != null? personAddress.Settlement.Name : null,
                    personAddress.Address),
                ADDRESS_TRANS = string.Format(
                    "{0}, {1}",
                    personAddress.AddressAlt,
                    personAddress.Settlement != null? personAddress.Settlement.NameAlt : null)
            };
        }

        private NomValue GetCountry(PersonAddressDO personAddress)
        {
            int? countryId = personAddress.Settlement != null? personAddress.Settlement.ParentValueId : (int?)null;

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
                })).ToList();

            return otherLicences;
        }

        private PersonRatingEditionDO GetRtoRating(IEnumerable<PartVersion<PersonRatingDO>> includedRatings, IEnumerable<PartVersion<PersonRatingEditionDO>> ratingEditions)
        {
            var rtoRatingPart = includedRatings.FirstOrDefault(r => r.Content.Authorization != null && r.Content.Authorization.Code == "RTO");

            List<PersonRatingEditionDO> rtoRatings = new List<PersonRatingEditionDO>();
            foreach (var edition in ratingEditions)
            {
                var rating = includedRatings.Where(r => r.Part.Index == edition.Content.RatingPartIndex).Single();
                if (rating.Content.Authorization != null && rating.Content.Authorization.Code == "RTO")
                {
                    rtoRatings.Add(edition.Content);
                }
            }

            return rtoRatings.OrderBy(e => e.Index).LastOrDefault();
        }

        private List<object> GetLangLevels(IEnumerable<PersonLangCertDO> includedLangCerts)
        {
            return includedLangCerts.Select(l =>
                new
                {
                    LEVEL = l.LangLevel.Name,
                    ISSUE_DATE = l.DocumentDateValidFrom,
                    VALID_DATE = l.DocumentDateValidTo
                })
                .ToList<object>();
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

        private List<object> GetRatings(IEnumerable<PartVersion<PersonRatingDO>> includedRatings, IEnumerable<PartVersion<PersonRatingEditionDO>> ratingEditions)
        {
            var authorizationGroupIds = this.nomRepository.GetNomValues("authorizationGroups")
                .Where(nv => nv.Code == "FT" || nv.Code == "FC")
                .Select(nv => nv.NomValueId);

            List<object> ratings = new List<object>();
            foreach (var edition in ratingEditions.OrderBy(r => r.Content.DocumentDateValidFrom))
            {
                var rating = includedRatings.Where(r => r.Part.Index == edition.Content.RatingPartIndex).Single();
                if (rating.Content.Authorization == null || (rating.Content.Authorization.Code != "RTO" && !authorizationGroupIds.Contains(rating.Content.Authorization.ParentValueId.Value)))
                {
                    ratings.Add(new
                    {
                        TYPE = string.Format(
                            "{0} {1}",
                            rating.Content.RatingClass == null ? string.Empty : rating.Content.RatingClass.Name,
                            rating.Content.RatingType == null ? string.Empty : rating.Content.RatingType.Code).Trim(),
                        AUTH_NOTES = string.Format(
                            "{0} {1}",
                            rating.Content.Authorization == null ? string.Empty : rating.Content.Authorization.Code,
                            edition.Content.NotesAlt).Trim(),
                        VALID_DATE = edition.Content.DocumentDateValidTo
                    });
                }
            }

            return ratings;
        }

        private List<object> GetRatingsDataByCode(IEnumerable<PartVersion<PersonRatingDO>> includedRatings, IEnumerable<PartVersion<PersonRatingEditionDO>> ratingEditions, string code)
        {
            var authorizationGroup = this.nomRepository.GetNomValues("authorizationGroups")
                .First(nv => nv.Code == code);

            List<object> ratings = new List<object>();
            foreach (var edition in ratingEditions.OrderBy(r => r.Content.DocumentDateValidFrom))
            {
                var rating = includedRatings.Where(r => r.Part.Index == edition.Content.RatingPartIndex).Single();
                if (rating.Content.Authorization != null && authorizationGroup.NomValueId == rating.Content.Authorization.ParentValueId.Value && rating.Content.Authorization.Code != "RTO")
                {
                    ratings.Add(new
                    {
                        TYPE = string.Format(
                            "{0} {1} {2}",
                            rating.Content.RatingClass == null ? string.Empty : rating.Content.RatingClass.Name,
                            rating.Content.RatingType == null ? string.Empty : rating.Content.RatingType.Code,
                            rating.Content.Authorization == null ? string.Empty : rating.Content.Authorization.Code).Trim(),
                        AUTH_NOTES = string.Format("{0}", edition.Content.NotesAlt).Trim(),
                        VALID_DATE = edition.Content.DocumentDateValidTo
                    });
                }
            }

            return ratings;
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

            var emptyEntry = new List<object>() { new { ORDER_NO = "10" } };

            return result.Count() > 0 ? result : emptyEntry;
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

        private List<object> GetAbbreviations()
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
                LicenceDictionary.LicenceAbbreviation["TRI"]
            };
        }
    }
}

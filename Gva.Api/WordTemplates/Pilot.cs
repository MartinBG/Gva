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
    public class Pilot : IDataGenerator
    {
        private static string publisherCaaCode = "BG";

        private ILotRepository lotRepository;
        private INomRepository nomRepository;

        public Pilot(
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
                return new string[] { "pilot" };
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
            var includedLangCerts = lastEdition.IncludedLangCerts
                .Select(i => lot.Index.GetPart<PersonLangCertDO>("personDocumentLangCertificates/" + i).Content);
            var includedRatings = lastEdition.IncludedRatings
                .Select(i => lot.Index.GetPart<PersonRatingDO>("ratings/" + i));
            var ratingEditions = lot.Index.GetParts<PersonRatingEditionDO>("ratingEditions");
            var includedLicences = lastEdition.IncludedLicences
                .Select(i => lot.Index.GetPart<PersonLicenceDO>("licences/" + i));
            var includedMedicals = lastEdition.IncludedMedicals
                .Select(i => lot.Index.GetPart<PersonMedicalDO>("personDocumentMedicals/" + i).Content);

            var inspectorId = lastEdition.Inspector == null ? (int?)null : lastEdition.Inspector.NomValueId;
            List<object> instructorData = new List<object>();
            if (inspectorId.HasValue)
            {
                var inspectorLot = this.lotRepository.GetLotIndex(inspectorId.Value);
                var inspectorRatings = inspectorLot.Index.GetParts<PersonRatingDO>("ratings");
                var inspectorRatingEditions = inspectorLot.Index.GetParts<PersonRatingEditionDO>("ratingEditions");

                instructorData = this.GetInstructorData(inspectorRatings, inspectorRatingEditions);
            }

            var licenceType = this.nomRepository.GetNomValue("licenceTypes", licence.LicenceType.NomValueId);
            var licenceCaCode = licenceType.TextContent.Get<string>("codeCA");
            var otherLicences = this.GetOtherLicences(licenceCaCode, lot, lastEdition, includedLicences);
            var rtoRating = this.GetRtoRating(includedRatings, ratingEditions);
            var engLevel = this.GetEngLevel(includedLangCerts);
            var ratings = this.GetRaitings(includedRatings, ratingEditions);
            var country = this.GetCountry(personAddress);
            var licenceNumber = string.Format(
                "BGR. {0} - {1} - {2}",
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
                    L_LICENCE_PRIV = this.GetLicencePrivileges(licenceType.Code, lastEdition),
                    RTO_NOTES = rtoRating.Notes,
                    RTO_NOTES_EN = rtoRating.NotesAlt,
                    ENG_LEVEL = engLevel,
                    T_RATING = ratings,
                    INSTRUCTOR = instructorData,
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
                    REVAL = new object[10],
                    REVAL2 = new object[10],
                    REVAL3 = new object[10],
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

        private List<object> GetLicencePrivileges(string licenceTypeCode, PersonLicenceEditionDO edition)
        {
            List<dynamic> result = new object[0].ToList();

            if (licenceTypeCode == "PPH" ||
                licenceTypeCode == "CPH" ||
                licenceTypeCode == "ATPA" ||
                licenceTypeCode == "CPA" ||
                licenceTypeCode == "ATPH" ||
                licenceTypeCode == "PPA")
            {
                dynamic dateValidPrivilege = LicenceDictionary.LicencePrivilege["dateValid2"];
                string dateValid = edition.DocumentDateValidTo.Value.ToString("dd.MM.yyyy");
                string dateValidTrans = edition.DocumentDateValidTo.Value.ToString("dd MMMM yyyy");

                result = new List<object>()
                {
                    LicenceDictionary.LicencePrivilege["validWithMedCert"],
                    LicenceDictionary.LicencePrivilege["requiresLegalID"]
                };

                result.Add(new
                {
                    NO = dateValidPrivilege.NO,
                    NAME_BG = string.Format(dateValidPrivilege.NAME_BG, dateValid),
                    NAME_TRANS = string.Format(dateValidPrivilege.NAME_TRANS, dateValidTrans)
                });
            }

            return result.OrderBy(p => p.NO).ToList<object>();
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

        private List<object> GetRaitings(IEnumerable<PartVersion<PersonRatingDO>> includedRatings, IEnumerable<PartVersion<PersonRatingEditionDO>> ratingEditions)
        {
            var authorizationGroupIds = this.nomRepository.GetNomValues("authorizationGroups")
                .Where(nv => nv.Code == "FT" || nv.Code == "FC")
                .Select(nv => nv.NomValueId);

            var result = includedRatings
                .Where(r => r.Content.Authorization != null && r.Content.Authorization.Code != "RTO" && !authorizationGroupIds.Contains(r.Content.Authorization.ParentValueId.Value))
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
                                r.Content.Authorization == null ? string.Empty : r.Content.Authorization.Name,
                                lastEdition.Content.NotesAlt).Trim(),
                            VALID_DATE = lastEdition.Content.DocumentDateValidTo
                        };
                    }).ToList<object>();

            result = Utils.FillBlankData(result, 19);
            return result;
        }

        private List<object> GetInstructorData(IEnumerable<PartVersion<PersonRatingDO>> inspectorRatings, IEnumerable<PartVersion<PersonRatingEditionDO>> inspectorRatingEditions)
        {
            var authorizationGroup = this.nomRepository.GetNomValues("authorizationGroups")
                .First(nv => nv.Code == "FT");

            var result = inspectorRatings
                .Where(p => p.Content.Authorization != null && p.Content.Authorization.Code != "RTO" && p.Content.Authorization.ParentValueId == authorizationGroup.NomValueId)
                .Select(p =>
                    {
                        var instrRatingEdPart = inspectorRatingEditions.Where(e => e.Content.RatingPartIndex == p.Part.Index).OrderBy(e => e.Content.Index).Last();
                        return new
                        {
                            TYPE = string.Format(
                                "{0} {1} {2}",
                                p.Content.RatingClass == null ? string.Empty : p.Content.RatingClass.Code,
                                p.Content.RatingType == null ? string.Empty : p.Content.RatingType.Code,
                                p.Content.Authorization == null ? string.Empty : p.Content.Authorization.Code).Trim(),
                            VALID_DATE = instrRatingEdPart.Content.DocumentDateValidFrom,
                            AUTH_NOTES = instrRatingEdPart.Content.NotesAlt
                        };
                    }).ToList<object>();

            result = Utils.FillBlankData(result, 4);
            return result;
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
                    LIMITATION = string.Join(",", m.Limitations.Select(l => l.Name))
                }).ToList<object>();
            return result;
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
            if (licenceTypeCode == "CPH" ||
                licenceTypeCode == "ATPA" ||
                licenceTypeCode == "PPH" ||
                licenceTypeCode == "PPA" ||
                licenceTypeCode == "CPA" ||
                licenceTypeCode == "ATPH")
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

            return new object[0].ToList();
        }
    }
}

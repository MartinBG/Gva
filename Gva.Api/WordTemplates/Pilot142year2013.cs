﻿using System;
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

        public string GeneratorCode
        {
            get
            {
                return "pilot142year2013";
            }
        }

        public string GeneratorName
        {
            get
            {
                return "Нов лиценз за пилоти";
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
                .OrderBy(e => e.Content.Index);
            var lastEdition = editions.Last().Content;
            var firstEdition = editions.First().Content;

            var includedTrainings = lastEdition.IncludedTrainings
                .Select(i => lot.Index.GetPart<PersonTrainingDO>("personDocumentTrainings/" + i).Content);
            var includedLangCerts = lastEdition.IncludedLangCerts
                .Select(i => lot.Index.GetPart<PersonLangCertDO>("personDocumentLangCertificates/" + i).Content);
            var includedRatings = lastEdition.IncludedRatings
                .Select(i => i.Ind)
                .Distinct()
                .Select(i => lot.Index.GetPart<PersonRatingDO>("ratings/" + i.Value));
            var ratingEditions = lastEdition.IncludedRatings.Select(i => lot.Index.GetPart<PersonRatingEditionDO>("ratingEditions/" + i.Index));
            var includedLicences = lastEdition.IncludedLicences
                .Select(i => lot.Index.GetPart<PersonLicenceDO>("licences/" + i));
            var includedMedicals = lastEdition.IncludedMedicals
                .Select(i => lot.Index.GetPart<PersonMedicalDO>("personDocumentMedicals/" + i).Content);
            var includedExams = lastEdition.IncludedExams
                .Select(i => lot.Index.GetPart<PersonTrainingDO>("personDocumentTrainings/" + i).Content);

            NomValue FTgroup = this.nomRepository.GetNomValues("authorizationGroups").First(nv => nv.Code == "FT");
            NomValue FCgroup = this.nomRepository.GetNomValues("authorizationGroups").First(nv => nv.Code == "FC");
            List<object> instructorData = PilotUtils.GetRatingsDataByCode(includedRatings, ratingEditions, FTgroup, this.nomRepository);
            List<object> examinerData = PilotUtils.GetRatingsDataByCode(includedRatings, ratingEditions, FCgroup, this.nomRepository);

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

            var licenceType = this.nomRepository.GetNomValue("licenceTypes", licence.LicenceTypeId.Value);
            var licenceCaCode = licenceType.TextContent.Get<string>("codeCA");
            var otherLicences = PilotUtils.GetOtherLicences(publisherCaaCode, licenceCaCode, lot, firstEdition, includedLicences, this.nomRepository);
            var rtoRating = PilotUtils.GetRtoRating(includedRatings, ratingEditions, nomRepository);
            var langLevel = includedLangCerts.Where(c => c.LangLevelId.HasValue)
                .Select(c =>
                    {
                        string langLevelName = this.nomRepository.GetNomValue("langLevels", c.LangLevelId.Value).Name;
                        return new
                        {
                            LEVEL = langLevelName,
                            VALID_DATE = langLevelName.Contains("6") ? "for life" :
                                (c.DocumentDateValidTo.HasValue ? c.DocumentDateValidTo.Value.ToString("dd/MM/yyyy") : "unlimited")
                        };
                    })
                .ToList<object>();
            
            var allIncludedLimitations66 = this.nomRepository.GetNomValues("limitations66")
                .Where(l => lastEdition.Limitations.Contains(l.NomValueId));

            var limitationsP8 = allIncludedLimitations66
                .Where(l => l.TextContent.Get<int>("point") == 8)
                .Select(l => new { LIMIT_NAME = l.Name });
            var limitationsP13 = allIncludedLimitations66
                .Where(l => l.TextContent.Get<int>("point") == 13)
                .Select(l => new { LIMIT_NAME = l.Name });

            List<int> authorizationGroupIds = nomRepository.GetNomValues("authorizationGroups")
                .Where(nv => nv.Code == "FT" || nv.Code == "FC")
                .Select(nv => nv.NomValueId)
                .ToList();
            var ratings = PilotUtils.GetRatings(includedRatings, ratingEditions, authorizationGroupIds, this.nomRepository);
            var country = Utils.GetCountry(personData, this.nomRepository);
            var licenceNumber = string.Format(
                "BGR. {0} - {1} - {2}",
                licenceType.Code.Replace("/", "."),
                Utils.PadLicenceNumber(licence.LicenceNumber),
                personData.Lin);
            var licenceHolder = this.GetPersonData(personData, personAddress);

            var countryNom = personData.CountryId.HasValue ? this.nomRepository.GetNomValue("countries", personData.CountryId.Value) : null;

            var privileges = this.GetLicencePrivileges();
            var json = new
            {
                root = new
                {
                    L_LICENCE_NO = licenceNumber,
                    L_LICENCE_NO2 = licenceNumber,
                    L_LICENCE_HOLDER = licenceHolder,
                    L_LICENCE_HOLDER2 = licenceHolder,
                    COUNTRY_NAME_BG = countryNom != null ? countryNom.Name : null,
                    COUNTRY_CODE = countryNom != null ? countryNom.Code : null,
                    COUNTRY_NAME_BG2 = countryNom != null ? countryNom.Name : null,
                    COUNTRY_CODE2 = countryNom != null ? countryNom.Code : null,
                    ISSUE_DATE = lastEdition.DocumentDateValidFrom.Value,
                    ISSUE_DATE2 = lastEdition.DocumentDateValidFrom.Value,
                    OTHER_LICENCE = otherLicences,
                    OTHER_LICENCE2 = otherLicences,
                    L_LICENCE_PRIV = privileges,
                    L_LICENCE_PRIV2 = privileges,
                    RTO_NOTES = rtoRating != null ? rtoRating.Notes : null,
                    RTO_NOTES_EN = rtoRating != null ? rtoRating.NotesAlt : null,
                    RTO_NOTES2 = rtoRating != null ? rtoRating.Notes : null,
                    RTO_NOTES_EN2 = rtoRating != null ? rtoRating.NotesAlt : null,
                    ENG_LEVEL = Utils.FillBlankData(langLevel, 1),
                    ENG_LEVEL2 = Utils.FillBlankData(langLevel, 1),
                    LIMITSp8 = limitationsP8,
                    LIMITSp13 = limitationsP13,
                    LIMITS2p8 = limitationsP8,
                    LIMITS2p13 = limitationsP13,
                    T_RATING = ratings,
                    T_RATING2 = ratings,
                    INSTR_DATA = instructorData.Count > 0 ? new { INSTRUCTOR = instructorData } : null,
                    INSTR_DATA1 = instructorData.Count > 0 ? new { INSTRUCTOR = instructorData } : null,
                    INSTRUCTOR_NO_ENTRIES = instrNoEntries,
                    INSTRUCTOR_NO_ENTRIES2 = instrNoEntries,
                    EXAMINER_DATA = examinerData.Count > 0 ? new { EXAMINER = examinerData } : null,
                    EXAMINER_DATA1 = examinerData.Count > 0 ? new { EXAMINER = examinerData } : null,
                    EXAMINER_NO_ENTRIES = examinerNoData,
                    EXAMINER_NO_ENTRIES2 = examinerNoData,
                    L_ABBREVIATION = this.GetAbbreviations()
                }
            };

            return json;
        }

        private object GetPersonData(PersonDataDO personData, PersonAddressDO personAddress)
        {
            var address = Utils.GetAddress(personAddress, this.nomRepository);
            var placeOfBirth = Utils.GetPlaceOfBirth(personData, nomRepository);

            return new
            {
                FAMILY_BG = personData.LastName.ToUpper(),
                FAMILY_TRANS = personData.LastNameAlt.ToUpper(),
                FIRST_NAME_BG = personData.FirstName.ToUpper(),
                FIRST_NAME_TRANS = personData.FirstNameAlt.ToUpper(),
                SURNAME_BG = personData.MiddleName.ToUpper(),
                SURNAME_TRANS = personData.MiddleNameAlt.ToUpper(),
                DATE_OF_BIRTH = personData.DateOfBirth,
                PLACE_OF_BIRTH = placeOfBirth.Item2,
                PLACE_OF_BIRTH_TRAN = placeOfBirth.Item1,
                ADDRESS = address.Item2,
                ADDRESS_TRANS = address.Item1
            };
        }

        private List<object> GetLicencePrivileges()
        {
            return new List<object>()
            {
                LicenceDictionary.LicencePrivilege["medCert"],
                LicenceDictionary.LicencePrivilege["photo"]
            };
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

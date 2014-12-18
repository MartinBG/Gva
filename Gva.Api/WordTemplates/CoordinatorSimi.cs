﻿using System.Collections.Generic;
using System.Linq;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Json;
using Gva.Api.ModelsDO.Persons;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.WordTemplates
{
    public class CoordinatorSimi : IDataGenerator
    {
        private ILotRepository lotRepository;
        private INomRepository nomRepository;

        public CoordinatorSimi(
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
                return new string[] { "coordinator_simi" };
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

            var licenceType = this.nomRepository.GetNomValue("licenceTypes", licence.LicenceType.NomValueId);

            var includedTrainings = lastEdition.IncludedTrainings
                .Select(i => lot.Index.GetPart<PersonTrainingDO>("personDocumentTrainings/" + i).Content);
            var includedLangCerts = lastEdition.IncludedLangCerts
                .Select(i => lot.Index.GetPart<PersonLangCertDO>("personDocumentLangCertificates/" + i).Content);
            var includedRatings = lastEdition.IncludedRatings.Select(i => i.Ind).Distinct()
                .Select(ind => lot.Index.GetPart<PersonRatingDO>("ratings/" + ind));
            var ratingEditions = lastEdition.IncludedRatings.Select(i => lot.Index.GetPart<PersonRatingEditionDO>("ratingEditions/" + i.Index));
            var includedExams = lastEdition.IncludedExams
                .Select(i => lot.Index.GetPart<PersonTrainingDO>("personDocumentTrainings/" + i).Content);
            var documents = this.GetDocuments(licenceType.Code, includedTrainings, includedExams);
            var licenceCodeCa = licenceType.TextContent.Get<string>("codeCA");

            IEnumerable<PersonLangCertDO> engLevels = includedLangCerts.Where(t => t.DocumentRole.Alias == "engCert" && t.LangLevelEntries.Count() > 0);

            dynamic lLangLevel = null;
            dynamic tLangLevel = null;
            if (engLevels.Count() > 0)
            {
                lLangLevel = engLevels.Select(l => new
                {
                    LEVEL = l.LangLevelEntries.Last().LangLevel.Name,
                    VALID_DATE = l.DocumentDateValidTo
                });

                tLangLevel = engLevels.Select(l => new
                {
                    LEVEL = l.LangLevelEntries.Last().LangLevel.Name,
                    ISSUE_DATE = l.DocumentDateValidFrom,
                    VALID_DATE = l.DocumentDateValidTo
                });
            }

            var endorsements = this.GetEndorsements(includedRatings, ratingEditions);
            var tEndorsements = this.GetTEndorsements(includedRatings, ratingEditions);
            var lEndorsements = this.GetLEndorsements(includedRatings, ratingEditions);

            var licenceNumber = string.Format(
                "BGR. {0} - {1} - {2}",
                licenceType.Code,
                Utils.PadLicenceNumber(licence.LicenceNumber),
                personData.Lin);

            List<dynamic> licencePrivileges = this.GetLicencePrivileges();

            var json = new
            {
                root = new
                {
                    L_NAME = licenceType.Name,
                    L_NAME_TRANS = licenceType.NameAlt,
                    L_LICENCE_TYPE_CA_CODE = licenceCodeCa,
                    L_NAME1 = licenceType.Name,
                    L_NAME1_TRANS = licenceType.NameAlt,
                    L_LICENCE_TYPE_CA_CODE2 = licenceCodeCa,
                    L_LICENCE_NO = licenceNumber,
                    L_LICENCE_HOLDER = this.GetPersonData(personData, personAddress),
                    L_LICENCE_PRIV = licencePrivileges.Select(p => p.NAME_BG),
                    L_LICENCE_PRIV_TRANS = licencePrivileges.Select(p => p.NAME_TRANS),
                    ENDORSEMENT = endorsements,
                    L_ENDORSEMENT = lEndorsements,
                    L_LANG_LEVEL = lLangLevel,
                    L_FIRST_ISSUE_DATE = firstEdition.DocumentDateValidFrom,
                    L_ISSUE_DATE = lastEdition.DocumentDateValidFrom,
                    T_ENDORSEMENT = tEndorsements,
                    T_LICENCE_HOLDER = Utils.GetLicenceHolder(personData, personAddress),
                    T_LICENCE_NO = licenceNumber,
                    T_LICENCE_CODE = licenceType.Code,
                    T_ACTION = firstEdition.LicenceAction.Name.ToUpper(),
                    T_FIRST_ISSUE_DATE = firstEdition.DocumentDateValidFrom,
                    T_ISSUE_DATE = lastEdition.DocumentDateValidFrom,
                    T_LANG_LEVEL = tLangLevel,
                    T_DOCUMENTS = documents.Take(documents.Length / 2),
                    T_DOCUMENTS2 = documents.Skip(documents.Length / 2),
                    L_ABBREVIATION = this.GetAbbreviations()
                }
            };

            return json;
        }

        private object GetPersonData(PersonDataDO personData, PersonAddressDO personAddress)
        {
            var placeOfBirth = personData.PlaceOfBirth;
            NomValue country = null;
            NomValue nationality = null;
            if (placeOfBirth != null)
            {
                country = this.nomRepository.GetNomValue("countries", placeOfBirth.ParentValueId.Value);
                nationality = this.nomRepository.GetNomValue("countries", personData.Country.NomValueId);
            }

            return new
            {
                FAMILY_BG = personData.LastName.ToUpper(),
                FAMILY_TRANS = personData.LastNameAlt.ToUpper(),
                FIRST_NAME_BG = personData.FirstName.ToUpper(),
                FIRST_NAME_TRANS = personData.FirstNameAlt.ToUpper(),
                SURNAME_BG = personData.MiddleName.ToUpper(),
                SURNAME_TRANS = personData.MiddleNameAlt.ToUpper(),
                DATE_PLACE_OF_BIRTH = new
                {
                    DATE_OF_BIRTH = personData.DateOfBirth,
                    PLACE_OF_BIRTH = new
                    {
                        COUNTRY_NAME = country != null ? country.Name : null,
                        TOWN_VILLAGE_NAME = placeOfBirth != null ? placeOfBirth.Name : null
                    },
                    PLACE_OF_BIRTH_TRANS = new
                    {
                        COUNTRY_NAME = country != null ? country.NameAlt : null,
                        TOWN_VILLAGE_NAME = placeOfBirth != null ? placeOfBirth.NameAlt : null
                    }
                },
                ADDRESS = personAddress.Address,
                ADDRESS_TRANS = personAddress.AddressAlt,
                NATIONALITY = new
                {
                    COUNTRY_NAME_BG = nationality != null? nationality.Name : null,
                    COUNTRY_CODE = nationality != null? nationality.TextContent.Get<string>("nationalityCodeCA") : null
                }
            };
        }

        private object[] GetDocuments(
            string licenceTypeCode,
            IEnumerable<PersonTrainingDO> includedTrainings,
            IEnumerable<PersonTrainingDO> includedExams)
        {
            string[] documentRoleCodes;
            bool hasRoles = LicenceDictionary.LicenceRole.TryGetValue(licenceTypeCode, out documentRoleCodes);

            if (!hasRoles)
            {
                return new object[0];
            }

            var trainings = includedTrainings
                .Where(e => e.Valid.Code == "Y")
                .Select(t =>
                    new
                    {
                        DOC = new
                        {
                            DOC_ROLE = t.DocumentRole.Name,
                            SUB_DOC = new
                            {
                                CLASS = t.DocumentType.Name,
                                DOC_NO = t.DocumentNumber,
                                DATE = t.DocumentDateValidFrom
                            }
                        }
                    }).ToArray<dynamic>();

            var examRole = this.nomRepository.GetNomValue("documentRoles", "exam");
            var exams = includedExams
                .Where(e => e.Valid != null && e.Valid.Code == "Y")
                .Select(e =>
                    new
                    {
                        DOC = new
                        {
                            DOC_ROLE = examRole.Name,
                            SUB_DOC = new
                            {
                                CLASS = e.DocumentType.Name,
                                DOC_NO = e.DocumentNumber,
                                DATE = e.DocumentDateValidFrom
                            }
                        }
                    }).ToArray<dynamic>();

            return trainings
                .Union(exams)
                .OrderBy(d => d.DOC.SUB_DOC.DATE)
                .ToArray<object>();
        }

        private List<object> GetEndorsements(IEnumerable<PartVersion<PersonRatingDO>> includedRatings, IEnumerable<PartVersion<PersonRatingEditionDO>> ratingEditions)
        {
            List<object> endosments = new List<object>();
            foreach(var edition in ratingEditions)
            {
                var rating = includedRatings.Where(r => r.Part.Index == edition.Content.RatingPartIndex).Single();
                if(rating.Content.Authorization != null)
                {
                    endosments.Add(new {
                        NAME = rating.Content.Authorization.Code,
                        DATE = edition.Content.DocumentDateValidFrom
                    });
                }
            };

            return endosments;
        }

        private List<object> GetTEndorsements(IEnumerable<PartVersion<PersonRatingDO>> includedRatings, IEnumerable<PartVersion<PersonRatingEditionDO>> ratingEditions)
        {
            List<object> endosments = new List<object>();
            foreach (var edition in ratingEditions)
            {
                var rating = includedRatings.Where(r => r.Part.Index == edition.Content.RatingPartIndex).Single();
                if (rating.Content.Authorization != null)
                {
                    endosments.Add(new
                    {
                        ICAO = rating.Content.LocationIndicator == null ? null : rating.Content.LocationIndicator.Code,
                        AUTH = rating.Content.Authorization.Code,
                        SECTOR = rating.Content.Sector,
                        ISSUE_DATE = edition.Content.DocumentDateValidFrom
                    });
                }
            };

            return endosments;
        }

        
        private List<object> GetLEndorsements(IEnumerable<PartVersion<PersonRatingDO>> includedRatings, IEnumerable<PartVersion<PersonRatingEditionDO>> ratingEditions)
        {
            List<object> endosments = new List<object>();
            foreach (var edition in ratingEditions)
            {
                var rating = includedRatings.Where(r => r.Part.Index == edition.Content.RatingPartIndex).Single();
                if (rating.Content.Authorization != null)
                {
                    endosments.Add(new
                    {
                        ICAO = rating.Content.LocationIndicator == null ? null : rating.Content.LocationIndicator.Code,
                        AUTH = rating.Content.Authorization.Code,
                        SECTOR = rating.Content.Sector,
                        VALID_DATE = edition.Content.DocumentDateValidTo
                    });
                }
            };

            return endosments;
        }

        private IEnumerable<object> GetAbbreviations()
        {
            return new[]
            {
                LicenceDictionary.LicenceAbbreviation["AFIS"],
                LicenceDictionary.LicenceAbbreviation["ASM"],
                LicenceDictionary.LicenceAbbreviation["ATFM"],
                LicenceDictionary.LicenceAbbreviation["FDA"],
                LicenceDictionary.LicenceAbbreviation["FIS"],
                LicenceDictionary.LicenceAbbreviation["OJTI"],
                LicenceDictionary.LicenceAbbreviation["SAR"],
                LicenceDictionary.LicenceAbbreviation["SIMI"]
            };
        }

        private List<object> GetLicencePrivileges()
        {
            return new List<object>()
            {
                LicenceDictionary.LicencePrivilege["coordinatorSimi1"],
                LicenceDictionary.LicencePrivilege["coordinatorSimi2"],
                LicenceDictionary.LicencePrivilege["coordinatorSimi3"],
                LicenceDictionary.LicencePrivilege["coordinatorSimi4"],
                LicenceDictionary.LicencePrivilege["coordinatorSimi5"]
            };
        }
    }
}

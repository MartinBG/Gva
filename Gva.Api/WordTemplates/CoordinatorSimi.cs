using System;
using System.Collections.Generic;
using System.Linq;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Json;
using Gva.Api.ModelsDO.Persons;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            var includedRatings = lastEdition.IncludedRatings
                .Select(i => lot.Index.GetPart<PersonRatingDO>("ratings/" + i).Content);
            //TODO - includedExams! var includedExams = lastEdition.GetItems<int>("includedExams")
            //TODO - includedExams!     .Select(i => lot.Index.GetPart("personDocumentExams/" + i).Content);
            //TODO - includedExams! var documents = this.GetDocuments(licenceType.Code, includedTrainings, includedExams);
            var licenceCodeCa = licenceType.TextContent.Get<string>("codeCA");

            IEnumerable<PersonTrainingDO> engLevels = includedTrainings.Where(t => t.DocumentRole.Alias == "engTraining")
                .Where(t => t.EngLangLevel != null);

            dynamic lLangLevel = null;
            dynamic tLangLevel = null;
            if (engLevels.Count() > 0)
            {
                lLangLevel = engLevels.Select(l => new
                {
                    LEVEL = l.EngLangLevel.Name,
                    VALID_DATE = l.DocumentDateValidTo
                });

                 tLangLevel = engLevels.Select(l => new
                 {
                    LEVEL = l.EngLangLevel.Name,
                    ISSUE_DATE = l.DocumentDateValidFrom,
                    VALID_DATE = l.DocumentDateValidTo
                });
            }

            var endorsements = includedRatings
                .Where(r => r.Authorization != null)
                .Select(r =>
                    new {
                        NAME = r.Authorization.Code,
                        DATE = r.Editions.Last().DocumentDateValidFrom
                    });
            var tEndorsements = includedRatings
                .Where(r => r.Authorization != null)
                .Select(r =>
                    new {
                        ICAO = r.LocationIndicator == null ? null : r.LocationIndicator.Code,
                        AUTH = r.Authorization.Code,
                        SECTOR = r.Sector,
                        ISSUE_DATE = r.Editions.Last().DocumentDateValidFrom
                    });
            var lEndorsements = includedRatings
                .Where(r => r.Authorization != null)
                .Select(r =>
                    new
                    {
                        ICAO = r.LocationIndicator == null ? null : r.LocationIndicator.Code,
                        AUTH = r.Authorization.Code,
                        SECTOR = r.Sector,
                        VALID_DATE = r.Editions.Last().DocumentDateValidTo
                    });
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
                    //TODO - includedExams! T_DOCUMENTS = documents.Take(documents.Length / 2),
                    //TODO - includedExams! T_DOCUMENTS2 = documents.Skip(documents.Length / 2),
                    L_ABBREVIATION = this.GetAbbreviations()
                }
            };

            return json;
        }

        private object GetPersonData(PersonDataDO personData, PersonAddressDO personAddress)
        {
            var placeOfBirth = personData.PlaceOfBirth;
            var country = this.nomRepository.GetNomValue("countries", placeOfBirth.ParentValueId.Value);
            var nationality = this.nomRepository.GetNomValue("countries", personData.Country.NomValueId);

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
                        COUNTRY_NAME = country.Name,
                        TOWN_VILLAGE_NAME = placeOfBirth.Name
                    },
                    PLACE_OF_BIRTH_TRANS = new
                    {
                        COUNTRY_NAME = country.NameAlt,
                        TOWN_VILLAGE_NAME = placeOfBirth.NameAlt
                    }
                },
                ADDRESS = personAddress.Address,
                ADDRESS_TRANS = personAddress.AddressAlt,
                NATIONALITY = new
                {
                    COUNTRY_NAME_BG = nationality.Name,
                    COUNTRY_CODE = nationality.TextContent.Get<string>("nationalityCodeCA")
                }
            };
        }

        private object[] GetDocuments(string licenceTypeCode, IEnumerable<JObject> includedTrainings, IEnumerable<JObject> includedExams)
        {
            string[] documentRoleCodes;
            bool hasRoles = LicenceDictionary.LicenceRole.TryGetValue(licenceTypeCode, out documentRoleCodes);

            if (!hasRoles)
            {
                return new object[0];
            }

            var trainings = includedTrainings
                .Where(e => e.Get<string>("valid.code") == "Y")
                .Select(t =>
                    new
                    {
                        DOC = new
                        {
                            DOC_ROLE = t.Get<string>("documentRole.name"),
                            SUB_DOC = new
                            {
                                CLASS = t.Get<string>("documentType.name"),
                                DOC_NO = t.Get<string>("documentNumber"),
                                DATE = t.Get<DateTime>("documentDateValidFrom")
                            }
                        }
                    }).ToArray<dynamic>();

            var exams = includedExams
                .Where(e => e.Get<string>("valid.code") == "Y")
                .Select(e =>
                    new
                    {
                        DOC = new
                        {
                            DOC_ROLE = e.Get<string>("documentRole.name"),
                            SUB_DOC = new
                            {
                                CLASS = e.Get<string>("documentType.name"),
                                DOC_NO = e.Get<string>("documentNumber"),
                                DATE = e.Get<DateTime>("documentDateValidFrom")
                            }
                        }
                    }).ToArray<dynamic>();

            return trainings
                .Union(exams)
                .OrderBy(d => d.DOC.SUB_DOC.DATE)
                .ToArray<object>();
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

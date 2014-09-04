using System;
using System.Collections.Generic;
using System.Linq;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Json;
using Gva.Api.ModelsDO.Persons;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.WordTemplates
{
    public class RVD_Licence : IDataGenerator
    {

        private ILotRepository lotRepository;
        private INomRepository nomRepository;

        public RVD_Licence(
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
                return new string[] { "rvd_licence" };
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
            //TODO - includedExams!    .Select(i => lot.Index.GetPart("personDocumentExams/" + i).Content);

            var classes = this.GetClasses(includedRatings);
            //TODO - includedExams! var documents = this.GetDocuments(includedTrainings, includedExams);
            var licenceCodeCa = licenceType.TextContent.Get<string>("codeCA");
            var licenceNumber = string.Format(
                "BG {0} - {1} - {2}",
                licenceType.Code,
                Utils.PadLicenceNumber(licence.LicenceNumber),
                personData.Lin);

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
                    L_LICENCE_PRIV = this.GetLicencePrivileges(),
                    L_FIRST_ISSUE_DATE = firstEdition.DocumentDateValidFrom,
                    L_ISSUE_DATE = lastEdition.DocumentDateValidFrom,
                    T_LICENCE_HOLDER = Utils.GetLicenceHolder(personData, personAddress),
                    T_LICENCE_NO = licenceNumber,
                    T_LICENCE_CODE = licenceType.Code,
                    T_ACTION = lastEdition.LicenceAction.Name,
                    T_FIRST_ISSUE_DATE = firstEdition.DocumentDateValidFrom,
                    T_ISSUE_DATE = lastEdition.DocumentDateValidFrom,
                    //TODO - includedExams! T_DOCUMENTS = documents,
                    T_CLASSES = classes.Length > 10 ? classes.Take(classes.Length / 2) : classes,
                    L_CLASSES = classes.Length > 10 ? classes.Take(classes.Length / 2) : classes,
                    T_CLASSES2 = classes.Length > 10 ? classes.Skip(classes.Length / 2) : new object[0],
                    L_CLASSES2 = classes.Length > 10 ? classes.Skip(classes.Length / 2) : new object[0]
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

        private object[] GetDocuments(IEnumerable<JObject> includedTrainings, IEnumerable<JObject> includedExams)
        {
            var trainings = includedTrainings
                .Where(t => t.Get<string>("valid.code") == "Y")
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
                                DOC_TYPE = e.Get<string>("documentType.name"),
                                DOC_NO = e.Get<string>("documentNumber"),
                                DATE = e.Get<DateTime>("documentDateValidFrom"),
                                DOC_PUBLISHER = e.Get<string>("documentPublisher")
                            }
                        }
                    }).ToArray<dynamic>();

            return trainings
                .Union(exams)
                .OrderBy(d => d.DOC.SUB_DOC.DATE)
                .ToArray<object>();
        }

        private List<object> GetLicencePrivileges()
        {
            return new List<object>()
            {
                LicenceDictionary.LicencePrivilege["ATSM1"],
                LicenceDictionary.LicencePrivilege["ATSM2"],
                LicenceDictionary.LicencePrivilege["requiresLegalID"]
            };
        }

        private object[] GetClasses(IEnumerable<PersonRatingDO> includedRatings)
        {
            return includedRatings
                .Where(r => r.StaffType.Code == "M")
                .Select(r =>
                {
                    PersonRatingEditionDO lastEdition = r.Editions.Last();

                    return new
                    {
                        LEVEL = r.PersonRatingLevel == null ? null : r.PersonRatingLevel.Code,
                        RATING = r.RatingClass == null ? null : r.RatingClass.Code,
                        SUBRATING = string.Join(",", lastEdition.RatingSubClasses.Select(s => s.Code)),
                        LICENCE = r.Authorization == null ? null : r.Authorization.Code,
                        LIMITATION = string.Join(",", lastEdition.Limitations.Select(s => s.Name)),
                        ISSUE_DATE = lastEdition.DocumentDateValidFrom,
                        VALID_DATE = lastEdition.DocumentDateValidTo
                    };
                }).ToArray<dynamic>();
        }
    }
}

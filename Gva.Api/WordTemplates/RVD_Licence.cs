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
            var personData = lot.Index.GetPart("personData").Content;
            var personAddressPart = lot.Index.GetParts("personAddresses")
               .FirstOrDefault(a => a.Content.Get<string>("valid.code") == "Y");
            var personAddress = personAddressPart == null ?
                new JObject() :
                personAddressPart.Content;
            var licence = lot.Index.GetPart(path).Content;
            var editions = licence.GetItems<JObject>("editions");
            var firstEdition = editions.First();
            var lastEdition = editions.Last();

            var licenceType = this.nomRepository.GetNomValue("licenceTypes", licence.Get<int>("licenceType.nomValueId"));

            var includedTrainings = lastEdition.GetItems<int>("includedTrainings")
                .Select(i => lot.Index.GetPart("personDocumentTrainings/" + i).Content);
            var includedRatings = lastEdition.GetItems<int>("includedRatings")
                .Select(i => lot.Index.GetPart("ratings/" + i).Content);
            var includedExams = lastEdition.GetItems<int>("includedExams")
                .Select(i => lot.Index.GetPart("personDocumentExams/" + i).Content);
            var classes = this.GetClasses(includedRatings);
            var documents = this.GetDocuments(includedTrainings, includedExams);
            var licenceCodeCa = licenceType.TextContent.Get<string>("codeCA");
            var licenceNumber = string.Format(
                "BG {0} - {1} - {2}",
                licenceType.Code,
                licence.Get<string>("licenceNumber"),
                personData.Get<string>("lin"));

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
                    L_LICENCE_PRIV = this.GetLicencePrivileges(licence),
                    L_FIRST_ISSUE_DATE = firstEdition.Get<DateTime>("documentDateValidFrom"),
                    L_ISSUE_DATE = lastEdition.Get<DateTime>("documentDateValidFrom"),
                    T_LICENCE_HOLDER = this.GetLicenceHolder(personData, personAddress),
                    T_LICENCE_NO = licenceNumber,
                    T_LICENCE_CODE = licenceType.Code,
                    T_ACTION = lastEdition.Get<string>("licenceAction.name"),
                    T_FIRST_ISSUE_DATE = firstEdition.Get<DateTime>("documentDateValidFrom"),
                    T_ISSUE_DATE = lastEdition.Get<DateTime>("documentDateValidFrom"),
                    T_DOCUMENTS = documents,
                    T_CLASSES = classes.Length > 10 ? classes.Take(classes.Length / 2) : classes,
                    L_CLASSES = classes.Length > 10 ? classes.Take(classes.Length / 2) : classes,
                    T_CLASSES2 = classes.Length > 10 ? classes.Skip(classes.Length / 2) : new object[0],
                    L_CLASSES2 = classes.Length > 10 ? classes.Skip(classes.Length / 2) : new object[0]
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
                DATE_PLACE_OF_BIRTH = new
                {
                    DATE_OF_BIRTH = personData.Get<DateTime>("dateOfBirth"),
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
                    personAddress.Get<string>("settlement.name"),
                    personAddress.Get<string>("address")),
                ADDRESS_TRANS = string.Format(
                    "{0}, {1}",
                    personAddress.Get<string>("addressAlt"),
                    personAddress.Get<string>("settlement.nameAlt"))
            };
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

        private List<object> GetLicencePrivileges(JObject licence)
        {
            return new List<object>()
            {
                LicenceDictionary.LicencePrivilege["ATSM1"],
                LicenceDictionary.LicencePrivilege["ATSM2"],
                LicenceDictionary.LicencePrivilege["requiresLegalID"]
            };
        }

        private object[] GetClasses(IEnumerable<JObject> includedRatings)
        {
            return includedRatings
                .Where(r => r.Get<string>("staffType.code") == "M")
                .Select(r =>
                {
                    JObject lastEdition = r.GetItems<JObject>("editions").Last();
                    var ratingClass = r.Get<NomValue>("ratingClass");
                    var authorization = r.Get<NomValue>("authorization");
                    string subratings = string.Join(",", lastEdition.GetItems("ratingSubClasses").Select(s => s.Get("code")));
                    string limitations = string.Join(",", lastEdition.GetItems("limitations").Select(s => s.Get("name")));
                    return new
                    {
                        LEVEL = r.Get<string>("personRatingLevel.code"),
                        RATING = ratingClass.Code,
                        SUBRATING = subratings,
                        LICENCE = authorization.Code,
                        LIMITATION = limitations,
                        ISSUE_DATE = lastEdition.Get<DateTime>("documentDateValidFrom"),
                        VALID_DATE = lastEdition.Get<DateTime>("documentDateValidTo")
                    };
                }).ToArray<dynamic>();
        }
    }
}

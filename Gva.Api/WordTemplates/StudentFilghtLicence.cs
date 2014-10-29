using System.Collections.Generic;
using System.Linq;
using Common.Api.Repositories.NomRepository;
using Common.Json;
using Gva.Api.ModelsDO.Persons;
using Regs.Api.Repositories.LotRepositories;
using Regs.Api.Models;

namespace Gva.Api.WordTemplates
{
    public class StudentFilghtLicence : IDataGenerator
    {
        private static Dictionary<string, List<object>> licencePrivileges = new Dictionary<string, List<object>>
        {
            {
                "SP(H)",
                new List<object>()
                {
                    LicenceDictionary.LicencePrivilege["instr"],
                    LicenceDictionary.LicencePrivilege["medCertClass1or2"],
                    LicenceDictionary.LicencePrivilege["idDoc"]
                }
            },
            {
                "SP(A)",
                new List<object>()
                {
                    LicenceDictionary.LicencePrivilege["instr"],
                    LicenceDictionary.LicencePrivilege["medCertClass1or2"],
                    LicenceDictionary.LicencePrivilege["idDoc"]
                }
            }
        };

        private ILotRepository lotRepository;
        private INomRepository nomRepository;

        public StudentFilghtLicence(
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
                return new string[] { "student_flight_licence" };
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
            var includedMedicals = lastEdition.IncludedMedicals
                .Select(i => lot.Index.GetPart<PersonMedicalDO>("personDocumentMedicals/" + i).Content);
            var includedRatings = lastEdition.IncludedRatings
                .Select(i => lot.Index.GetPart<PersonRatingDO>("ratings/" + i));
            var ratingEditions = lot.Index.GetParts<PersonRatingEditionDO>("ratingEditions");

            var licenceCaCode = this.nomRepository.GetNomValue("licenceTypes", licence.LicenceType.NomValueId).TextContent.Get<string>("codeCA");
            var licenceTypeCode = licence.LicenceType.Code;
            var licenceNumber = string.Format(
                "BGR {0} - {1} - {2}",
                licenceTypeCode,
                Utils.PadLicenceNumber(licence.LicenceNumber),
                personData.Lin);

            var documents = this.GetDocuments(licenceTypeCode, includedTrainings);

            var json = new
            {
                root = new
                {
                    L_LICENCE_TYPE_CA_CODE = licenceCaCode,
                    L_LICENCE_TYPE_CA_CODE2 = licenceCaCode,
                    L_LICENCE_NO = licenceNumber,
                    L_LICENCE_HOLDER = this.GetPersonData(personData, personAddress),
                    L_LICENCE_PRIV = this.GetLicencePrivileges(licenceTypeCode, lastEdition),
                    L_FIRST_ISSUE_DATE = firstEdition.DocumentDateValidFrom,
                    L_ISSUE_DATE = lastEdition.DocumentDateValidFrom,
                    T_LICENCE_HOLDER = Utils.GetLicenceHolder(personData, personAddress),
                    T_LICENCE_NO = licenceNumber,
                    T_FIRST_ISSUE_DATE = firstEdition.DocumentDateValidFrom,
                    T_VALID_DATE = lastEdition.DocumentDateValidTo,
                    T_ACTION = lastEdition.LicenceAction.Name.ToUpper(),
                    T_ISSUE_DATE = lastEdition.DocumentDateValidFrom,
                    T_DOCUMENTS = documents.Take(documents.Length / 2),
                    T_DOCUMENTS2 = documents.Skip(documents.Length / 2),
                    T_MED_CERT = this.GetMedCerts(licenceTypeCode, includedMedicals, personData),
                    T_RATING = this.GetRaitings(includedRatings, ratingEditions),
                    L_RATING = this.GetScools(includedRatings, ratingEditions),
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
                ADDRESS = string.Format(
                    "{0}, {1}",
                    personAddress.Settlement.Name,
                    personAddress.Address),
                ADDRESS_TRANS = string.Format(
                    "{0}, {1}",
                    personAddress.AddressAlt,
                    personAddress.Settlement.NameAlt),
                NATIONALITY = new
                {
                    COUNTRY_NAME_BG = nationality.Name,
                    COUNTRY_CODE = nationality.TextContent.Get<string>("nationalityCodeCA")
                }
            };
        }

        private List<object> GetLicencePrivileges(string licenceTypeCode, PersonLicenceEditionDO edition)
        {
            List<object> privileges;
            List<dynamic> result = new object[0].ToList();

            if (licencePrivileges.TryGetValue(licenceTypeCode, out privileges))
            {
                if (licenceTypeCode == "SP(H)" || licenceTypeCode == "SP(A)")
                {
                    dynamic dateValidPrivilege = LicenceDictionary.LicencePrivilege["dateValid"];
                    string dateValid = edition.DocumentDateValidTo.Value.ToString("dd.MM.yyyy");
                    string dateValidTrans = edition.DocumentDateValidTo.Value.ToString("dd MMMM yyyy");


                    result = new List<object>(privileges);
                    result.Add(new
                    {
                        NO = dateValidPrivilege.NO,
                        NAME_BG = string.Format(dateValidPrivilege.NAME_BG, dateValid),
                        NAME_TRANS = string.Format(dateValidPrivilege.NAME_TRANS, dateValidTrans)
                    });
                }
            }

            return result
                .OrderBy(p => p.NO)
                .ToList<object>();
        }

        private object[] GetDocuments(string licenceTypeCode, IEnumerable<PersonTrainingDO> includedTrainings)
        {
            string[] documentRoleCodes;
            bool hasRoles = LicenceDictionary.LicenceRole.TryGetValue(licenceTypeCode, out documentRoleCodes);

            if (!hasRoles)
            {
                return new object[0];
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

        private object[] GetMedCerts(string licenceTypeCode, IEnumerable<PersonMedicalDO> includedMedicals, PersonDataDO personData)
        {
            int orderNumber = licenceTypeCode == "SP(A)" ? 8 : 1;

            return includedMedicals.Select(m =>
                new
                {
                    ORDER_NO = orderNumber,
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
                    LIMITATION = m.Limitations != null? string.Join(",", m.Limitations.Select(l => l.Name)) : string.Empty
                }).ToArray<object>();
        }

        private List<object> GetRaitings(IEnumerable<PartVersion<PersonRatingDO>> includedRatings, IEnumerable<PartVersion<PersonRatingEditionDO>> ratingEditions)
        {
            var result = includedRatings.Select(r =>
                {
                    var lastEdition = ratingEditions.Where(e => e.Content.RatingPartIndex == r.Part.Index).OrderBy(e => e.Content.Index).Last();

                    return new
                    {
                        TYPE = string.Format(
                            "{0} {1}",
                            r.Content.RatingClass == null ? null : r.Content.RatingClass.Name,
                            r.Content.RatingType == null ? null : r.Content.RatingType.Name),
                        AUTH_NOTES = string.Format(
                            "{0} {1}",
                            r.Content.Authorization == null ? null : r.Content.Authorization.Name,
                            lastEdition.Content.Notes),
                        ISSUE_DATE = lastEdition.Content.DocumentDateValidFrom,
                        VALID_DATE = lastEdition.Content.DocumentDateValidTo
                    };
                }).ToList<object>();

            result = Utils.FillBlankData(result, 3);
            return result;
        }

        private object[] GetScools(IEnumerable<PartVersion<PersonRatingDO>> includedRatings, IEnumerable<PartVersion<PersonRatingEditionDO>> ratingEditions)
        {
            return includedRatings
                .Where(r => r.Content.RatingType != null && r.Content.RatingClass != null)
                .Select(r =>
                    {
                        var lastEdition = ratingEditions.Where(e => e.Content.RatingPartIndex == r.Part.Index).OrderBy(e => e.Content.Index).Last();

                        return new
                        {
                            SCHOOL = lastEdition.Content.Notes,
                            TYPE = string.Format(
                                "{0} {1}",
                                r.Content.RatingClass.Name,
                                r.Content.RatingType.Name),
                            ISSUE_DATE = lastEdition.Content.DocumentDateValidFrom,
                            VALID_DATE = lastEdition.Content.DocumentDateValidTo
                        };
                    }).ToArray<object>();
        }

        private IEnumerable<object> GetAbbreviations()
        {
            return new[]
            {
                LicenceDictionary.LicenceAbbreviation["ATP"],
                LicenceDictionary.LicenceAbbreviation["SP"],
            };
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using Common.Api.Repositories.NomRepository;
using Common.Json;
using Gva.Api.ModelsDO.Persons;
using Regs.Api.Repositories.LotRepositories;
using Regs.Api.Models;

namespace Gva.Api.WordTemplates
{
    public class FlightLicence : IDataGenerator
    {
        private static Dictionary<string, List<object>> licencePrivileges = new Dictionary<string, List<object>>
        {
            {
                "F/CL",
                new List<object>()
                {
                    LicenceDictionary.LicencePrivilege["12typeVS"],
                    LicenceDictionary.LicencePrivilege["medCert2"],
                    LicenceDictionary.LicencePrivilege["idDoc"]
                }
            },
            {
                "ATPL(A)",
                new List<object>()
                {
                    LicenceDictionary.LicencePrivilege["ATPL(A)"],
                    LicenceDictionary.LicencePrivilege["medCert2"],
                    LicenceDictionary.LicencePrivilege["idDoc"],
                    LicenceDictionary.LicencePrivilege["PPL(A)/CPL(A)/IR(A)"],
                    LicenceDictionary.LicencePrivilege["PIC/CO-com"]
                }
            },
            {
                "ATPL(H)",
                new List<object>()
                {
                    LicenceDictionary.LicencePrivilege["ATPL(H)"],
                    LicenceDictionary.LicencePrivilege["medCert2"],
                    LicenceDictionary.LicencePrivilege["idDoc"],
                    LicenceDictionary.LicencePrivilege["PPL(H)/CPL(H)/IR(H)"],
                    LicenceDictionary.LicencePrivilege["PIC/CO-com"]
                }
            },
            {
                "CPL(A)",
                new List<object>()
                {
                    LicenceDictionary.LicencePrivilege["CPL(A)"],
                    LicenceDictionary.LicencePrivilege["medCert2"],
                    LicenceDictionary.LicencePrivilege["idDoc"],
                    LicenceDictionary.LicencePrivilege["PPL(A)2"],
                    LicenceDictionary.LicencePrivilege["PIC/CO"],
                    LicenceDictionary.LicencePrivilege["PIC"],
                    LicenceDictionary.LicencePrivilege["CO"]
                }
            },
            {
                "CPL(H)",
                new List<object>()
                {
                    LicenceDictionary.LicencePrivilege["CPL(H)"],
                    LicenceDictionary.LicencePrivilege["medCert2"],
                    LicenceDictionary.LicencePrivilege["idDoc"],
                    LicenceDictionary.LicencePrivilege["PPL(H)2"],
                    LicenceDictionary.LicencePrivilege["PIC/CO"],
                    LicenceDictionary.LicencePrivilege["PIC-heli"],
                    LicenceDictionary.LicencePrivilege["CO-heli"]
                }
            },
            {
                "FDL",
                new List<object>()
                {
                    LicenceDictionary.LicencePrivilege["FDL"],
                    LicenceDictionary.LicencePrivilege["idDoc"]
                }
            },
            {
                "FEL",
                new List<object>()
                {
                    LicenceDictionary.LicencePrivilege["F/EL"],
                    LicenceDictionary.LicencePrivilege["medCert2"],
                    LicenceDictionary.LicencePrivilege["idDoc"]
                }
            },
            {
                "FOL",
                new List<object>()
                {
                    LicenceDictionary.LicencePrivilege["F/OL"],
                    LicenceDictionary.LicencePrivilege["medCert2"],
                    LicenceDictionary.LicencePrivilege["idDoc"]
                }
            },
            {
                "PL(FB)",
                new List<object>()
                {
                    LicenceDictionary.LicencePrivilege["PL(FB)"],
                    LicenceDictionary.LicencePrivilege["PL(FB)-noPoW"],
                    LicenceDictionary.LicencePrivilege["medCert3"],
                    LicenceDictionary.LicencePrivilege["idDoc2"]
                }
            },
            {
                "PL(G)",
                new List<object>()
                {
                    LicenceDictionary.LicencePrivilege["PL(G)"],
                    LicenceDictionary.LicencePrivilege["medCert2"],
                    LicenceDictionary.LicencePrivilege["idDoc"],
                    LicenceDictionary.LicencePrivilege["VFR"],
                    LicenceDictionary.LicencePrivilege["noPoW"]
                }
            },
            {
                "PPL(A)",
                new List<object>()
                {
                    LicenceDictionary.LicencePrivilege["PPL(A)"],
                    LicenceDictionary.LicencePrivilege["medCertClass2"],
                    LicenceDictionary.LicencePrivilege["idDoc"],
                    LicenceDictionary.LicencePrivilege["VFRXII"]
                }
            },
            {
                "PPL(H)",
                new List<object>()
                {
                    LicenceDictionary.LicencePrivilege["PPL(H)"],
                    LicenceDictionary.LicencePrivilege["medCertClass2"],
                    LicenceDictionary.LicencePrivilege["idDoc"],
                    LicenceDictionary.LicencePrivilege["VFRXII"]
                }
            },
            {
                "PPL(SA)",
                new List<object>()
                {
                    LicenceDictionary.LicencePrivilege["PPL(SA)"],
                    LicenceDictionary.LicencePrivilege["medCert2"],
                    LicenceDictionary.LicencePrivilege["idDoc"],
                    LicenceDictionary.LicencePrivilege["VFR"],
                    LicenceDictionary.LicencePrivilege["noPoW"]
                }
            }
        };

        private ILotRepository lotRepository;
        private INomRepository nomRepository;
        private int index;

        public FlightLicence(
            ILotRepository lotRepository,
            INomRepository nomRepository)
        {
            this.lotRepository = lotRepository;
            this.nomRepository = nomRepository;
            index = 1;
        }

        public string[] TemplateNames
        {
            get
            {
                return new string[] { "convoy", "flight_licence", "flight" };
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

            var licenceNumber = string.Format(
                "BG {0} - {1} - {2}",
                licence.LicenceType.Code,
                Utils.PadLicenceNumber(licence.LicenceNumber),
                personData.Lin);
            var licenceType = this.nomRepository.GetNomValue("licenceTypes", licence.LicenceType.NomValueId);
            var licenceCaCode = licenceType.TextContent.Get<string>("codeCA");

            var documents = this.GetDocuments(licenceType.Code, includedTrainings);

            var json = new
            {
                root = new
                {
                    L_LICENCE_TYPE_CA_CODE = licenceCaCode,
                    L_LICENCE_TYPE_CA_CODE2 = licenceCaCode,
                    L_LICENCE_TYPE_NAME = licenceType.Name.ToUpper(),
                    L_LICENCE_TYPE_NAME_TRANS = licenceType.NameAlt == null ? string.Empty : licenceType.NameAlt.ToUpper(),
                    L_LICENCE_NO = licenceNumber,
                    L_LICENCE_HOLDER = this.GetPersonData(personData, personAddress),
                    L_LICENCE_PRIV = this.GetLicencePrivilege(licenceType.Code, lastEdition),
                    L_FIRST_ISSUE_DATE = firstEdition.DocumentDateValidFrom,
                    L_ISSUE_DATE = lastEdition.DocumentDateValidFrom,
                    T_LICENCE_HOLDER = Utils.GetLicenceHolder(personData, personAddress),
                    T_LICENCE_TYPE_NAME = licenceType.Name.ToLower(),
                    T_LICENCE_NO = licenceNumber,
                    T_FIRST_ISSUE_DATE = firstEdition.DocumentDateValidFrom,
                    T_VALID_DATE = lastEdition.DocumentDateValidTo,
                    T_ACTION = lastEdition.LicenceAction.Name.ToUpper(),
                    T_ISSUE_DATE = lastEdition.DocumentDateValidFrom,
                    T_DOCUMENTS = documents.Take(documents.Length / 2),
                    T_DOCUMENTS2 = documents.Skip(documents.Length / 2),
                    T_MED_CERT = this.GetMedCerts(licenceType.Code, includedMedicals, personData),
                    T_RATING = this.GetTRatings(includedRatings, ratingEditions),
                    L_RATING = this.GetLRatings(includedRatings, ratingEditions),
                    L_ABBREVIATION = this.GetAbbreviations(licenceType.Code)
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
                    personAddress.Settlement != null? personAddress.Settlement.Name : null,
                    personAddress.Address),
                ADDRESS_TRANS = string.Format(
                    "{0}, {1}",
                    personAddress.AddressAlt,
                    personAddress.Settlement != null ? personAddress.Settlement.NameAlt : null),
                NATIONALITY = new
                {
                    COUNTRY_NAME_BG = nationality.Name,
                    COUNTRY_CODE = nationality.TextContent.Get<string>("nationalityCodeCA")
                }
            };
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
                .Where(t => t.Valid.Code == "Y" && documentRoleCodes.Contains(t.DocumentRole.Code))
                .OrderBy(t => t.DocumentDateValidFrom)
                .Select(t =>
                    new
                    {
                        DOC = new
                        {
                            DOC_ROLE = string.Format("{0}. {1}", this.index++, t.DocumentRole.Name),
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

        private object[] GetMedCerts(
            string licenceTypeCode,
            IEnumerable<PersonMedicalDO> includedMedicals,
            PersonDataDO personData)
        {
            return includedMedicals.Select(m =>
                new
                {
                    ORDER_NO = index++,
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
                }).ToArray<object>();
        }

        private List<object> GetTRatings(IEnumerable<PartVersion<PersonRatingDO>> includedRatings, IEnumerable<PartVersion<PersonRatingEditionDO>> ratingEditions)
        {
            var result = includedRatings.Select(r =>
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
                        lastEdition.Content.Notes).Trim(),
                    ISSUE_DATE = lastEdition.Content.DocumentDateValidFrom,
                    VALID_DATE = lastEdition.Content.DocumentDateValidTo
                };
            }).ToList<object>();

            result = Utils.FillBlankData(result, 11);
            return result;
        }

        private List<object> GetLRatings(IEnumerable<PartVersion<PersonRatingDO>> includedRatings, IEnumerable<PartVersion<PersonRatingEditionDO>> ratingEditions)
        {
            var result = includedRatings.Select(r =>
            {
                var lastEdition = ratingEditions.Where(e => e.Content.RatingPartIndex == r.Part.Index).OrderBy(e => e.Content.Index).Last();

                return new
                {
                    TYPE = string.Format(
                        "{0} {1}",
                        r.Content.RatingClass == null ? string.Empty : r.Content.RatingClass.Name,
                        r.Content.RatingType == null ? string.Empty : r.Content.RatingType.Name).Trim(),
                    AUTH = r.Content.Authorization == null ? string.Empty : r.Content.Authorization.Name,
                    NOTES = lastEdition.Content.Notes,
                    ISSUE_DATE = lastEdition.Content.DocumentDateValidFrom,
                    VALID_DATE = lastEdition.Content.DocumentDateValidTo
                };
            }).ToList<object>();


            result = Utils.FillBlankData(result, 11);
            return result;
        }

        private List<object> GetLicencePrivilege(string licenceTypeCode, PersonLicenceEditionDO edition)
        {
            List<object> privileges;
            List<dynamic> result = new object[0].ToList();

            if (licencePrivileges.TryGetValue(licenceTypeCode, out privileges))
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

            return result.OrderBy(p => p.NO).ToList<object>();
        }

        private IEnumerable<object> GetAbbreviations(string licenceTypeCode)
        {
            if (licenceTypeCode == "ATPL(A)" ||
                licenceTypeCode == "ATPL(H)" ||
                licenceTypeCode == "CPL(A)" ||
                licenceTypeCode == "CPL(H)" ||
                licenceTypeCode == "PL(FB)" ||
                licenceTypeCode == "PL(G)" ||
                licenceTypeCode == "PPL(A)" ||
                licenceTypeCode == "PPL(H)" ||
                licenceTypeCode == "PPL(SA)")
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

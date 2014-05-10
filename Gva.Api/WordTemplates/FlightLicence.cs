using System;
using System.Collections.Generic;
using System.Linq;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Json;
using Newtonsoft.Json.Linq;
using Regs.Api.Repositories.LotRepositories;

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
                    LicenceDictionary.LicencePrivilege["PPL(A)"],
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
                    LicenceDictionary.LicencePrivilege["PPL(H)"],
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

        public JObject GetData(int lotId, string path, int index)
        {
            var lot = this.lotRepository.GetLotIndex(lotId);
            var personData = lot.GetPart("personData").Content;
            var personAddressPart = lot.GetParts("personAddresses")
               .FirstOrDefault(a => a.Content.Get<string>("valid.code") == "Y");
            var personAddress = personAddressPart == null ?
                new JObject() :
                personAddressPart.Content;
            var licence = lot.GetPart(path).Content;
            var edition = licence.Get<JObject>(string.Format("editions[{0}]", index));
            var firstEdition = licence.Get<JObject>("editions[0]");

            var includedTrainings = edition.GetItems<int>("includedTrainings")
                .Select(i => lot.GetPart("personDocumentTrainings/" + i).Content);
            var includedMedicals = edition.GetItems<int>("includedMedicals")
                .Select(i => lot.GetPart("personDocumentMedicals/" + i).Content);
            var includedRatings = edition.GetItems<int>("includedRatings")
                .Select(i => lot.GetPart("ratings/" + i).Content);

            var licenceNumber = string.Format(
                "BG {0} - {1} - {2}",
                licence.Get<string>("licenceType.code"),
                licence.Get<string>("licenceNumber"),
                personData.Get<string>("lin"));
            var licenceType = this.nomRepository.GetNomValue("licenceTypes", licence.Get<int>("licenceType.nomValueId"));
            var licenceCaCode = JObject.Parse(this.nomRepository.GetNomValue(
                    "licenceTypes",
                    licence.Get<int>("licenceType.nomValueId")).TextContent)
                .Get<string>("codeCA");

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
                    L_LICENCE_PRIV = this.GetLicencePrivilege(licenceType.Code, edition),
                    L_FIRST_ISSUE_DATE = firstEdition.Get<DateTime>("documentDateValidFrom"),
                    L_ISSUE_DATE = edition.Get<DateTime>("documentDateValidFrom"),
                    T_LICENCE_HOLDER = this.GetLicenceHolder(personData, personAddress),
                    T_LICENCE_TYPE_NAME = licenceType.Name.ToLower(),
                    T_LICENCE_NO = licenceNumber,
                    T_FIRST_ISSUE_DATE = firstEdition.Get<DateTime>("documentDateValidFrom"),
                    T_VALID_DATE = edition.Get<DateTime>("documentDateValidTo"),
                    T_ACTION = edition.Get<string>("licenceAction.name").ToUpper(),
                    T_ISSUE_DATE = edition.Get<DateTime>("documentDateValidFrom"),
                    T_DOCUMENTS = documents.Take(documents.Length / 2),
                    T_DOCUMENTS2 = documents.Skip(documents.Length / 2),
                    T_MED_CERT = this.GetMedCerts(licenceType.Code, includedMedicals, personData),
                    T_RATING = this.GetTRatings(includedRatings),
                    L_RATING = this.GetLRatings(includedRatings),
                    L_ABBREVIATION = this.GetAbbreviations(licenceType.Code)
                }
            };

            return JObject.FromObject(json);
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

        private object GetPersonData(JObject personData, JObject personAddress)
        {
            var placeOfBirth = personData.Get<NomValue>("placeOfBirth");
            var country = this.nomRepository.GetNomValue("countries", placeOfBirth.ParentValueId.Value);
            var nationality = this.nomRepository.GetNomValue("countries", personData.Get<int>("country.nomValueId"));

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
                    personAddress.Get<string>("settlement.nameAlt")),
                NATIONALITY = new
                {
                    COUNTRY_NAME_BG = nationality.Name,
                    COUNTRY_CODE = JObject.Parse(nationality.TextContent).Get<string>("nationalityCodeCA")
                }
            };
        }

        private object[] GetDocuments(string licenceTypeCode, IEnumerable<JObject> includedTrainings)
        {
            string[] documentRoleCodes;
            bool hasRoles = LicenceDictionary.LicenceRole.TryGetValue(licenceTypeCode, out documentRoleCodes);

            if (!hasRoles)
            {
                return new object[0];
            }

            return includedTrainings
                .Where(t => t.Get<string>("valid.code") == "Y" && documentRoleCodes.Contains(t.Get<string>("documentRole.code")))
                .OrderBy(t => t.Get<DateTime>("documentDateValidFrom"))
                .Select(t =>
                    new
                    {
                        DOC = new
                        {
                            DOC_ROLE = string.Format("{0}. {1}", this.index++, t.Get<string>("documentRole.name")),
                            SUB_DOC = new
                            {
                                DOC_TYPE = t.Get<string>("documentType.name"),
                                DOC_NO = t.Get<string>("documentNumber"),
                                DATE = t.Get<DateTime>("documentDateValidFrom"),
                                DOC_PUBLISHER = t.Get<string>("documentPublisher")
                            }
                        }
                    }).ToArray<object>();
        }

        private object[] GetMedCerts(string licenceTypeCode, IEnumerable<JObject> includedMedicals, JObject personData)
        {
            return includedMedicals.Select(m =>
                new
                {
                    ORDER_NO = index++,
                    NO = string.Format(
                        "{0}-{1}-{2}-{3}",
                        m.Get<string>("documentNumberPrefix"),
                        m.Get<string>("documentNumber"),
                        personData.Get<string>("lin"),
                        m.Get<string>("documentNumberSuffix")),
                    ISSUE_DATE = m.Get<DateTime>("documentDateValidFrom"),
                    VALID_DATE = m.Get<DateTime>("documentDateValidTo"),
                    CLASS = m.Get<string>("medClass.name"),
                    PUBLISHER = m.Get<string>("documentPublisher.name"),
                    LIMITATION = string.Join(",", m.GetItems<JObject>("limitations").Select(l => l.Get<string>("name")))
                }).ToArray<object>();
        }

        private List<object> GetTRatings(IEnumerable<JObject> includedRatings)
        {
            return includedRatings.Select(r =>
            {
                JObject lastEdition = r.GetItems<JObject>("editions").Last();

                return new
                {
                    TYPE = string.Format(
                        "{0} {1}",
                        r.Get<string>("ratingClass.name"),
                        r.Get<string>("ratingType.name")),
                    AUTH_NOTES = string.Format(
                        "{0} {1}",
                        r.Get<string>("authorization.name"),
                        lastEdition.Get<string>("notes")),
                    ISSUE_DATE = lastEdition.Get<DateTime>("documentDateValidFrom"),
                    VALID_DATE = lastEdition.Get<DateTime>("documentDateValidTo")
                };
            }).ToList<object>();
        }

        private List<object> GetLRatings(IEnumerable<JObject> includedRatings)
        {
            return includedRatings.Select(r =>
            {
                JObject lastEdition = r.GetItems<JObject>("editions").Last();

                return new
                {
                    TYPE = string.Format(
                        "{0} {1}",
                        r.Get<string>("ratingClass.name"),
                        r.Get<string>("ratingType.name")),
                    AUTH = r.Get<string>("authorization.name"),
                    NOTES = lastEdition.Get<string>("notes"),
                    ISSUE_DATE = lastEdition.Get<DateTime>("documentDateValidFrom"),
                    VALID_DATE = lastEdition.Get<DateTime>("documentDateValidTo")
                };
            }).ToList<object>();
        }

        private List<object> GetLicencePrivilege(string licenceTypeCode, JObject edition)
        {
            List<object> privileges;
            List<dynamic> result = new object[0].ToList();

            if (licencePrivileges.TryGetValue(licenceTypeCode, out privileges))
            {
                dynamic dateValidPrivilege = LicenceDictionary.LicencePrivilege["dateValid"];
                string dateValid = edition.Get<DateTime>("documentDateValidTo").ToString("dd.MM.yyyy");

                result = new List<object>(privileges);
                result.Add(new
                {
                    NAME_BG = string.Format(dateValidPrivilege.NAME_BG, dateValid),
                    NAME_TRANS = string.Format(dateValidPrivilege.NAME_TRANS, dateValid),
                    NO = dateValidPrivilege.NO
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

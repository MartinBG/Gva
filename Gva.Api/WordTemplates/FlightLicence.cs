using System.Collections.Generic;
using System.Linq;
using Common.Api.Repositories.NomRepository;
using Common.Json;
using Gva.Api.ModelsDO.Persons;
using Regs.Api.Repositories.LotRepositories;
using Regs.Api.Models;
using Common.Api.Models;
using System;

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
        private int number;

        public FlightLicence(
            ILotRepository lotRepository,
            INomRepository nomRepository)
        {
            this.lotRepository = lotRepository;
            this.nomRepository = nomRepository;
            number = 6;
        }

        public string GeneratorCode
        {
            get
            {
                return "flightLicence";
            }
        }

        public string GeneratorName
        {
            get
            {
                return "Лиценз за летателна правоспособност";
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
            var includedExams = lastEdition.IncludedExams
                .Select(i => lot.Index.GetPart<PersonTrainingDO>("personDocumentTrainings/" + i).Content);
            var includedChecks = lastEdition.IncludedChecks
                .Select(i => lot.Index.GetPart<PersonCheckDO>("personDocumentChecks/" + i).Content);
            var includedLangCerts = lastEdition.IncludedLangCerts
                .Select(i => lot.Index.GetPart<PersonLangCertDO>("personDocumentLangCertificates/" + i).Content);
            var includedRatings = lastEdition.IncludedRatings
                .Select(i => i.Ind)
                .Distinct()
                .Select(i => lot.Index.GetPart<PersonRatingDO>("ratings/" + i.Value));
            var ratingEditions = lastEdition.IncludedRatings.Select(i => lot.Index.GetPart<PersonRatingEditionDO>("ratingEditions/" + i.Index));

            var licenceNumberCode = licence.LicenceType.Code.Replace("/", ".").Replace("(", "").Replace(")", "");
            var licenceNumber = string.Format(
                "BGR. {0} - {1} - {2}",
                licenceNumberCode.EndsWith("L") ? licenceNumberCode.Remove(licenceNumberCode.LastIndexOf("L")) : licenceNumberCode,
                Utils.PadLicenceNumber(licence.LicenceNumber),
                personData.Lin);
            var licenceType = this.nomRepository.GetNomValue("licenceTypes", licence.LicenceType.NomValueId);
            var licenceCaCode = licenceType.TextContent.Get<string>("codeCA");

            var documents = this.GetDocuments(licenceType.Code, includedTrainings, includedChecks, includedLangCerts, includedExams);

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
                    T_DOCUMENTS = documents.Take((documents.Count() / 2) + 1),
                    T_DOCUMENTS2 = documents.Skip((documents.Count() / 2) + 1),
                    T_MED_CERT = Utils.GetMedCerts(this.number++, includedMedicals, personData),
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
                    COUNTRY_NAME_BG = nationality != null ? nationality.Name : null,
                    COUNTRY_CODE = nationality != null ? nationality.TextContent.Get<string>("nationalityCodeCA") : null
                }
            };
        }

        private List<object> GetDocuments(
            string licenceTypeCode,
            IEnumerable<PersonTrainingDO> includedTrainings,
            IEnumerable<PersonCheckDO> includedChecks,
            IEnumerable<PersonLangCertDO> includedLangCerts,
            IEnumerable<PersonTrainingDO> includedExams)
        {
            string[] documentRoleCodes;
            bool hasRoles = LicenceDictionary.LicenceRole.TryGetValue(licenceTypeCode, out documentRoleCodes);
            List<object> documents = new List<object>();

            if (!hasRoles)
            {
                return documents;
            }

            if(licenceTypeCode == "CPL(H)")
            {
                NomValue educationRole = this.nomRepository.GetNomValue("documentRoles", "education");
                var education = Utils.GetTrainingsByCode(includedTrainings, educationRole.Code, documentRoleCodes);

                documents.Add(new
                {
                    DOC = new
                    {
                        DOC_ROLE = String.Format("{0}. {1}", number++, LicenceDictionary.DocumentTitle["Education"]),
                        SUB_DOC = Utils.FillBlankData(education, 1)
                    }
                });
            }

            if ((new List<string> { "F/CL", "FDL", "PPL(A)", "CPL(A)", "ATPL(A)", "CPL(H)", "ATPL(H)", "PPL(SA)", "FEL" }).Contains(licenceTypeCode))
            {
                NomValue theoreticalTrainingRole = this.nomRepository.GetNomValue("documentRoles", "theoreticalTraining");
                var theoreticalTrainings = Utils.GetTrainingsByCode(includedTrainings, theoreticalTrainingRole.Code, documentRoleCodes);

                documents.Add(new
                {
                    DOC = new
                    {
                        DOC_ROLE = String.Format("{0}. {1}", number++, LicenceDictionary.DocumentTitle["TheoreticalTraining"]),
                        SUB_DOC = Utils.FillBlankData(theoreticalTrainings, 1)
                    }
                });
            }

            if ((new List<string> { "F/CL", "FDL" }).Contains(licenceTypeCode))
            {
                NomValue practicalTrainingRole = this.nomRepository.GetNomValue("documentRoles", "practicalTraining");
                var practicalTrainings = Utils.GetTrainingsByCode(includedTrainings, practicalTrainingRole.Code, documentRoleCodes);

                documents.Add(new
                {
                    DOC = new
                    {
                        DOC_ROLE = String.Format("{0}. {1}", number++, LicenceDictionary.DocumentTitle["PracticalTraining"]),
                        SUB_DOC = Utils.FillBlankData(practicalTrainings, 1)
                    }
                });
            }

            if ("F/CL" == licenceTypeCode)
            {
                NomValue practicalCheckRole = this.nomRepository.GetNomValue("documentRoles", "practicalCheck");
                var practicalChecks = Utils.FillBlankData(Utils.GetChecksByCode(includedChecks, practicalCheckRole.Code, documentRoleCodes), 1);

                documents.Add(new
                {
                    DOC = new
                    {
                        DOC_ROLE = String.Format("{0}. {1}", number++, LicenceDictionary.DocumentTitle["PracticalCheck"]),
                        SUB_DOC = practicalChecks
                    }
                });
            }

            if ((new List<string> { "PPL(A)", "CPL(A)", "ATPL(A)", "PPL(SA)", "FEL" }).Contains(licenceTypeCode))
            {
                NomValue flyingTrainingRole = this.nomRepository.GetNomValue("documentRoles", "flyingTraining");
                var flyingTrainings = Utils.GetTrainingsByCode(includedTrainings, flyingTrainingRole.Code, documentRoleCodes);

                documents.Add(new
                {
                    DOC = new
                    {
                        DOC_ROLE = String.Format("{0}. {1}", number++, LicenceDictionary.DocumentTitle["FlyingTraining"]),
                        SUB_DOC = Utils.FillBlankData(flyingTrainings, 1)
                    }
                });
            }

            if ((new List<string> { "PPL(A)", "CPL(A)", "ATPL(A)", "CPL(H)", "ATPL(H)", "PPL(SA)", "FEL" }).Contains(licenceTypeCode))
            {
                NomValue flyingCheckRole = this.nomRepository.GetNomValue("documentRoles", "flyingCheck");
                var flyingChecks = Utils.GetTrainingsByCode(includedTrainings, flyingCheckRole.Code, documentRoleCodes);

                documents.Add(new
                {
                    DOC = new
                    {
                        DOC_ROLE = String.Format("{0}. {1}", number++, LicenceDictionary.DocumentTitle["FlyingCheck"]),
                        SUB_DOC = Utils.FillBlankData(flyingChecks, 1)
                    }
                });
            }

            if ((new List<string> { "F/CL", "FDL", "PPL(A)", "CPL(A)", "ATPL(A)", "PPL(SA)", "FEL", "PL(FB)" }).Contains(licenceTypeCode))
            {
                NomValue theoreticalExamRole = this.nomRepository.GetNomValue("documentRoles", "exam");
                var theoreticalExams = includedExams
                    .Where(t => documentRoleCodes.Contains(t.DocumentRole.Code) && t.DocumentRole.Code == theoreticalExamRole.Code)
                    .Select(t =>
                        new
                        {
                            DOC_TYPE = t.DocumentType.Name.ToLower(),
                            DOC_NO = t.DocumentNumber,
                            DATE = t.DocumentDateValidFrom,
                            DOC_PUBLISHER = t.DocumentPublisher
                        }).ToList<object>();

                documents.Add(new
                {
                    DOC = new
                    {
                        DOC_ROLE = String.Format("{0}. {1}", number++, LicenceDictionary.DocumentTitle["TheoreticalExam"]),
                        SUB_DOC = Utils.FillBlankData(theoreticalExams, 1)
                    }
                });
            }

            if ((new List<string> { "CPL(A)", "ATPL(A)", "PL(FB)", "FEL" }).Contains(licenceTypeCode))
            {
                NomValue simulatorRole = this.nomRepository.GetNomValue("documentRoles", "simulator");
                var simulators = Utils.GetTrainingsByCode(includedTrainings, simulatorRole.Code, documentRoleCodes);

                documents.Add(new
                {
                    DOC = new
                    {
                        DOC_ROLE = String.Format("{0}. {1}", number++, LicenceDictionary.DocumentTitle["Simulator"]),
                        SUB_DOC = Utils.FillBlankData(simulators, 1)
                    }
                });
            }

            if ((new List<string> { "CPL(A)", "ATPL(A)" }).Contains(licenceTypeCode))
            {
                NomValue engCertRole = this.nomRepository.GetNomValue("documentRoles", "engCert");
                var engCerts = Utils.GetLangCertsByCode(includedLangCerts, engCertRole.Code, documentRoleCodes);

                documents.Add(new
                {
                    DOC = new
                    {
                        DOC_ROLE = String.Format("{0}. {1}", number++, LicenceDictionary.DocumentTitle["EngLangCert"]),
                        SUB_DOC = Utils.FillBlankData(engCerts, 1)
                    }
                });
            }

            if ("ATPL(A)" == licenceTypeCode)
            {
                NomValue bgCertRole = this.nomRepository.GetNomValue("documentRoles", "bgCert");
                var bgCerts = Utils.GetLangCertsByCode(includedLangCerts, bgCertRole.Code, documentRoleCodes);

                documents.Add(new
                {
                    DOC = new
                    {
                        DOC_ROLE = String.Format("{0}. {1}", number++, LicenceDictionary.DocumentTitle["BgLangCert"]),
                        SUB_DOC = Utils.FillBlankData(bgCerts, 1)
                    }
                });
            }

            return documents;
        }

        private List<object> GetTRatings(IEnumerable<PartVersion<PersonRatingDO>> includedRatings, IEnumerable<PartVersion<PersonRatingEditionDO>> ratingEditions)
        {
            List<object> tRatings = new List<object>();
            foreach (var edition in ratingEditions)
            {
                var rating = includedRatings.Where(r => r.Part.Index == edition.Content.RatingPartIndex).Single();

                var firstRatingEdition = this.lotRepository.GetLotIndex(edition.Part.LotId)
                        .Index.GetParts<PersonRatingEditionDO>("ratingEditions")
                        .Where(epv => epv.Content.RatingPartIndex == edition.Content.RatingPartIndex)
                        .OrderByDescending(epv => epv.Content.Index)
                        .Last();

                tRatings.Add(new
                {
                    TYPE = string.Format(
                        "{0} {1}",
                        rating.Content.RatingClass == null ? string.Empty : rating.Content.RatingClass.Code,
                        rating.Content.RatingTypes.Count() > 0 ? string.Join(", ", rating.Content.RatingTypes.Select(rt => rt.Code)) : "").Trim(),
                    AUTH_NOTES = string.Format(
                        "{0} {1}",
                        rating.Content.Authorization == null ? string.Empty : rating.Content.Authorization.Code,
                        edition.Content.Notes).Trim(),
                    ISSUE_DATE = firstRatingEdition.Content.DocumentDateValidFrom,
                    VALID_DATE = edition.Content.DocumentDateValidTo
                });
            }

            return Utils.FillBlankData(tRatings, 11);
        }

        private List<object> GetLRatings(IEnumerable<PartVersion<PersonRatingDO>> includedRatings, IEnumerable<PartVersion<PersonRatingEditionDO>> ratingEditions)
        {
            List<object> lRatings = new List<object>();
            foreach (var edition in ratingEditions)
            {
                var firstRatingEdition = this.lotRepository.GetLotIndex(edition.Part.LotId)
                        .Index.GetParts<PersonRatingEditionDO>("ratingEditions")
                        .Where(epv => epv.Content.RatingPartIndex == edition.Content.RatingPartIndex)
                        .OrderByDescending(epv => epv.Content.Index)
                        .Last();

                var rating = includedRatings.Where(r => r.Part.Index == edition.Content.RatingPartIndex).Single();
                lRatings.Add(new
                {
                    TYPE = string.Format(
                        "{0} {1}",
                        rating.Content.RatingClass == null ? string.Empty : rating.Content.RatingClass.Code,
                        rating.Content.RatingTypes.Count() > 0 ? string.Join(", ", rating.Content.RatingTypes.Select(rt => rt.Code)) : "").Trim(),
                    AUTH = rating.Content.Authorization == null ? string.Empty : rating.Content.Authorization.Code,
                    NOTES = edition.Content.Notes,
                    ISSUE_DATE = firstRatingEdition.Content.DocumentDateValidFrom,
                    VALID_DATE = edition.Content.DocumentDateValidTo
                });
            }

            return Utils.FillBlankData(lRatings, 11);
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

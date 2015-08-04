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
        private int number;

        public Pilot(
            ILotRepository lotRepository,
            INomRepository nomRepository)
        {
            this.lotRepository = lotRepository;
            this.nomRepository = nomRepository;
            this.number = 6;
        }

        public string GeneratorCode
        {
            get
            {
                return "pilot";
            }
        }

        public string GeneratorName
        {
            get
            {
                return "Лиценз за пилот - стар";
            }
        }

        public object GetData(int lotId, string path)
        {
            var lot = this.lotRepository.GetLotIndex(lotId);
            var personData = lot.Index.GetPart<PersonDataDO>("personData").Content;
            var personAddressPart = lot.Index.GetParts<PersonAddressDO>("personAddresses")
               .FirstOrDefault(a => this.nomRepository.GetNomValue("boolean", a.Content.ValidId.Value).Code == "Y");
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
            var includedChecks = lastEdition.IncludedChecks
                .Select(i => lot.Index.GetPart<PersonCheckDO>("personDocumentChecks/" + i).Content);

            NomValue FTgroup = this.nomRepository.GetNomValues("authorizationGroups").First(nv => nv.Code == "FT");
            List<object> instructorData = PilotUtils.GetRatingsDataByCode(includedRatings, ratingEditions, FTgroup, this.nomRepository);

            var licenceType = this.nomRepository.GetNomValue("licenceTypes", licence.LicenceTypeId.Value);
            var licenceCaCode = licenceType.TextContent.Get<string>("codeCA");
            var otherLicences = PilotUtils.GetOtherLicences(publisherCaaCode, licenceCaCode, lot, firstEdition, includedLicences, this.nomRepository);
            var rtoRating = PilotUtils.GetRtoRating(includedRatings, ratingEditions);
            var langCerts = Utils.FillBlankData(Utils.GetLangCerts(includedLangCerts), 1);

            List<int> authorizationGroupIds = nomRepository.GetNomValues("authorizationGroups")
                .Where(nv => nv.Code == "FT" || nv.Code == "FC")
                .Select(nv => nv.NomValueId)
                .ToList();

            var ratings = Utils.FillBlankData(PilotUtils.GetRatings(includedRatings, ratingEditions, authorizationGroupIds, this.nomRepository), 19);
            var country = Utils.GetCountry(personData, this.nomRepository);
            var licenceNumber = string.Format(
                " BGR {0} - {1} - {2}",
                licenceType.Code.Replace("(", "").Replace(")", "").Replace("/", "."),
                Utils.PadLicenceNumber(licence.LicenceNumber),
                personData.Lin);
            var documents = this.GetDocuments(licence, licenceType, includedTrainings, includedExams, includedChecks);
            var nationality = this.nomRepository.GetNomValue("countries", personData.Country.NomValueId);

            string licenceAction = lastEdition.LicenceActionId.HasValue ? this.nomRepository.GetNomValue("licenceActions", lastEdition.LicenceActionId.Value).Name.ToUpper() : null;

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
                    RTO_NOTES = rtoRating != null ? rtoRating.Notes : null,
                    RTO_NOTES_EN = rtoRating != null ? rtoRating.NotesAlt : null,
                    ENG_LEVEL = langCerts,
                    T_RATING = ratings,
                    INSTRUCTOR = Utils.FillBlankData(instructorData, 4),
                    T_LICENCE_HOLDER = Utils.GetLicenceHolder(personData, personAddress, this.nomRepository),
                    T_LICENCE_TYPE_NAME = licenceType.Name.ToLower(),
                    T_LICENCE_NO = licenceNumber,
                    T_FIRST_ISSUE_DATE = firstEdition.DocumentDateValidFrom,
                    T_VALID_DATE = lastEdition.DocumentDateValidTo,
                    T_ACTION = licenceAction,
                    T_ISSUE_DATE = lastEdition.DocumentDateValidFrom,
                    OTHER_LICENCE2 = otherLicences,
                    T_DOCUMENTS = documents.Take(4),
                    T_MED_CERT = Utils.GetMedCerts(this.number++, includedMedicals, personData),
                    T_DOCUMENTS2 = documents.Skip(4),
                    RTO_NOTES2 = rtoRating != null ? rtoRating.Notes : null,
                    RTO_NOTES2_EN = rtoRating != null ? rtoRating.NotesAlt : null,
                    ENG_LEVEL1 = langCerts,
                    L_RATING = ratings,
                    INSTRUCTOR1 = Utils.FillBlankData(instructorData, 4),
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
                PLACE_OF_BIRTH_TRANS = placeOfBirth.Item1,
                ADDRESS = address.Item2,
                ADDRESS_TRANS = address.Item1
            };
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
                string dateValid = string.Empty;
                string dateValidTrans = string.Empty;
                if (edition.DocumentDateValidTo.HasValue)
                {
                    dateValid = edition.DocumentDateValidTo.Value.ToString("dd.MM.yyyy");
                    dateValidTrans = edition.DocumentDateValidTo.Value.ToString("dd MMMM yyyy");
                }

                result = new List<object>()
                {
                    LicenceDictionary.LicencePrivilege["validWithMedCert"],
                    LicenceDictionary.LicencePrivilege["requiresLegalID_Pilot"]
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

        private List<object> GetDocuments(
            PersonLicenceDO licence,
            NomValue licenceType,
            IEnumerable<PersonTrainingDO> includedTrainings,
            IEnumerable<PersonTrainingDO> includedExams,
            IEnumerable<PersonCheckDO> includedChecks)
        {
            if (licenceType.Code == "ATPA" || licenceType.Code == "CPA")
            {
                string[] documentRoleCodes;
                bool hasRoles = LicenceDictionary.LicenceRole.TryGetValue(licenceType.Code, out documentRoleCodes);

                if (!hasRoles)
                {
                    return null;
                }

                NomValue theoreticalTrainingRole = this.nomRepository.GetNomValue("documentRoles", "theoreticalTraining");
                NomValue theoreticalExamRole = this.nomRepository.GetNomValue("documentRoles", "exam");
                NomValue simulatorRole = this.nomRepository.GetNomValue("documentRoles", "simulator");
                NomValue flyingCheckRole = this.nomRepository.GetNomValue("documentRoles", "flyingCheck");
                NomValue flyingTrainingRole = this.nomRepository.GetNomValue("documentRoles", "flyingTraining");

                var theoreticalTrainings = Utils.GetTrainingsByCode(includedTrainings, theoreticalTrainingRole.Code, documentRoleCodes);
                var flyingTrainings = Utils.GetTrainingsByCode(includedTrainings, flyingTrainingRole.Code, documentRoleCodes);

                var exams = includedExams.Where(d => d.Valid.Code == "Y" && documentRoleCodes.Contains(d.DocumentRole.Code) && d.DocumentRole.Code == theoreticalExamRole.Code)
                   .Select(t => new
                   {
                       DOC_TYPE = t.DocumentType.Name.ToLower(),
                       DOC_NO = t.DocumentNumber,
                       DATE = t.DocumentDateValidFrom,
                       DOC_PUBLISHER = t.DocumentPublisher
                   })
                   .ToList<object>();

                exams = Utils.FillBlankData(exams, 1);

                var simulators = Utils.FillBlankData(Utils.GetChecksByCode(includedChecks, simulatorRole.Code, documentRoleCodes), 1);
                var flyingChecks = Utils.FillBlankData(Utils.GetChecksByCode(includedChecks, flyingCheckRole.Code, documentRoleCodes), 1);

                var result = new List<object>()
                {
                    new 
                    {
                        DOC = new
                        {
                            DOC_ROLE = String.Format("{0}. {1}", number++, LicenceDictionary.DocumentTitle["TheoreticalTraining"]),
                            SUB_DOC = Utils.FillBlankData(theoreticalTrainings, 1)
                        }
                    },
                    new 
                    {
                        DOC = new
                        {
                            DOC_ROLE = String.Format("{0}. {1}", number++, LicenceDictionary.DocumentTitle["FlyingTraining"]),
                            SUB_DOC = Utils.FillBlankData(flyingTrainings, 1)
                        }
                    },
                    new 
                    {
                        DOC = new
                        {
                            DOC_ROLE = String.Format("{0}. {1}", number++, LicenceDictionary.DocumentTitle["TheoreticalExam"]),
                            SUB_DOC = exams
                        }
                    },
                    new 
                    {
                        DOC = new
                        {
                            DOC_ROLE = String.Format("{0}. {1}", number++, LicenceDictionary.DocumentTitle["FlyingCheck"]),
                            SUB_DOC = flyingChecks
                        }
                    },
                    
                };
                
                if (licenceType.Code == "ATPA")
                {
                    result.Add(new
                    {
                        DOC = new
                        {
                            DOC_ROLE = String.Format("{0}. {1}", number++, LicenceDictionary.DocumentTitle["Simulator"]),
                            SUB_DOC = simulators
                        }
                    });
                }

                return result;
            }
            else 
            {
                return new List<object>();
            }
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

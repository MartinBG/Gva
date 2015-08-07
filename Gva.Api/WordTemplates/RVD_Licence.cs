using System.Collections.Generic;
using System.Linq;
using Common.Api.Repositories.NomRepository;
using Common.Json;
using Gva.Api.ModelsDO.Persons;
using Regs.Api.Repositories.LotRepositories;
using Regs.Api.Models;
using Gva.Api.ModelsDO;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.FileRepository;
using Common.Api.Models;

namespace Gva.Api.WordTemplates
{
    public class RVD_Licence : IDataGenerator
    {

        private ILotRepository lotRepository;
        private INomRepository nomRepository;
        private IFileRepository fileRepository;
        private ICaseTypeRepository caseTypeRepository;

        public RVD_Licence(
            ILotRepository lotRepository,
            INomRepository nomRepository,
            IFileRepository fileRepository,
            ICaseTypeRepository caseTypeRepository)
        {
            this.lotRepository = lotRepository;
            this.nomRepository = nomRepository;
            this.fileRepository = fileRepository;
            this.caseTypeRepository = caseTypeRepository;
        }

        public string GeneratorCode
        {
            get
            {
                return "rvdLicence";
            }
        }

        public string GeneratorName
        {
            get
            {
                return "Лиценз за РВД";
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
                .OrderBy(e => e.Content.Index)
                .Select(e => e.Content);

            var firstEdition = editions.First();
            var lastEdition = editions.Last();

            var licenceType = this.nomRepository.GetNomValue("licenceTypes", licence.LicenceTypeId.Value);

            var includedTrainings = lastEdition.IncludedTrainings
                .Select(i => lot.Index.GetPart<PersonTrainingDO>("personDocumentTrainings/" + i).Content);
            var includedRatings = lastEdition.IncludedRatings
                .Select(i => i.Ind)
                .Distinct()
                .Select(i => lot.Index.GetPart<PersonRatingDO>("ratings/" + i.Value));
            var ratingEditions = lastEdition.IncludedRatings.Select(i => lot.Index.GetPart<PersonRatingEditionDO>("ratingEditions/" + i.Index));
            var includedExams = lastEdition.IncludedExams
                .Select(i => lot.Index.GetPart<PersonTrainingDO>("personDocumentTrainings/" + i).Content);
            var classes = this.GetClasses(includedRatings, ratingEditions);
            var documents = this.GetDocuments(includedTrainings, includedExams);
            var licenceCodeCa = licenceType.TextContent.Get<string>("codeCA");
            var licenceNumber = string.Format(
                "BGR. {0} - {1} - {2}",
                licenceType.Code,
                Utils.PadLicenceNumber(licence.LicenceNumber),
                personData.Lin);

            string licenceAction = lastEdition.LicenceActionId.HasValue ? this.nomRepository.GetNomValue("licenceActions", lastEdition.LicenceActionId.Value).Name.ToUpper() : null;

            var json = new
            {
                root = new
                {
                    L_NAME = licenceType.Name.ToUpper(),
                    L_NAME_TRANS = licenceType.NameAlt.ToUpper(),
                    L_LICENCE_TYPE_CA_CODE = licenceCodeCa,
                    L_NAME1 = licenceType.Name.ToUpper(),
                    L_NAME1_TRANS = licenceType.NameAlt.ToUpper(),
                    L_LICENCE_TYPE_CA_CODE2 = licenceCodeCa,
                    L_LICENCE_NO = licenceNumber,
                    L_LICENCE_HOLDER = this.GetPersonData(personData, personAddress),
                    L_LICENCE_PRIV = this.GetLicencePrivileges(),
                    L_FIRST_ISSUE_DATE = firstEdition.DocumentDateValidFrom,
                    L_ISSUE_DATE = lastEdition.DocumentDateValidFrom,
                    T_LICENCE_HOLDER = Utils.GetLicenceHolder(personData, personAddress, this.nomRepository),
                    T_LICENCE_NO = licenceNumber,
                    T_LICENCE_CODE = licenceType.Code,
                    T_ACTION = licenceAction,
                    T_FIRST_ISSUE_DATE = firstEdition.DocumentDateValidFrom,
                    T_ISSUE_DATE = lastEdition.DocumentDateValidFrom,
                    T_DOCUMENTS = documents,
                    T_CLASSES = classes.Length > 20 ? classes.Take(classes.Length / 2) : classes,
                    L_CLASSES = classes.Length > 20 ? classes.Take(classes.Length / 2) : classes,
                    T_CLASSES2 = classes.Length > 20 ? classes.Skip(classes.Length / 2) : new List<object>() { new object() },
                    L_CLASSES2 = classes.Length > 20 ? classes.Skip(classes.Length / 2) : new List<object>() { new object() }
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
                
            }
            if (personData.Country != null)
            {
                nationality = this.nomRepository.GetNomValue("countries", personData.Country.NomValueId);
            }

            NomValue settlement = null;
            if (personAddress.SettlementId.HasValue)
            {
                settlement = this.nomRepository.GetNomValue("cities", personAddress.SettlementId.Value);
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
                        TOWN_VILLAGE_NAME = placeOfBirth != null ? placeOfBirth.Name : null,
                    },
                    PLACE_OF_BIRTH_TRANS = new
                    {
                        COUNTRY_NAME = country != null ? country.NameAlt : null,
                        TOWN_VILLAGE_NAME = placeOfBirth != null ? placeOfBirth.NameAlt : null,
                    }
                },
                ADDRESS = string.Format(
                    "{0}, {1}",
                    settlement != null ? settlement.Name : null,
                    personAddress.Address),
                ADDRESS_TRANS = string.Format(
                    "{0}, {1}",
                    personAddress.AddressAlt,
                    settlement != null ? settlement.NameAlt : null),
                NATIONALITY = new
                {
                    COUNTRY_NAME_BG = country.Name,
                    COUNTRY_CODE = nationality.TextContent.Get<string>("nationalityCodeCA"),
                }
            };
        }

        private object[] GetDocuments(
            IEnumerable<PersonTrainingDO> includedTrainings,
            IEnumerable<PersonTrainingDO> includedExams)
        {
            int trueValidId = this.nomRepository.GetNomValue("boolean", "yes").NomValueId;
            var trainings = includedTrainings
                .Where(t => t.ValidId == trueValidId)
                .Select(t =>
                    new
                    {
                        DOC = new
                        {
                            DOC_ROLE = this.nomRepository.GetNomValue("documentRoles", t.DocumentRoleId.Value).Name,
                            SUB_DOC = new
                            {
                                DOC_TYPE = this.nomRepository.GetNomValue("documentTypes", t.DocumentTypeId.Value).Name,
                                DOC_NO = t.DocumentNumber,
                                DATE = t.DocumentDateValidFrom,
                                DOC_PUBLISHER = t.DocumentPublisher
                            }
                        }
                    }).ToArray<dynamic>();

            var examRole = this.nomRepository.GetNomValue("documentRoles", "exam");
            
            var exams = includedExams
                .Where(e => e.ValidId.HasValue != null && e.ValidId == trueValidId)
                .Select(e =>
                    new
                    {
                        DOC = new
                        {
                            DOC_ROLE = examRole.Name,
                            SUB_DOC = new
                            {
                                DOC_TYPE = this.nomRepository.GetNomValue("documentTypes", e.DocumentTypeId.Value).Name,
                                DOC_NO = e.DocumentNumber,
                                DATE = e.DocumentDateValidFrom,
                                DOC_PUBLISHER = e.DocumentPublisher
                            }
                        }
                    }).ToArray<dynamic>();

            return trainings
                .Union(exams)
                .ToArray<object>();
        }

        private List<object> GetLicencePrivileges()
        {
            return new List<object>()
            {
                LicenceDictionary.LicencePrivilege["ATSM1"],
                LicenceDictionary.LicencePrivilege["ATSM2"],
                LicenceDictionary.LicencePrivilege["requiresLegalID_RVD"]
            };
        }

        private object[] GetClasses(IEnumerable<PartVersion<PersonRatingDO>> includedRatings, IEnumerable<PartVersion<PersonRatingEditionDO>> ratingEditions)
        {
            int caseTypeId = this.caseTypeRepository.GetCaseType("to_suvd").GvaCaseTypeId;
            List<object> classes = new List<object>();
            foreach (var edition in ratingEditions)
            {
                var rating = includedRatings.Where(r => r.Part.Index == edition.Content.RatingPartIndex).Single();
                if(this.fileRepository.GetFileReference(rating.PartId, caseTypeId) != null)
                {
                    var firstRatingEdition = this.lotRepository.GetLotIndex(rating.Part.LotId)
                        .Index.GetParts<PersonRatingEditionDO>("ratingEditions")
                        .Where(epv => epv.Content.RatingPartIndex == rating.Part.Index)
                        .OrderByDescending(epv => epv.Content.Index)
                        .Last();

                    classes.Add(new
                    {
                        LEVEL = rating.Content.PersonRatingLevelId.HasValue ? this.nomRepository.GetNomValue("", rating.Content.PersonRatingLevelId.Value).Code : null,
                        RATING = rating.Content.RatingClassId.HasValue ? this.nomRepository.GetNomValue("ratingClasses", rating.Content.RatingClassId.Value).Code : null,
                        SUBRATING = edition.Content.RatingSubClasses.Count > 0 ? string.Join(",", this.nomRepository.GetNomValues("ratingSubClasses", edition.Content.RatingSubClasses.ToArray()).Select(s => s.Code)) : string.Empty,
                        LICENCE = rating.Content.AuthorizationId.HasValue ? this.nomRepository.GetNomValue("authorizations", rating.Content.AuthorizationId.Value).Code : null,
                        LIMITATION = edition.Content.Limitations.Count > 0 ? string.Join(",", this.nomRepository.GetNomValues("limitations66", edition.Content.Limitations.ToArray()).Select(s => s.Name)) : string.Empty,
                        ISSUE_DATE = firstRatingEdition.Content.DocumentDateValidFrom,
                        VALID_DATE = edition.Content.DocumentDateValidTo
                    });
                }
            }
            
            return classes.ToArray();
        }
    }
}

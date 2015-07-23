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
    public class FCLAuthForm : IDataGenerator
    {
        private ILotRepository lotRepository;
        private INomRepository nomRepository;

        public FCLAuthForm(
            ILotRepository lotRepository,
            INomRepository nomRepository)
        {
            this.lotRepository = lotRepository;
            this.nomRepository = nomRepository;
        }

        public string GeneratorCode
        {
            get
            {
                return "fclAuthForm";
            }
        }

        public string GeneratorName
        {
            get
            {
                return "Форма за потвърждаване за fcl лиценз";
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

            var licencePartVersion = lot.Index.GetPart<PersonLicenceDO>(path);
            var licence = licencePartVersion.Content;
            var editions = lot.Index.GetParts<PersonLicenceEditionDO>("licenceEditions")
                .Where(e => e.Content.LicencePartIndex == licencePartVersion.Part.Index)
                .OrderBy(e => e.Content.Index);
            var lastEdition = editions.Last().Content;

            var includedRatings = lastEdition.IncludedRatings
                .Select(i => i.Ind)
                .Distinct()
                .Select(i => lot.Index.GetPart<PersonRatingDO>("ratings/" + i.Value));
            var ratingEditions = lastEdition.IncludedRatings.Select(i => lot.Index.GetPart<PersonRatingEditionDO>("ratingEditions/" + i.Index));
            var ratings = this.GetRatings(includedRatings, ratingEditions);
            
            var includedLangCerts = lastEdition.IncludedLangCerts
                .Select(i => lot.Index.GetPart<PersonLangCertDO>("personDocumentLangCertificates/" + i).Content);

            var langCerts = includedLangCerts.Select(l => new 
            {
                NAME = string.Format("{0} {1}", l.LangLevel.Name, l.DocumentDateValidTo.HasValue ? l.DocumentDateValidTo.Value.ToString("dd.MM.yyyy") : null)
            });

            var includedMedicals = lastEdition.IncludedMedicals
                .Select(i => lot.Index.GetPart<PersonMedicalDO>("personDocumentMedicals/" + i).Content);

            var bgMedical = includedMedicals
                .Where(m => m.DocumentNumberPrefix.Contains("MED BG") || m.DocumentNumberPrefix.Contains("BGR"))
                .OrderByDescending(d => d.DocumentDateValidTo)
                .FirstOrDefault();

            var medicalData = bgMedical != null ? new 
            {
                LIMITATIONS = bgMedical.Limitations.Count() > 0 ? string.Join(", ", bgMedical.Limitations.Select(l => l.Code)) : null,
                VALID_UNTIL = bgMedical.DocumentDateValidTo.HasValue ? bgMedical.DocumentDateValidTo.Value.ToString("dd.MM.yyyy") : null
            } : null;

            var licenceType = this.nomRepository.GetNomValue("licenceTypes", licence.LicenceTypeId.Value);

            var licenceNumber = string.Format(
                "BGR. {0} - {1} - {2}",
                licenceType.Code.Replace("/", "."),
                Utils.PadLicenceNumber(licence.LicenceNumber),
                personData.Lin);

            var country = Utils.GetCountry(personAddress, this.nomRepository); 
            var countryCode = country != null ? (country.TextContent != null ? country.TextContent.Get<string>("nationalityCodeCA") : null) : null;
            string address = Utils.GetAuthFormAddress(personAddress, country, this.nomRepository);
            string medicalClass = bgMedical != null ? bgMedical.MedClass.Name : "";

            var json = new
            {
                root = new
                {
                    STATE = country != null ? country.NameAlt : null,
                    LICENCE_CODE = licenceType.TextContent.Get<string>("codeCA"),
                    LICENCE_NUMBER = licenceNumber,
                    NAMES_ALT = string.Format("{0} {1} {2}", personData.FirstNameAlt.ToUpper(), personData.MiddleNameAlt.ToUpper(), personData.LastNameAlt.ToUpper()),
                    DATE_OF_BIRTH = personData.DateOfBirth.HasValue ? personData.DateOfBirth.Value.ToString("dd.MM.yyyy") : "",
                    DATE_OF_ISSUE = lastEdition.DocumentDateValidFrom,
                    ADDRESS_ALT = address,
                    NATIONALITY = countryCode,
                    RATINGS = Utils.FillBlankData(ratings, 1),
                    LANGUAGES_AND_LIMITATIONS = langCerts,
                    MEDICAL_CLASS = medicalClass,
                    MEDICAL_LIMITATIONS = medicalData,
                    CURRENT_DATE = DateTime.Now
                }
            };

            return json;
        }

        private List<object> GetRatings(
            IEnumerable<PartVersion<PersonRatingDO>> includedRatings,
            IEnumerable<PartVersion<PersonRatingEditionDO>> ratingEditions)
        {
            List<object> ratings = new List<object>();
            foreach (var edition in ratingEditions)
            {
                var rating = includedRatings.Where(r => r.Part.Index == edition.Content.RatingPartIndex).Single();
                 NomValue authorization = rating.Content.Authorization != null ? nomRepository.GetNomValue(rating.Content.Authorization.NomValueId) : null;
                 if (authorization == null ||
                     (authorization.Code != "RTO" &&
                     (authorization.ParentValueId.HasValue ? (authorization.Code != "FC" && authorization.Code != "FT") : true)))
                 {
                     ratings.Add(new
                     {
                         CLASS_TYPE = rating.Content.AircraftTypeGroup != null ? rating.Content.AircraftTypeGroup.Code :
                             string.Format(
                                 "{0} {1}",
                                 rating.Content.RatingClass == null ? string.Empty : rating.Content.RatingClass.Code,
                                 rating.Content.RatingTypes.Count() > 0 ? string.Join(", ", rating.Content.RatingTypes.Select(rt => rt.Code)) : "").Trim(),
                         DATE = edition.Content.DocumentDateValidTo
                     });
                 }
            }

            return ratings;
        }
    }
}

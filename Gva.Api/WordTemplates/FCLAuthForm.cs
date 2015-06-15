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
               .FirstOrDefault(a => a.Content.Valid.Code == "Y");
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
            var medicalsData = includedMedicals.Count() > 0 ?
                includedMedicals
                .Select(m => new 
                {
                    LIMITATIONS = m.Limitations.Count() > 0 ? string.Join(", ", m.Limitations.Select(l => l.Code)) : null,
                    VALID_UNTIL = m.DocumentDateValidTo.HasValue ? m.DocumentDateValidTo.Value.ToString("dd.MM.yyyy") : null
                }) : null;

            var licenceType = this.nomRepository.GetNomValue("licenceTypes", licence.LicenceType.NomValueId);

            var licenceNumber = string.Format(
                "BGR. {0} - {1} - {2}",
                licenceType.Code.Replace("/", "."),
                Utils.PadLicenceNumber(licence.LicenceNumber),
                personData.Lin);

            var country = Utils.GetCountry(personAddress, this.nomRepository);
            var countryCode = country != null ? (country.TextContent != null ? country.TextContent.Get<string>("nationalityCodeCA") : null) : null;
            string medicalClass = includedMedicals.Count() > 0 ? includedMedicals.First().MedClass.Name : "";

            var json = new
            {
                root = new
                {
                    STATE = country != null ? country.NameAlt : null,
                    LICENCE_CODE = licenceType.Code,
                    LICENCE_NUMBER = licenceNumber,
                    NAMES_ALT = string.Format("{0} {1} {2}", personData.FirstName.ToUpper(), personData.MiddleName.ToUpper(), personData.LastName.ToUpper()),
                    DATE_OF_BIRTH = personData.DateOfBirth.HasValue ? personData.DateOfBirth.Value.ToString("dd.MM.yyyy") : "",
                    DATE_OF_ISSUE = lastEdition.DocumentDateValidFrom,
                    ADDRESS_ALT = personAddress != null ? personAddress.AddressAlt : null,
                    NATIONALITY = countryCode,
                    RATINGS = Utils.FillBlankData(ratings, 1),
                    LANGUAGES_AND_LIMITATIONS = langCerts,
                    MEDICAL_CLASS = medicalClass,
                    MEDICAL_LIMITATIONS = medicalsData,
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

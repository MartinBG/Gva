using System.Collections.Generic;
using System.Linq;
using Common.Api.Repositories.NomRepository;
using Common.Json;
using Gva.Api.ModelsDO.Persons;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.WordTemplates
{
    public class AMLNationalRating : IAMLNationalRatingDataGenerator
    {
        private ILotRepository lotRepository;
        private INomRepository nomRepository;

        public AMLNationalRating(
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
                return "AMLNationalRating";
            }
        }

        public string GeneratorName
        {
            get
            {
                return "AML Национален Клас";
            }
        }

        public object GetData(int lotId, string path, int ratingPartIndex, int editionPartIndex)
        {
            var lot = this.lotRepository.GetLotIndex(lotId);
            var personData = lot.Index.GetPart<PersonDataDO>("personData").Content;

            var includedRating = lot.Index.GetPart<PersonRatingDO>(string.Format("ratings/{0}", ratingPartIndex));
            var ratingEdition = lot.Index.GetPart<PersonRatingEditionDO>(string.Format("ratingEditions/{0}", editionPartIndex));

            var licence = lot.Index.GetPart<PersonLicenceDO>(path).Content;
            var licenceNumber = string.Format(
                " BGR. AM - {0} - {1}",
                Utils.PadLicenceNumber(licence.LicenceNumber),
                personData.Lin);

            List<string> validAliases = new List<string> { "A", "B 1", "B 2", "C" };
            List<string> validCodes = new List<string> { "1", "2", "3", "4", "5", "6", "7" };
            var acLimitations = this.GetACLimitations(includedRating, ratingEdition, validAliases, validCodes);
            var engineType = includedRating.Content.AircraftTypeGroupId.HasValue ? this.nomRepository.GetNomValue("aircraftTypeGroups", includedRating.Content.AircraftTypeGroupId.Value).Code : null;
            var category = includedRating.Content.AircraftTypeCategoryId.HasValue ? this.nomRepository.GetNomValue("aircraftClases66", includedRating.Content.AircraftTypeCategoryId.Value).Code : null;

            var json = new
            {
                root = new
                {
                    ENGINE_TYPE = engineType,
                    CATEGORY = category,
                    VALID_DATE = ratingEdition.Content.DocumentDateValidTo,
                    ENDORSEMENT_DATE = ratingEdition.Content.DocumentDateValidFrom,
                    REMARKS = ratingEdition.Content.Notes,
                    AC_LIMITATIONS = acLimitations.Count() > 0 ? acLimitations : new object(),
                    LICENCE_NO = licenceNumber,
                    LIN = personData.Lin,
                    EGN = personData.Uin,
                    LICENCE_NO2 = licenceNumber,
                    ENGINE_TYPE2 = engineType,
                    CATEGORY2 = category,
                    VALID_DATE2 = ratingEdition.Content.DocumentDateValidTo,
                    ENDORSEMENT_DATE2 = ratingEdition.Content.DocumentDateValidFrom
                }
            };

            return json;
        }

        private object[] GetACLimitations(
           PartVersion<PersonRatingDO> rating,
           PartVersion<PersonRatingEditionDO> edition,
           List<string> validAliases,
           List<string> validCodes)
        {
            List<object> acLimitations = new List<object>();

            if (rating.Content.AircraftTypeGroupId.HasValue && rating.Content.AircraftTypeCategoryId.HasValue &&
                (edition.Content.Limitations != null && edition.Content.Limitations.Count > 0))
            {
                var aircraftTypeCategory = this.nomRepository.GetNomValue("aircraftClases66", rating.Content.AircraftTypeCategoryId.Value);
                if (validCodes.Contains(nomRepository.GetNomValue("aircraftGroup66", aircraftTypeCategory.ParentValueId.Value).Code) &&
                validAliases.Contains(nomRepository.GetNomValue("aircraftClases66", aircraftTypeCategory.NomValueId).TextContent.Get<string>("alias")))
                {
                    acLimitations.Add(new
                    {
                        AIRCRAFT = this.nomRepository.GetNomValue("aircraftTypeGroups", rating.Content.AircraftTypeGroupId.Value).Name,
                        CAT = aircraftTypeCategory.Code.Contains("C") ? "C" : aircraftTypeCategory.Code,
                        LIM = string.Join(",", this.nomRepository.GetNomValues("limitations66", edition.Content.Limitations.ToArray()).Select(l => l.Name))
                    });
                }
            }
            
            return acLimitations.ToArray();
        }
    }
}

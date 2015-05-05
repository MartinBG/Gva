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
            var engineType = includedRating.Content.AircraftTypeGroup != null ? includedRating.Content.AircraftTypeGroup.Code : null;
            var category = includedRating.Content.AircraftTypeCategory != null ? includedRating.Content.AircraftTypeCategory.Code : null;

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

            if (rating.Content.AircraftTypeGroup != null && rating.Content.AircraftTypeCategory != null &&
                validCodes.Contains(nomRepository.GetNomValue("aircraftGroup66", rating.Content.AircraftTypeCategory.ParentValueId.Value).Code) &&
                validAliases.Contains(nomRepository.GetNomValue("aircraftClases66", rating.Content.AircraftTypeCategory.NomValueId).TextContent.Get<string>("alias")) &&
                (edition.Content.Limitations != null && edition.Content.Limitations.Count > 0))
            {
                string category = null;
                if (rating.Content.AircraftTypeCategory != null)
                {
                    category = rating.Content.AircraftTypeCategory.Code.Contains("C") ? "C" : rating.Content.AircraftTypeCategory.Code;
                }

                acLimitations.Add(new
                {
                    AIRCRAFT = this.nomRepository.GetNomValue("aircraftTypeGroups", rating.Content.AircraftTypeGroup.NomValueId).Name,
                    CAT = category,
                    LIM = string.Join(",", edition.Content.Limitations.Select(l => l.Name))
                });
            }
            
            return acLimitations.ToArray();
        }
    }
}

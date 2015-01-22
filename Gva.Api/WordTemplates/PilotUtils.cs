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
    public class PilotUtils
    {
        internal static List<object> GetRatings(
            IEnumerable<PartVersion<PersonRatingDO>> includedRatings,
            IEnumerable<PartVersion<PersonRatingEditionDO>> ratingEditions,
            List<int> authorizationGroupIds)
        {
            List<object> ratings = new List<object>();
            foreach (var edition in ratingEditions.OrderBy(r => r.Content.DocumentDateValidFrom))
            {
                var rating = includedRatings.Where(r => r.Part.Index == edition.Content.RatingPartIndex).Single();
                if (rating.Content.Authorization == null ||
                    (rating.Content.Authorization.Code != "RTO" && !authorizationGroupIds.Contains(rating.Content.Authorization.ParentValueId.Value)))
                {
                    ratings.Add(new
                    {
                        TYPE = string.Format(
                            "{0} {1}",
                            rating.Content.RatingClass == null ? string.Empty : rating.Content.RatingClass.Code,
                            rating.Content.RatingTypes.Count() > 0 ? string.Join(", ", rating.Content.RatingTypes.Select(rt => rt.Code)) : "").Trim(),
                        AUTH_NOTES = string.Format(
                            "{0} {1}",
                            rating.Content.Authorization == null ? string.Empty : rating.Content.Authorization.Code,
                            edition.Content.NotesAlt).Trim(),
                        VALID_DATE = edition.Content.DocumentDateValidTo
                    });
                }
            }

            return ratings;
        }

        internal static List<object> GetRatingsDataByCode(
            IEnumerable<PartVersion<PersonRatingDO>> includedRatings,
            IEnumerable<PartVersion<PersonRatingEditionDO>> ratingEditions,
            NomValue authorizationGroup)
        {
            List<object> ratings = new List<object>();
            foreach (var edition in ratingEditions.OrderBy(r => r.Content.DocumentDateValidFrom))
            {
                var rating = includedRatings.Where(r => r.Part.Index == edition.Content.RatingPartIndex).Single();
                if (rating.Content.Authorization != null && authorizationGroup.NomValueId == rating.Content.Authorization.ParentValueId.Value && rating.Content.Authorization.Code != "RTO")
                {
                    ratings.Add(new
                    {
                        TYPE = string.Format(
                            "{0} {1} {2}",
                            rating.Content.RatingClass == null ? string.Empty : rating.Content.RatingClass.Name,
                            rating.Content.RatingTypes.Count() > 0 ? string.Join(", ", rating.Content.RatingTypes.Select(rt => rt.Code)) : "",
                            rating.Content.Authorization == null ? string.Empty : rating.Content.Authorization.Code).Trim(),
                        AUTH_NOTES = string.Format("{0}", edition.Content.NotesAlt).Trim(),
                        VALID_DATE = edition.Content.DocumentDateValidTo
                    });
                }
            }

            return ratings;
        }

        internal static List<object> GetOtherLicences(
            string licenceCaCode,
            string publisherCaaCode,
            Lot lot,
            PersonLicenceEditionDO edition,
            IEnumerable<PartVersion<PersonLicenceDO>> includedLicences,
            INomRepository nomRepository)
        {
            var otherLicences = new List<object>()
            {
                new
                {
                    LIC_NO = publisherCaaCode,
                    ISSUE_DATE = edition.DocumentDateValidFrom,
                    C_CODE = licenceCaCode
                }
            };

            otherLicences = otherLicences.Concat(includedLicences.Select(l =>
            {
                var lastEdition = lot.Index.GetParts<PersonLicenceEditionDO>("licenceEditions")
                    .Where(e => e.Content.LicencePartIndex == l.Part.Index)
                    .OrderBy(e => e.Content.Index)
                    .Last()
                    .Content;

                return new
                {
                    LIC_NO = nomRepository.GetNomValue("licenceTypes", l.Content.LicenceType.NomValueId).TextContent.Get<string>("codeCA"),
                    ISSUE_DATE = lastEdition.DocumentDateValidFrom,
                    C_CODE = publisherCaaCode
                };
            }))
                .ToList();

            return otherLicences;
        }

        internal static PersonRatingEditionDO GetRtoRating(
            IEnumerable<PartVersion<PersonRatingDO>> includedRatings,
            IEnumerable<PartVersion<PersonRatingEditionDO>> ratingEditions)
        {
            var rtoRatingPart = includedRatings.FirstOrDefault(r => r.Content.Authorization != null && r.Content.Authorization.Code == "RTO");

            List<PersonRatingEditionDO> rtoRatings = new List<PersonRatingEditionDO>();
            foreach (var edition in ratingEditions)
            {
                var rating = includedRatings.Where(r => r.Part.Index == edition.Content.RatingPartIndex).Single();
                if (rating.Content.Authorization != null && rating.Content.Authorization.Code == "RTO")
                {
                    rtoRatings.Add(edition.Content);
                }
            }

            return rtoRatings.OrderBy(e => e.Index).LastOrDefault();
        }

    }
}

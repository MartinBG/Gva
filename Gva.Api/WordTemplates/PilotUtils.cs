﻿using System;
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
            List<int> authorizationGroupIds,
            INomRepository nomRepository)
        {
            List<object> ratings = new List<object>();
            foreach (var edition in ratingEditions)
            {
                var rating = includedRatings.Where(r => r.Part.Index == edition.Content.RatingPartIndex).Single();

                NomValue authorization = rating.Content.AuthorizationId.HasValue ? nomRepository.GetNomValue("authorizations", rating.Content.AuthorizationId.Value) : null;
                if (authorization == null ||
                    (authorization.Code != "RTO" &&
                    (authorization.ParentValueId.HasValue ? !authorizationGroupIds.Contains(authorization.ParentValueId.Value) : true)))
                {
                    ratings.Add(new
                    {
                        TYPE = string.Format(
                            "{0} {1}",
                            rating.Content.RatingClassId.HasValue ? nomRepository.GetNomValue("ratingClasses", rating.Content.RatingClassId.Value).Code : string.Empty,
                            rating.Content.RatingTypes.Count() > 0 ? string.Join(", ", nomRepository.GetNomValues("ratingTypes", rating.Content.RatingTypes.ToArray()).Select(rt => rt.Code)) : "").Trim(),
                        AUTH_NOTES = string.Format(
                            "{0} {1}",
                            authorization == null ? string.Empty : authorization.Code,
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
            NomValue authorizationGroup,
            INomRepository nomRepository)
        {
            List<object> ratings = new List<object>();
            foreach (var edition in ratingEditions)
            {
                var rating = includedRatings.Where(r => r.Part.Index == edition.Content.RatingPartIndex).Single();
                if (rating.Content.AuthorizationId.HasValue)
                {
                    NomValue authorization = nomRepository.GetNomValue("authorizations", rating.Content.AuthorizationId.Value);
                    if (authorization.ParentValueId != null &&
                        authorizationGroup.NomValueId == authorization.ParentValueId.Value &&
                        authorization.Code != "RTO")
                    {
                        ratings.Add(new
                        {
                            TYPE = string.Format(
                                "{0} {1} {2}",
                                rating.Content.RatingClassId.HasValue ? nomRepository.GetNomValue("ratingClasses", rating.Content.RatingClassId.Value).Name : string.Empty,
                                rating.Content.RatingTypes.Count() > 0 ? string.Join(", ", nomRepository.GetNomValues("ratingTypes", rating.Content.RatingTypes.ToArray()).Select(rt => rt.Code)) : "",
                                authorization == null ? string.Empty : authorization.Code).Trim(),
                            AUTH_NOTES = string.Format("{0}", edition.Content.NotesAlt).Trim(),
                            VALID_DATE = edition.Content.DocumentDateValidTo
                        });
                    }
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
                var firstEdition = lot.Index.GetParts<PersonLicenceEditionDO>("licenceEditions")
                    .Where(e => e.Content.LicencePartIndex == l.Part.Index)
                    .OrderBy(e => e.Content.Index)
                    .First()
                    .Content;

                return new
                {
                    LIC_NO = nomRepository.GetNomValue("licenceTypes", l.Content.LicenceTypeId.Value).TextContent.Get<string>("codeCA"),
                    ISSUE_DATE = firstEdition.DocumentDateValidFrom,
                    C_CODE = publisherCaaCode
                };
            }))
            .ToList();

            return otherLicences;
        }

        internal static PersonRatingEditionDO GetRtoRating(
            IEnumerable<PartVersion<PersonRatingDO>> includedRatings,
            IEnumerable<PartVersion<PersonRatingEditionDO>> ratingEditions,
            INomRepository nomRepository)
        {
            var rtoRatingPart = includedRatings.FirstOrDefault(r => r.Content.AuthorizationId.HasValue && nomRepository.GetNomValue("authorizations", r.Content.AuthorizationId.Value).Code == "RTO");

            List<PersonRatingEditionDO> rtoRatings = new List<PersonRatingEditionDO>();
            foreach (var edition in ratingEditions)
            {
                var rating = includedRatings.Where(r => r.Part.Index == edition.Content.RatingPartIndex).Single();
                if (rating.Content.AuthorizationId.HasValue && nomRepository.GetNomValue("authorizations", rating.Content.AuthorizationId.Value).Code == "RTO")
                {
                    rtoRatings.Add(edition.Content);
                }
            }

            return rtoRatings.OrderBy(e => e.Index).LastOrDefault();
        }

    }
}

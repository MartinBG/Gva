using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Json;
using Gva.Api.ModelsDO.Persons;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.WordTemplates
{
    public class InstructorExaminerUtils
    {
        internal static List<object> GetPrivileges(List<string> authCodes, INomRepository nomRepository)
        {
            List<object> result = new List<object>();
            foreach (string code in authCodes)
            {
                var authorizationGroup = nomRepository.GetNomValues("instructorExaminerCertificateAttachmentAuthorizations").Where(i =>
                    Regex.Matches(code, "(^" + i.Code + Regex.Escape("(") + ")").Count > 0 ||
                    Regex.Matches(code, "(^" + i.Code + "\\s\\w+)").Count > 0 ||
                    i.Code == code)
                    .FirstOrDefault();

                if (authorizationGroup != null)
                {
                    result = result.Union(nomRepository.GetNomValues("instructorExaminerCertificateAttachmentPrivileges")
                        .Where(p => p.ParentValueId == authorizationGroup.NomValueId)
                        .Select(p => new
                        {
                            REFERENCE = p.Code,
                            PRIVILEGES = p.Name,
                            PRIVILEGES_ALT = p.NameAlt
                        })).ToList();
                }
            }

            return result;
        }

        internal static Tuple<List<object>, List<string>> GetRatingsPerInstructorExaminerByAuthCode(
                   IEnumerable<PartVersion<PersonRatingDO>> includedRatings,
                   IEnumerable<PartVersion<PersonRatingEditionDO>> ratingEditions,
                   string authCode,
                   INomRepository nomRepository)
        {
            NomValue authorizationGroup = nomRepository.GetNomValues("authorizationGroups").First(nv => nv.Code == authCode);
            List<object> ratings = new List<object>();
            List<string> authCodes = new List<string>();
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
                            EXAMINER = authorization.Code,
                            CLASS_TYPE = string.Format(
                                "{0} {1}",
                                rating.Content.RatingClassId.HasValue ? nomRepository.GetNomValue("ratingClasses", rating.Content.RatingClassId.Value).Code : string.Empty,
                                rating.Content.RatingTypes.Count() > 0 ? string.Join(", ", nomRepository.GetNomValues("ratingTypes", rating.Content.RatingTypes.ToArray()).Select(rt => rt.Code)) : ""),
                            DATE = edition.Content.DocumentDateValidTo
                        });

                        authCodes.Add(authorization.Code);
                    }
                }
            }

            return new Tuple<List<object>, List<string>>(ratings, authCodes);
        }
    }
}

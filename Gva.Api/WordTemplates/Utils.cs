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
    public class Utils
    {
        internal static string GetPhonesString(PersonDataDO personData)
        {
            string[] phones = {
                personData.Phone5,
                personData.Phone1,
                personData.Phone2,
                personData.Phone3,
                personData.Phone4
            };
            phones = phones.Where(p => !String.IsNullOrEmpty(p)).ToArray();
            return String.Join(", ", phones);
        }

        internal static object GetLicenceHolder(PersonDataDO personData, PersonAddressDO personAddress)
        {
            return new
            {
                NAME = string.Format(
                    "{0} {1} {2}",
                    personData.FirstName,
                    personData.MiddleName,
                    personData.LastName).ToUpper(),
                LIN = personData.Lin,
                EGN = personData.Uin,
                ADDRESS = string.Format(
                    "{0}, {1}",
                    personAddress.Settlement !=null ? personAddress.Settlement.Name : null,
                    personAddress.Address),
                TELEPHONE = GetPhonesString(personData)
            };
        }

        internal static string PadLicenceNumber(int? licenceNumber = 0)
        {
            return licenceNumber.ToString().PadLeft(5, '0');
        }

        internal static List<object> FillBlankData(List<object> data, int size)
        {
            while (data.Count < size)
            {
                data.Add(new object());
            }
            return data;
        }

        internal static List<object> GetLangCertsByCode(IEnumerable<PersonLangCertDO> includedLangCerts, string roleCode, string[] documentRoleCodes)
        {
            return includedLangCerts
                .Where(t => t.Valid.Code == "Y" && documentRoleCodes.Contains(t.DocumentRole.Code) && t.DocumentRole.Code == roleCode)
                .Select(t =>
                    new
                    {
                        DOC_TYPE = t.DocumentType.Name.ToLower(),
                        DOC_NO = t.DocumentNumber,
                        DATE = t.DocumentDateValidFrom,
                        DOC_PUBLISHER = t.DocumentPublisher
                    }).ToList<object>();
        }

        internal static List<object> GetTrainingsByCode(IEnumerable<PersonTrainingDO> includedTraings, string roleCode, string[] documentRoleCodes)
        {
            return includedTraings
                .Where(t => t.Valid.Code == "Y" && documentRoleCodes.Contains(t.DocumentRole.Code) && t.DocumentRole.Code == roleCode)
                .Select(t =>
                    new
                    {
                        DOC_TYPE = t.DocumentType.Name.ToLower(),
                        DOC_NO = t.DocumentNumber,
                        DATE = t.DocumentDateValidFrom,
                        DOC_PUBLISHER = t.DocumentPublisher
                    }).ToList<object>();
        }

        internal static List<object> GetChecksByCode(IEnumerable<PersonCheckDO> includedChecks, string roleCode, string[] documentRoleCodes)
        {
            return includedChecks
                .Where(t => t.Valid.Code == "Y" && documentRoleCodes.Contains(t.DocumentRole.Code) && t.DocumentRole.Code == roleCode)
                .Select(t =>
                    new
                    {
                        DOC_TYPE = t.DocumentType.Name.ToLower(),
                        DOC_NO = t.DocumentNumber,
                        DATE = t.DocumentDateValidFrom,
                        DOC_PUBLISHER = t.DocumentPublisher
                    }).ToList<object>();
        }

        internal static List<object> GetExamsByCode(
            IEnumerable<PersonTrainingDO> includedExams,
            IEnumerable<PersonCheckDO> includedChecks,
            IEnumerable<PersonTrainingDO> includedTrainings,
            string examRoleCode,
            string[] documentRoleCodes)
        {
            var exams = includedExams.Where(d => d.DocumentRole.Code == examRoleCode)
               .Select(t => new
               {
                   DOC_TYPE = t.DocumentType.Name.ToLower(),
                   DOC_NO = t.DocumentNumber,
                   DATE = t.DocumentDateValidFrom,
                   DOC_PUBLISHER = t.DocumentPublisher
               })
               .ToList<object>();

            var trainings = Utils.GetTrainingsByCode(includedTrainings, examRoleCode, documentRoleCodes);
            var checks = Utils.GetChecksByCode(includedChecks, examRoleCode, documentRoleCodes);

            return exams.Union(checks).Union(trainings).ToList<object>();
        }

        internal static List<object> GetMedCerts(int orderNumber, IEnumerable<PersonMedicalDO> includedMedicals, PersonDataDO personData)
        {
            var medicals = includedMedicals.Select(m =>
                new
                {
                    ORDER_NO = orderNumber,
                    NO = string.Format(
                        "{0}-{1}-{2}-{3}",
                        m.DocumentNumberPrefix,
                        m.DocumentNumber,
                        personData.Lin,
                        m.DocumentNumberSuffix),
                    ISSUE_DATE = m.DocumentDateValidFrom,
                    VALID_DATE = m.DocumentDateValidTo,
                    CLASS = m.MedClass.Name.ToUpper(),
                    PUBLISHER = m.DocumentPublisher.Name,
                    LIMITATION = m.Limitations.Count > 0 ? string.Join(",", m.Limitations.Select(l => l.Name)) : string.Empty
                }).ToList<object>();

            if (medicals.Count() == 0)
            {
                medicals.Add(new
                {
                    ORDER_NO = orderNumber
                });
            }

            return medicals;
        }

        internal static List<object> GetLangCerts(IEnumerable<PersonLangCertDO> includedLangCerts)
        {
            return includedLangCerts.Select(c => new
            {
                LEVEL = c.LangLevel.Name,
                ISSUE_DATE = c.DocumentDateValidFrom,
                VALID_DATE = c.LangLevel.Code.Contains("6") ? "unlimited" : (c.DocumentDateValidTo.HasValue? c.DocumentDateValidTo.Value.ToString("dd.MM.yyyy") : null)

            })
            .ToList<object>();
        }

        internal static List<object> GetRatings(
            IEnumerable<PartVersion<PersonRatingDO>> includedRatings,
            IEnumerable<PartVersion<PersonRatingEditionDO>> editions,
            ILotRepository lotRepository)
        {
            List<object> ratingEditions = new List<object>();
            foreach (var edition in editions)
            {
                var rating = includedRatings.Where(r => r.Part.Index == edition.Content.RatingPartIndex).Single();
                var ratingTypesCodes = rating.Content.RatingTypes.Count() > 0 ? string.Join(", ", rating.Content.RatingTypes.Select(rt => rt.Code)) : "";
                var ratingClassName = rating.Content.RatingClass == null ? null : rating.Content.RatingClass.Code;
                var authorizationCode = rating.Content.Authorization == null ? null : rating.Content.Authorization.Code;
                var firstRatingEdition = lotRepository.GetLotIndex(rating.Part.LotId)
                        .Index.GetParts<PersonRatingEditionDO>("ratingEditions")
                        .Where(epv => epv.Content.RatingPartIndex == rating.Part.Index)
                        .OrderByDescending(epv => epv.Content.Index)
                        .Last();

                ratingEditions.Add(new
                {
                    CLASS_AUTH = string.IsNullOrEmpty(ratingClassName) && string.IsNullOrEmpty(ratingTypesCodes) ?
                        authorizationCode :
                        string.Format(
                            "{0} {1} {2}",
                            ratingTypesCodes,
                            ratingClassName,
                            string.IsNullOrEmpty(authorizationCode) ? string.Empty : " / " + authorizationCode).Trim(),
                    ISSUE_DATE = firstRatingEdition.Content.DocumentDateValidFrom,
                    VALID_DATE = edition.Content.DocumentDateValidTo
                });
            }

            return Utils.FillBlankData(ratingEditions, 11);
        }

        internal static NomValue GetCountry(PersonAddressDO personAddress, INomRepository nomRepository)
        {
            int? countryId = personAddress.Settlement != null ? personAddress.Settlement.ParentValueId : null;
            NomValue country = countryId.HasValue ?
                nomRepository.GetNomValue("countries", countryId.Value) :
                new NomValue
                {
                    Name = null,
                    TextContentString = string.Empty
                };

            return country;
        }

        internal static List<object> GetEndorsements(
            IEnumerable<PartVersion<PersonRatingDO>> includedRatings,
            IEnumerable<PartVersion<PersonRatingEditionDO>> ratingEditions,
            ILotRepository lotRepository)
        {
            List<object> endorsments = new List<object>();
            foreach (var edition in ratingEditions)
            {
                var rating = includedRatings.Where(r => r.Part.Index == edition.Content.RatingPartIndex).Single();
                if (rating.Content.Authorization != null)
                {
                    var firstRatingEdition = lotRepository.GetLotIndex(rating.Part.LotId)
                        .Index.GetParts<PersonRatingEditionDO>("ratingEditions")
                        .Where(epv => epv.Content.RatingPartIndex == rating.Part.Index)
                        .OrderByDescending(epv => epv.Content.Index)
                        .Last();

                    endorsments.Add(new
                    {
                        NAME = rating.Content.Authorization.Code,
                        DATE = firstRatingEdition.Content.DocumentDateValidFrom.Value
                    });
                }
            };

            return (from endorsment in endorsments
                    group endorsment by ((dynamic)endorsment).NAME into newGroup
                    let d = newGroup.OrderBy(g => ((dynamic)g).DATE).FirstOrDefault()
                    select d)
                 .ToList<object>();
        }
    }
}

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

        internal static Tuple<string, string> GetAddress(PersonAddressDO personAddress, INomRepository nomRepository)
        {
            List<string> addressData = new List<string>();
            List<string> addressDataAlt = new List<string>();

            if (personAddress.SettlementId.HasValue)
            {
                NomValue settlement = nomRepository.GetNomValue("cities", personAddress.SettlementId.Value);
                NomValue country = nomRepository.GetNomValue("countries", settlement.ParentValueId.Value);
                addressDataAlt.Add(country.NameAlt);
                addressDataAlt.Add(settlement.NameAlt);

                addressData.Add(country.Name);
                addressData.Add(settlement.Name);
            }

            if (personAddress != null)
            {
                if (!string.IsNullOrEmpty(personAddress.AddressAlt))
                {
                    addressDataAlt.Add(personAddress.AddressAlt);
                }
                if (!string.IsNullOrEmpty(personAddress.Address))
                {
                    addressData.Add(personAddress.Address);
                }
            }

            return new Tuple<string, string>(string.Join(", ", addressDataAlt), string.Join(", ", addressData));
        }

        internal static Tuple<string, string> GetPlaceOfBirth(PersonDataDO personData, INomRepository nomRepository)
        {
            string placeOfBirth = null;
            string placeOfBirthAlt = null;
            if(personData.PlaceOfBirthId.HasValue)
            {
                NomValue city = nomRepository.GetNomValue("cities", personData.PlaceOfBirthId.Value);
                NomValue country = nomRepository.GetNomValue("countries", city.ParentValueId.Value);
                placeOfBirth = string.Format("{0}, {1}", country.Name, city.Name);
                placeOfBirthAlt = string.Format("{0}, {1}", country.NameAlt, city.NameAlt);
            }

            return new Tuple<string, string>(placeOfBirthAlt, placeOfBirth);
        }

        internal static object GetLicenceHolder(PersonDataDO personData, PersonAddressDO personAddress, INomRepository nomRepository)
        {
            var address = GetAddress(personAddress, nomRepository).Item2;
            return new
            {
                NAME = string.Format(
                    "{0} {1} {2}",
                    personData.FirstName,
                    personData.MiddleName,
                    personData.LastName).ToUpper(),
                LIN = personData.Lin,
                EGN = personData.Uin,
                ADDRESS = address,
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

        internal static List<object> GetLangCertsById(IEnumerable<PersonLangCertDO> includedLangCerts, int roleId, int[] documentRoleIds, INomRepository nomRepository)
        {
            int validTrueId = nomRepository.GetNomValue("boolean", "yes").NomValueId;
            return includedLangCerts
                .Where(t => t.ValidId == validTrueId && documentRoleIds.Contains(t.DocumentRoleId.Value) && t.DocumentRoleId == roleId)
                .Select(t =>
                    new
                    {
                        DOC_TYPE = nomRepository.GetNomValue("documentTypes", t.DocumentTypeId.Value).Name.ToLower(),
                        DOC_NO = t.DocumentNumber,
                        DATE = t.DocumentDateValidFrom,
                        DOC_PUBLISHER = t.DocumentPublisher
                    }).ToList<object>();
        }

        internal static List<object> GetTrainingsById(IEnumerable<PersonTrainingDO> includedTraings, int roleId, int[] documentRoleIds, INomRepository nomRepository)
        {
            int validTrueId = nomRepository.GetNomValue("boolean", "yes").NomValueId;

            return includedTraings
                .Where(t => t.ValidId == validTrueId && documentRoleIds.Contains(t.DocumentRoleId.Value) && t.DocumentRoleId == roleId)
                .Select(t =>
                    new
                    {
                        DOC_TYPE = nomRepository.GetNomValue("documentTypes", t.DocumentTypeId.Value).Name.ToLower(),
                        DOC_NO = t.DocumentNumber,
                        DATE = t.DocumentDateValidFrom,
                        DOC_PUBLISHER = t.DocumentPublisher
                    }).ToList<object>();
        }

        internal static List<object> GetChecksById(IEnumerable<PersonCheckDO> includedChecks, int roleId, int[] documentRoleIds, INomRepository nomRepository)
        {
            int validTrueId = nomRepository.GetNomValue("boolean", "yes").NomValueId;
            return includedChecks
                .Where(t => t.ValidId == validTrueId && documentRoleIds.Contains(t.DocumentRoleId.Value) && t.DocumentRoleId == roleId)
                .Select(t =>
                    new
                    {
                        DOC_TYPE = nomRepository.GetNomValue("documentTypes", t.DocumentTypeId.Value).Name.ToLower(),
                        DOC_NO = t.DocumentNumber,
                        DATE = t.DocumentDateValidFrom,
                        DOC_PUBLISHER = t.DocumentPublisher
                    }).ToList<object>();
        }

        internal static List<object> GetExamsById(
            IEnumerable<PersonTrainingDO> includedExams,
            IEnumerable<PersonCheckDO> includedChecks,
            IEnumerable<PersonTrainingDO> includedTrainings,
            int examRoleId,
            int[] documentRoleIds,
            INomRepository nomRepository)
        {
            var exams = includedExams.Where(d => d.DocumentRoleId == examRoleId)
               .Select(t => new
               {
                   DOC_TYPE = nomRepository.GetNomValue("documentTypes", t.DocumentTypeId.Value).Name.ToLower(),
                   DOC_NO = t.DocumentNumber,
                   DATE = t.DocumentDateValidFrom,
                   DOC_PUBLISHER = t.DocumentPublisher
               })
               .ToList<object>();

            var trainings = Utils.GetTrainingsById(includedTrainings, examRoleId, documentRoleIds, nomRepository);
            var checks = Utils.GetChecksById(includedChecks, examRoleId, documentRoleIds, nomRepository);

            return exams.Union(checks).Union(trainings).ToList<object>();
        }

        internal static List<object> GetMedCerts(
            int orderNumber, IEnumerable<PersonMedicalDO> includedMedicals,
            PersonDataDO personData,
            INomRepository nomRepository)
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
                    CLASS = m.MedClassId.HasValue ? nomRepository.GetNomValue("medClasses", m.MedClassId.Value).Name.ToUpper() : null,
                    PUBLISHER = m.DocumentPublisherId.HasValue ? nomRepository.GetNomValue("medDocPublishers", m.DocumentPublisherId.Value).Name : null,
                    LIMITATION = m.Limitations.Count > 0 ? string.Join(",",nomRepository.GetNomValues("medLimitation", m.Limitations.ToArray()).Select(l => l.Name)) : null
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

        internal static List<object> GetLangCerts(IEnumerable<PersonLangCertDO> includedLangCerts, INomRepository nomRepository)
        {
            return includedLangCerts.Where(l => l.LangLevelId.HasValue)
                .Select(c => 
                {
                    var langLevel = nomRepository.GetNomValue("langLevels", c.LangLevelId.Value);
                    return new
                    {
                        LEVEL = langLevel.Name,
                        ISSUE_DATE = c.DocumentDateValidFrom,
                        VALID_DATE = langLevel.Code.Contains("6") ? "unlimited" : (c.DocumentDateValidTo.HasValue ? c.DocumentDateValidTo.Value.ToString("dd.MM.yyyy") : null)
                    };
                })
            .ToList<object>();
        }

        internal static List<object> GetATCLLangCerts(IEnumerable<PersonLangCertDO> includedLangCerts, INomRepository nomRepository)
        {
            var langLevel = new List<object>();
            foreach (var cert in includedLangCerts.Where(c => c.LangLevelId.HasValue))
            {
                var langLevelNom = nomRepository.GetNomValue("langLevels", cert.LangLevelId.Value);
                if (langLevelNom.TextContent.GetItems<string>("roleAliases").First() == "bgCert")
                {
                    langLevel.Add(new
                    {
                        LEVEL = langLevelNom.Name,
                        ISSUE_DATE = cert.DocumentDateValidFrom,
                        VALID_DATE = langLevelNom.Code.Contains("6") ? "unlimited" : (cert.DocumentDateValidTo.HasValue ? cert.DocumentDateValidTo.Value.ToString("dd.MM.yyyy") : null)
                    });
                }
                else
                {
                    langLevel.Add(new
                    {
                        LEVEL = langLevelNom.Name,
                        ISSUE_DATE = cert.DocumentDateValidFrom,
                        VALID_DATE = cert.DocumentDateValidTo.HasValue ? cert.DocumentDateValidTo.Value.ToString("dd.MM.yyyy") : null
                    });
                }
            }

            return langLevel;
        }

        internal static List<object> GetRatings(
            IEnumerable<PartVersion<PersonRatingDO>> includedRatings,
            IEnumerable<PartVersion<PersonRatingEditionDO>> editions,
            ILotRepository lotRepository,
            INomRepository nomRepository)
        {
            List<object> ratingEditions = new List<object>();
            foreach (var edition in editions)
            {
                var rating = includedRatings.Where(r => r.Part.Index == edition.Content.RatingPartIndex).Single();
                var ratingTypesCodes = rating.Content.RatingTypes.Count() > 0 ? string.Join(", ",  nomRepository.GetNomValues("ratingTypes", rating.Content.RatingTypes.ToArray()).Select(rt => rt.Code)) : "";
                var ratingClassName = rating.Content.RatingClassId.HasValue ?  nomRepository.GetNomValue("ratingClasses", rating.Content.RatingClassId.Value).Code : null;
                var authorizationCode = rating.Content.AuthorizationId.HasValue ? nomRepository.GetNomValue("authorizations", rating.Content.AuthorizationId.Value).Code : null;
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

        internal static NomValue GetCountry(PersonDataDO personData, INomRepository nomRepository)
        {
            NomValue placeOfBirth = personData.PlaceOfBirthId.HasValue ? nomRepository.GetNomValue("cities", personData.PlaceOfBirthId.Value) : null;
            int? countryId = placeOfBirth != null ? placeOfBirth.ParentValueId : (int?)null;
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
            ILotRepository lotRepository,
            INomRepository nomRepository)
        {
            List<object> endorsments = new List<object>();
            foreach (var edition in ratingEditions)
            {
                var rating = includedRatings.Where(r => r.Part.Index == edition.Content.RatingPartIndex).Single();
                if (rating.Content.AuthorizationId.HasValue)
                {
                    var firstRatingEdition = lotRepository.GetLotIndex(rating.Part.LotId)
                        .Index.GetParts<PersonRatingEditionDO>("ratingEditions")
                        .Where(epv => epv.Content.RatingPartIndex == rating.Part.Index)
                        .OrderByDescending(epv => epv.Content.Index)
                        .Last();

                    endorsments.Add(new
                    {
                        NAME = nomRepository.GetNomValue("authorizations", rating.Content.AuthorizationId.Value).Code,
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

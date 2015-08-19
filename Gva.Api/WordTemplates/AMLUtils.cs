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
    public class AMLUtils
    {
        internal static object[] GetCategoryNP(
            IEnumerable<PartVersion<PersonRatingDO>> includedRatings,
            IEnumerable<PartVersion<PersonRatingEditionDO>> ratingEditions,
            List<string> validAliases,
            List<string> validCodes,
            INomRepository nomRepository,
            ILotRepository lotRepository)
        {

            List<object> categories = new List<object>();
            foreach (var edition in ratingEditions)
            {
                var rating = includedRatings.Where(r => r.Part.Index == edition.Content.RatingPartIndex).Single();
                if (rating.Content.AircraftTypeGroupId.HasValue &&
                    rating.Content.AircraftTypeCategoryId.HasValue &&
                    edition.Content.Limitations != null &&
                    edition.Content.Limitations.Count > 0)
                {
                    NomValue categoryNom = nomRepository.GetNomValue("aircraftClases66", rating.Content.AircraftTypeCategoryId.Value);
                    NomValue categoryGroup = nomRepository.GetNomValue("aircraftGroup66", categoryNom.ParentValueId.Value);
                    if (validCodes.Contains(categoryGroup.Code) &&
                        validAliases.Contains(categoryNom.TextContent.Get<string>("alias")))
                    {

                        var firstRatingEdition = lotRepository.GetLotIndex(rating.Part.LotId)
                            .Index.GetParts<PersonRatingEditionDO>("ratingEditions")
                            .Where(epv => epv.Content.RatingPartIndex == rating.Part.Index)
                            .OrderByDescending(epv => epv.Content.Index)
                            .Last();

                        categories.Add(new
                        {
                            TYPE = nomRepository.GetNomValue("aircraftTypeGroups", rating.Content.AircraftTypeGroupId.Value).Name,
                            CAT = categoryNom.Code.Contains("C") ? "C" : categoryNom.Code,
                            DATE_FROM = firstRatingEdition.Content.DocumentDateValidFrom,
                            DATE_TO = edition.Content.DocumentDateValidTo,
                            LIMIT = string.Join(",", nomRepository.GetNomValues("limitations66", edition.Content.Limitations.ToArray()).Select(l => l.Name))
                        });
                    }
                }
            }

            return categories.ToArray();
        }

        internal static object[] GetACLimitations(
            IEnumerable<PartVersion<PersonRatingDO>> includedRatings,
            IEnumerable<PartVersion<PersonRatingEditionDO>> ratingEditions,
            List<string> validAliases,
            List<string> validCodes,
            INomRepository nomRepository)
        {
            List<object> acLimitations = new List<object>();
            foreach (var edition in ratingEditions)
            {
                var rating = includedRatings.Where(r => r.Part.Index == edition.Content.RatingPartIndex).Single();
                NomValue aircraftTypeCategory = rating.Content.AircraftTypeCategoryId.HasValue ? nomRepository.GetNomValue("aircraftClases66", rating.Content.AircraftTypeCategoryId.Value) : null;
                
                if (rating.Content.AircraftTypeGroupId.HasValue && aircraftTypeCategory != null &&
                    validCodes.Contains(nomRepository.GetNomValue("aircraftGroup66", aircraftTypeCategory.ParentValueId.Value).Code) &&
                    validAliases.Contains(nomRepository.GetNomValue("aircraftClases66", rating.Content.AircraftTypeCategoryId.Value).TextContent.Get<string>("alias")) &&
                    (edition.Content.Limitations != null && edition.Content.Limitations.Count > 0))
                {
                    string category = null;
                    if (rating.Content.AircraftTypeCategoryId.HasValue)
                    {
                        NomValue categoryNom = nomRepository.GetNomValue("aircraftClases66", rating.Content.AircraftTypeCategoryId.Value);
                        category = categoryNom.Code.Contains("C") ? "C" : categoryNom.Code;
                    }

                    acLimitations.Add(new
                    {
                        AIRCRAFT = nomRepository.GetNomValue("aircraftTypeGroups", rating.Content.AircraftTypeGroupId.Value).Name,
                        CAT = category,
                        LIM = string.Join(",", nomRepository.GetNomValues("limitations66", edition.Content.Limitations.ToArray()).Select(l => l.Name))
                    });
                }
            }

            return acLimitations.ToArray();
        }

        internal static object[] GetAircrafts(
            IEnumerable<PartVersion<PersonRatingDO>> includedRatings,
            IEnumerable<PartVersion<PersonRatingEditionDO>> ratingEditions,
            Lot lot,
            INomRepository nomRepository)
        {
            List<object> aircrafts = new List<object>();
            foreach (var edition in ratingEditions)
            {
                var rating = includedRatings.Where(r => r.Part.Index == edition.Content.RatingPartIndex).Single();
                if (rating.Content.AircraftTypeCategoryId.HasValue && edition.Content.Limitations.Count == 0)
                {
                    var firstRatingEdition = lot.Index.GetParts<PersonRatingEditionDO>("ratingEditions")
                        .Where(epv => epv.Content.RatingPartIndex == rating.Part.Index)
                        .OrderByDescending(epv => epv.Content.Index)
                        .Last();

                    NomValue categoryNom = nomRepository.GetNomValue("aircraftClases66", rating.Content.AircraftTypeCategoryId.Value);
                    string category = categoryNom.Code.Contains("C") ? "C" : categoryNom.Code;
                    var acType = rating.Content.AircraftTypeGroupId.HasValue ? nomRepository.GetNomValue("aircraftTypeGroups", rating.Content.AircraftTypeGroupId.Value).Name : null;

                    aircrafts.Add(new
                    {
                        AC_TYPE = acType,
                        CATEGORY = category,
                        DATE = firstRatingEdition.Content.DocumentDateValidFrom
                    });
                }
            }

            return aircrafts.ToArray();
        }

        internal static object[] GetCategory(
            IEnumerable<PartVersion<PersonRatingDO>> includedRatings,
            IEnumerable<PartVersion<PersonRatingEditionDO>> ratingEditions,
            List<string> validAliases,
            List<string> validCodes,
            INomRepository nomRepository,
            Lot lot)
        {
            List<object> categories = new List<object>();
            foreach (var edition in ratingEditions)
            {
                var rating = includedRatings.Where(r => r.Part.Index == edition.Content.RatingPartIndex).Single();
                if (rating.Content.AircraftTypeGroupId.HasValue && rating.Content.AircraftTypeCategoryId.HasValue)
                {
                    NomValue categoryNom = nomRepository.GetNomValue("aircraftClases66", rating.Content.AircraftTypeCategoryId.Value);
                    NomValue categoryGroup = nomRepository.GetNomValue("aircraftGroup66", categoryNom.ParentValueId.Value);
                    if(validCodes.Contains(categoryGroup.Code) &&
                        validAliases.Contains(categoryNom.TextContent.Get<string>("alias")))
                    {
                        var firstRatingEdition = lot.Index.GetParts<PersonRatingEditionDO>("ratingEditions")
                            .Where(epv => epv.Content.RatingPartIndex == rating.Part.Index)
                            .OrderByDescending(epv => epv.Content.Index)
                            .Last();

                        categories.Add(new
                        {
                            TYPE = nomRepository.GetNomValue("aircraftTypeGroups", rating.Content.AircraftTypeGroupId.Value).Name,
                            CAT = categoryNom.Code.Contains("C") ? "C" : categoryNom.Code,
                            DATE = firstRatingEdition.Content.DocumentDateValidFrom,
                            LIMIT = (edition.Content.Limitations != null && edition.Content.Limitations.Count > 0) ?
                                string.Join(",", nomRepository.GetNomValues("limitations66", edition.Content.Limitations.ToArray()).Select(l => l.Name)) :
                                ""
                        });
                    }
                }
            }

            return categories.ToArray();
        }

        internal static object[] GetLimitations(PersonLicenceEditionDO lastLicenceEdition, INomRepository nomRepository)
        {
            string noLimitations = "No limitations";
            IList<object> limitations = new List<object>();

            if (NullOrEmpty(lastLicenceEdition.AmlLimitations.At_a_Ids) && NullOrEmpty(lastLicenceEdition.AmlLimitations.At_b1_Ids) &&
                NullOrEmpty(lastLicenceEdition.AmlLimitations.Ap_a_Ids) && NullOrEmpty(lastLicenceEdition.AmlLimitations.Ap_b1_Ids) &&
                NullOrEmpty(lastLicenceEdition.AmlLimitations.Ht_a_Ids) && NullOrEmpty(lastLicenceEdition.AmlLimitations.Ht_b1_Ids) &&
                NullOrEmpty(lastLicenceEdition.AmlLimitations.Hp_a_Ids) && NullOrEmpty(lastLicenceEdition.AmlLimitations.Hp_b1_Ids) &&
                NullOrEmpty(lastLicenceEdition.AmlLimitations.Avionics_Ids))
            {
                return new object[0];
            }

            List<int> AT_a_Ids = lastLicenceEdition.AmlLimitations == null ?
                new List<int>() :
                lastLicenceEdition.AmlLimitations.At_a_Ids;
            List<int> AT_b1_Ids = lastLicenceEdition.AmlLimitations == null ?
                new List<int>() :
                lastLicenceEdition.AmlLimitations.At_b1_Ids;
            List<int> AP_a_Ids = lastLicenceEdition.AmlLimitations == null ?
                new List<int>() :
                lastLicenceEdition.AmlLimitations.Ap_a_Ids;
            List<int> AP_b1_Ids = lastLicenceEdition.AmlLimitations == null ?
                new List<int>() :
                lastLicenceEdition.AmlLimitations.Ap_b1_Ids;
            List<int> HT_a_Ids = lastLicenceEdition.AmlLimitations == null ?
                new List<int>() :
                lastLicenceEdition.AmlLimitations.Ht_a_Ids;
            List<int> HT_b1_Ids = lastLicenceEdition.AmlLimitations == null ?
                new List<int>() :
                lastLicenceEdition.AmlLimitations.Ht_b1_Ids;
            List<int> HP_a_Ids = lastLicenceEdition.AmlLimitations == null ?
                new List<int>() :
                lastLicenceEdition.AmlLimitations.Hp_a_Ids;
            List<int> HP_b1_Ids = lastLicenceEdition.AmlLimitations == null ?
                new List<int>() :
                lastLicenceEdition.AmlLimitations.Hp_b1_Ids;
            List<int> avionics_Ids = lastLicenceEdition.AmlLimitations == null ?
                new List<int>() :
                lastLicenceEdition.AmlLimitations.Avionics_Ids;

            if (AT_a_Ids != null && AT_a_Ids.Count > 0)
            {
                limitations.Add(new
                {
                    NAME = "Aeroplanes Turbine",
                    CAT = "A 1",
                    LIMT = string.Join(",", AT_a_Ids.Select(l => nomRepository.GetNomValue(l).Name))
                });
            }

            if (AT_b1_Ids != null && AT_b1_Ids.Count > 0)
            {
                limitations.Add(new
                {
                    NAME = "Aeroplanes Turbine",
                    CAT = "B 1.1",
                    LIMT =  string.Join(",", AT_b1_Ids.Select(l => nomRepository.GetNomValue(l).Name))
                });
            }

            if (AP_a_Ids != null && AP_a_Ids.Count > 0)
            {
                limitations.Add(new
                {
                    NAME = "Aeroplanes Piston",
                    CAT = "A 2",
                    LIMT = string.Join(",", AP_a_Ids.Select(l => nomRepository.GetNomValue(l).Name))
                });
            }

            if (AP_b1_Ids != null && AP_b1_Ids.Count > 0)
            {
                limitations.Add(new
                {
                    NAME = "Aeroplanes Piston",
                    CAT = "B 1.2",
                    LIMT = string.Join(",", AP_b1_Ids.Select(l => nomRepository.GetNomValue(l).Name))
                });
            }

            if (HT_a_Ids != null && HT_a_Ids.Count > 0)
            {
                limitations.Add(new
                {
                    NAME = "Helicopters Turbine",
                    CAT = "A 3",
                    LIMT =  string.Join(",", HT_a_Ids.Select(l => nomRepository.GetNomValue(l).Name))
                });
            }

            if (HT_b1_Ids != null && HT_b1_Ids.Count > 0)
            {
                limitations.Add(new
                {
                    NAME = "Helicopters Turbine",
                    CAT = "B 1.3",
                    LIMT = string.Join(",", HT_b1_Ids.Select(l => nomRepository.GetNomValue(l).Name))
                });
            }

            if (HP_a_Ids != null && HP_a_Ids.Count > 0)
            {
                limitations.Add(new
                {
                    NAME = "Helicopters Piston",
                    CAT = "A 4",
                    LIMT = HP_a_Ids != null && HP_a_Ids.Count > 0 ? string.Join(",", HP_a_Ids.Select(l => nomRepository.GetNomValue(l).Name)) : noLimitations
                });
            }

            if (HP_b1_Ids != null && HP_b1_Ids.Count > 0)
            {
                limitations.Add(new
                {
                    NAME = "Helicopters Piston",
                    CAT = "B 1.4",
                    LIMT = string.Join(",", HP_b1_Ids.Select(l => nomRepository.GetNomValue(l).Name))
                });
            }

            if (avionics_Ids != null && avionics_Ids.Count > 0)
            {

                limitations.Add(new
                {
                    NAME = "Avionics",
                    CAT = "B 2",
                    LIMT = string.Join(",", avionics_Ids.Select(l => nomRepository.GetNomValue(l).Name))
                });
            }

            return limitations.ToArray<object>();
        }

        private static bool NullOrEmpty<T>(ICollection<T> c)
        {
            return c == null || c.Count == 0;
        }
    }
}

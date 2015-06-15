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
                if (rating.Content.AircraftTypeGroup != null && rating.Content.AircraftTypeCategory != null &&
                    validCodes.Contains(nomRepository.GetNomValue("aircraftGroup66", rating.Content.AircraftTypeCategory.ParentValueId.Value).Code) &&
                    validAliases.Contains(nomRepository.GetNomValue("aircraftClases66", rating.Content.AircraftTypeCategory.NomValueId).TextContent.Get<string>("alias")) &&
                    edition.Content.Limitations != null && edition.Content.Limitations.Count > 0)
                {
                    var firstRatingEdition = lotRepository.GetLotIndex(rating.Part.LotId)
                        .Index.GetParts<PersonRatingEditionDO>("ratingEditions")
                        .Where(epv => epv.Content.RatingPartIndex == rating.Part.Index)
                        .OrderByDescending(epv => epv.Content.Index)
                        .Last();

                    string category = null;
                    if (rating.Content.AircraftTypeCategory != null)
                    {
                        category = rating.Content.AircraftTypeCategory.Code.Contains("C") ? "C" : rating.Content.AircraftTypeCategory.Code;
                    }

                    categories.Add(new
                    {
                        TYPE = nomRepository.GetNomValue("aircraftTypeGroups", rating.Content.AircraftTypeGroup.NomValueId).Name,
                        CAT = category,
                        DATE_FROM = firstRatingEdition.Content.DocumentDateValidFrom,
                        DATE_TO = edition.Content.DocumentDateValidTo,
                        LIMIT = string.Join(",", edition.Content.Limitations.Select(l => l.Name))
                    });
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
                        AIRCRAFT = nomRepository.GetNomValue("aircraftTypeGroups", rating.Content.AircraftTypeGroup.NomValueId).Name,
                        CAT = category,
                        LIM = string.Join(",", edition.Content.Limitations.Select(l => l.Name))
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
                if (rating.Content.AircraftTypeCategory != null && edition.Content.Limitations.Count == 0)
                {
                    var firstRatingEdition = lot.Index.GetParts<PersonRatingEditionDO>("ratingEditions")
                        .Where(epv => epv.Content.RatingPartIndex == rating.Part.Index)
                        .OrderByDescending(epv => epv.Content.Index)
                        .Last();

                    string category = rating.Content.AircraftTypeCategory.Code.Contains("C") ? "C" : rating.Content.AircraftTypeCategory.Code;


                    aircrafts.Add(new
                    {
                        AC_TYPE = rating.Content.AircraftTypeGroup != null ? nomRepository.GetNomValue("aircraftTypeGroups", rating.Content.AircraftTypeGroup.NomValueId).Name : null,
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
                if (rating.Content.AircraftTypeGroup != null && rating.Content.AircraftTypeCategory != null &&
                    validCodes.Contains(nomRepository.GetNomValue("aircraftGroup66", rating.Content.AircraftTypeCategory.ParentValueId.Value).Code) &&
                    validAliases.Contains(nomRepository.GetNomValue("aircraftClases66", rating.Content.AircraftTypeCategory.NomValueId).TextContent.Get<string>("alias")))
                {
                    var firstRatingEdition = lot.Index.GetParts<PersonRatingEditionDO>("ratingEditions")
                        .Where(epv => epv.Content.RatingPartIndex == rating.Part.Index)
                        .OrderByDescending(epv => epv.Content.Index)
                        .Last();

                    string category = null;
                    if(rating.Content.AircraftTypeCategory != null)
                    {
                        category = rating.Content.AircraftTypeCategory.Code.Contains("C") ? "C" : rating.Content.AircraftTypeCategory.Code;
                    }

                    categories.Add(new
                    {
                        TYPE = nomRepository.GetNomValue("aircraftTypeGroups", rating.Content.AircraftTypeGroup.NomValueId).Name,
                        CAT = category,
                        DATE = firstRatingEdition.Content.DocumentDateValidFrom,
                        LIMIT = (edition.Content.Limitations != null && edition.Content.Limitations.Count > 0) ?
                            string.Join(",", edition.Content.Limitations.Select(l => l.Name)) :
                            ""
                    });
                }
            }

            return categories.ToArray();
        }

        internal static object[] GetLimitations(PersonLicenceEditionDO lastLicenceEdition)
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

            List<NomValue> AT_a_Ids = lastLicenceEdition.AmlLimitations == null ?
                new List<NomValue>() :
                lastLicenceEdition.AmlLimitations.At_a_Ids;
            List<NomValue> AT_b1_Ids = lastLicenceEdition.AmlLimitations == null ?
                new List<NomValue>() :
                lastLicenceEdition.AmlLimitations.At_b1_Ids;
            List<NomValue> AP_a_Ids = lastLicenceEdition.AmlLimitations == null ?
                new List<NomValue>() :
                lastLicenceEdition.AmlLimitations.Ap_a_Ids;
            List<NomValue> AP_b1_Ids = lastLicenceEdition.AmlLimitations == null ?
                new List<NomValue>() :
                lastLicenceEdition.AmlLimitations.Ap_b1_Ids;
            List<NomValue> HT_a_Ids = lastLicenceEdition.AmlLimitations == null ?
                new List<NomValue>() :
                lastLicenceEdition.AmlLimitations.Ht_a_Ids;
            List<NomValue> HT_b1_Ids = lastLicenceEdition.AmlLimitations == null ?
                new List<NomValue>() :
                lastLicenceEdition.AmlLimitations.Ht_b1_Ids;
            List<NomValue> HP_a_Ids = lastLicenceEdition.AmlLimitations == null ?
                new List<NomValue>() :
                lastLicenceEdition.AmlLimitations.Hp_a_Ids;
            List<NomValue> HP_b1_Ids = lastLicenceEdition.AmlLimitations == null ?
                new List<NomValue>() :
                lastLicenceEdition.AmlLimitations.Hp_b1_Ids;
            List<NomValue> avionics_Ids = lastLicenceEdition.AmlLimitations == null ?
                new List<NomValue>() :
                lastLicenceEdition.AmlLimitations.Avionics_Ids;

            limitations.Add(new
            {
                NAME = "Aeroplanes Turbine",
                CAT = "A 1",
                LIMT = AT_a_Ids != null && AT_a_Ids.Count > 0 ? string.Join(",", AT_a_Ids.Select(l => l.Name)) : noLimitations
            });

            limitations.Add(new
            {
                NAME = "Aeroplanes Turbine",
                CAT = "B 1.1",
                LIMT = AT_b1_Ids != null && AT_b1_Ids.Count > 0 ? string.Join(",", AT_b1_Ids.Select(l => l.Name)) : noLimitations
            });

            limitations.Add(new
            {
                NAME = "Aeroplanes Piston",
                CAT = "A 2",
                LIMT = AP_a_Ids != null && AP_a_Ids.Count > 0 ? string.Join(",", AP_a_Ids.Select(l => l.Name)) : noLimitations
            });

            limitations.Add(new
            {
                NAME = "Aeroplanes Piston",
                CAT = "B 1.2",
                LIMT = AP_b1_Ids != null && AP_b1_Ids.Count > 0 ? string.Join(",", AP_b1_Ids.Select(l => l.Name)) : noLimitations
            });

            limitations.Add(new
            {
                NAME = "Helicopters Turbine",
                CAT = "A 3",
                LIMT = HT_a_Ids != null && HT_a_Ids.Count > 0 ? string.Join(",", HT_a_Ids.Select(l => l.Name)) : noLimitations
            });

            limitations.Add(new
            {
                NAME = "Helicopters Turbine",
                CAT = "B 1.3",
                LIMT = HT_b1_Ids != null && HT_b1_Ids.Count > 0 ? string.Join(",", HT_b1_Ids.Select(l => l.Name)) : noLimitations
            });

            limitations.Add(new
            {
                NAME = "Helicopters Piston",
                CAT = "A 4",
                LIMT = HP_a_Ids != null && HP_a_Ids.Count > 0 ? string.Join(",", HP_a_Ids.Select(l => l.Name)) : noLimitations
            });

            limitations.Add(new
            {
                NAME = "Helicopters Piston",
                CAT = "B 1.4",
                LIMT = HP_b1_Ids != null && HP_b1_Ids.Count > 0 ? string.Join(",", HP_b1_Ids.Select(l => l.Name)) : noLimitations
            });
            limitations.Add(new
            {
                NAME = "Avionics",
                CAT = "B 2",
                LIMT = avionics_Ids != null && avionics_Ids.Count > 0 ? string.Join(",", avionics_Ids.Select(l => l.Name)) : noLimitations
            });

            return limitations.ToArray<object>();
        }

        private static bool NullOrEmpty<T>(ICollection<T> c)
        {
            return c == null || c.Count == 0;
        }
    }
}

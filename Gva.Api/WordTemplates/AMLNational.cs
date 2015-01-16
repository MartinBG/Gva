﻿using System.Collections.Generic;
using System.Linq;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Json;
using Gva.Api.ModelsDO.Persons;
using Regs.Api.Repositories.LotRepositories;
using Regs.Api.Models;

namespace Gva.Api.WordTemplates
{
    public class AMLNational : IDataGenerator
    {
        private ILotRepository lotRepository;
        private INomRepository nomRepository;

        public AMLNational(
            ILotRepository lotRepository,
            INomRepository nomRepository)
        {
            this.lotRepository = lotRepository;
            this.nomRepository = nomRepository;
        }

        public string[] TemplateNames
        {
            get
            {
                return new string[] { "AML_national" };
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

            var licencePart = lot.Index.GetPart<PersonLicenceDO>(path);
            var licence = licencePart.Content;
            var editions = lot.Index.GetParts<PersonLicenceEditionDO>("licenceEditions")
                .Where(e => e.Content.LicencePartIndex == licencePart.Part.Index)
                .OrderBy(e => e.Content.Index)
                .Select(e => e.Content);

            var firstEdition = editions.First();
            var lastEdition = editions.Last();

            var includedRatings = lastEdition.IncludedRatings.Select(i => i.Ind).Distinct()
                .Select(ind => lot.Index.GetPart<PersonRatingDO>("ratings/" + ind));
            var ratingEditions = lastEdition.IncludedRatings.Select(i => lot.Index.GetPart<PersonRatingEditionDO>("ratingEditions/" + i.Index));

            var licenceType = this.nomRepository.GetNomValue("licenceTypes", licence.LicenceType.NomValueId);
            var country = this.GetCountry(personAddress);
            var licenceNumber = string.Format(
                " BGR. AM - {0} - {1}",
                Utils.PadLicenceNumber(licence.LicenceNumber),
                personData.Lin);
            var personName = string.Format("{0} {1} {2}",
                    personData.FirstName,
                    personData.MiddleName,
                    personData.LastName).ToUpper();

            var limitations = lastEdition.AmlLimitations != null ? this.GetLimitations(lastEdition) : new object[0];
            var acLimitations = this.GetACLimitations(includedRatings, ratingEditions);

            var json = new
            {
                root = new
                {
                    LICENCE_TYPE = licenceType.Code,
                    LICENCE_NO = licenceNumber,
                    PERSON = personName,
                    PERSON_EN = string.Format("{0} {1} {2}",
                        personData.FirstNameAlt,
                        personData.MiddleNameAlt,
                        personData.LastNameAlt).ToUpper(),
                    DATE_OF_BIRTH = personData.DateOfBirth,
                    SEX = personData.Sex.Code,
                    ADDRESS = string.Format(
                        "{0}, {1}",
                        personAddress.Settlement != null ? personAddress.Settlement.Name : null,
                        personAddress.Address),
                    NATIONALITY = country != null ? country.Name : null,
                    NATIONALITY_EN = country != null ? country.Code : null,
                    ISSUE_DATE = lastEdition.DocumentDateValidFrom,
                    FIRST_ISSUE_DATE = firstEdition.DocumentDateValidFrom,
                    LIC_NO2 = licenceNumber,
                    CAT1 = "A",
                    CAT2 = "B 1",
                    CAT3 = "B 2",
                    CAT4 = "C",
                    CATEGORIES = this.GetCategories(includedRatings),
                    LIC_NO3 = licenceNumber,
                    NAME = personName,
                    DATE_OF_BIRTH1 = string.Format("{0:dd.mm.yyyy} {1}",
                        personData.DateOfBirth,
                        personData.PlaceOfBirth != null ? personData.PlaceOfBirth.Name : null),
                    ADDR = personAddress.Address,
                    NATIONALITY1 = country != null ? country.Name : null,
                    LICNO = licenceNumber,
                    T_ISSUE_DATE = lastEdition.DocumentDateValidFrom,
                    T_VALID_DATE = lastEdition.DocumentDateValidTo,
                    CATEGORY = this.GetCategory(includedRatings, ratingEditions),
                    NA = limitations.Count() == 0 && acLimitations.Count() == 0 ? "No limitations" : "",
                    LIMITATIONS = (limitations.Count() > 0 || acLimitations.Count() > 0) ? limitations : new object(),
                    AC_LIMITATIONS = acLimitations.Count() > 0 ? acLimitations : new object(),
                    VALID_DATE = lastEdition.DocumentDateValidTo,
                    LIC_NO4 = licenceNumber
                }
            };

            return json;
        }
        private NomValue GetCountry(PersonAddressDO personAddress)
        {
            int? countryId = personAddress.Settlement != null?  personAddress.Settlement.ParentValueId : null;
            NomValue country = countryId.HasValue ?
                this.nomRepository.GetNomValue("countries", countryId.Value) :
                new NomValue
                {
                    Name = null,
                    TextContentString = string.Empty
                };

            return country;
        }

        private object[] GetCategories(IEnumerable<PartVersion<PersonRatingDO>> includedRatings)
        {
            List<string> validCodes = new List<string> { "1", "2", "3", "4", "5", "6", "7" };
            IEnumerable<NomValue> aircraftGroups66 = this.nomRepository.GetNomValues("aircraftGroup66").Where(r => validCodes.Contains(r.Code));
            IEnumerable<NomValue> aircraftClases66 = this.nomRepository.GetNomValues("aircraftClases66");

            List<object> results = new List<object>();
            foreach (var group66 in aircraftGroups66)
            {
                IEnumerable<int> categoriesIds = includedRatings
                    .Where(rating => rating.Content.AircraftTypeCategory != null && rating.Content.AircraftTypeCategory.ParentValueId == group66.NomValueId)
                    .Select(rating => rating.Content.AircraftTypeCategory.NomValueId);

                IEnumerable<NomValue> categories = categoriesIds.Select(categoryId => this.nomRepository.GetNomValue("aircraftClases66", categoryId));
                IEnumerable<string> aliases = categories.Select(category => category.TextContent.Get<string>("alias"));

                if (group66.NameAlt != "Reserved")
                {
                    results.Add(new
                    {
                        NAME = new
                        {
                            EN = group66.NameAlt,
                            BG = group66.Name
                        },
                        CAT1 = !aircraftClases66.Where(e => e.ParentValueId == group66.NomValueId && e.Code.Contains("A")).Any() ?
                        "n/a" : aliases.Contains("A") ?
                        aircraftClases66.Where(e => e.ParentValueId == group66.NomValueId && e.Code.Contains("A")).First().Code : "X",
                        CAT2 = !aircraftClases66.Where(e => e.ParentValueId == group66.NomValueId && e.Code.Contains("B 1")).Any() ?
                        "n/a" : aliases.Contains("B 1") ?
                        aircraftClases66.Where(e => e.ParentValueId == group66.NomValueId && e.Code.Contains("B 1")).First().Code : "X",
                        CAT3 = !aircraftClases66.Where(e => e.ParentValueId == group66.NomValueId && e.Code.Contains("B 2")).Any() ?
                        "n/a" : aliases.Contains("B 2") ?
                        aircraftClases66.Where(e => e.ParentValueId == group66.NomValueId && e.Code.Contains("B 2")).First().Code : "X",
                        CAT4 = !aircraftClases66.Where(e => e.ParentValueId == group66.NomValueId && e.Code.Contains("C")).Any() ?
                        "n/a" : aliases.Contains("C") ?
                        aircraftClases66.Where(e => e.ParentValueId == group66.NomValueId && e.Code.Contains("C")).First().Code : "X",
                    });
                }
                else
                {
                    results.Add(new
                    {
                        NAME = new
                        {
                            EN = group66.NameAlt,
                            BG = group66.Name
                        }
                    });
                }
            }

            return results.ToArray<object>();
        }

        private object[] GetCategory(IEnumerable<PartVersion<PersonRatingDO>> includedRatings, IEnumerable<PartVersion<PersonRatingEditionDO>> ratingEditions)
        {
            List<string> validAliases = new List<string> { "A", "B 1", "B 2", "C" };
            List<string> validCodes = new List<string> { "1", "2", "3", "4", "5", "6" };

            List<object> categories = new List<object>();
            foreach (var edition in ratingEditions)
            {
                var rating = includedRatings.Where(r => r.Part.Index == edition.Content.RatingPartIndex).Single();
                if (rating.Content.AircraftTypeGroup != null && rating.Content.AircraftTypeCategory != null &&
                    validCodes.Contains(this.nomRepository.GetNomValue("aircraftGroup66", rating.Content.AircraftTypeCategory.ParentValueId.Value).Code) &&
                    validAliases.Contains(this.nomRepository.GetNomValue("aircraftClases66", rating.Content.AircraftTypeCategory.NomValueId).TextContent.Get<string>("alias")))
                {
                    var firstRatingEdition = this.lotRepository.GetLotIndex(rating.Part.LotId)
                        .Index.GetParts<PersonRatingEditionDO>("ratingEditions")
                        .Where(epv => epv.Content.RatingPartIndex == rating.Part.Index)
                        .OrderByDescending(epv => epv.Content.Index)
                        .Last();

                    categories.Add(new
                    {
                        TYPE = rating.Content.AircraftTypeGroup.Name,
                        CAT = rating.Content.AircraftTypeCategory.Code,
                        DATE = firstRatingEdition.Content.DocumentDateValidFrom,
                        LIMIT = (edition.Content.Limitations != null && edition.Content.Limitations.Count > 0) ?
                            string.Join(",", edition.Content.Limitations.Select(l => l.Name)) :
                            ""
                    });
                }
            }

            if (categories.Count() == 0) {
                categories.Add(new object());
            }

            return categories.ToArray();
        }

        private object[] GetACLimitations(IEnumerable<PartVersion<PersonRatingDO>> includedRatings, IEnumerable<PartVersion<PersonRatingEditionDO>> ratingEditions)
        {
            List<string> validAliases = new List<string> { "A", "B 1", "B 2", "C" };
            List<string> validCodes = new List<string> { "1", "2", "3", "4", "5", "6" };

            List<object> acLimitations = new List<object>();
            foreach (var edition in ratingEditions)
            {
                var rating = includedRatings.Where(r => r.Part.Index == edition.Content.RatingPartIndex).Single();
                if (rating.Content.AircraftTypeGroup != null && rating.Content.AircraftTypeCategory != null &&
                    validCodes.Contains(this.nomRepository.GetNomValue("aircraftGroup66", rating.Content.AircraftTypeCategory.ParentValueId.Value).Code) &&
                    validAliases.Contains(this.nomRepository.GetNomValue("aircraftClases66", rating.Content.AircraftTypeCategory.NomValueId).TextContent.Get<string>("alias")) &&
                    (edition.Content.Limitations != null && edition.Content.Limitations.Count > 0))
                {
                    acLimitations.Add(new
                    {
                        AIRCRAFT = rating.Content.AircraftTypeGroup.Name,
                        CAT = rating.Content.AircraftTypeCategory.Code,
                        LIM = string.Join(",", edition.Content.Limitations.Select(l => l.Name))
                    });
                }
            }

            return acLimitations.ToArray();
        }

        private object[] GetLimitations(PersonLicenceEditionDO lastLicenceEdition)
        {
            IList<object> limitations = new List<object>();

            if (lastLicenceEdition.AmlLimitations.At_a_Ids == null && lastLicenceEdition.AmlLimitations.At_b1_Ids == null &&
                lastLicenceEdition.AmlLimitations.Ap_a_Ids == null && lastLicenceEdition.AmlLimitations.Ap_b1_Ids == null &&
                lastLicenceEdition.AmlLimitations.Ht_a_Ids == null && lastLicenceEdition.AmlLimitations.Ht_b1_Ids == null &&
                lastLicenceEdition.AmlLimitations.Hp_a_Ids == null && lastLicenceEdition.AmlLimitations.Hp_b1_Ids == null &&
                lastLicenceEdition.AmlLimitations.Avionics_Ids == null)
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
                LIMT = (AT_a_Ids != null && AT_a_Ids.Count > 0) ? string.Join(",", AT_a_Ids.Select(l => l.Name)) : "No limitation"
            });

            limitations.Add(new
            {
                NAME = "Aeroplanes Turbine",
                CAT = "B 1.1",
                LIMT = (AT_b1_Ids != null && AT_b1_Ids.Count > 0) ? string.Join(",", AT_b1_Ids.Select(l => l.Name)) : "No limitation"
            });

            limitations.Add(new
            {
                NAME = "Aeroplanes Piston",
                CAT = "A 2",
                LIMT = (AP_a_Ids != null && AP_a_Ids.Count > 0) ? string.Join(",", AP_a_Ids.Select(l => l.Name)) : "No limitation"
            });

            limitations.Add(new
            {
                NAME = "Aeroplanes Piston",
                CAT = "B 1.2",
                LIMT = (AP_b1_Ids != null && AP_b1_Ids.Count > 0) ? string.Join(",", AP_b1_Ids.Select(l => l.Name)) : "No limitation"
            });

            limitations.Add(new
            {
                NAME = "Helicopters Turbine",
                CAT = "A 3",
                LIMT = (HT_a_Ids != null && HT_a_Ids.Count > 0) ? string.Join(",", HT_a_Ids.Select(l => l.Name)) : "No limitation"
            });

            limitations.Add(new
            {
                NAME = "Helicopters Turbine",
                CAT = "B 1.3",
                LIMT = (HT_b1_Ids != null && HT_b1_Ids.Count > 0) ? string.Join(",", HT_b1_Ids.Select(l => l.Name)) : "No limitation"
            });

            limitations.Add(new
            {
                NAME = "Helicopters Piston",
                CAT = "A 4",
                LIMT = (HP_a_Ids != null && HP_a_Ids.Count > 0) ? string.Join(",", HP_a_Ids.Select(l => l.Name)) : "No limitation"
            });

            limitations.Add(new
            {
                NAME = "Helicopters Piston",
                CAT = "B 1.4",
                LIMT = (HT_b1_Ids != null && HT_b1_Ids.Count > 0) ? string.Join(",", HP_b1_Ids.Select(l => l.Name)) : "No limitation"
            });
            limitations.Add(new
            {
                NAME = "Acionics",
                CAT = "B 2",
                LIMT = (avionics_Ids != null && avionics_Ids.Count > 0) ? string.Join(",", avionics_Ids.Select(l => l.Name)) : "No limitation"
            });

            return limitations.ToArray<object>(); ;
        }
    }
}

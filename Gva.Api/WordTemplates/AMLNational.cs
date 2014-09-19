﻿using System.Collections.Generic;
using System.Linq;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Json;
using Gva.Api.ModelsDO.Persons;
using Regs.Api.Repositories.LotRepositories;

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

            var includedRatings = lastEdition.IncludedRatings
                .Select(i => lot.Index.GetPart<PersonRatingDO>("ratings/" + i).Content);
            var licenceType = this.nomRepository.GetNomValue("licenceTypes", licence.LicenceType.NomValueId);
            var country = this.GetCountry(personAddress);
            var licenceNumber = string.Format(
                "BG {0} - {1} - {2}",
                licence.LicenceType.Code,
                Utils.PadLicenceNumber(licence.LicenceNumber),
                personData.Lin);
            var personName = string.Format("{0} {1} {2}",
                    personData.FirstName,
                    personData.MiddleName,
                    personData.LastName).ToUpper();

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
                    SEX = personData.Sex.Name,
                    ADDRESS = string.Format(
                        "{0}, {1}",
                        personAddress.Settlement != null ? personAddress.Settlement.Name : null,
                        personAddress.Address),
                    NATIONALITY = country.Name,
                    NATIONALITY_EN = country.Code,
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
                        personData.PlaceOfBirth.Name),
                    ADDR = personAddress.Address,
                    NATIONALITY1 = country.Name,
                    LICNO = licenceNumber,
                    T_ISSUE_DATE = lastEdition.DocumentDateValidFrom,
                    T_VALID_DATE = lastEdition.DocumentDateValidTo,
                    CATEGORY = this.GetCategory(includedRatings),
                    LIMITATIONS = this.GetLimitations(lastEdition),
                    AC_LIMITATIONS = this.GetACLimitations(includedRatings),
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

        private object[] GetCategories(IEnumerable<PersonRatingDO> includedRatings)
        {
            List<string> validCodes = new List<string> { "1", "2", "3", "4", "5", "6" };
            IEnumerable<NomValue> aircraftGroups66 = this.nomRepository.GetNomValues("aircraftGroup66").Where(r => validCodes.Contains(r.Code));

            List<object> results = new List<object>();
            foreach (var group66 in aircraftGroups66)
            {
                IEnumerable<int> categoriesIds = includedRatings
                    .Where(rating => rating.AircraftTypeCategory != null && rating.AircraftTypeCategory.ParentValueId == group66.NomValueId)
                    .Select(rating => rating.AircraftTypeCategory.NomValueId);

                IEnumerable<NomValue> categories = categoriesIds.Select(categoryId => this.nomRepository.GetNomValue("aircraftClases66", categoryId));
                IEnumerable<string> aliases = categories.Select(category => category.TextContent.Get<string>("alias"));

                results.Add(new
                {
                    NAME = new
                    {
                        EN = group66.NameAlt,
                        BG = group66.Name
                    },
                    CAT1 = aliases.Contains("A") ? "X" : "n/a",
                    CAT2 = aliases.Contains("B 1") ? "X" : "n/a",
                    CAT3 = aliases.Contains("B 2") ? "X" : "n/a",
                    CAT4 = aliases.Contains("C") ? "X" : "n/a"
                });
            }

            return results.ToArray<object>();
        }

        private object[] GetCategory(IEnumerable<PersonRatingDO> includedRatings)
        {
            List<string> validAliases = new List<string> { "A", "B 1", "B 2", "C" };
            List<string> validCodes = new List<string> { "1", "2", "3", "4", "5", "6" };

            return includedRatings
                .Where(r => r.AircraftTypeGroup != null && r.AircraftTypeCategory != null &&
                    validCodes.Contains(this.nomRepository.GetNomValue("aircraftGroup66", r.AircraftTypeCategory.ParentValueId.Value).Code) &&
                    validAliases.Contains(this.nomRepository.GetNomValue("aircraftClases66", r.AircraftTypeCategory.NomValueId).TextContent.Get<string>("alias")))
                .Select(r =>
                    {
                        var lastEdition = r.Editions.Last();
                        return new
                        {
                            TYPE = r.AircraftTypeGroup.Name,
                            CAT = r.AircraftTypeCategory.Code,
                            DATE = lastEdition.DocumentDateValidFrom,
                            LIMIT = (lastEdition.Limitations != null && lastEdition.Limitations.Count > 0) ? string.Join(",", lastEdition.Limitations.Select(l => l.Name)) : "NP"
                        };
                    }).ToArray<object>();
        }

        private object[] GetACLimitations(IEnumerable<PersonRatingDO> includedRatings)
        {
            List<string> validAliases = new List<string> { "A", "B 1", "B 2", "C" };
            List<string> validCode = new List<string> { "1", "2", "3", "4", "5", "6" };

            return includedRatings
                .Where(r => r.AircraftTypeGroup != null && r.AircraftTypeCategory != null &&
                    validCode.Contains(this.nomRepository.GetNomValue("aircraftGroup66", r.AircraftTypeCategory.ParentValueId.Value).Code) &&
                    validAliases.Contains(this.nomRepository.GetNomValue("aircraftClases66", r.AircraftTypeCategory.NomValueId).TextContent.Get<string>("alias")))
                .Select(r =>
                    {
                        var lastEdition = r.Editions.Last();
                        return new
                            {
                                AIRCRAFT = r.AircraftTypeGroup.Name,
                                CAT = r.AircraftTypeCategory.Code,
                                LIM = (lastEdition.Limitations != null && lastEdition.Limitations.Count > 0) ? string.Join(",", lastEdition.Limitations.Select(l => l.Name)) : "NP"
                            };
                    }).ToArray<object>();
        }

        private object[] GetLimitations(PersonLicenceEditionDO lastLicenceEdition)
        {
            IList<object> limitations = new List<object>();

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

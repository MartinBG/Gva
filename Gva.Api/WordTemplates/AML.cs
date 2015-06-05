using System.Collections.Generic;
using System.Linq;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Json;
using Gva.Api.ModelsDO.Persons;
using Regs.Api.Repositories.LotRepositories;
using Regs.Api.Models;

namespace Gva.Api.WordTemplates
{
    public class AML : IDataGenerator
    {
        private ILotRepository lotRepository;
        private INomRepository nomRepository;

        public AML(
            ILotRepository lotRepository,
            INomRepository nomRepository)
        {
            this.lotRepository = lotRepository;
            this.nomRepository = nomRepository;
        }

        public string GeneratorCode
        {
            get
            {
                return "AML";
            }
        }

        public string GeneratorName
        {
            get
            {
                return "AML";
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
                .Select(i => i.Ind)
                .Distinct()
                .Select(i => lot.Index.GetPart<PersonRatingDO>("ratings/" + i.Value));
            var ratingEditions = lastEdition.IncludedRatings.Select(i => lot.Index.GetPart<PersonRatingEditionDO>("ratingEditions/" + i.Index));

            var licenceType = this.nomRepository.GetNomValue("licenceTypes", licence.LicenceType.NomValueId);
            var country = Utils.GetCountry(personAddress, this.nomRepository);
            string licenceCode = licence.LicenceType.Code;
            var licenceNumber = string.Format(
                "BG.66.{0} - {1}",
                Utils.PadLicenceNumber(licence.LicenceNumber),
                personData.Lin);
            var personNameBG = string.Format("{0} {1} {2}",
                personData.FirstName,
                personData.MiddleName,
                personData.LastName).ToUpper();
            var personNameAlt = string.Format("{0} {1} {2}",
                personData.FirstNameAlt,
                personData.MiddleNameAlt,
                personData.LastNameAlt).ToUpper();

            List<string> validAliases = new List<string> { "A", "B 1", "B 2", "C" };
            List<string> validCodes = new List<string> { "1", "2", "3", "4", "5", "6", "7" };

            var categoryNP = AMLUtils.GetCategoryNP(includedRatings, ratingEditions, validAliases, validCodes, this.nomRepository, this.lotRepository);
            var acLimitations = AMLUtils.GetACLimitations(includedRatings, ratingEditions, validAliases, validCodes, this.nomRepository);
            var limitations = lastEdition.AmlLimitations != null ? AMLUtils.GetLimitations(lastEdition) : new object[0];

            var json = new
            {
                root = new
                {
                    LIC_NO1 = licenceNumber,
                    PERSON_NAME = new
                    {
                        NAME_EN = personNameAlt,
                        NAME = personNameBG
                    },
                    BIRTH = new
                    {
                        DATE = personData.DateOfBirth,
                        PLACE_EN = personData.PlaceOfBirth != null ? personData.PlaceOfBirth.NameAlt : null,
                        PLACE = personData.PlaceOfBirth != null ? personData.PlaceOfBirth.Name : null,
                    },
                    ADDRESS = new
                    {
                        ADDR_EN = string.Format(
                            "{0}, {1}",
                            personAddress.Settlement != null ? personAddress.Settlement.NameAlt : null,
                            personAddress.AddressAlt),
                        ADDR = string.Format(
                            "{0}, {1}",
                            personAddress.Settlement != null ? personAddress.Settlement.Name : null,
                            personAddress.Address),
                    },
                    NATIONALITY = new
                    {
                        NAME = country != null ? country.Code : null,
                        CODE = country != null ? country.Name : null
                    },
                    LIC_NO2 = licenceNumber,
                    NAME = personNameBG,
                    BIRTH1 = string.Format("{0:dd.MM.yyyy} {1}",
                       personData.DateOfBirth,
                       personData.PlaceOfBirth != null ? personData.PlaceOfBirth.Name : null),
                    ADDR = personAddress.Address,
                    NATIONALITY1 = country != null ? country.Name : null,
                    LICNO = licenceNumber,
                    T_ISSUE_DATE = lastEdition.DocumentDateValidFrom,
                    T_VALID_DATE = lastEdition.DocumentDateValidTo,
                    CATEGORY = AMLUtils.GetCategory(includedRatings, ratingEditions, validAliases, validCodes, this.nomRepository, lot),
                    CATEGORY_NP = categoryNP,
                    VALIDITY = lastEdition.DocumentDateValidTo,
                    CAT1 = "A",
                    CAT2 = "B 1",
                    CAT3 = "B 2",
                    CAT4 = "C",
                    CATEGORIES = this.GetCategories(includedRatings, validCodes),
                    IS_DATE = lastEdition.DocumentDateValidFrom,
                    NA = categoryNP.Length == 0 ? "NOT APPLICABLE" : " ",
                    IS_DATE2 = lastEdition.DocumentDateValidFrom,
                    LIC_NO3 = licenceNumber,
                    LIC_NO4 = licenceNumber,
                    LIC_NO41 = licenceNumber,
                    AIRCRAFTS = AMLUtils.GetAircrafts(includedRatings, ratingEditions, lot, this.nomRepository),
                    LIC_NO5 = licenceNumber,
                    NA2 = limitations.Count() == 0 && acLimitations.Count() == 0 ? "No limitations" : "",
                    LIMITATIONS = (limitations.Count() > 0 || acLimitations.Count() > 0) ? limitations : new object(),
                    AC_LIMITATIONS = acLimitations.Count() > 0 ? acLimitations : new object(),
                    VALID_DATE = lastEdition.DocumentDateValidTo,
                    LIC_NO6 = licenceNumber
                }
            };

            return json;
        }

        private object[] GetCategories(IEnumerable<PartVersion<PersonRatingDO>> includedRatings, List<string> validCodes)
        {
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
                       CAT43 = !aircraftClases66.Where(e => e.ParentValueId == group66.NomValueId && e.Code.Contains("C")).Any() ?
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
    }
}

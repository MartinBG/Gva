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
    public class AML_III : IDataGenerator
    {
        private ILotRepository lotRepository;
        private INomRepository nomRepository;

        public AML_III(
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
                return "part66";
            }
        }

        public string GeneratorName
        {
            get
            {
                return "Part 66";
            }
        }

        public object GetData(int lotId, string path)
        {
            var lot = this.lotRepository.GetLotIndex(lotId);
            var personData = lot.Index.GetPart<PersonDataDO>("personData").Content;
            int validTrueId = this.nomRepository.GetNomValue("boolean", "yes").NomValueId;
            var personAddressPart = lot.Index.GetParts<PersonAddressDO>("personAddresses")
               .FirstOrDefault(a => a.Content.ValidId == validTrueId);
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

            var licenceType = this.nomRepository.GetNomValue("licenceTypes", licence.LicenceTypeId.Value);
            
            string licenceCode = licenceType.Code;
            var licenceNumber = string.Format(
                "BG.66.A {0} - {1}",
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

            List<string> validAliases = new List<string> { "A", "B 1", "B 2", "B 3", "C" };
            List<string> validCodes = new List<string> { "1", "2", "3", "4", "5", "9", "10", "11" };
            var categoryNP = AMLUtils.GetCategoryNP(includedRatings, ratingEditions, validAliases, validCodes, this.nomRepository, this.lotRepository);
            var acLimitations = AMLUtils.GetACLimitations(includedRatings, ratingEditions, validAliases, validCodes, this.nomRepository);

            var limitations = lastEdition.AmlLimitations != null ? AMLUtils.GetLimitations(lastEdition, this.nomRepository) : new object[0];
            bool isForeigner = false;

            var country = Utils.GetCountry(personData, this.nomRepository);
            if (country != null)
            {
                isForeigner = country.Code != "BG" && country.Code != "BGR";
            }

            var address = Utils.GetAddress(personAddress, this.nomRepository);
            var placeOfBirth = Utils.GetPlaceOfBirth(personData, nomRepository);

            var json = new
            {
                root = new
                {
                    LIC_NO1 = licenceNumber,
                    PERSON_NAME = new
                    {
                        NAME_EN = personNameAlt,
                        NAME = !isForeigner ? personNameBG : null,
                        BG_LABEL_NAME = !isForeigner ? "Име на притежателя:" : null
                    },
                    BIRTH = new
                    {
                        DATE = personData.DateOfBirth,
                        PLACE_EN = placeOfBirth.Item1,
                        PLACE = !isForeigner && personData.PlaceOfBirth != null ? placeOfBirth.Item2 : null,
                        BG_LABEL_PLACE = !isForeigner ? "Дата и място на раждане:" : null
                    },
                    ADDRESS = new
                    {
                        ADDR_EN = address.Item1,
                        ADDR = !isForeigner ? address.Item2 : null,
                        BG_LABEL_ADDR = !isForeigner ? "Адрес на притежателя:" : null
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
                       placeOfBirth.Item2),
                    ADDR = address.Item2,
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
                    CAT5 = "B 3",
                    CATEGORIES = this.GetCategories(includedRatings, validCodes),
                    IS_DATE = lastEdition.DocumentDateValidFrom,
                    NA = categoryNP.Length == 0 ? "NOT APPLICABLE" : " ",
                    LIC_NO3 = licenceNumber,
                    LIC_NO4 = licenceNumber,
                    LIC_NO41 = licenceNumber,
                    AIRCRAFTS = AMLUtils.GetAircrafts(includedRatings, ratingEditions, lot, this.nomRepository),
                    LIC_NO5 = licenceNumber,
                    NA2 = limitations.Count() == 0 && acLimitations.Count() == 0 ? "No limitations" : "",
                    LIMITATIONS = (limitations.Count() > 0 || acLimitations.Count() > 0) ? limitations :
                        new object[]
                        {
                            new
                            {
                                NAME = "No limitations"
                            }
                        },
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
                    .Where(rating => rating.Content.AircraftTypeCategory != null &&
                        (rating.Content.AircraftTypeCategory.ParentValueId == group66.NomValueId 
                        || (rating.Content.AircraftTypeCategory.Code =="C 1" && group66.Code == "9")
                        || (rating.Content.AircraftTypeCategory.Code == "C 2" && group66.Code == "10")))
                    .Select(rating => rating.Content.AircraftTypeCategory.NomValueId);

                IEnumerable<NomValue> categories = categoriesIds.Select(categoryId => this.nomRepository.GetNomValue("aircraftClases66", categoryId));
                IEnumerable<string> aliases = categories.Select(category => category.TextContent.Get<string>("alias"));

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
                    "n/a" : aliases.Contains("C") ? "C" : "X",
                    CAT5 = !aircraftClases66.Where(e => e.ParentValueId == group66.NomValueId && e.Code.Contains("B 3")).Any() ?
                    "n/a" : aliases.Contains("B 3") ?
                    aircraftClases66.Where(e => e.ParentValueId == group66.NomValueId && e.Code.Contains("B 3")).First().Code : "X",
                });
            }

            return results.ToArray<object>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Regs.Api.Repositories.LotRepositories;

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

        public string[] TemplateNames
        {
            get
            {
                return new string[] { "AML_III" };
            }
        }

        public JObject GetData(int lotId, string path, int index)
        {
            var lot = this.lotRepository.GetLotIndex(lotId);
            var personData = lot.GetPart("personData").Content;
            var personAddressPart = lot.GetParts("personAddresses")
               .FirstOrDefault(a => a.Content.Get<string>("valid.code") == "Y");
             var personAddress = personAddressPart == null ?
                new JObject() :
                personAddressPart.Content;
            var licence = lot.GetPart(path).Content;
            var firstEdition = licence.Get<JObject>("editions[0]");
            var lastEdition = licence.Get<JObject>(string.Format("editions[{0}]", index));
            var includedRatings = lastEdition.GetItems<int>("includedRatings")
                .Select(i => lot.GetPart("ratings/" + i).Content);
            var licenceType = this.nomRepository.GetNomValue("licenceTypes", licence.Get<int>("licenceType.nomValueId"));
            var country = this.GetCountry(personAddress);
            string licenceCode = licence.Get<string>("licenceType.code");
            var licenceNumber = string.Format(
                "{0}.{1} - {2}",
                licenceCode != string.Empty ? licenceCode : "BG",
                licence.Get<string>("licenceNumber"),
                personData.Get<string>("lin"));
            var personNameBG = string.Format("{0} {1} {2}",
                    personData.Get<string>("firstName").ToUpper(),
                    personData.Get<string>("middleName").ToUpper(),
                    personData.Get<string>("lastName").ToUpper());
            var personNameAlt = string.Format("{0} {1} {2}",
                    personData.Get<string>("firstNameAlt").ToUpper(),
                    personData.Get<string>("middleNameAlt").ToUpper(),
                    personData.Get<string>("lastNameAlt").ToUpper());

            var categoryNP = this.GetCategoryNP(includedRatings);

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
                        DATE = personData.Get<DateTime>("dateOfBirth"),
                        PLACE_EN = personData.Get<string>("placeOfBirth.nameAlt"),
                        PLACE = personData.Get<string>("placeOfBirth.name")
                    },
                    ADDRESS = new 
                    {
                        ADDR_EN = string.Format(
                            "{0}, {1}",
                            personAddress.Get<string>("settlement.nameAlt"),
                            personAddress.Get<string>("addressAlt")),
                        ADDR = string.Format(
                            "{0}, {1}",
                            personAddress.Get<string>("settlement.name"),
                            personAddress.Get<string>("address")),
                    },
                    NATIONALITY = new
                    {
                        NAME = country.Name,
                        CODE = country.Code
                    },
                    LIC_NO2 = licenceNumber,
                    NAME = personNameBG,
                    BIRTH1 = string.Format("{0:dd.mm.yyyy} {1}",
                       personData.Get<DateTime>("dateOfBirth"),
                       personData.Get<string>("placeOfBirth.name")),
                    ADDR = personAddress.Get<string>("address"),
                    NATIONALITY1 = country.Name,
                    LICNO = licenceNumber,
                    T_ISSUE_DATE = lastEdition.Get<DateTime>("documentDateValidFrom"),
                    T_VALID_DATE = lastEdition.Get<DateTime>("documentDateValidTo"),
                    CATEGORY = this.GetCategory(includedRatings),
                    CATEGORY_NP = categoryNP,
                    VALIDITY = lastEdition.Get<DateTime>("documentDateValidTo"),
                    CAT1 = "A",
                    CAT2 = "B 1",
                    CAT3 = "B 2",
                    CAT4 = "B 3",
                    CAT5 = "C",
                    CATEGORIES = this.GetCategories(includedRatings),
                    IS_DATE = lastEdition.Get<DateTime>("documentDateValidFrom"),
                    NA = categoryNP.Length == 0 ? "NOT APPLICABLE" : " ",
                    IS_DATE2 = lastEdition.Get<DateTime>("documentDateValidFrom"),
                    LIC_NO3 = licenceNumber,
                    LIC_NO4 = licenceNumber,
                    LIC_NO41 = licenceNumber,
                    AIRCRAFTS = this.GetAircrafts(includedRatings),
                    LIC_NO5 = licenceNumber,
                    LIMITATIONS = this.GetLimitations(lastEdition),
                    AC_LIMITATIONS = this.GetACLimitations(includedRatings),
                    VALID_DATE = lastEdition.Get<DateTime>("documentDateValidTo"),
                    LIC_NO6 = licenceNumber
                }
            };

            return JObject.FromObject(json);
        }

        private NomValue GetCountry(JObject personAddress)
        {
            int? countryId = personAddress.Get<int?>("settlement.parentValueId");
            NomValue country = countryId.HasValue ?
                this.nomRepository.GetNomValue("countries", countryId.Value) :
                new NomValue
                {
                    Name = null,
                    TextContentString = string.Empty
                };

            return country;
        }

        private object[] GetAircrafts(IEnumerable<JObject> includedRatings)
        {
            return includedRatings
                .Where(r => r.GetItems<JObject>("editions").Last().GetItems("limitations").Count() == 0)
                .Select(r =>
                {
                    JObject lastEdition = r.GetItems<JObject>("editions").Last();
                    dynamic date =  "";
                    if(lastEdition.Get<string>("aircraftTypeGroup.name") != "No type")
                    {
                        date = lastEdition.Get<DateTime>("documentDateValidFrom");
                    }

                    return new
                    {
                        AC_TYPE = r.Get<string>("aircraftTypeGroup.name"),
	                    CATEGORY = r.Get<string>("aircraftTypeCategory.code"),
	                    DATE = date
                    };
                }).ToArray<object>();
        }

        private object[] GetCategories(IEnumerable<JObject> includedRatings)
        {
            List<string> validCodes = new List<string> { "1", "2", "3", "4", "5", "9", "10", "11" };
            IEnumerable<NomValue> aircraftGroups66 = this.nomRepository.GetNomValues("aircraftGroup66").Where(r => validCodes.Contains(r.Code));

            List<object> results = new List<object>();
            foreach (var group66 in aircraftGroups66)
            {
                IEnumerable<int> categoriesIds = includedRatings
                    .Where(rating => rating.Get<int>("aircraftTypeCategory.parentValueId") == group66.NomValueId)
                    .Select(rating => rating.Get<int>("aircraftTypeCategory.nomValueId"));

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
                    CAT43 = aliases.Contains("B 3") ? "X" : "n/a",
                    CAT5 = aliases.Contains("C") ? "X" : "n/a"
                });
            }
            return results.ToArray<object>();
        }

        private object[] GetCategory(IEnumerable<JObject> includedRatings)
        {
            List<string> validAliases = new List<string> { "A", "B 1", "B 2", "B 3", "C" };
            List<string> validCodes = new List<string> { "1", "2", "3", "4", "5", "9", "10", "11" };

            return includedRatings
                .Where(r => r.Get<JObject>("aircraftTypeGroup") != null && r.Get<JObject>("aircraftTypeCategory") != null &&
                    validCodes.Contains(this.nomRepository.GetNomValue("aircraftGroup66", r.Get<int>("aircraftTypeCategory.parentValueId")).Code) && 
                    validAliases.Contains(this.nomRepository.GetNomValue("aircraftClases66", r.Get<int>("aircraftTypeCategory.nomValueId")).TextContent.Get<string>("alias")))
                .Select(r => new{
                    TYPE = r.Get<string>("aircraftTypeGroup.name"),
                    CAT = r.Get<string>("aircraftTypeCategory.code"),
                    DATE = r.GetItems<JObject>("editions").Last().Get<DateTime>("documentDateValidFrom"),
                    LIMIT = r.GetItems<JObject>("editions").Last().GetItems("limitations").Count() > 0 ?
                    string.Join(",", r.GetItems<JObject>("editions").Last().GetItems("limitations").Select(l => l.Get<string>("name"))) : "NP"
                }).ToArray<object>();
        }

        private object[] GetCategoryNP(IEnumerable<JObject> includedRatings)
        {
            List<string> validAliases = new List<string> { "A", "B 1", "B 2", "B 3", "C" };
            List<string> validCodes = new List<string> { "1", "2", "3", "4", "5", "9", "10", "11" };

            return includedRatings
                 .Where(r => r.Get<JObject>("aircraftTypeGroup") != null && r.Get<JObject>("aircraftTypeCategory") != null &&
                    validCodes.Contains(this.nomRepository.GetNomValue("aircraftGroup66", r.Get<int>("aircraftTypeCategory.parentValueId")).Code) &&
                    validAliases.Contains(this.nomRepository.GetNomValue("aircraftClases66", r.Get<int>("aircraftTypeCategory.nomValueId")).TextContent.Get<string>("alias")))
                .Select(r => new
                {
                    TYPE = r.Get<string>("aircraftTypeGroup.name"),
                    CAT = r.Get<string>("aircraftTypeCategory.code"),
                    DATE_FROM = r.GetItems<JObject>("editions").Last().Get<DateTime>("documentDateValidFrom"),
                    DATE_TO = r.GetItems<JObject>("editions").Last().Get<DateTime>("documentDateValidTo"),
                    LIMIT = r.GetItems<JObject>("editions").Last().GetItems("limitations").Count() > 0 ?
                    string.Join(",", r.GetItems<JObject>("editions").Last().GetItems("limitations").Select(l => l.Get<string>("name"))) : "NP"
                }).ToArray<object>();
        }

        private object[] GetACLimitations(IEnumerable<JObject> includedRatings)
        {
            List<string> validAliases = new List<string> { "A", "B 1", "B 2", "B 3", "C" };
            List<string> validCodes = new List<string> { "1", "2", "3", "4", "5", "9", "10", "11" };

            return includedRatings
                 .Where(r => r.Get<JObject>("aircraftTypeGroup") != null && r.Get<JObject>("aircraftTypeCategory") != null &&
                    validCodes.Contains(this.nomRepository.GetNomValue("aircraftGroup66", r.Get<int>("aircraftTypeCategory.parentValueId")).Code) &&
                    validAliases.Contains(this.nomRepository.GetNomValue("aircraftClases66", r.Get<int>("aircraftTypeCategory.nomValueId")).TextContent.Get<string>("alias")))
                .Where(r => r.Get<JObject>("aircraftTypeGroup") != null)
                .Select(r => new
                {
                    AIRCRAFT = r.Get<string>("aircraftTypeGroup.name"),
                    CAT = r.Get<string>("aircraftTypeCategory.code"),
                    LIM = r.GetItems<JObject>("editions").Last().GetItems("limitations").Count() > 0 ?
                    string.Join(",", r.GetItems<JObject>("editions").Last().GetItems("limitations").Select(l => l.Get<string>("name"))) : "NP"
                }).ToArray<object>();
        }

        private object[] GetLimitations(JObject lastLicenceEdition)
        {
            IList<object> limitations= new List<object>();

            IEnumerable<JObject> AT_a_Ids = lastLicenceEdition.GetItems<JObject>("AMLlimitations.AT_a_Ids");
            IEnumerable<JObject> AT_b1_Ids = lastLicenceEdition.GetItems<JObject>("AMLlimitations.AT_b1_Ids");
            IEnumerable<JObject> AP_a_Ids = lastLicenceEdition.GetItems<JObject>("AMLlimitations.AP_a_Ids");
            IEnumerable<JObject> AP_b1_Ids = lastLicenceEdition.GetItems<JObject>("AMLlimitations.AP_b1_Ids");
            IEnumerable<JObject> HT_a_Ids = lastLicenceEdition.GetItems<JObject>("AMLlimitations.HT_a_Ids");
            IEnumerable<JObject> HT_b1_Ids = lastLicenceEdition.GetItems<JObject>("AMLlimitations.HT_b1_Ids");
            IEnumerable<JObject> HP_a_Ids = lastLicenceEdition.GetItems<JObject>("AMLlimitations.HP_a_Ids");
            IEnumerable<JObject> HP_b1_Ids = lastLicenceEdition.GetItems<JObject>("AMLlimitations.HP_b1_Ids");
            IEnumerable<JObject> avionics_Ids = lastLicenceEdition.GetItems<JObject>("AMLlimitations.avionics_Ids");

            limitations.Add(new
            {
                NAME = "Aeroplanes Turbine",
                CAT = "A 1",
                LIMT = AT_a_Ids.Count() > 0 ? string.Join(",", AT_a_Ids.Select(l => l.Get<string>("name")).ToList()) : "No limitation"
            });
           
            limitations.Add(new
            {
                NAME = "Aeroplanes Turbine",
                CAT = "B 1.1",
                LIMT = AT_b1_Ids.Count() > 0 ? string.Join(",", AT_b1_Ids.Select(l => l.Get<string>("name")).ToList()) : "No limitation"
            });

            limitations.Add(new
            {
                NAME = "Aeroplanes Piston",
                CAT = "A 2",
                LIMT = AP_a_Ids.Count() > 0 ? string.Join(",", AP_a_Ids.Select(l => l.Get<string>("name")).ToList()) : "No limitation"
            });

            limitations.Add(new
            {
                NAME = "Aeroplanes Piston",
                CAT = "B 1.2",
                LIMT = AP_b1_Ids.Count() > 0 ? string.Join(",", AP_b1_Ids.Select(l => l.Get<string>("name")).ToList()) : "No limitation"
            });

            limitations.Add(new
            {
                NAME = "Helicopters Turbine",
                CAT = "A 3",
                LIMT = HT_a_Ids.Count() > 0 ? string.Join(",", HT_a_Ids.Select(l => l.Get<string>("name")).ToList()) : "No limitation"
            });

            limitations.Add(new
            {
                NAME = "Helicopters Turbine",
                CAT = "B 1.3",
                LIMT = HT_b1_Ids.Count() > 0 ? string.Join(",", HT_b1_Ids.Select(l => l.Get<string>("name")).ToList()) : "No limitation"
            });

            limitations.Add(new
            {
                NAME = "Helicopters Piston",
                CAT = "A 4",
                LIMT = HP_a_Ids.Count() > 0 ? string.Join(",", HP_a_Ids.Select(l => l.Get<string>("name")).ToList()) : "No limitation"
            });

            limitations.Add(new
            {
                NAME = "Helicopters Piston",
                CAT = "B 1.4",
                LIMT = HP_b1_Ids.Count() > 0 ? string.Join(",", HP_b1_Ids.Select(l => l.Get<string>("name")).ToList()) : "No limitation"
            });
            limitations.Add(new
            {
                NAME = "Acionics",
                CAT = "B 2",
                LIMT = avionics_Ids.Count() > 0 ? string.Join(",", avionics_Ids.Select(l => l.Get<string>("name")).ToList()) : "No limitation"
            });

            return limitations.ToArray<object>();
        }
    }
}

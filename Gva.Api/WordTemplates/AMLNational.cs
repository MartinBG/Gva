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

        public object GetData(int lotId, string path, int index)
        {
            var lot = this.lotRepository.GetLotIndex(lotId);
            var personData = lot.Index.GetPart("personData").Content;
            var personAddressPart = lot.Index.GetParts("personAddresses")
               .FirstOrDefault(a => a.Content.Get<string>("valid.code") == "Y");
             var personAddress = personAddressPart == null ?
                new JObject() :
                personAddressPart.Content;
            var licence = lot.Index.GetPart(path).Content;
            var firstEdition = licence.Get<JObject>("editions[0]");
            var lastEdition = licence.Get<JObject>(string.Format("editions[{0}]", index));
            var includedRatings = lastEdition.GetItems<int>("includedRatings")
                .Select(i => lot.Index.GetPart("ratings/" + i).Content);
            var licenceType = this.nomRepository.GetNomValue("licenceTypes", licence.Get<int>("licenceType.nomValueId"));
            var country = this.GetCountry(personAddress);
            var licenceNumber = string.Format(
                "BG {0} - {1} - {2}",
                licence.Get<string>("licenceType.code"),
                licence.Get<string>("licenceNumber"),
                personData.Get<string>("lin"));
            var personName = string.Format("{0} {1} {2}",
                    personData.Get<string>("firstName").ToUpper(),
                    personData.Get<string>("middleName").ToUpper(),
                    personData.Get<string>("lastName").ToUpper());
            var json = new
            {
                root = new
                {
                    LICENCE_TYPE = licenceType.Code,
                    LICENCE_NO = licenceNumber,
                    PERSON = personName,
                    PERSON_EN = string.Format("{0} {1} {2}",
                        personData.Get<string>("firstNameAlt").ToUpper(),
                        personData.Get<string>("middleNameAlt").ToUpper(),
                        personData.Get<string>("lastNameAlt").ToUpper()),
                    DATE_OF_BIRTH = personData.Get<DateTime>("dateOfBirth"),
                    SEX = personData.Get<string>("sex.name"),
                    ADDRESS = string.Format(
                        "{0}, {1}",
                        personAddress.Get<string>("settlement.name"),
                        personAddress.Get<string>("address")),
                    NATIONALITY = country.Name,
                    NATIONALITY_EN = country.Code,
                    ISSUE_DATE = lastEdition.Get<DateTime>("documentDateValidFrom"),
                    FIRST_ISSUE_DATE = firstEdition.Get<DateTime>("documentDateValidFrom"),
                    LIC_NO2 = licenceNumber,
                    CAT1 = "A", 
                    CAT2 = "B 1",
                    CAT3 = "B 2",
                    CAT4 = "C",
                    CATEGORIES = this.GetCategories(includedRatings),
                    LIC_NO3 = licenceNumber,
                    NAME = personName,
                    DATE_OF_BIRTH1 = string.Format("{0:dd.mm.yyyy} {1}",
                        personData.Get<DateTime>("dateOfBirth"),
                        personData.Get<string>("placeOfBirth.name")),
                    ADDR =  personAddress.Get<string>("address"),
                    NATIONALITY1 = country.Name,
                    LICNO = licenceNumber,
                    T_ISSUE_DATE = lastEdition.Get<DateTime>("documentDateValidFrom"),
                    T_VALID_DATE = lastEdition.Get<DateTime>("documentDateValidTo"),
                    CATEGORY = this.GetCategory(includedRatings),
                    LIMITATIONS = this.GetLimitations(lastEdition),
                    AC_LIMITATIONS = this.GetACLimitations(includedRatings),
                    VALID_DATE = lastEdition.Get<DateTime>("documentDateValidTo"),
                    LIC_NO4 = licenceNumber
                }
            };

            return json;
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
        
        private object[] GetCategories(IEnumerable<JObject> includedRatings)
        {
            List<string> validCodes = new List<string> { "1", "2", "3", "4", "5", "6" };
            IEnumerable<NomValue> aircraftGroups66 = this.nomRepository.GetNomValues("aircraftGroup66").Where(r => validCodes.Contains(r.Code));

            List<object> results = new List<object>();
            foreach (var group66 in aircraftGroups66)
            {
                IEnumerable<int> categoriesIds = includedRatings
                    .Where(rating => rating.Get<int>("aircraftTypeCategory.parentValueId") == group66.NomValueId)
                    .Select(rating => rating.Get<int>("aircraftTypeCategory.nomValueId"));

                IEnumerable<NomValue> categories = categoriesIds.Select(categoryId => this.nomRepository.GetNomValue("aircraftClases66", categoryId));
                IEnumerable<string> aliases =   categories.Select(category => category.TextContent.Get<string>("alias"));

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

        private object[] GetCategory(IEnumerable<JObject> includedRatings)
        {
            List<string> validAliases = new List<string> { "A", "B 1", "B 2", "C" };
            List<string> validCodes = new List<string> { "1", "2", "3", "4", "5", "6" };

            return includedRatings
                .Where(r => r.Get<JObject>("aircraftTypeGroup") != null && r.Get<JObject>("aircraftTypeCategory") != null &&
                    validCodes.Contains(this.nomRepository.GetNomValue("aircraftGroup66", r.Get<int>("aircraftTypeCategory.parentValueId")).Code) && 
                    validAliases.Contains(this.nomRepository.GetNomValue("aircraftClases66", r.Get<int>("aircraftTypeCategory.nomValueId")).TextContent.Get<string>("alias")))
                .Select(r => new
                {
                    TYPE = r.Get<string>("aircraftTypeGroup.name"),
                    CAT = r.Get<string>("aircraftTypeCategory.code"),
                    DATE = r.GetItems<JObject>("editions").Last().Get<DateTime>("documentDateValidFrom"),
                    LIMIT = r.GetItems<JObject>("editions").Last().GetItems("limitations").Count() > 0 ?
                    string.Join(",", r.GetItems<JObject>("editions").Last().GetItems("limitations").Select(l => l.Get<string>("name"))) : "NP"
                }).ToArray<object>();
        }

        private object[] GetACLimitations(IEnumerable<JObject> includedRatings)
        {
            List<string> validAliases = new List<string> { "A", "B 1", "B 2", "C" };
            List<string> validCode = new List<string> { "1", "2", "3", "4", "5", "6"};

            return includedRatings
                .Where(r => r.Get<JObject>("aircraftTypeGroup") != null && r.Get<JObject>("aircraftTypeCategory") != null &&
                    validCode.Contains(this.nomRepository.GetNomValue("aircraftGroup66", r.Get<int>("aircraftTypeCategory.parentValueId")).Code) && 
                    validAliases.Contains(this.nomRepository.GetNomValue("aircraftClases66", r.Get<int>("aircraftTypeCategory.nomValueId")).TextContent.Get<string>("alias")))
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

            return limitations.ToArray<object>(); ;
        }
    }
}

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

        public JObject GetData(int lotId, string path, int index)
        {
            var lot = this.lotRepository.GetLotIndex(lotId);
            var personData = lot.GetPart("personData").Content;
            var personAddressPart = lot.GetParts("personAddresses")
               .FirstOrDefault(a => a.Content.Get<string>("valid.code") == "Y");
             var personAddress = personAddressPart == null ?
                new JObject() :
                personAddressPart.Content;
            var part = lot.GetPart(path);
            var firstEdition = part.Content.Get<JObject>(string.Format("editions[0]"));
            var lastEdition = part.Content.Get<JObject>(string.Format("editions[{0}]", index));
            var includedRatings = lastEdition.GetItems<int>("includedRatings")
                .Select(i => lot.GetPart("ratings/" + i).Content);
            var licenceType = this.nomRepository.GetNomValue("licenceTypes", part.Content.Get<int>("licenceType.nomValueId"));
            var country = this.GetCountry(personAddress);
            var licenceNumber = string.Format(
                "BGR. {0} - {1} - {2}",
                part.Content.Get<string>("licenceType.code"),
                part.Content.Get<string>("licenceNumber"),
                personData.Get<string>("lin"));
            var personName = string.Format("{0} {1} {2}",
                    personData.Get<string>("firstName"),
                    personData.Get<string>("middleName"),
                    personData.Get<string>("lastName"));
            var json = new
            {
                root = new
                {
                    LICENCE_TYPE = licenceType.Code,
                    LICENCE_NO = licenceNumber,
                    PERSON = personName,
                    PERSON_EN = string.Format("{0} {1} {2}",
                    personData.Get<string>("firstNameAlt"),
                    personData.Get<string>("middleNameAlt"),
                    personData.Get<string>("lastNameAlt")),
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
                    CAT4 = "B 3",
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
                    TextContent = string.Empty
                };

            return country;
        }
        
        private List<object> GetCategories(IEnumerable<JObject> includedRatings)
        {
            List<NomValue> aircraftGroup66 = new List<NomValue>();
            foreach (var rating in includedRatings)
            {
               NomValue group66 = this.nomRepository.GetNomValue("aircraftGroup66", rating.Get<int>("aircraftTypeCategory.parentValueId"));
               if (group66 != null && !aircraftGroup66.Contains(group66) && group66.Code != "7")
               {
                   aircraftGroup66.Add(group66);
               }
            }

            List<object> results = new List<object>();
            foreach (var group66 in aircraftGroup66)
            {
                IEnumerable<int> categoriesIds = includedRatings
                    .Where(rating => rating.Get<int>("aircraftTypeCategory.parentValueId") == group66.NomValueId)
                    .Select(rating => rating.Get<int>("aircraftTypeCategory.nomValueId"));

                IEnumerable<NomValue> categories = categoriesIds.Select(categoryId => this.nomRepository.GetNomValue("aircraftClases66", categoryId));
                IEnumerable<string> aliases =   categories.Select(category => JsonConvert.DeserializeObject<JObject>(category.TextContent).Get<string>("alias"));

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
                        CAT4 = aliases.Contains("B 3") ? "X" : "n/a"
                    });
            }
            return results;
        }

        private IEnumerable<object> GetCategory(IEnumerable<JObject> includedRatings)
        {
            List<string> validAliases = new List<string> { "A", "B 1", "B 2", "C" };
            return includedRatings
                .Where(r => r.Get<JObject>("aircraftTypeGroup") != null &&
                    this.nomRepository.GetNomValue("aircraftGroup66", r.Get<int>("aircraftTypeCategory.parentValueId")).Code != "7" && 
                    validAliases.Contains(
                    JsonConvert.DeserializeObject<JObject>(
                    this.nomRepository.GetNomValue("aircraftClases66", r.Get<int>("aircraftTypeCategory.nomValueId"))
                    .TextContent).Get<string>("alias")))
                .Select(r => new{
                    TYPE = r.Get<string>("aircraftTypeGroup.name"),
                    CAT = r.Get<string>("aircraftTypeCategory.code"),
                    DATE = r.GetItems<JObject>("editions").Last().Get<DateTime>("documentDateValidFrom"),
                    LIMIT = r.GetItems<JObject>("editions").Last().GetItems("limitations").Count() > 0 ?
                    string.Join(",", r.GetItems<JObject>("editions").Last().GetItems("limitations").Select(l => l.Get<string>("name"))) : "NP"
                });
        }

        private IEnumerable<object> GetACLimitations(IEnumerable<JObject> includedRatings)
        {
            List<string> validAliases = new List<string> { "A", "B 1", "B 2", "C" };
            List<string> validCode = new List<string> { "1", "2", "3", "4", "5", "6"};

            return includedRatings
                .Where(r => r.Get<JObject>("aircraftTypeGroup") != null &&
                    validCode.Contains(this.nomRepository.GetNomValue("aircraftGroup66", r.Get<int>("aircraftTypeCategory.parentValueId")).Code) && 
                    validAliases.Contains(
                    JsonConvert.DeserializeObject<JObject>(
                    this.nomRepository.GetNomValue("aircraftClases66", r.Get<int>("aircraftTypeCategory.nomValueId")).TextContent)
                    .Get<string>("alias")))
                .Select(r => new
                {
                    AIRCRAFT = r.Get<string>("aircraftTypeGroup.name"),
                    CAT = r.Get<string>("aircraftTypeCategory.code"),
                    LIM = r.GetItems<JObject>("editions").Last().GetItems("limitations").Count() > 0 ?
                    string.Join(",", r.GetItems<JObject>("editions").Last().GetItems("limitations").Select(l => l.Get<string>("name"))) : "NP"
                });
        }

        private IEnumerable<object> GetLimitations(JObject lastLicenceEdition)
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
            if (AT_a_Ids.Count() > 0)
            {
                limitations.Add(new
                {
                    NAME = "Aeroplanes Turbine",
                    CAT = "A 1",
                    LIMT = string.Join(",", AT_a_Ids.Select(l => l.Get<string>("name")).ToList())
                });
            }
            if (AT_b1_Ids.Count() > 0)
            {
                limitations.Add(new
                {
                    NAME = "Aeroplanes Turbine",
                    CAT = "B 1.1",
                    LIMT = string.Join(",", AT_b1_Ids.Select(l => l.Get<string>("name")).ToList())
                });
            }
            if (AP_a_Ids.Count() > 0)
            {
                limitations.Add(new
                {
                    NAME = "Aeroplanes Piston",
                    CAT = "A 2",
                    LIMT = string.Join(",", AP_a_Ids.Select(l => l.Get<string>("name")).ToList())
                });
            }
            if (AP_b1_Ids.Count() > 0)
            {
                limitations.Add(new
                {
                    NAME = "Aeroplanes Piston",
                    CAT = "B 1.2",
                    LIMT = string.Join(",", AP_b1_Ids.Select(l => l.Get<string>("name")).ToList())
                });
            }
            if (HT_a_Ids.Count() > 0)
            {
                limitations.Add(new
                {
                    NAME = "Helicopters Turbine",
                    CAT = "A 3",
                    LIMT = string.Join(",", HT_a_Ids.Select(l => l.Get<string>("name")).ToList())
                });
            }
            if (HT_b1_Ids.Count() > 0)
            {
                limitations.Add(new
                {
                    NAME = "Helicopters Turbine",
                    CAT = "B 1.3",
                    LIMT = string.Join(",", HT_b1_Ids.Select(l => l.Get<string>("name")).ToList())
                });
            }
            if (HP_a_Ids.Count() > 0)
            {
                limitations.Add(new
                {
                    NAME = "Helicopters Piston",
                    CAT = "A 4",
                    LIMT = string.Join(",", HP_a_Ids.Select(l => l.Get<string>("name")).ToList())
                });
            }
            if (HP_b1_Ids.Count() > 0)
            {
                limitations.Add(new
                {
                    NAME = "Helicopters Piston",
                    CAT = "B 1.4",
                    LIMT = string.Join(",", HP_b1_Ids.Select(l => l.Get<string>("name")).ToList())
                });
            }
            if (avionics_Ids.Count() > 0)
            {
                limitations.Add(new
                {
                    NAME = "Acionics",
                    CAT = "B 2",
                    LIMT = string.Join(",", avionics_Ids.Select(l => l.Get<string>("name")).ToList())
                });
            }

            return limitations;
        }
    }
}

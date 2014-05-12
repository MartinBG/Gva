using System;
using System.Collections.Generic;
using System.Linq;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.WordTemplates
{
    public class CAL03year2013 : IDataGenerator
    {
        private ILotRepository lotRepository;
        private INomRepository nomRepository;

        public CAL03year2013(
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
                return new string[] { "CAL03_2013" };
            }
        }

        public JObject GetData(int lotId, string path, int index)
        {
            var lot = this.lotRepository.GetLotIndex(lotId);
            var personData = lot.GetPart("personData").Content;
            var licence = lot.GetPart(path).Content;
            var edition = licence.Get<JObject>(string.Format("editions[{0}]", index));

            var includedRatings = edition.GetItems<int>("includedRatings")
                .Select(i => lot.GetPart("ratings/" + i).Content);
    
            var licenceType = this.nomRepository.GetNomValue("licenceTypes", licence.Get<int>("licenceType.nomValueId"));

            var ratings = this.GetRatings(includedRatings);
            var placeOfBirth = personData.Get<NomValue>("placeOfBirth");
            var country = this.nomRepository.GetNomValue("countries", placeOfBirth.ParentValueId.Value);
            var nationality = this.nomRepository.GetNomValue("countries", personData.Get<int>("country.nomValueId"));

            var refNumber = string.Format(
                "BG {0} - {1} - {2}",
                licenceType.Code,
                licence.Get<string>("licenceNumber"),
                personData.Get<string>("lin"));
            var personName = string.Format("{0} {1} {2}",
                    personData.Get<string>("firstName"),
                    personData.Get<string>("middleName"),
                    personData.Get<string>("lastName"));
            var personNameAlt = string.Format("{0} {1} {2}",
                    personData.Get<string>("firstNameAlt"),
                    personData.Get<string>("middleNameAlt"),
                    personData.Get<string>("lastNameAlt"));

            var json = new
            {
                root = new
                {
                    REF_NO1 = refNumber,
                    PERSON_NAME1 = personName,
                    PERSON_NAME_TRANS1 = personNameAlt,
                    BIRTHDATE1 = personData.Get<DateTime>("dateOfBirth"),
                    BIRTHCOUNTRY1 = country.Name,
                    BIRTHCOUNTRY_TRANS1 = country.NameAlt,
                    BIRTHPLACE1 = placeOfBirth.Name,
                    BIRTHPLACE_TRANS1 = placeOfBirth.NameAlt,
                    NATIONALITY1 = nationality.Name,
                    NATIONALITY_CODE1 =JObject.Parse(nationality.TextContent).Get<string>("nationalityCodeCA"),
                    ISSUE_DATE1 = edition.Get<DateTime>("documentDateValidFrom"),
                    REF_NO2 = refNumber,
                    PERSON_NAME2 = personName,
                    PERSON_NAME_TRANS2 = personNameAlt,
                    BIRTHDATE2 = personData.Get<DateTime>("dateOfBirth"),
                    BIRTHCOUNTRY2 = country.Name,
                    BIRTHCOUNTRY_TRANS2 = country.NameAlt,
                    BIRTHPLACE2 = placeOfBirth.Name,
                    BIRTHPLACE_TRANS2 = placeOfBirth.NameAlt,
                    NATIONALITY2 = nationality.Name,
                    NATIONALITY_CODE2 = JObject.Parse(nationality.TextContent).Get<string>("nationalityCodeCA"),
                    ISSUE_DATE2 = edition.Get<DateTime>("documentDateValidFrom"),
                    REF_NO21 = refNumber,
                    REF_NO22 = refNumber,
                    T_RATING1 = ratings,
                    T_RATING2 = ratings,
                    REF_NO3 = refNumber,
                    REF_NO4 = refNumber,
                    REF_NO5 = refNumber,
                    REF_NO6 = refNumber,
                }
            };

            return JObject.FromObject(json);
        }

        private object[] GetRatings(IEnumerable<JObject> includedRatings)
        {
            return includedRatings
                .Select(r =>
                {
                    JObject lastEdition = r.GetItems<JObject>("editions").Last();
                    var ratingType = r.Get<NomValue>("ratingType");
                    var ratingClass = r.Get<NomValue>("ratingClass");
                    var authorization = r.Get<NomValue>("authorization");

                    return new
                    {
                        CLASS_AUTH = ratingClass == null && ratingType == null ?
                            authorization.Name :
                            string.Format(
                                "{0} {1} {2}",
                                ratingType == null ? string.Empty : ratingType.Name,
                                ratingClass == null ? string.Empty : ratingClass.Name,
                                authorization == null ? string.Empty : "/ " + authorization.Name).Trim(),
                        ISSUE_DATE = lastEdition.Get<DateTime>("documentDateValidFrom"),
                        VALID_DATE = lastEdition.Get<DateTime>("documentDateValidTo")
                    };
                }).ToArray<object>();
        }

    }
}

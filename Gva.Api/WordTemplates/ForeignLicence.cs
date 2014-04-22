using System;
using System.Collections.Generic;
using System.Linq;
using Common.Api.Repositories.NomRepository;
using Common.Json;
using Newtonsoft.Json.Linq;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.WordTemplates
{
    public class ForeignLicence : IDataGenerator
    {
        private ILotRepository lotRepository;

        public ForeignLicence(ILotRepository lotRepository)
        {
            this.lotRepository = lotRepository;
        }

        public string[] TemplateNames
        {
            get
            {
                return new string[] { "foreign_licence" };
            }
        }

        public JObject GetData(int lotId, string path, int index)
        {
            var lot = this.lotRepository.GetLotIndex(lotId);
            var personData = lot.GetPart("personData").Content;
            var personEmplPart = lot.GetParts("personDocumentEmployments")
                .FirstOrDefault(a => a.Content.Get<string>("valid.code") == "Y");
            var personEmployment = personEmplPart == null ?
                new JObject() :
                personEmplPart.Content;
            var licence = lot.GetPart(path).Content;
            var edition = licence.Get<JObject>(string.Format("editions[{0}]", index));

            var includedRatings = edition.GetItems<int>("includedRatings")
                .Select(i => lot.GetPart("ratings/" + i).Content);

            dynamic licenceHolder = this.GetLicenceHolder(personData);
            string occupation = this.GetOccupation(includedRatings);

            var json = new
            {
                root = new
                {
                    LICENCE_NO = licence.Get<string>("licenceNumber"),
                    FOREIGN_LICENCE_NO = licence.Get<string>("foreignLicenceNumber"),
                    LICENCE_HOLDER = licenceHolder,
                    COMPANY = personEmployment.Get<string>("organization.nameAlt"),
                    OCCUPATION = occupation,
                    COUNTRY = personData.Get<string>("country.nameAlt"),
                    VALID_DATE = edition.Get<DateTime>("documentDateValidTo"),
                    ISSUE_DATE = edition.Get<DateTime>("documentDateValidFrom"),
                    D_LICENCE_NO = licence.Get<string>("licenceNumber"),
                    D_LICENCE_HOLDER = new
                    {
                        D_NAME = licenceHolder.NAME,
                        D_FAMILY = licenceHolder.FAMILY
                    },
                    D_COMPANY = personEmployment.Get<string>("organization.nameAlt"),
                    D_OCCUPATION = occupation,
                    D_COUNTRY = personData.Get<string>("country.nameAlt"),
                    D_VALID_DATE = edition.Get<DateTime>("documentDateValidTo"),
                    D_ISSUE_DATE = edition.Get<DateTime>("documentDateValidFrom")
                }
            };

            return JObject.FromObject(json);
        }

        private object GetLicenceHolder(JObject personData)
        {
            return new
            {
                NAME = string.Format(
                    "{0} {1}",
                    personData.Get<string>("firstNameAlt"),
                    personData.Get<string>("middleNameAlt")
                ).ToUpper(),
                FAMILY = personData.Get<string>("lastNameAlt").ToUpper()
            };
        }

        private string GetOccupation(IEnumerable<JObject> includedRatings)
        {
            var resultArr = includedRatings.Select(r =>
                {
                    var ratingType = r.Get<string>("ratingType.name");
                    var ratingClass = r.Get<string>("ratingClass.name");
                    var authorization = r.Get<string>("authorization.name");

                    return string.Format(
                        "{0} {1} {2}",
                        ratingClass,
                        ratingType,
                        authorization == null ?
                            string.Empty :
                            ratingType == null && ratingClass == null ? authorization : ", " + authorization
                    ).Trim();
                }).ToArray<string>();

            return string.Join("   /   ", resultArr);
        }
    }
}

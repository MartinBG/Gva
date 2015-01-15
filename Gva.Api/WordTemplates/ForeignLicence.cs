using System.Collections.Generic;
using System.Linq;
using Gva.Api.ModelsDO.Persons;
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

        public object GetData(int lotId, string path)
        {
            var lot = this.lotRepository.GetLotIndex(lotId);
            var personData = lot.Index.GetPart<PersonDataDO>("personData").Content;
            var personEmplPart = lot.Index.GetParts<PersonEmploymentDO>("personDocumentEmployments")
                .FirstOrDefault(a => a.Content.Valid.Code == "Y");
            var personEmploymentOrg = personEmplPart == null || personEmplPart.Content.Organization == null ?
                null :
                personEmplPart.Content.Organization.NameAlt;

            var licencePart = lot.Index.GetPart<PersonLicenceDO>(path);
            var licence = licencePart.Content;
            var lastEdition = lot.Index.GetParts<PersonLicenceEditionDO>("licenceEditions")
                .Where(e => e.Content.LicencePartIndex == licencePart.Part.Index)
                .OrderBy(e => e.Content.Index)
                .Last()
                .Content;

            var includedRatings = lastEdition.IncludedRatings
                .Select(i => lot.Index.GetPart<PersonRatingDO>("ratings/" + i.Ind).Content);

            dynamic licenceHolder = this.GetLicenceHolder(personData);
            string occupation = this.GetOccupation(includedRatings);

            var json = new
            {
                root = new
                {
                    LICENCE_NO = Utils.PadLicenceNumber(licence.LicenceNumber),
                    FOREIGN_LICENCE_NO = licence.ForeignLicenceNumber,
                    LICENCE_HOLDER = licenceHolder,
                    COMPANY = personEmploymentOrg,
                    OCCUPATION = occupation,
                    COUNTRY = personData.Country.NameAlt,
                    VALID_DATE = lastEdition.DocumentDateValidTo,
                    ISSUE_DATE = lastEdition.DocumentDateValidFrom,
                    D_LICENCE_NO = Utils.PadLicenceNumber(licence.LicenceNumber),
                    D_LICENCE_HOLDER = new
                    {
                        D_NAME = licenceHolder.NAME,
                        D_FAMILY = licenceHolder.FAMILY
                    },
                    D_COMPANY = personEmploymentOrg,
                    D_OCCUPATION = occupation,
                    D_COUNTRY = personData.Country.NameAlt,
                    D_VALID_DATE = lastEdition.DocumentDateValidTo,
                    D_ISSUE_DATE = lastEdition.DocumentDateValidFrom
                }
            };

            return json;
        }

        private object GetLicenceHolder(PersonDataDO personData)
        {
            return new
            {
                NAME = string.Format(
                    "{0} {1}",
                    personData.FirstNameAlt,
                    personData.MiddleNameAlt
                ).ToUpper(),
                FAMILY = personData.LastNameAlt.ToUpper()
            };
        }

        private string GetOccupation(IEnumerable<PersonRatingDO> includedRatings)
        {
            var resultArr = includedRatings.Select(r =>
                {
                    var ratingType = r.RatingType == null ? null : r.RatingType.Code;
                    var ratingClass = r.RatingClass == null ? null : r.RatingClass.Code;
                    var authorization = r.Authorization == null ? null : r.Authorization.Code;

                    return string.Format(
                        "{0} {1} {2}",
                        ratingClass,
                        ratingType,
                        string.IsNullOrEmpty(authorization) ?
                            string.Empty :
                            string.IsNullOrEmpty(ratingType) && string.IsNullOrEmpty(ratingClass) ? authorization : ", " + authorization
                    ).Trim();
                }).ToArray<string>();

            return string.Join("   /   ", resultArr);
        }
    }
}

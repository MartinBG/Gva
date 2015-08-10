using System.Collections.Generic;
using System.Linq;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.OrganizationRepository;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.WordTemplates
{
    public class ForeignLicence : IDataGenerator
    {
        private ILotRepository lotRepository;
        private INomRepository nomRepository;
        private IOrganizationRepository organizationRepository;

        public ForeignLicence(
            ILotRepository lotRepository,
            INomRepository nomRepository,
            IOrganizationRepository organizationRepository)
        {
            this.lotRepository = lotRepository;
            this.nomRepository = nomRepository;
            this.organizationRepository = organizationRepository;
        }

        public string GeneratorCode
        {
            get
            {
                return "foreignLicence";
            }
        }

        public string GeneratorName
        {
            get
            {
                return "Лиценз за чужденец";
            }
        }

        public object GetData(int lotId, string path)
        {
            var lot = this.lotRepository.GetLotIndex(lotId);
            var personData = lot.Index.GetPart<PersonDataDO>("personData").Content;
            int validTrueId = this.nomRepository.GetNomValue("boolean", "yes").NomValueId;
            var personEmplPart = lot.Index.GetParts<PersonEmploymentDO>("personDocumentEmployments")
                .FirstOrDefault(a => a.Content.ValidId == validTrueId);
            var organization = personEmplPart == null || !personEmplPart.Content.OrganizationId.HasValue ? null :
                        this.organizationRepository.GetOrganization(personEmplPart.Content.OrganizationId.Value);
            NomValue organizationNom = organization != null ?
                new NomValue()
                {
                    NomValueId = organization.LotId,
                    Name = organization.Name,
                    NameAlt = organization.NameAlt
                } : null;

            var personEmploymentOrg = organizationNom != null?  organizationNom.NameAlt : null;

            var licencePart = lot.Index.GetPart<PersonLicenceDO>(path);
            var licence = licencePart.Content;
            var lastEdition = lot.Index.GetParts<PersonLicenceEditionDO>("licenceEditions")
                .Where(e => e.Content.LicencePartIndex == licencePart.Part.Index)
                .OrderBy(e => e.Content.Index)
                .Last()
                .Content;

            var includedRatings = lastEdition.IncludedRatings
                .Select(i => i.Ind)
                .Distinct()
                .Select(i => lot.Index.GetPart<PersonRatingDO>("ratings/" + i.Value).Content);

            dynamic licenceHolder = this.GetLicenceHolder(personData);
            string occupation = this.GetOccupation(includedRatings);

            string countryNameAlt = personData.CountryId.HasValue ? this.nomRepository.GetNomValue("countries", personData.CountryId.Value).NameAlt : null;
            var json = new
            {
                root = new
                {
                    LICENCE_NO = Utils.PadLicenceNumber(licence.LicenceNumber),
                    FOREIGN_LICENCE_NO = licence.ForeignLicenceNumber,
                    LICENCE_HOLDER = licenceHolder,
                    COMPANY = personEmploymentOrg,
                    OCCUPATION = occupation,
                    COUNTRY = countryNameAlt,
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
                    D_COUNTRY = countryNameAlt,
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
                    var ratingTypes = r.RatingTypes.Count() > 0 ? string.Join(", ", this.nomRepository.GetNomValues("ratingTypes", r.RatingTypes.ToArray()).Select(rt => rt.Code)) : "";
                    var ratingClass = r.RatingClassId.HasValue ? this.nomRepository.GetNomValue("ratingClasses", r.RatingClassId.Value).Code : null;
                    var authorization = r.AuthorizationId.HasValue ? this.nomRepository.GetNomValue("authorizations", r.AuthorizationId.Value).Code : null;

                    return string.Format(
                        "{0} {1} {2}",
                        ratingClass,
                        ratingTypes,
                        string.IsNullOrEmpty(authorization) ?
                            string.Empty :
                            string.IsNullOrEmpty(ratingTypes) && string.IsNullOrEmpty(ratingClass) ? authorization : ", " + authorization
                    ).Trim();
                }).ToArray<string>();

            return string.Join("   /   ", resultArr);
        }
    }
}

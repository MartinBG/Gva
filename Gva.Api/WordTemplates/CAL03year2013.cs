﻿using System.Collections.Generic;
using System.Linq;
using Common.Api.Repositories.NomRepository;
using Common.Json;
using Gva.Api.ModelsDO.Persons;
using Regs.Api.Repositories.LotRepositories;
using Regs.Api.Models;
using Common.Api.Models;

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

        public object GetData(int lotId, string path)
        {
            var lot = this.lotRepository.GetLotIndex(lotId);
            var personData = lot.Index.GetPart<PersonDataDO>("personData").Content;

            var licencePart = lot.Index.GetPart<PersonLicenceDO>(path);
            var licence = licencePart.Content;
            var lastEdition = lot.Index.GetParts<PersonLicenceEditionDO>("licenceEditions")
                .Where(e => e.Content.LicencePartIndex == licencePart.Part.Index)
                .OrderBy(e => e.Content.Index)
                .Last()
                .Content;

            var includedRatings = lastEdition.IncludedRatings.Select(i => i.Ind).Distinct()
              .Select(ind => lot.Index.GetPart<PersonRatingDO>("ratings/" + ind));
            var ratingEditions = lastEdition.IncludedRatings.Select(i => lot.Index.GetPart<PersonRatingEditionDO>("ratingEditions/" + i.Index));

            var licenceType = this.nomRepository.GetNomValue("licenceTypes", licence.LicenceType.NomValueId);

            var ratings = this.GetRatings(includedRatings, ratingEditions);
            var placeOfBirth = personData.PlaceOfBirth;
            NomValue country = null;
            NomValue nationality = null;
            if (placeOfBirth != null) 
            {
                country = this.nomRepository.GetNomValue("countries", placeOfBirth.ParentValueId.Value);
                nationality = this.nomRepository.GetNomValue("countries", personData.Country.NomValueId);
            }


            var refNumber = string.Format(
                "BG {0} - {1} - {2}",
                licenceType.Code,
                Utils.PadLicenceNumber(licence.LicenceNumber),
                personData.Lin);
            var personName = string.Format("{0} {1} {2}",
                    personData.FirstName,
                    personData.MiddleName,
                    personData.LastName);
            var personNameAlt = string.Format("{0} {1} {2}",
                    personData.FirstNameAlt,
                    personData.MiddleNameAlt,
                    personData.LastNameAlt);

            var json = new
            {
                root = new
                {
                    REF_NO1 = refNumber,
                    PERSON_NAME1 = personName,
                    PERSON_NAME_TRANS1 = personNameAlt,
                    BIRTHDATE1 = personData.DateOfBirth,
                    BIRTHCOUNTRY1 = country != null ? country.Name : null,
                    BIRTHCOUNTRY_TRANS1 = country != null ? country.NameAlt : null,
                    BIRTHPLACE1 = placeOfBirth != null? placeOfBirth.Name : null,
                    BIRTHPLACE_TRANS1 = placeOfBirth != null ? placeOfBirth.NameAlt : null,
                    NATIONALITY1 = nationality != null? nationality.Name : null,
                    NATIONALITY_CODE1 = nationality != null? nationality.TextContent.Get<string>("nationalityCodeCA") : null,
                    ISSUE_DATE1 = lastEdition.DocumentDateValidFrom,
                    REF_NO2 = refNumber,
                    PERSON_NAME2 = personName,
                    PERSON_NAME_TRANS2 = personNameAlt,
                    BIRTHDATE2 = personData.DateOfBirth,
                    BIRTHCOUNTRY2 = country != null ? country.Name : null,
                    BIRTHCOUNTRY_TRANS2 = country != null ? country.NameAlt : null,
                    BIRTHPLACE2 = placeOfBirth != null ? placeOfBirth.Name : null,
                    BIRTHPLACE_TRANS2 = placeOfBirth != null ? placeOfBirth.NameAlt : null,
                    NATIONALITY2 = nationality != null ? nationality.Name : null,
                    NATIONALITY_CODE2 = nationality != null ? nationality.TextContent.Get<string>("nationalityCodeCA") : null,
                    ISSUE_DATE2 = lastEdition.DocumentDateValidFrom,
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

            return json;
        }


        private List<object> GetRatings(IEnumerable<PartVersion<PersonRatingDO>> includedRatings, IEnumerable<PartVersion<PersonRatingEditionDO>> editions)
        {
            List<object> ratingEditions = new List<object>();
            foreach (var edition in editions)
            {
                var rating = includedRatings.Where(r => r.Part.Index == edition.Content.RatingPartIndex).Single();
                var ratingTypeName = rating.Content.RatingType == null ? null : rating.Content.RatingType.Name;
                var ratingClassName = rating.Content.RatingClass == null ? null : rating.Content.RatingClass.Name;
                var authorizationName = rating.Content.Authorization == null ? null : rating.Content.Authorization.Name;
                ratingEditions.Add(new
                {
                    CLASS_AUTH = string.IsNullOrEmpty(ratingClassName) && string.IsNullOrEmpty(ratingTypeName) ?
                        authorizationName :
                        string.Format(
                            "{0} {1} {2}",
                            ratingTypeName,
                            ratingClassName,
                            string.IsNullOrEmpty(authorizationName) ? string.Empty : "/ " + authorizationName).Trim(),
                    ISSUE_DATE = edition.Content.DocumentDateValidFrom,
                    VALID_DATE = edition.Content.DocumentDateValidTo
                });
            }

            ratingEditions = Utils.FillBlankData(ratingEditions, 10);
            return ratingEditions;
        }

    }
}

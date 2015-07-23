using System;
using Common.Api.Models;
using Gva.Api.Models;
using Gva.Api.Models.Views.Person;

namespace Gva.Api.ModelsDO.Persons
{
    public class StampedLicenceDO
    {
        public StampedLicenceDO(GvaLicenceEdition licenceEdition)
        {
            this.LotId = licenceEdition.LotId;
            this.PartIndex = licenceEdition.LicencePartIndex;
            this.EditionIndex = licenceEdition.EditionIndex;
            this.StampNumber = licenceEdition.StampNumber;
            this.DateValidFrom = licenceEdition.DateValidFrom;
            this.DateValidTo = licenceEdition.DateValidTo;
            if (licenceEdition.GvaStageId.HasValue)
            {
                this.IsReady = licenceEdition.GvaStageId >= GvaConstants.IsReadyLicence;
                this.IsReceived = licenceEdition.GvaStageId >= GvaConstants.IsReceivedLicence;
            }

            if (licenceEdition.OfficiallyReissuedStageId.HasValue)
            {
                this.IsOfficiallyReissued = true;
                this.IsReady = licenceEdition.OfficiallyReissuedStageId >= GvaConstants.IsReadyLicence;
                this.IsReceived = licenceEdition.OfficiallyReissuedStageId >= GvaConstants.IsReceivedLicence;
            }
            else
            {
                this.IsOfficiallyReissued = false;
            }

            this.LicenceAction = licenceEdition.LicenceAction;
            this.LicenceNumber = licenceEdition.LicenceNumber;

            if (licenceEdition.Person != null)
            {
                this.Person = new PersonViewDO(licenceEdition.Person);
            }

            if (licenceEdition.Application != null)
            {
                this.Application = new ApplicationNomDO(licenceEdition.GvaApplication);
            }

            this.LicencePartIndex = licenceEdition.LicencePartIndex;
            this.EditionPartIndex = licenceEdition.EditionPartIndex;
            this.FirstDocDateValidFrom = licenceEdition.FirstDocDateValidFrom;
            this.Valid = licenceEdition.Valid;

            if (licenceEdition.LotFile != null)
            {
                this.Case = new CaseDO(licenceEdition.LotFile);
            }
        }

        public StampedLicenceDO(GvaViewPrintedRatingEdition ratingEdition)
        {
            this.LotId = ratingEdition.LotId;
            this.PartIndex = ratingEdition.LicencePartIndex;
            this.EditionPartIndex = ratingEdition.LicenceEditionPartIndex;
            this.RatingPartIndex = ratingEdition.RatingPartIndex;
            this.RatingEditionPartIndex = ratingEdition.RatingEditionPartIndex;
            this.DateValidFrom = ratingEdition.LicenceEdition.DateValidFrom;
            this.DateValidTo = ratingEdition.LicenceEdition.DateValidTo;
            this.LicenceAction = ratingEdition.LicenceEdition.LicenceAction;
            this.LicenceNumber = ratingEdition.LicenceEdition.Licence.LicenceNumber;
            this.IsOfficiallyReissued = false;

            if (ratingEdition.Person != null)
            {
                this.Person = new PersonViewDO(ratingEdition.Person);
            }

            this.LicencePartIndex = ratingEdition.LicencePartIndex;
            this.EditionPartIndex = ratingEdition.LicenceEditionPartIndex;
            this.FirstDocDateValidFrom = ratingEdition.LicenceEdition.FirstDocDateValidFrom;
            this.Valid = ratingEdition.LicenceEdition.Licence.Valid;
            this.IsReady = ratingEdition.LicenceStatusId.HasValue ? ratingEdition.LicenceStatusId.Value >= GvaConstants.IsReadyLicence : false;
            this.IsReceived = ratingEdition.LicenceStatusId.HasValue ? ratingEdition.LicenceStatusId.Value >= GvaConstants.IsReceivedLicence : false;
        }

        public int LotId { get; set; }

        public int PartIndex { get; set; }

        public int EditionIndex { get; set; }

        public int? RatingPartIndex { get; set; }

        public int? RatingEditionPartIndex { get; set; }

        public string StampNumber { get; set; }

        public DateTime DateValidFrom { get; set; }

        public DateTime? DateValidTo { get; set; }

        public int? LicenceNumber { get; set; }

        public bool IsReceived { get; set; }

        public bool IsReady { get; set; }

        public bool IsDone { get; set; }

        public PersonViewDO Person { get; set; }

        public ApplicationNomDO Application { get; set; }

        public NomValue LicenceAction { get; set; }

        public int LicencePartIndex { get; set; }

        public int EditionPartIndex { get; set; }

        public DateTime FirstDocDateValidFrom { get; set; }

        public bool Valid { get; set; }

        public CaseDO Case { get; set; }

        public bool IsOfficiallyReissued { get; set; }
    }
}

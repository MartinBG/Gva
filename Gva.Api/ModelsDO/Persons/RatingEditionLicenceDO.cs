using System;
using Gva.Api.Models;
namespace Gva.Api.ModelsDO.Persons
{
    public class RatingEditionLicenceDO
    {
        public RatingEditionLicenceDO()
        { }

        public RatingEditionLicenceDO(PrintedRatingEditionDO rating)
        {
            this.RatingPartIndex = rating.RatingPartIndex;
            this.RatingEditionPartIndex = rating.RatingEditionPartIndex;
            this.IsReady = rating.LicenceStatusId >= GvaConstants.IsReadyLicence;
            this.IsReceived = rating.LicenceStatusId >= GvaConstants.IsReceivedLicence;
            this.HasFileId = true;
            this.NoNumber = rating.NoNumber;
        } 

        public int RatingPartIndex { get; set; }

        public int RatingEditionPartIndex { get; set; }

        public bool HasFileId { get; set; }

        public bool IsReady { get; set; }

        public bool IsReceived { get; set; }

        public bool? NoNumber { get; set; }
    }
}

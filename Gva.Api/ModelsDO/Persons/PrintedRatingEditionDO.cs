using System;
namespace Gva.Api.ModelsDO.Persons
{
    public class PrintedRatingEditionDO
    {
        public int RatingPartIndex { get; set; }

        public int RatingEditionPartIndex { get; set; }

        public Guid PrintedEditionBlobKey { get; set; }

        public int FileId  { get; set; }

        public int? LicenceStatusId { get; set; }

        public bool? NoNumber { get; set; }
    }
}

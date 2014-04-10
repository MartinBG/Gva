using System;
using Common.Json;
using Newtonsoft.Json.Linq;
using Regs.Api.Models;

namespace Gva.Api.ModelsDO
{
    public class RatingPartVersionDO
    {
        public RatingPartVersionDO(
            PartVersion rating,
            PartVersion ratingEdition,
            PartVersion firstRatingEdition)
        {
            this.PartIndex = rating.Part.Index.Value;
            this.Rating = rating.Content;
            this.RatingEdition = ratingEdition == null ? null : new PartVersionDO(ratingEdition);
            this.FirstEditionValidFrom = firstRatingEdition.Content.Get<DateTime>("documentDateValidFrom");
        }

        public int PartIndex { get; set; }

        public JObject Rating { get; set; }

        public PartVersionDO RatingEdition { get; set; }

        public DateTime FirstEditionValidFrom { get; set; }
    }
}

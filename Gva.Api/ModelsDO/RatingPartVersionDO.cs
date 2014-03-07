using System;
using Newtonsoft.Json.Linq;

namespace Gva.Api.ModelsDO
{
    public class RatingPartVersionDO
    {
        public int PartIndex { get; set; }

        public JObject Rating { get; set; }

        public PartVersionDO RatingEdition { get; set; }

        public DateTime FirstEditionValidFrom { get; set; }
    }
}

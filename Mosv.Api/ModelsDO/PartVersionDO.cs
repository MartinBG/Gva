using System.Collections.Generic;
using System.Linq;
using Mosv.Api.Models;
using Newtonsoft.Json.Linq;
using Regs.Api.Models;

namespace Mosv.Api.ModelsDO
{
    public class PartVersionDO
    {
        public PartVersionDO(PartVersion partVersion)
        {
            this.PartIndex = partVersion.Part.Index;
            this.PartId = partVersion.PartId;
            this.Part = partVersion.Content;
        }

        public int PartIndex { get; set; }

        public int PartId { get; set; }

        public JObject Part { get; set; }
    }
}

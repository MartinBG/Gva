using System.Linq;
using Gva.Api.Models;
using Newtonsoft.Json.Linq;
using Regs.Api.Models;

namespace Gva.Api.ModelsDO
{
    public class PartVersionDO
    {
        public PartVersionDO(PartVersion partVersion, GvaLotFile[] lotFiles = null)
        {
            this.PartIndex = partVersion.Part.Index.Value;
            this.PartId = partVersion.PartId;
            this.Part = partVersion.Content;
            if (lotFiles != null)
            {
                this.Files = lotFiles
                    .Select(lf => new FileDO(lf))
                    .ToArray();
            }
        }

        public int PartIndex { get; set; }

        public int PartId { get; set; }

        public JObject Part { get; set; }

        public FileDO[] Files { get; set; }
    }
}

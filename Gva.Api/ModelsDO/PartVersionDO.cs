using System.Collections.Generic;
using System.Linq;
using Gva.Api.Models;
using Newtonsoft.Json.Linq;
using Regs.Api.Models;

namespace Gva.Api.ModelsDO
{
    public class PartVersionDO
    {
        public PartVersionDO(PartVersion partVersion)
        {
            this.PartIndex = partVersion.Part.Index;
            this.PartId = partVersion.PartId;
            this.Part = partVersion.Content;
            this.Files = new List<FileDO>();
            this.Applications = new List<ApplicationNomDO>();
        }

        public PartVersionDO(PartVersion partVersion, GvaLotFile[] lotFiles)
            : this(partVersion)
        {
            this.Files = lotFiles
                .Select(lf => new FileDO(lf))
                .ToList();
        }

        public PartVersionDO(PartVersion partVersion, GvaApplication[] lotObjects)
            : this(partVersion)
        {
            this.Applications = lotObjects
                .Select(ga => new ApplicationNomDO(ga))
                .ToList();
        }

        public int PartIndex { get; set; }

        public int PartId { get; set; }

        public JObject Part { get; set; }

        public List<FileDO> Files { get; set; }

        public List<ApplicationNomDO> Applications { get; set; }
    }
}

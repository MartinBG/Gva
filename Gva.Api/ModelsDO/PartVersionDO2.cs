using System.Collections.Generic;
using System.Linq;
using Gva.Api.Models;
using Regs.Api.Models;

namespace Gva.Api.ModelsDO
{
    public class PartVersionDO2<T> where T : class, new()
    {
        public PartVersionDO2(PartVersion partVersion)
        {
            this.PartIndex = partVersion.Part.Index;
            this.PartId = partVersion.PartId;
            this.Part = partVersion.Content.ToObject<T>();
            this.Files = new List<FileDO>();
            this.Applications = new List<ApplicationNomDO>();
        }

        public PartVersionDO2(PartVersion partVersion, GvaLotFile[] lotFiles)
            : this(partVersion)
        {
            this.Files = lotFiles
                .Select(lf => new FileDO(lf))
                .ToList();
        }

        public PartVersionDO2(PartVersion partVersion, GvaApplication[] lotObjects)
            : this(partVersion)
        {
            this.Applications = lotObjects
                .Select(ga => new ApplicationNomDO(ga))
                .ToList();
        }

        public PartVersionDO2(T part, List<FileDO> files = null)
            : this()
        {
            this.Part = part;

            if (files != null)
            {
                this.Files = files;
            }
        }

        public PartVersionDO2()
        {
            this.Files = new List<FileDO>();
            this.Applications = new List<ApplicationNomDO>();
            this.Part = new T();
        }

        public int PartIndex { get; set; }

        public int PartId { get; set; }

        public T Part { get; set; }

        public List<FileDO> Files { get; set; }

        public List<ApplicationNomDO> Applications { get; set; }
    }
}

using System.Collections.Generic;
using System.Linq;
using Gva.Api.Models;
using Regs.Api.Models;

namespace Gva.Api.ModelsDO
{
    public class FilePartVersionDO<T> where T : class, new()
    {
        public FilePartVersionDO(PartVersion<T> partVersion)
        {
            this.PartIndex = partVersion.Part.Index;
            this.PartId = partVersion.PartId;
            this.Part = partVersion.Content;
            this.Files = new List<FileDO>();
        }

        public FilePartVersionDO(PartVersion<T> partVersion, GvaLotFile[] lotFiles)
            : this(partVersion)
        {
            this.Files = lotFiles
                .Select(lf => new FileDO(lf))
                .ToList();
        }

        public FilePartVersionDO(T part, List<FileDO> files = null)
            : this()
        {
            this.Part = part;

            if (files != null)
            {
                this.Files = files;
            }
        }

        public FilePartVersionDO()
        {
            this.Files = new List<FileDO>();
            this.Part = new T();
        }

        public int? PartIndex { get; set; }

        public int PartId { get; set; }

        public T Part { get; set; }

        public List<FileDO> Files { get; set; }
    }
}

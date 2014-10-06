using System.Collections.Generic;
using System.Linq;
using Gva.Api.Models;
using Regs.Api.Models;

namespace Gva.Api.ModelsDO
{
    public class CaseTypesPartDO<T> where T : class, new()
    {
        public CaseTypesPartDO(PartVersion<T> partVersion)
        {
            this.PartIndex = partVersion.Part.Index;
            this.PartId = partVersion.PartId;
            this.Part = partVersion.Content;
            this.Cases = new List<CaseDO>();
        }

        public CaseTypesPartDO(PartVersion<T> partVersion, GvaLotFile[] lotFiles)
            : this(partVersion)
        {
            this.Cases = lotFiles
                .Select(lf => new CaseDO(lf))
                .ToList();
        }

        public CaseTypesPartDO(T part, List<CaseDO> cases)
            : this()
        {
            this.Part = part;
            this.Cases = cases;
        }

        public CaseTypesPartDO()
        {
            this.Part = new T();
            this.Cases = new List<CaseDO>();
        }

        public int? PartIndex { get; set; }

        public int PartId { get; set; }

        public T Part { get; set; }

        public List<CaseDO> Cases { get; set; }
    }
}

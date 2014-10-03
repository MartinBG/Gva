using Gva.Api.Models;
using Regs.Api.Models;

namespace Gva.Api.ModelsDO
{
    public class CaseTypePartDO<T> where T : class, new()
    {
        public CaseTypePartDO(PartVersion<T> partVersion)
        {
            this.PartIndex = partVersion.Part.Index;
            this.PartId = partVersion.PartId;
            this.Part = partVersion.Content;
        }

        public CaseTypePartDO(PartVersion<T> partVersion, GvaLotFile lotFile)
            : this(partVersion)
        {
            this.Case = lotFile == null ? null : new CaseDO(lotFile);
        }

        public CaseTypePartDO(T part, CaseDO caseDO = null)
            : this()
        {
            this.Part = part;
            this.Case = caseDO;
        }

        public CaseTypePartDO()
        {
            this.Part = new T();
        }

        public int? PartIndex { get; set; }

        public int PartId { get; set; }

        public T Part { get; set; }

        public CaseDO Case { get; set; }
    }
}

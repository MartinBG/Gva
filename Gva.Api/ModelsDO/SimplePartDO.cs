using Gva.Api.Models;
using Regs.Api.Models;

namespace Gva.Api.ModelsDO
{
    public class SimplePartDO<T> where T : class, new()
    {
        public SimplePartDO(PartVersion<T> partVersion)
        {
            this.PartIndex = partVersion.Part.Index;
            this.PartId = partVersion.PartId;
            this.Part = partVersion.Content;
        }

        public SimplePartDO()
        {
            this.Part = new T();
        }

        public SimplePartDO(T part)
        {
            this.Part = part;
        }

        public int? PartIndex { get; set; }

        public int PartId { get; set; }

        public T Part { get; set; }
    }
}

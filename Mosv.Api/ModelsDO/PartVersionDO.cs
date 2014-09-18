using Regs.Api.Models;

namespace Mosv.Api.ModelsDO
{
    public class PartVersionDO<T> where T : class, new()
    {
        public PartVersionDO(PartVersion<T> partVersion)
        {
            this.PartIndex = partVersion.Part.Index;
            this.PartId = partVersion.PartId;
            this.Part = partVersion.Content;
        }

        public int PartIndex { get; set; }

        public int PartId { get; set; }

        public T Part { get; set; }
    }
}

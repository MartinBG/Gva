using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Gva.Api.ModelsDO
{
    class FilePartVersionDO
    {
        public int PartIndex { get; set; }

        public int PartId { get; set; }

        public JObject Part { get; set; }

        public FileDO[] Files { get; set; }
    }
}

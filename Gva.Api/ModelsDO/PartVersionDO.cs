using Newtonsoft.Json.Linq;

namespace Gva.Api.ModelsDO
{
    public class PartVersionDO
    {
        public int PartIndex { get; set; }

        public JObject Part { get; set; }
    }
}

using Newtonsoft.Json.Linq;

namespace Gva.Web.Models
{
    public class PartVersionDO
    {
        public int PartIndex { get; set; }

        public JObject Part { get; set; }

        public JObject File { get; set; }
    }
}
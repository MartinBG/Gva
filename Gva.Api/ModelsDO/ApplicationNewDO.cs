using Docs.Api.DataObjects;
using Gva.Api.Models;
using Newtonsoft.Json.Linq;
using System;

namespace Gva.Api.ModelsDO
{
    public class ApplicationNewDO
    {
        public int LotId { get; set; }

        public DocDO Doc { get; set; }

        public JObject AppPart { get; set; }

        public JObject AppFile { get; set; }
    }
}

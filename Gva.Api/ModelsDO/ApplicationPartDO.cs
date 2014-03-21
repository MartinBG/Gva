using Docs.Api.DataObjects;
using Gva.Api.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Gva.Api.ModelsDO
{
    public class ApplicationPartDO
    {
        public string SetPartAlias { get; set; }
        public dynamic AppPart { get; set; }
        public dynamic AppFile { get; set; }

        //link existing
        public int PartId { get; set; }
        public int DocFileId { get; set; }

        //new part
        public int DocId { get; set; }
    }
}

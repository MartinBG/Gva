using Docs.Api.Models;
using Gva.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docs.Api.DataObjects
{
    public class ApplicationLotObjectDO
    {
        public ApplicationLotObjectDO(GvaLotObject lotObject)
        {
            this.SetPartName = lotObject.LotPart.SetPart.Name;
            this.SetPartAlias = lotObject.LotPart.SetPart.Alias;
            this.PartIndex = lotObject.LotPart.Index;
        }

        public string SetPartName { get; set; }
        public string SetPartAlias { get; set; }
        public int? PartIndex { get; set; }
    }
}

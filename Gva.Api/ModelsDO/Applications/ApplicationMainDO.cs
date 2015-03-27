using System.Collections.Generic;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Applications
{
    public class ApplicationMainDO
    {
        public int LotId { get; set; }

        public int GvaApplicationId { get; set; }

        public int PartIndex { get; set; }

        public string Set { get; set; }
    }
}

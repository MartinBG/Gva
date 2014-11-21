using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gva.Api.Models;
using Gva.Api.Models.Views.Person;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Persons
{
    public class IncludedCheckDO
    {
        public int? LotId { get; set; }

        public int? PartIndex { get; set; }
    }
}

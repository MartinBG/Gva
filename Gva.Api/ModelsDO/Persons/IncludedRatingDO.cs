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
    public class IncludedRatingDO
    {
        public int? Ind { get; set; }

        public int? Index { get; set; }

        public int? OrderNum { get; set; }
    }
}

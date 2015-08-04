using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gva.Api.Models;
using Gva.Api.Models.Views.Person;
using Common.Api.Models;

namespace Gva.ManageLotsTool.OldDOs
{
    public class PersonLangLevelDO_Old
    {
        public NomValue LangLevel { get; set; }

        public DateTime? ChangeDate { get; set; }
    }
}

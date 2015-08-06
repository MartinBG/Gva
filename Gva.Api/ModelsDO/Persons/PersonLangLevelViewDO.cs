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
    public class PersonLangLevelViewDO
    {
        public PersonLangLevelViewDO() {}

        public NomValue LangLevel { get; set; }

        public DateTime? ChangeDate { get; set; }
    }
}

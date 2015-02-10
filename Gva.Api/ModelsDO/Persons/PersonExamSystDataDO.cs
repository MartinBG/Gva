using System;
using System.Collections.Generic;
using Gva.Api.Models;
using Gva.Api.ModelsDO.Common;

namespace Gva.Api.ModelsDO.Persons
{
    public class PersonExamSystDataDO
    {
        public PersonExamSystDataDO()
        {
            Exams = new List<PersonExamSystExamDO>();
            States = new List<PersonExamSystStateDO>();
        }

        public List<PersonExamSystExamDO> Exams { get; set; }

        public List<PersonExamSystStateDO> States { get; set; }
    }
}

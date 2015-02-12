using System;
using System.Collections.Generic;
using Gva.Api.Models;
using Gva.Api.ModelsDO.Common;
using Gva.Api.ModelsDO.ExaminationSystem;

namespace Gva.Api.ModelsDO.Persons
{
    public class PersonExamSystDataDO
    {
        public PersonExamSystDataDO()
        {
            States = new List<PersonExamSystStateDO>();
        }

        public List<PersonExamSystStateDO> States { get; set; }
    }
}

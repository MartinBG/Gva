﻿using System;
using Gva.Api.Models;
using Gva.Api.ModelsDO.Common;

namespace Gva.Api.ModelsDO.Persons
{
    public class PersonExamSystStateDO
    {
        public string QualificationCode { get; set; }

        public string QualificationName { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string State { get; set; }

        public string StateMethod { get; set; }

        public string Notes { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using Common.Api.Models;
using Gva.Api.Models;
using Gva.Api.ModelsDO.Aircrafts;
using Gva.Api.ModelsDO.Airports;
using Gva.Api.ModelsDO.Equipments;
using Gva.Api.ModelsDO.ExaminationSystem;
using Gva.Api.ModelsDO.Persons;

namespace Gva.Api.ModelsDO.Applications
{
    public class AppExamSystExamDO
    {
        public GvaExSystExamDO ExamData { get; set; }

        public DateTime Date { get; set; }
    }
}

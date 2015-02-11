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
    public class AppExaminationSystemDataDO
    {
        public AppExaminationSystemDataDO()
        {
            Qualifications = new List<AppExamSystQualificationDO>();
            Exams = new List<AppExamSystExamDO>();
        }

        public List<AppExamSystQualificationDO> Qualifications { get; set; }

        public List<AppExamSystExamDO> Exams { get; set; }

        public NomValue School { get; set; }

        public NomValue CertCampaign { get; set; }
        
    }
}

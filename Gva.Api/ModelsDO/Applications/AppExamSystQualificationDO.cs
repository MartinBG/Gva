using System;
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
    public class AppExamSystQualificationDO
    {
        public string QualificationCode { get; set; }

        public string QualificationName { get; set; }

        public string State { get; set; }

        public NomValue LicenceType { get; set; }
    }
}

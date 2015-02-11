using System;
using Gva.Api.Models;
using Gva.Api.ModelsDO.Common;

namespace Gva.Api.ModelsDO.ExaminationSystem
{
    public class GvaExSystExamDO
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public string QualificationName { get; set; }

        public string QualificationCode { get; set; }
    }
}

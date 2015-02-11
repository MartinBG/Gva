using System;
using Gva.Api.Models;
using Gva.Api.ModelsDO.Common;

namespace Gva.Api.ModelsDO.ExaminationSystem
{
    public class GvaExSystExamineeDO
    {
        public string Uin { get; set; }

        public int? Lin { get; set; }

        public string ExamName { get; set; }

        public string ExamCode { get; set; }

        public DateTime? EndTime { get; set; }

        public string TotalScore { get; set; }

        public string ResultStatus { get; set; }

        public string CertCampName { get; set; }

        public string CertCampCode { get; set; }

        public string QualificationCode { get; set; }

        public int? LotId { get; set; }
    }
}

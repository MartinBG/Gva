using Gva.Api.Models;
using System;

namespace Gva.Api.ModelsDO.Applications
{
    public class ApplicationExamListDO
    {
        public int AppExamId { get; set; }

        public int AppPartId { get; set; }

        public int? LotId { get; set; }

        public DateTime? DocumentDate { get; set; }

        public string DocumentNumber { get; set; }

        public int? PersonLin { get; set; }

        public string PersonUin { get; set; }

        public string PersonNames { get; set; }

        public string StageName { get; set; }

        public string ExamName { get; set; }

        public string ExamCode { get; set; }

        public string CertCampName { get; set; }

        public string CertCampCode { get; set; }

        public DateTime? ExamDate { get; set; }
    }
}

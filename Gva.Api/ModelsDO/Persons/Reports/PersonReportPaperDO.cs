using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Persons.Reports
{
    public class PersonReportPaperDO
    {
        public int PaperId { get; set; }

        public string PaperName { get; set; }

        public int FirstNumber { get; set; }

        public int IssuedCount { get; set; }

        public int SkippedCount { get; set; }

        public int? LastIssuedNumber { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }
    }
}

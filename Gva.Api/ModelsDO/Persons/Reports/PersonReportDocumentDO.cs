using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Persons.Reports
{
    public class PersonReportDocumentDO
    {
        public int LotId { get; set; }

        public int? Lin { get; set; }

        public string Role { get; set; }

        public string Type { get; set; }

        public string Number { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string Publisher { get; set; }

        public string Limitations { get; set; }

        public string MedClass { get; set; }

        public bool? Valid { get; set; }
    }
}

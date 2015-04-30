using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Persons.Reports
{
    public class PersonReportLicenceDO
    {
        public int? Lin { get; set; }

        public string Uin { get; set; }

        public string LicenceTypeName { get; set; }

        public string LicenceCode { get; set; }

        public string Names { get; set; }

        public DateTime? DateValidFrom { get; set; }

        public DateTime? DateValidTo { get; set; }

        public DateTime? FirstIssueDate { get; set; }

        public string LicenceAction { get; set; }

        public string StampNumber { get; set; }
    }
}

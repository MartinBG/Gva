using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Persons
{
    public class PersonReportDO
    {
        public PersonReportDO() 
        {
            IncludedChecks = new List<int>();
        }

        public List<int> IncludedChecks { get; set; }

        public DateTime? Date { get; set; }

        public string ReportNumber { get; set; }
    }
}

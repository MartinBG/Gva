using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Persons
{
    public class PersonReportViewDO
    {
        public PersonReportViewDO() 
        {
            IncludedChecks = new List<int>();
            IncludedPersonChecks = new List<IncludedPersonDO>();
        }

        public List<IncludedPersonDO> IncludedPersonChecks { get; set; }

        public List<int> IncludedChecks { get; set; }

        public DateTime? Date { get; set; }

        public string ReportNumber { get; set; }
    }
}

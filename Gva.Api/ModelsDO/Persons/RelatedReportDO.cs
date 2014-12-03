﻿using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Persons
{
    public class RelatedReportDO
    {
        public DateTime? Date { get; set; }

        public string ReportNumber { get; set; }

        public string Publisher { get; set; }
    }
}
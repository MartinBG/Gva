using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Persons
{
    public class PersonAppExamDO
    {
        public int AppId { get; set; }

        public int LotId { get; set; }

        public string CertCampCode { get; set; }

        public string ExamCode { get; set; }
    }
}

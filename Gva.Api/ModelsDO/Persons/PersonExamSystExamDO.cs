using System;
using Gva.Api.Models;
using Gva.Api.ModelsDO.Common;
using Gva.Api.ModelsDO.ExaminationSystem;

namespace Gva.Api.ModelsDO.Persons
{
    public class PersonExamSystExamDO
    {
        public GvaExSystTestDO Test { get; set; }

        public DateTime EndTime { get; set; }

        public string TotalScore { get; set; }

        public string Status { get; set; }

        public GvaExSystCertCampaignDO CertCamp { get; set; }
    }
}

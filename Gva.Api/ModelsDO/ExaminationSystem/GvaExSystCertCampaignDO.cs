using System;
using Gva.Api.Models;
using Gva.Api.ModelsDO.Common;

namespace Gva.Api.ModelsDO.ExaminationSystem
{
    public class GvaExSystCertCampaignDO
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public DateTime? ValidFrom { get; set; }

        public DateTime? ValidTo { get; set; }

        public string QualificationName { get; set; }

        public string QualificationCode { get; set; }
    }
}

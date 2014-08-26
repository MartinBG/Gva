using System.ComponentModel.DataAnnotations;
using Common.Api.Models;
using System;
using System.Collections.Generic;
using Gva.Api.ModelsDO.Common;

namespace Gva.Api.ModelsDO.Organizations
{
    public class OrganizationRecommendationSectionDO
    {
        public List<OrganizationRecommendationDetailDO> Details { get; set; }

        public string SectionCode { get; set; }

        public string SectionName { get; set; }
    }
}

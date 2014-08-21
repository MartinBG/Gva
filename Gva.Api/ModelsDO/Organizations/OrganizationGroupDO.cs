using System.ComponentModel.DataAnnotations;
using Common.Api.Models;
using System;
using System.Collections.Generic;
using Gva.Api.ModelsDO.Common;

namespace Gva.Api.ModelsDO.Organizations
{
    public class OrganizationGroupDO
    {
        public List<AuditDetailDO> Group { get; set; }

        public string GroupTitle { get; set; }
    }
}

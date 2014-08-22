using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;
using Gva.Api.ModelsDO.Common;

namespace Gva.Api.ModelsDO.Organizations
{
    public class OrganizationPartDO
    {
        public OrganizationPartDO()
        {
            this.Examiners = new List<NomValue>();
        }

        public List<NomValue> Examiners { get; set; }
    }
}

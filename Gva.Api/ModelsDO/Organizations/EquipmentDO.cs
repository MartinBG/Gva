using System.ComponentModel.DataAnnotations;
using Common.Api.Models;
using System;
using System.Collections.Generic;

namespace Gva.Api.ModelsDO.Organizations
{
    public class EquipmentDO
    {
        public string Name { get; set; }

        public string Id { get; set; }

        public int? Count { get; set; }
    }
}

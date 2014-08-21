using System.ComponentModel.DataAnnotations;
using Common.Api.Models;
using System;
using System.Collections.Generic;

namespace Gva.Api.ModelsDO.Organizations
{
    public class LimsMGDO
    {
        public string AircraftTypeGroup { get; set; }

        public string QualitySystem { get; set; }

        public NomValue Awapproval { get; set; }

        public NomValue Pfapproval { get; set; }
    }
}

using System.Collections.Generic;
using System.Linq;
using Gva.Api.Models;
using Newtonsoft.Json.Linq;
using Regs.Api.Models;
using Common.Json;
using System;

namespace Gva.Api.ModelsDO
{
    public class RegistrationViewDO
    {
        public int FirstIndex { get; set; }

        public int? PrevIndex { get; set; }

        public int? CurrentIndex { get; set; }

        public int? NextIndex { get; set; }

        public int LastIndex { get; set; }

        public int? AirworthinessIndex { get; set; }

        public bool HasAirworthiness { get; set; }

        public JObject FirstReg { get; set; }

        public JObject LastReg { get; set; }
    }
}

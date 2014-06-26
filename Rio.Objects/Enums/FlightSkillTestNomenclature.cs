using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class FlightSkillTestNomenclature
    {
        public string Name
        {
            get
            {
                return App_LocalResources.FlightSkillTestNomenclature.ResourceManager.GetString(ResourceKey);
            }
        }

        public string Code { get; set; }
        public string ResourceKey { get; set; }

        public static readonly FlightSkillTestNomenclature CPL = new FlightSkillTestNomenclature { ResourceKey = "CPL", Code = "01" };
        public static readonly FlightSkillTestNomenclature IR = new FlightSkillTestNomenclature { ResourceKey = "IR", Code = "02" };
        public static readonly FlightSkillTestNomenclature ClassVS = new FlightSkillTestNomenclature { ResourceKey = "ClassVS", Code = "03" };

        public static readonly IEnumerable<FlightSkillTestNomenclature> Values =
            new List<FlightSkillTestNomenclature>
            {
                CPL,
                IR,
                ClassVS
            };
    }
}

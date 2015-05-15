using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Rio.Objects.Enums
{
    public class StatedDurationNomenclature
    {
        [ScriptIgnore]
        public string Name
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ResourceKey))
                {
                    return string.Empty;
                }
                else
                {
                    return App_LocalResources.StatedDurationNomenclature.ResourceManager.GetString(ResourceKey);
                }
            }
        }

        [ScriptIgnore]
        public string Code { get; set; }
        [ScriptIgnore]
        public string ResourceKey { get; set; }

        public static readonly StatedDurationNomenclature PeriodTime = new StatedDurationNomenclature { ResourceKey = "PeriodTime", Code = "periodTime" };
        public static readonly StatedDurationNomenclature Unlimited = new StatedDurationNomenclature { ResourceKey = "Unlimited", Code = "unlimited" };


        public static List<StatedDurationNomenclature> Values = new List<StatedDurationNomenclature>()
        {
            PeriodTime,
            Unlimited
        };
    }
}

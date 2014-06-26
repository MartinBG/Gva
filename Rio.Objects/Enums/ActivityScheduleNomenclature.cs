using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Rio.Objects.Enums
{
    public class ActivityScheduleNomenclature
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
                    return App_LocalResources.ActivityScheduleNomenclature.ResourceManager.GetString(ResourceKey);
                }
            }
        }

        [ScriptIgnore]
        public string Code { get; set; }
        [ScriptIgnore]
        public string ResourceKey { get; set; }

        public static readonly ActivityScheduleNomenclature With = new ActivityScheduleNomenclature { ResourceKey = "With", Code = "01" };
        public static readonly ActivityScheduleNomenclature Without = new ActivityScheduleNomenclature { ResourceKey = "Without", Code = "02" };


        public static List<ActivityScheduleNomenclature> Values = new List<ActivityScheduleNomenclature>()
        {
            With,
            Without
        };
    }
}

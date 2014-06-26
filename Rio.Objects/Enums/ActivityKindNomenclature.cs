using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Rio.Objects.Enums
{
    public class ActivityKindNomenclature
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
                    return App_LocalResources.ActivityKindNomenclature.ResourceManager.GetString(ResourceKey);
                }
            }
        }

        [ScriptIgnore]
        public string Code { get; set; }
        [ScriptIgnore]
        public string ResourceKey { get; set; }
        [ScriptIgnore]
        public bool HasSchedule { get; set; }

        public static readonly ActivityKindNomenclature International = new ActivityKindNomenclature { ResourceKey = "International", Code = "01" };
        public static readonly ActivityKindNomenclature Internal = new ActivityKindNomenclature { ResourceKey = "Internal", Code = "02" };
        public static readonly ActivityKindNomenclature Other = new ActivityKindNomenclature { ResourceKey = "Other", Code = "03" };

        public static readonly ActivityKindNomenclature Cargo = new ActivityKindNomenclature { ResourceKey = "Cargo", Code = "04", HasSchedule = true };
        public static readonly ActivityKindNomenclature Passengers = new ActivityKindNomenclature { ResourceKey = "Passengers", Code = "05", HasSchedule = true };
        public static readonly ActivityKindNomenclature Trucks = new ActivityKindNomenclature { ResourceKey = "Trucks", Code = "06", HasSchedule = true };
        public static readonly ActivityKindNomenclature Emergency = new ActivityKindNomenclature { ResourceKey = "Emergency", Code = "07", HasSchedule = false };


        public static List<ActivityKindNomenclature> Values = new List<ActivityKindNomenclature>()
        {
            International,
            Internal,
            Other
        };

        public static List<ActivityKindNomenclature> CommercialValues = new List<ActivityKindNomenclature>()
        {
            Cargo,
            Passengers,
            Trucks,
            Emergency
        };
    }
}

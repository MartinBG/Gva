using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Rio.Objects.Enums
{
    public class ApplyForm18BKindNomenclature
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
                    return App_LocalResources.ApplyForm18BKindNomenclature.ResourceManager.GetString(ResourceKey);
                }
            }
        }

        [ScriptIgnore]
        public string Code { get; set; }
        [ScriptIgnore]
        public string ResourceKey { get; set; }

        public static readonly ApplyForm18BKindNomenclature Applicable = new ApplyForm18BKindNomenclature { ResourceKey = "Applicable", Code = "applicable" };
        public static readonly ApplyForm18BKindNomenclature NonApplicable = new ApplyForm18BKindNomenclature { ResourceKey = "NonApplicable", Code = "nonApplicable" };


        public static List<ApplyForm18BKindNomenclature> Values = new List<ApplyForm18BKindNomenclature>()
        {
            Applicable,
            NonApplicable
        };
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Rio.Objects.Enums
{
    public class SpecialRestrictionNomenclature
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
                    return App_LocalResources.SpecialRestrictionNomenclature.ResourceManager.GetString(ResourceKey);
                }
            }
        }

        [ScriptIgnore]
        public string Code { get; set; }
        [ScriptIgnore]
        public string ResourceKey { get; set; }

        public static readonly SpecialRestrictionNomenclature Day = new SpecialRestrictionNomenclature { ResourceKey = "Day", Code = "01" };
        public static readonly SpecialRestrictionNomenclature Night = new SpecialRestrictionNomenclature { ResourceKey = "Night", Code = "02" };

        public static readonly SpecialRestrictionNomenclature Other = new SpecialRestrictionNomenclature { ResourceKey = "Other", Code = "03" };


        public static List<SpecialRestrictionNomenclature> Values = new List<SpecialRestrictionNomenclature>()
        {
            Day,
            Night
        };
    }
}

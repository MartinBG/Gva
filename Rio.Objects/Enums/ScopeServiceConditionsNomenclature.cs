using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Rio.Objects.Enums
{
    public class ScopeServiceConditionsNomenclature
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
                    return App_LocalResources.ScopeServiceConditionsNomenclature.ResourceManager.GetString(ResourceKey);
                }
            }
        }

        [ScriptIgnore]
        public string Code { get; set; }
        [ScriptIgnore]
        public string ResourceKey { get; set; }

        public static readonly ScopeServiceConditionsNomenclature ATS = new ScopeServiceConditionsNomenclature { ResourceKey = "ATS", Code = "01" };
        public static readonly ScopeServiceConditionsNomenclature CNS = new ScopeServiceConditionsNomenclature { ResourceKey = "CNS", Code = "02" };
        public static readonly ScopeServiceConditionsNomenclature AIS = new ScopeServiceConditionsNomenclature { ResourceKey = "AIS", Code = "03" };
        public static readonly ScopeServiceConditionsNomenclature MET = new ScopeServiceConditionsNomenclature { ResourceKey = "MET", Code = "04" };


        public static List<ScopeServiceConditionsNomenclature> Values = new List<ScopeServiceConditionsNomenclature>()
        {
            
        };
    }
}

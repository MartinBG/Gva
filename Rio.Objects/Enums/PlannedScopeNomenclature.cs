using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Rio.Objects.Enums
{
    public class PlannedScopeNomenclature
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
                    return App_LocalResources.PlannedScopeNomenclature.ResourceManager.GetString(ResourceKey);
                }
            }
        }

        [ScriptIgnore]
        public string Code { get; set; }
        [ScriptIgnore]
        public string ResourceKey { get; set; }

        public static readonly PlannedScopeNomenclature LST = new PlannedScopeNomenclature { ResourceKey = "LST", Code = "01" };
        public static readonly PlannedScopeNomenclature LPC = new PlannedScopeNomenclature { ResourceKey = "LPC", Code = "02" };
        public static readonly PlannedScopeNomenclature OPS = new PlannedScopeNomenclature { ResourceKey = "OPS", Code = "03" };
        public static readonly PlannedScopeNomenclature Recent = new PlannedScopeNomenclature { ResourceKey = "Recent", Code = "04" };
        public static readonly PlannedScopeNomenclature CATI = new PlannedScopeNomenclature { ResourceKey = "CATI", Code = "05" };
        public static readonly PlannedScopeNomenclature CATII = new PlannedScopeNomenclature { ResourceKey = "CATII", Code = "06" };
        public static readonly PlannedScopeNomenclature CATIIIA = new PlannedScopeNomenclature { ResourceKey = "CATIIIA", Code = "07" };
        public static readonly PlannedScopeNomenclature CATIIIB = new PlannedScopeNomenclature { ResourceKey = "CATIIIB", Code = "08" };
        public static readonly PlannedScopeNomenclature Pilots = new PlannedScopeNomenclature { ResourceKey = "Pilots", Code = "09" };
        public static readonly PlannedScopeNomenclature Difference = new PlannedScopeNomenclature { ResourceKey = "Difference", Code = "10" };
        public static readonly PlannedScopeNomenclature Zero = new PlannedScopeNomenclature { ResourceKey = "Zero", Code = "11" };
        public static readonly PlannedScopeNomenclature Special = new PlannedScopeNomenclature { ResourceKey = "Special", Code = "12" };

        public static List<PlannedScopeNomenclature> Values = new List<PlannedScopeNomenclature>()
        {
            LST,
            LPC,
            OPS,
            Recent,
            CATI,
            CATII,
            CATIIIA,
            CATIIIB,
            Pilots,
            Difference,
            Zero,
            Special
        };
    }
}

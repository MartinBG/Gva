using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Rio.Objects.Enums
{
    public class SpecialPermissionNomenclature
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
                    return App_LocalResources.SpecialPermissionNomenclature.ResourceManager.GetString(ResourceKey);
                }
            }
        }

        [ScriptIgnore]
        public string Code { get; set; }
        [ScriptIgnore]
        public string ResourceKey { get; set; }

        [ScriptIgnore]
        public bool HasRVR { get; set; }
        [ScriptIgnore]
        public bool HasDH { get; set; }
        [ScriptIgnore]
        public bool HasMinutes { get; set; }
        [ScriptIgnore]
        public bool HasNM { get; set; }
        
        public static readonly SpecialPermissionNomenclature Category2 = new SpecialPermissionNomenclature { ResourceKey = "Category2", Code = "01", HasRVR = true, HasDH = true };
        public static readonly SpecialPermissionNomenclature Category3A = new SpecialPermissionNomenclature { ResourceKey = "Category3A", Code = "02", HasRVR = true, HasDH = true };
        public static readonly SpecialPermissionNomenclature Category3B = new SpecialPermissionNomenclature { ResourceKey = "Category3B", Code = "03", HasRVR = true, HasDH = true };
        public static readonly SpecialPermissionNomenclature Category3C = new SpecialPermissionNomenclature { ResourceKey = "Category3C", Code = "04", HasRVR = true, HasDH = true };
        public static readonly SpecialPermissionNomenclature LVTO = new SpecialPermissionNomenclature { ResourceKey = "LVTO", Code = "05", HasRVR = true };
        public static readonly SpecialPermissionNomenclature MNPS = new SpecialPermissionNomenclature { ResourceKey = "MNPS", Code = "06" };
        public static readonly SpecialPermissionNomenclature RVSM = new SpecialPermissionNomenclature { ResourceKey = "RVSM", Code = "07" };
        public static readonly SpecialPermissionNomenclature RNP = new SpecialPermissionNomenclature { ResourceKey = "RNP", Code = "08" };
        public static readonly SpecialPermissionNomenclature BRNAV = new SpecialPermissionNomenclature { ResourceKey = "BRNAV", Code = "09" };
        public static readonly SpecialPermissionNomenclature PRNAV = new SpecialPermissionNomenclature { ResourceKey = "PRNAV", Code = "10" };
        public static readonly SpecialPermissionNomenclature ETOPS = new SpecialPermissionNomenclature { ResourceKey = "ETOPS", Code = "11", HasMinutes = true, HasNM = true };
        public static readonly SpecialPermissionNomenclature Dangerous = new SpecialPermissionNomenclature { ResourceKey = "Dangerous", Code = "12" };



        public static List<SpecialPermissionNomenclature> Values = new List<SpecialPermissionNomenclature>()
        {
            Category2,
            Category3A,
            Category3B,
            Category3C,
            LVTO,
            MNPS,
            RVSM,
            RNP,
            BRNAV,
            PRNAV,
            ETOPS,
            Dangerous
        };

    }
}

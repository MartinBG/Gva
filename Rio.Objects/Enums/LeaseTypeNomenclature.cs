using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Rio.Objects.Enums
{
    public class LeaseTypeNomenclature
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
                    return App_LocalResources.LeaseTypeNomenclature.ResourceManager.GetString(ResourceKey);
                }
            }
        }

        [ScriptIgnore]
        public string Code { get; set; }
        [ScriptIgnore]
        public string ResourceKey { get; set; }

        public static readonly LeaseTypeNomenclature Financial = new LeaseTypeNomenclature { ResourceKey = "Financial", Code = "01" };
        public static readonly LeaseTypeNomenclature Operative = new LeaseTypeNomenclature { ResourceKey = "Operative", Code = "02" };


        public static List<LeaseTypeNomenclature> Values = new List<LeaseTypeNomenclature>()
        {
            Financial,
            Operative
        };
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Rio.Objects.Enums
{
    public class AccessFormPublicInformationNomenclature
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
                    return App_LocalResources.AccessFormPublicInformationNomenclature.ResourceManager.GetString(ResourceKey);
                }
            }
        }

        [ScriptIgnore]
        public string Code { get; set; }
        [ScriptIgnore]
        public string ResourceKey { get; set; }

        public static readonly AccessFormPublicInformationNomenclature View = new AccessFormPublicInformationNomenclature { ResourceKey = "View", Code = "0006-000133" };
        public static readonly AccessFormPublicInformationNomenclature Oral = new AccessFormPublicInformationNomenclature { ResourceKey = "Oral", Code = "0006-000134" };
        public static readonly AccessFormPublicInformationNomenclature Paper = new AccessFormPublicInformationNomenclature { ResourceKey = "Paper", Code = "0006-000135" };
        public static readonly AccessFormPublicInformationNomenclature Electronic = new AccessFormPublicInformationNomenclature { ResourceKey = "Electronic", Code = "0006-000136" };

        public static readonly List<AccessFormPublicInformationNomenclature> Values = new List<AccessFormPublicInformationNomenclature>()
        {
            View, 
            Oral, 
            Paper,
            Electronic
        };

    }
}

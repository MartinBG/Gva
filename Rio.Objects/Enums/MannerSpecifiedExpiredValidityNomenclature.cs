using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Rio.Objects.Enums
{
    public class MannerSpecifiedExpiredValidityNomenclature
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
                    return App_LocalResources.MannerSpecifiedExpiredValidityNomenclature.ResourceManager.GetString(ResourceKey);
                }
            }
        }

        [ScriptIgnore]
        public string Code { get; set; }
        [ScriptIgnore]
        public string ResourceKey { get; set; }

        public static readonly MannerSpecifiedExpiredValidityNomenclature LessThanThreeYears = new MannerSpecifiedExpiredValidityNomenclature { ResourceKey = "LessThanThreeYears", Code = "01" };
        public static readonly MannerSpecifiedExpiredValidityNomenclature MoreThanThreeYears = new MannerSpecifiedExpiredValidityNomenclature { ResourceKey = "MoreThanThreeYears", Code = "02" };


        public static List<MannerSpecifiedExpiredValidityNomenclature> Values = new List<MannerSpecifiedExpiredValidityNomenclature>()
        {
            LessThanThreeYears,
            MoreThanThreeYears
        };
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Rio.Objects.Enums
{
    public class LicenseChangeTypeNomenclature
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
                    return App_LocalResources.LicenseChangeTypeNomenclature.ResourceManager.GetString(ResourceKey);
                }
            }
        }

        [ScriptIgnore]
        public string Code { get; set; }
        [ScriptIgnore]
        public string ResourceKey { get; set; }

        public static readonly LicenseChangeTypeNomenclature ChangeWithRestrictions = new LicenseChangeTypeNomenclature { ResourceKey = "ChangeWithRestrictions", Code = "01" };
        public static readonly LicenseChangeTypeNomenclature ChangeWithTheoreticalExam = new LicenseChangeTypeNomenclature { ResourceKey = "ChangeWithTheoreticalExam", Code = "02" };
        public static readonly LicenseChangeTypeNomenclature Removal = new LicenseChangeTypeNomenclature { ResourceKey = "Removal", Code = "03" };

        public static readonly List<LicenseChangeTypeNomenclature> Values = new List<LicenseChangeTypeNomenclature>()
        {
            ChangeWithRestrictions, 
            ChangeWithTheoreticalExam, 
            Removal
        };

    }
}

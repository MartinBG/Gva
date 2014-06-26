using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Rio.Objects.Enums
{
    public class DerogationsNomenclature
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
                    return App_LocalResources.DerogationsNomenclature.ResourceManager.GetString(ResourceKey);
                }
            }
        }

        [ScriptIgnore]
        public string Code { get; set; }
        [ScriptIgnore]
        public string ResourceKey { get; set; }

        public static readonly DerogationsNomenclature Awareness = new DerogationsNomenclature { ResourceKey = "Awareness", Code = "01" };
        public static readonly DerogationsNomenclature Categories = new DerogationsNomenclature { ResourceKey = "Categories", Code = "02" };
        public static readonly DerogationsNomenclature Provider = new DerogationsNomenclature { ResourceKey = "Provider", Code = "03" };
        public static readonly DerogationsNomenclature Documented = new DerogationsNomenclature { ResourceKey = "Documented", Code = "04" };

        public static readonly DerogationsNomenclature Subcategory1 = new DerogationsNomenclature { ResourceKey = "Subcategory1", Code = "05" };
        public static readonly DerogationsNomenclature Subcategory2 = new DerogationsNomenclature { ResourceKey = "Subcategory2", Code = "06" };
        public static readonly DerogationsNomenclature Subcategory3 = new DerogationsNomenclature { ResourceKey = "Subcategory3", Code = "07" };
        public static readonly DerogationsNomenclature Subcategory4 = new DerogationsNomenclature { ResourceKey = "Subcategory4", Code = "08" };


        public static List<DerogationsNomenclature> Values = new List<DerogationsNomenclature>()
        {
            
        };
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Rio.Objects.Enums
{
    public class OrganizationKindNomenclature
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
                    return App_LocalResources.OrganizationKindNomenclature.ResourceManager.GetString(ResourceKey);
                }
            }
        }

        [ScriptIgnore]
        public string Code { get; set; }
        [ScriptIgnore]
        public string ResourceKey { get; set; }

        public static readonly OrganizationKindNomenclature Complex = new OrganizationKindNomenclature { ResourceKey = "Complex", Code = "01" };
        public static readonly OrganizationKindNomenclature NonComplex = new OrganizationKindNomenclature { ResourceKey = "NonComplex", Code = "02" };

        public static readonly OrganizationKindNomenclature Own = new OrganizationKindNomenclature { ResourceKey = "Own", Code = "03" };
        public static readonly OrganizationKindNomenclature Hired = new OrganizationKindNomenclature { ResourceKey = "Hired", Code = "04" };


        public static List<OrganizationKindNomenclature> Values = new List<OrganizationKindNomenclature>()
        {
            Complex,
            NonComplex
        };
    }
}

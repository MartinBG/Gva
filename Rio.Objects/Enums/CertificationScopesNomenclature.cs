using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Rio.Objects.Enums
{
    public class CertificationScopesNomenclature
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
                    return App_LocalResources.CertificationScopesNomenclature.ResourceManager.GetString(ResourceKey);
                }
            }
        }

        [ScriptIgnore]
        public string Code { get; set; }
        [ScriptIgnore]
        public string ResourceKey { get; set; }

        public static readonly CertificationScopesNomenclature K = new CertificationScopesNomenclature { ResourceKey = "K", Code = "01" };
        public static readonly CertificationScopesNomenclature P = new CertificationScopesNomenclature { ResourceKey = "P", Code = "02" };
        public static readonly CertificationScopesNomenclature R = new CertificationScopesNomenclature { ResourceKey = "R", Code = "03" };
        public static readonly CertificationScopesNomenclature W = new CertificationScopesNomenclature { ResourceKey = "W", Code = "04" };
        public static readonly CertificationScopesNomenclature NO = new CertificationScopesNomenclature { ResourceKey = "NO", Code = "05" };
        public static readonly CertificationScopesNomenclature NR = new CertificationScopesNomenclature { ResourceKey = "NR", Code = "06" };
        public static readonly CertificationScopesNomenclature NT = new CertificationScopesNomenclature { ResourceKey = "NT", Code = "07" };
        public static readonly CertificationScopesNomenclature DF = new CertificationScopesNomenclature { ResourceKey = "DF", Code = "08" };


        public static List<CertificationScopesNomenclature> Values = new List<CertificationScopesNomenclature>()
        {
            K,
            P,
            R,
            W,
            NO,
            NR,
            NT,
            DF
        };
    }
}

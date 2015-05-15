using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Rio.Objects.Enums
{
    public class TypeInstrumentalMetrologyExpertiseApplicationNomenclature
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
                    return App_LocalResources.TypeInstrumentalMetrologyExpertiseApplicationNomenclature.ResourceManager.GetString(ResourceKey);
                }
            }
        }

        [ScriptIgnore]
        public string Code { get; set; }
        [ScriptIgnore]
        public string ResourceKey { get; set; }

        public static readonly TypeInstrumentalMetrologyExpertiseApplicationNomenclature Electrometer = new TypeInstrumentalMetrologyExpertiseApplicationNomenclature { Code = "01", ResourceKey = "Electrometer" };
        public static readonly TypeInstrumentalMetrologyExpertiseApplicationNomenclature Transformer = new TypeInstrumentalMetrologyExpertiseApplicationNomenclature { Code = "02", ResourceKey = "Transformer" };
        public static readonly TypeInstrumentalMetrologyExpertiseApplicationNomenclature Instrumental = new TypeInstrumentalMetrologyExpertiseApplicationNomenclature { Code = "03", ResourceKey = "Instrumental" };

        public static List<TypeInstrumentalMetrologyExpertiseApplicationNomenclature> Values = new List<TypeInstrumentalMetrologyExpertiseApplicationNomenclature>()
        {
            Electrometer,
            Transformer,
            Instrumental
        };
    }
}

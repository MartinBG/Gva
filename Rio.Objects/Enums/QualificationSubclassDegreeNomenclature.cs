using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Rio.Objects.Enums
{
    public class QualificationSubclassDegreeNomenclature
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
                    return App_LocalResources.QualificationSubclassDegreeNomenclature.ResourceManager.GetString(ResourceKey);
                }
            }
        }

        [ScriptIgnore]
        public string Code { get; set; }
        [ScriptIgnore]
        public string ResourceKey { get; set; }

        public static readonly QualificationSubclassDegreeNomenclature A = new QualificationSubclassDegreeNomenclature { ResourceKey = "A", Code = "01" };
        public static readonly QualificationSubclassDegreeNomenclature B = new QualificationSubclassDegreeNomenclature { ResourceKey = "B", Code = "02" };
        public static readonly QualificationSubclassDegreeNomenclature C = new QualificationSubclassDegreeNomenclature { ResourceKey = "C", Code = "03" };


        public static List<QualificationSubclassDegreeNomenclature> Values = new List<QualificationSubclassDegreeNomenclature>()
        {
           A,
           B,
           C
        };
    }
}

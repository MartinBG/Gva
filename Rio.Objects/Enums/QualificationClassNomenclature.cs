using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Rio.Objects.Enums
{
    public class QualificationClassNomenclature
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
                    return App_LocalResources.QualificationClassNomenclature.ResourceManager.GetString(ResourceKey);
                }
            }
        }

        [ScriptIgnore]
        public string Code { get; set; }
        [ScriptIgnore]
        public string ResourceKey { get; set; }

        public static readonly QualificationClassNomenclature A = new QualificationClassNomenclature { ResourceKey = "A", Code = "01" };
        public static readonly QualificationClassNomenclature B = new QualificationClassNomenclature { ResourceKey = "B", Code = "02" };
        public static readonly QualificationClassNomenclature C = new QualificationClassNomenclature { ResourceKey = "C", Code = "03" };
        public static readonly QualificationClassNomenclature D = new QualificationClassNomenclature { ResourceKey = "D", Code = "04" };
        public static readonly QualificationClassNomenclature E = new QualificationClassNomenclature { ResourceKey = "E", Code = "05" };
        public static readonly QualificationClassNomenclature F = new QualificationClassNomenclature { ResourceKey = "F", Code = "06" };


        public static List<QualificationClassNomenclature> Values = new List<QualificationClassNomenclature>()
        {
           A,
           B,
           C,
           D,
           E,
           F
        };
    }
}

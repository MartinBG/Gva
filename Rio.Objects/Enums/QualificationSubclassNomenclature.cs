using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Rio.Objects.Enums
{
    public class QualificationSubclassNomenclature
    {
        public string ResourceKey { get; private set; }
        public string Code { get; private set; }
        public string ParentValue { get; private set; }

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
                    return App_LocalResources.QualificationSubclassNomenclature.ResourceManager.GetString(ResourceKey);
                }
            }
        }

        public static readonly QualificationSubclassNomenclature A1 = new QualificationSubclassNomenclature { ResourceKey = "A1", Code = "01", ParentValue = QualificationClassNomenclature.A.Code };
        public static readonly QualificationSubclassNomenclature A2 = new QualificationSubclassNomenclature { ResourceKey = "A2", Code = "02", ParentValue = QualificationClassNomenclature.A.Code };
        public static readonly QualificationSubclassNomenclature A3 = new QualificationSubclassNomenclature { ResourceKey = "A3", Code = "03", ParentValue = QualificationClassNomenclature.A.Code };
        public static readonly QualificationSubclassNomenclature A4 = new QualificationSubclassNomenclature { ResourceKey = "A4", Code = "04", ParentValue = QualificationClassNomenclature.A.Code };
        public static readonly QualificationSubclassNomenclature A5 = new QualificationSubclassNomenclature { ResourceKey = "A5", Code = "05", ParentValue = QualificationClassNomenclature.A.Code };
        public static readonly QualificationSubclassNomenclature A6 = new QualificationSubclassNomenclature { ResourceKey = "A6", Code = "06", ParentValue = QualificationClassNomenclature.A.Code };
        public static readonly QualificationSubclassNomenclature A7 = new QualificationSubclassNomenclature { ResourceKey = "A7", Code = "07", ParentValue = QualificationClassNomenclature.A.Code };
        public static readonly QualificationSubclassNomenclature A8 = new QualificationSubclassNomenclature { ResourceKey = "A8", Code = "08", ParentValue = QualificationClassNomenclature.A.Code };

        public static readonly QualificationSubclassNomenclature B1 = new QualificationSubclassNomenclature { ResourceKey = "B1", Code = "09", ParentValue = QualificationClassNomenclature.B.Code };
        public static readonly QualificationSubclassNomenclature B2 = new QualificationSubclassNomenclature { ResourceKey = "B2", Code = "10", ParentValue = QualificationClassNomenclature.B.Code };
        public static readonly QualificationSubclassNomenclature B3 = new QualificationSubclassNomenclature { ResourceKey = "B3", Code = "11", ParentValue = QualificationClassNomenclature.B.Code };

        public static readonly QualificationSubclassNomenclature C1 = new QualificationSubclassNomenclature { ResourceKey = "C1", Code = "12", ParentValue = QualificationClassNomenclature.C.Code };
        public static readonly QualificationSubclassNomenclature C2 = new QualificationSubclassNomenclature { ResourceKey = "C2", Code = "13", ParentValue = QualificationClassNomenclature.C.Code };
        public static readonly QualificationSubclassNomenclature C3 = new QualificationSubclassNomenclature { ResourceKey = "C3", Code = "14", ParentValue = QualificationClassNomenclature.C.Code };
        public static readonly QualificationSubclassNomenclature C4 = new QualificationSubclassNomenclature { ResourceKey = "C4", Code = "15", ParentValue = QualificationClassNomenclature.C.Code };
        public static readonly QualificationSubclassNomenclature C5 = new QualificationSubclassNomenclature { ResourceKey = "C5", Code = "16", ParentValue = QualificationClassNomenclature.C.Code };
        public static readonly QualificationSubclassNomenclature C6 = new QualificationSubclassNomenclature { ResourceKey = "C6", Code = "17", ParentValue = QualificationClassNomenclature.C.Code };

        public static readonly QualificationSubclassNomenclature D1 = new QualificationSubclassNomenclature { ResourceKey = "D1", Code = "18", ParentValue = QualificationClassNomenclature.D.Code };
        public static readonly QualificationSubclassNomenclature D2 = new QualificationSubclassNomenclature { ResourceKey = "D2", Code = "19", ParentValue = QualificationClassNomenclature.D.Code };
        public static readonly QualificationSubclassNomenclature D3 = new QualificationSubclassNomenclature { ResourceKey = "D3", Code = "20", ParentValue = QualificationClassNomenclature.D.Code };
        public static readonly QualificationSubclassNomenclature D4 = new QualificationSubclassNomenclature { ResourceKey = "D4", Code = "21", ParentValue = QualificationClassNomenclature.D.Code };
        public static readonly QualificationSubclassNomenclature D5 = new QualificationSubclassNomenclature { ResourceKey = "D5", Code = "22", ParentValue = QualificationClassNomenclature.D.Code };
        public static readonly QualificationSubclassNomenclature D6 = new QualificationSubclassNomenclature { ResourceKey = "D6", Code = "23", ParentValue = QualificationClassNomenclature.D.Code };
        public static readonly QualificationSubclassNomenclature D7 = new QualificationSubclassNomenclature { ResourceKey = "D7", Code = "24", ParentValue = QualificationClassNomenclature.D.Code };

        public static readonly List<QualificationSubclassNomenclature> Values =
            new List<QualificationSubclassNomenclature>
            {
                A1,
                A2,
                A3,
                A4,
                A5,
                A6,
                A7,
                A8,

                B1,
                B2,
                B3,
                
                C1,
                C2,
                C3,
                C4,
                C5,
                C6,

                D1,
                D2,
                D3,
                D4,
                D5,
                D6,
                D7
            };
    }
}

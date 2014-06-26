using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class OVDQualificationClassWithoutPermissionNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature ADV = new BaseNomenclature("01", "ADV", "");
        public static readonly BaseNomenclature APP = new BaseNomenclature("02", "APP", "");
        public static readonly BaseNomenclature ACP = new BaseNomenclature("03", "ACP", "");
        public static readonly BaseNomenclature GMC = new BaseNomenclature("04", "GMC", "");

        public OVDQualificationClassWithoutPermissionNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
                ADV,
                APP,
                ACP,
                GMC
            };
        }
    }
}

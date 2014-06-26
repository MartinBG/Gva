using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class OVDQualificationClassPermissionNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature GMC = new BaseNomenclature("01", "GMC", "");
        public static readonly BaseNomenclature AIR = new BaseNomenclature("02", "AIR", "");
        public static readonly BaseNomenclature TWR = new BaseNomenclature("03", "TWR", "");
        public static readonly BaseNomenclature GMS = new BaseNomenclature("04", "GMS", "");
        public static readonly BaseNomenclature RAD = new BaseNomenclature("05", "RAD", "");
        public static readonly BaseNomenclature ADS = new BaseNomenclature("06", "ADS", "");
        public static readonly BaseNomenclature TCL = new BaseNomenclature("07", "TCL", "");
        public static readonly BaseNomenclature PAR = new BaseNomenclature("08", "PAR", "");
        public static readonly BaseNomenclature SPA = new BaseNomenclature("09", "SPA", "");

        public OVDQualificationClassPermissionNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
                GMC,
                AIR,
                TWR,
                GMS,
                RAD,
                ADS,
                TCL,
                PAR,
                SPA
            };
        }
    }
}

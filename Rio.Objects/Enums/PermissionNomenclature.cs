using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class PermissionNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature E1 = new BaseNomenclature("01", "CAT II (E-1)");
        public static readonly BaseNomenclature E2 = new BaseNomenclature("02", "CAT IIIA (E-2)");
        public static readonly BaseNomenclature E3 = new BaseNomenclature("03", "CAT IIIB (E-3)");
        public static readonly BaseNomenclature E4 = new BaseNomenclature("04", "CAT IIIC (E-4)");
        public static readonly BaseNomenclature E5 = new BaseNomenclature("05", "LVTO (E-5)");
        public static readonly BaseNomenclature E6 = new BaseNomenclature("06", "MNPS (E-6)");
        public static readonly BaseNomenclature E7 = new BaseNomenclature("07", "ETPOS (E-7)");
        public static readonly BaseNomenclature E8 = new BaseNomenclature("08", "R-NAV (E-8)");
        public static readonly BaseNomenclature E9 = new BaseNomenclature("09", "RVSM (E-9)");
        public static readonly BaseNomenclature E10 = new BaseNomenclature("10", "RNP (E-10)");
        public static readonly BaseNomenclature E11 = new BaseNomenclature("11", "DRG (E-11)");
        public PermissionNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
                E1,
                E2,
                E3,
                E4,
                E5,
                E6,
                E7,
                E8,
                E9,
                E10,
                E11
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class RightsTypeNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature FE = new BaseNomenclature("FE", "FE");
        public static readonly BaseNomenclature TR = new BaseNomenclature("TR", "TR");
        public static readonly BaseNomenclature CRE = new BaseNomenclature("CRE", "CRE");
        public static readonly BaseNomenclature IRE = new BaseNomenclature("IRE", "IRE");
        public static readonly BaseNomenclature SFE = new BaseNomenclature("SFE", "SFE");
        public static readonly BaseNomenclature FIE = new BaseNomenclature("FIE", "FIE");

        public RightsTypeNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
                FE,
                TR,
                CRE,
                IRE,
                SFE,
                FIE
            };
        }
    }
}

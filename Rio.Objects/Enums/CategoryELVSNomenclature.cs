using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class CategoryELVSNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature One = new BaseNomenclature("01", "I", "");
        public static readonly BaseNomenclature Two = new BaseNomenclature("02", "II", "");
        public static readonly BaseNomenclature Three = new BaseNomenclature("03", "III", "");
        public static readonly BaseNomenclature Four = new BaseNomenclature("04", "IV", "");
        public static readonly BaseNomenclature Five = new BaseNomenclature("05", "V", "");

        public CategoryELVSNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
                One,
                Two,
                Three,
                Four,
                Five
            };
        }
    }
}

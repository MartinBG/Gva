using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class FSTDTypeNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature FTD = new BaseNomenclature("01", "FTD");
        public static readonly BaseNomenclature FNPT = new BaseNomenclature("02", "FNPT");
        public static readonly BaseNomenclature FFS = new BaseNomenclature("03", "FFS");

        public FSTDTypeNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
                FTD, 
                FNPT,
                FFS 
            }.OrderBy(e=>e.Text).ToList();
        }
    }
}

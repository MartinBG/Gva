using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class SectorWorkplaceAviationGroundStaffNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature Sofia = new BaseNomenclature("01", "FS SOFIA", "");
        public static readonly BaseNomenclature Varna = new BaseNomenclature("02", "FS VARNA", "");
        public static readonly BaseNomenclature FIC = new BaseNomenclature("03", "FIC", "");

        public SectorWorkplaceAviationGroundStaffNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
                Sofia,
                Varna,
                FIC
            };
        }
    }
}

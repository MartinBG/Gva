using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class OVDQualificationClassWithPermissionNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature ADI = new BaseNomenclature("01", "ADI", "");
        public static readonly BaseNomenclature APS = new BaseNomenclature("02", "APS", "");
        public static readonly BaseNomenclature ACS = new BaseNomenclature("03", "ACS", "");

        public OVDQualificationClassWithPermissionNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
                ADI,
                APS,
                ACS
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class CoordinationActivitiesInteractionAirTrafficManagementPermissionNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature OJT = new BaseNomenclature("01", "OJT", "");
        public static readonly BaseNomenclature ASM = new BaseNomenclature("02", "ASM", "");
        public static readonly BaseNomenclature ATFM = new BaseNomenclature("03", "ATFM", "");
        public static readonly BaseNomenclature FDA = new BaseNomenclature("04", "FDA", "");
        public static readonly BaseNomenclature FIS = new BaseNomenclature("05", "FIS", "");
        public static readonly BaseNomenclature SAR = new BaseNomenclature("07", "SAR", "");
        public static readonly BaseNomenclature AFIS = new BaseNomenclature("08", "AFIS", "");

        public CoordinationActivitiesInteractionAirTrafficManagementPermissionNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
                OJT,
                ASM,
                ATFM,
                FDA,
                FIS,
                SAR,
                AFIS,
            };
        }
    }
}

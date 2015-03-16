using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class CourtNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature BlagoevgradCourt = new BaseNomenclature("1", "Окръжен съд - Благоевград", "");
        public static readonly BaseNomenclature BourgasCourt = new BaseNomenclature("2", "Окръжен съд - Бургас", "");
        public static readonly BaseNomenclature VarnaCourt = new BaseNomenclature("3", "Окръжен съд - Варна", "");
        public static readonly BaseNomenclature SofiaCityCourt = new BaseNomenclature("4", "Софийски градски съд", "");

        public CourtNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
                SofiaCityCourt,
                BlagoevgradCourt,
                BourgasCourt,
                VarnaCourt,
            };
        }
    }
}

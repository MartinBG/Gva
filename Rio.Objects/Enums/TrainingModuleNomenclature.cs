using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class TrainingModuleNomenclature : BaseNomenclature
    {
        public List<BaseNomenclature> InitialValues { get; set; }
        public List<BaseNomenclature> PeriodicalValues { get; set; }

        public static readonly BaseNomenclature M1 = new BaseNomenclature("01", "M1");
        public static readonly BaseNomenclature M2 = new BaseNomenclature("02", "M2");
        public static readonly BaseNomenclature M3 = new BaseNomenclature("03", "M3");
        public static readonly BaseNomenclature M4 = new BaseNomenclature("04", "M4");
        public static readonly BaseNomenclature M5 = new BaseNomenclature("05", "M5");
        public static readonly BaseNomenclature M6 = new BaseNomenclature("06", "M6");
        public static readonly BaseNomenclature M7 = new BaseNomenclature("07", "M7");
        public static readonly BaseNomenclature M8 = new BaseNomenclature("08", "M8");
        public static readonly BaseNomenclature M9 = new BaseNomenclature("09", "M9");
        public static readonly BaseNomenclature M10 = new BaseNomenclature("10", "M10");
        public static readonly BaseNomenclature M11 = new BaseNomenclature("11", "M11");
        public static readonly BaseNomenclature M12 = new BaseNomenclature("12", "M12");
        public static readonly BaseNomenclature M13 = new BaseNomenclature("13", "M13");
        public static readonly BaseNomenclature M14 = new BaseNomenclature("14", "M14");
        public static readonly BaseNomenclature M15 = new BaseNomenclature("15", "M15");
        public static readonly BaseNomenclature M16 = new BaseNomenclature("16", "M16");
        public static readonly BaseNomenclature M17 = new BaseNomenclature("17", "M17");
        public static readonly BaseNomenclature M18 = new BaseNomenclature("18", "M18");

        public TrainingModuleNomenclature()
        {
            this.InitialValues = new List<BaseNomenclature>()
            {
                M1,
                M2,
                M3,
                M4,
                M5,
                M6,
                M7,
                M8,
                M9,
                M10,
                M11,
                M12,
                M13,
                M14,
                M15,
                M16,
                M17,
                M18
            };

            this.PeriodicalValues = new List<BaseNomenclature>()
            {
                M1,
                M2,
                M3,
                M4,
                M5,
                M6,
                M7,
                M8,
                M9,
                M10,
                M11,
                M12,
                M13,
                M14,
                M15,
                M16,
                M17,
                M18
            };
        }
    }
}

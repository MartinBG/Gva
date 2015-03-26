using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class CategoryAircraftNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature LargeAero = new BaseNomenclature("A1", "Самолет с тегло 5,700 кг или повече", "");
        public static readonly BaseNomenclature SmallAero = new BaseNomenclature("A2", "Самолет с тегло до 5,700 кг", "");
        public static readonly BaseNomenclature LargeRotor = new BaseNomenclature("A3", "Хеликоптер с тегло 3,750 кг или повече", "");
        public static readonly BaseNomenclature SmallRotor = new BaseNomenclature("A4", "Хеликоптер с тегло до 3,750 кг", "");
        public static readonly BaseNomenclature LightAero = new BaseNomenclature("VA", "Свръх лек самолет", "");
        public static readonly BaseNomenclature LightRotor = new BaseNomenclature("VH", "Свръх лек хеликоптер", "");
        public static readonly BaseNomenclature Motor = new BaseNomenclature("VM", "Мотоделтапланер", "");
        public static readonly BaseNomenclature Glider = new BaseNomenclature("VN", "Безмоторен планер", "");
        public static readonly BaseNomenclature Balloon = new BaseNomenclature("FB", "Свободен балон", "");
        public static readonly BaseNomenclature Experiment = new BaseNomenclature("EX", "Експериментален", "");

        public CategoryAircraftNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
                LargeAero ,
                SmallAero ,
                LargeRotor,
                SmallRotor,
                LightAero ,
                LightRotor,
                Motor ,
                Glider ,
                Balloon  ,
                Experiment,
            };
        }
    }
}

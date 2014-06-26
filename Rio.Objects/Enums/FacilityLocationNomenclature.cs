using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class FacilityLocationNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature Sofia = new BaseNomenclature("01", "РЦ за ОВД - София");
        public static readonly BaseNomenclature Varna = new BaseNomenclature("02", "ЛЦ за ОВД - Варна");
        public static readonly BaseNomenclature Burgas = new BaseNomenclature("03", "ЛЦ за ОВД - Бургас");
        public static readonly BaseNomenclature Plovdiv = new BaseNomenclature("04", "ЛЦ за ОВД - Пловдив");
        public static readonly BaseNomenclature GornaOriahovica = new BaseNomenclature("05", "ЛЦ за ОВД - Горна Оряховица");


        public FacilityLocationNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
                Sofia,
                Varna,
                Burgas,
                Plovdiv,
                GornaOriahovica
            };
        }
    }
}

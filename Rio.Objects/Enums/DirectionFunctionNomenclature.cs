using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class DirectionFunctionNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature CEO = new BaseNomenclature("01", "Изпълнителен директор");
        public static readonly BaseNomenclature Code = new BaseNomenclature("02", "Ръководител качество летателна годност");
        public static readonly BaseNomenclature Quality = new BaseNomenclature("03", "Ръководител качество  полетни операции");
        public static readonly BaseNomenclature Security = new BaseNomenclature("04", "Ръководител сигурност");
        public static readonly BaseNomenclature Chief = new BaseNomenclature("05", "Ръководител безопасност полети");
        public static readonly BaseNomenclature Operations = new BaseNomenclature("06", "Директор летателна експлоатация");
        public static readonly BaseNomenclature OUPLG = new BaseNomenclature("07", "Ръководител ОУПЛГ");
        public static readonly BaseNomenclature CFO = new BaseNomenclature("08", "Финансов директор");
        public static readonly BaseNomenclature Sales = new BaseNomenclature("09", "Търговски директор");
        public static readonly BaseNomenclature Director = new BaseNomenclature("10", "Ръководител наземно обслужване");
        public static readonly BaseNomenclature Administrative = new BaseNomenclature("11", "Административен директор");
        public static readonly BaseNomenclature Training = new BaseNomenclature("12", "Ръководител подготовка");
        public static readonly BaseNomenclature Pilot = new BaseNomenclature("13", "Главен пилот");

        public DirectionFunctionNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
                CEO ,
                Code,
                Quality,
                Security ,
                Chief,
                Operations ,
                OUPLG ,
                CFO ,
                Sales,
                Director,
                Administrative,
                Training ,
                Pilot
            }.OrderBy(e => e.Text).ToList();
        }
    }
}

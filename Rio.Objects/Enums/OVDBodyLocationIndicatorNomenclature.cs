using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class OVDBodyLocationIndicatorNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature Bourgas = new BaseNomenclature("01", "LBBG БУРГАС", "");
        public static readonly BaseNomenclature Bohot = new BaseNomenclature("02", "LBBO БОХОТ-LM", "");
        public static readonly BaseNomenclature DolnaBania = new BaseNomenclature("03", "LBDB ДОЛНА БАНЯ", "");
        public static readonly BaseNomenclature GornaOriahovica = new BaseNomenclature("04", "LBGO ГОРНА ОРХЯХОВИЦА", "");
        public static readonly BaseNomenclature Grivica = new BaseNomenclature("05", "LBGR ГРИВИЦА", "");
        public static readonly BaseNomenclature Ihtiman = new BaseNomenclature("06", "LBHT ИХТИМАН", "");
        public static readonly BaseNomenclature Kainardja = new BaseNomenclature("07", "LBKJ КАЙНАРДЖА", "");
        public static readonly BaseNomenclature Kalvacha = new BaseNomenclature("08", "LBKL КЪЛВАЧА", "");
        public static readonly BaseNomenclature Lozen = new BaseNomenclature("09", "LBLN ЛОЗЕН", "");
        public static readonly BaseNomenclature Lesnovo = new BaseNomenclature("10", "LBLS ЛЕСНОВО", "");
        public static readonly BaseNomenclature Plovdiv = new BaseNomenclature("11", "LBPD ПЛОВДИВ", "");
        public static readonly BaseNomenclature Primorsko = new BaseNomenclature("12", "LBPR ПРИМОРСКО", "");
        public static readonly BaseNomenclature Erden = new BaseNomenclature("13", "LBRD ЕРДЕН", "");
        public static readonly BaseNomenclature Ruse = new BaseNomenclature("14", "LBRS РУСЕ", "");
        public static readonly BaseNomenclature Sofia = new BaseNomenclature("15", "LBSF СОФИЯ", "");
        public static readonly BaseNomenclature SofiaRPI = new BaseNomenclature("16", "LBSR СОФИЯ РПИ", "");
        public static readonly BaseNomenclature Striama = new BaseNomenclature("17", "LBST СТРЯМА", "");
        public static readonly BaseNomenclature StaraZagora = new BaseNomenclature("18", "LBSZ СТАРА ЗАГОРА", "");
        public static readonly BaseNomenclature Balchik = new BaseNomenclature("19", "LBWB БАЛЧИК", "");
        public static readonly BaseNomenclature Varna = new BaseNomenclature("20", "LBWN ВАРНА", "");
        public static readonly BaseNomenclature Izgrev = new BaseNomenclature("21", "LBWV ИЗГРЕВ", "");

        public OVDBodyLocationIndicatorNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
                Bourgas,
                Bohot,
                DolnaBania,
                GornaOriahovica,
                Grivica,
                Ihtiman,
                Kainardja,
                Kalvacha,
                Lozen,
                Lesnovo,
                Plovdiv,
                Primorsko,
                Erden,
                Ruse,
                Sofia,
                SofiaRPI,
                Striama,
                StaraZagora,
                Balchik,
                Varna,
                Izgrev,
            }.OrderBy(e => e.Text).ToList();
        }
    }
}

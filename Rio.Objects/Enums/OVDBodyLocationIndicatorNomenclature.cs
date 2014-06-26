using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class OVDBodyLocationIndicatorNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature Bourgas = new BaseNomenclature("01", "LBBG ÁÓĞÃÀÑ", "");
        public static readonly BaseNomenclature Bohot = new BaseNomenclature("02", "LBBO ÁÎÕÎÒ-LM", "");
        public static readonly BaseNomenclature DolnaBania = new BaseNomenclature("03", "LBDB ÄÎËÍÀ ÁÀÍß", "");
        public static readonly BaseNomenclature GornaOriahovica = new BaseNomenclature("04", "LBGO ÃÎĞÍÀ ÎĞÕßÕÎÂÈÖÀ", "");
        public static readonly BaseNomenclature Grivica = new BaseNomenclature("05", "LBGR ÃĞÈÂÈÖÀ", "");
        public static readonly BaseNomenclature Ihtiman = new BaseNomenclature("06", "LBHT ÈÕÒÈÌÀÍ", "");
        public static readonly BaseNomenclature Kainardja = new BaseNomenclature("07", "LBKJ ÊÀÉÍÀĞÄÆÀ", "");
        public static readonly BaseNomenclature Kalvacha = new BaseNomenclature("08", "LBKL ÊÚËÂÀ×À", "");
        public static readonly BaseNomenclature Lozen = new BaseNomenclature("09", "LBLN ËÎÇÅÍ", "");
        public static readonly BaseNomenclature Lesnovo = new BaseNomenclature("10", "LBLS ËÅÑÍÎÂÎ", "");
        public static readonly BaseNomenclature Plovdiv = new BaseNomenclature("11", "LBPD ÏËÎÂÄÈÂ", "");
        public static readonly BaseNomenclature Primorsko = new BaseNomenclature("12", "LBPR ÏĞÈÌÎĞÑÊÎ", "");
        public static readonly BaseNomenclature Erden = new BaseNomenclature("13", "LBRD ÅĞÄÅÍ", "");
        public static readonly BaseNomenclature Ruse = new BaseNomenclature("14", "LBRS ĞÓÑÅ", "");
        public static readonly BaseNomenclature Sofia = new BaseNomenclature("15", "LBSF ÑÎÔÈß", "");
        public static readonly BaseNomenclature SofiaRPI = new BaseNomenclature("16", "LBSR ÑÎÔÈß ĞÏÈ", "");
        public static readonly BaseNomenclature Striama = new BaseNomenclature("17", "LBST ÑÒĞßÌÀ", "");
        public static readonly BaseNomenclature StaraZagora = new BaseNomenclature("18", "LBSZ ÑÒÀĞÀ ÇÀÃÎĞÀ", "");
        public static readonly BaseNomenclature Balchik = new BaseNomenclature("19", "LBWB ÁÀË×ÈÊ", "");
        public static readonly BaseNomenclature Varna = new BaseNomenclature("20", "LBWN ÂÀĞÍÀ", "");
        public static readonly BaseNomenclature Izgrev = new BaseNomenclature("21", "LBWV ÈÇÃĞÅÂ", "");

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
